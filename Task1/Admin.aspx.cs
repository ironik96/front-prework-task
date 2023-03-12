using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Common;

namespace Task1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        // Column Fields
        private const string TYPE_ID_COLUMN = "id";
        private const string PROFILE_NAME_COLUMN = "profile_type_name";
        private const string ACCOUNT_NAME_COLUMN = "account_type_name";
        private const string CARD_NAME_COLUMN = "card_type_name";
        private const string IS_ACTIVE_COLUMN = "is_active";

        // Output Parameter
        private const string NEW_ID_PARAM = "new_id";

        // Procedures
        private const string GET_PROFILE_PROCEDURE = "GetProfileTypes";
        private const string CREATE_PROFILE_PROCEDURE = "CreateProfileType";
        private const string UPDATE_PROFILE_PROCEDURE = "UpdateProfileType";
        //--
        private const string GET_ACCOUNT_PROCEDURE = "GetAccountTypes";
        private const string CREATE_ACCOUNT_PROCEDURE = "CreateAccountType";
        private const string UPDATE_ACCOUNT_PROCEDURE = "UpdateAccountType";
        //--
        private const string GET_CARD_PROCEDURE = "GetCardTypes";
        private const string CREATE_CARD_PROCEDURE = "CreateCardType";
        private const string UPDATE_CARD_PROCEDURE = "UpdateCardType";


        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerAccount"].ConnectionString;
        private SqlDataAdapter SegmentAdapter;
        private SqlDataAdapter AccountAdapter;
        private SqlDataAdapter CardAdapter;
        private DataTable ProfileTable
        {
            get
            {
                return ViewState["ProfileTable"] as DataTable;
            }
            set
            {
                ViewState["ProfileTable"] = value;
            }
        }
        private DataTable AccountTable
        {
            get
            {
                return ViewState["AccountTable"] as DataTable;
            }
            set
            {
                ViewState["AccountTable"] = value;
            }
        }
        private DataTable CardTable
        {
            get
            {
                return ViewState["CardTable"] as DataTable;
            }
            set
            {
                ViewState["CardTable"] = value;
            }
        }
        private string SegmentSelectedValue
        {
            get
            {
                if (ViewState["SegmentSelectedValue"] == null) return null;
                return ViewState["SegmentSelectedValue"].ToString();
            }
            set
            {
                ViewState["SegmentSelectedValue"] = value;
            }
        }
        private string AccountSelectedValue
        {
            get
            {
                if (ViewState["AccountSelectedValue"] == null) return null;
                return ViewState["AccountSelectedValue"].ToString();
            }
            set
            {
                ViewState["AccountSelectedValue"] = value;
            }
        }
        private string CardSelectedValue
        {
            get
            {
                if (ViewState["CardSelectedValue"] == null) return null;
                return ViewState["CardSelectedValue"].ToString();
            }
            set
            {
                ViewState["CardSelectedValue"] = value;
            }
        }

        private bool ActionOnSegment
        {
            get => ModalType.Value == "profile";
        }
        private bool ActionOnAccount
        {
            get => ModalType.Value == "account";
        }
        private bool ActionOnCard
        {
            get => ModalType.Value == "card";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            SegmentAdapter = CreateAdapter(Connection, GET_PROFILE_PROCEDURE, CREATE_PROFILE_PROCEDURE, UPDATE_PROFILE_PROCEDURE, PROFILE_NAME_COLUMN);
            AccountAdapter = CreateAdapter(Connection, GET_ACCOUNT_PROCEDURE, CREATE_ACCOUNT_PROCEDURE, UPDATE_ACCOUNT_PROCEDURE, ACCOUNT_NAME_COLUMN);
            CardAdapter = CreateAdapter(Connection, GET_CARD_PROCEDURE, CREATE_CARD_PROCEDURE, UPDATE_CARD_PROCEDURE, CARD_NAME_COLUMN);

            if (!IsPostBack)
                ReadTables();
        }

        protected void AddType_Click(object sender, EventArgs e)
        {
            string newName = TypeInputBox_Add.Text;

            if (ActionOnSegment)
            {
                string newId = InsertType(newName, ProfileTable, SegmentAdapter, PROFILE_NAME_COLUMN);
                if (newId != null) SegmentSelectedValue = newId;
            }
            if (ActionOnAccount)
            {
                string newId = InsertType(newName, AccountTable, AccountAdapter, ACCOUNT_NAME_COLUMN);
                if (newId != null) AccountSelectedValue = newId;
            }
            if (ActionOnCard)
            {
                string newId = InsertType(newName, CardTable, CardAdapter, CARD_NAME_COLUMN);
                if (newId != null) CardSelectedValue = newId;
            }
            ReadTables();
        }

        protected void UpdateType_Click(object sender, EventArgs e)
        {
            string updatedName = TypeInputBox_Edit.Text;
            bool updated = false;

            if (ActionOnSegment)
            {
                string segmentId = ProfileTypeList.SelectedValue;
                updated = UpdateType(updatedName, segmentId, ProfileTable, SegmentAdapter, PROFILE_NAME_COLUMN);
            }
            if (ActionOnAccount)
            {
                string accountId = AccountTypeList.SelectedValue;
                updated = UpdateType(updatedName, accountId, AccountTable, AccountAdapter, ACCOUNT_NAME_COLUMN);
            }
            if (ActionOnCard)
            {
                string cardId = CardTypeList.SelectedValue;
                updated = UpdateType(updatedName, cardId, CardTable, CardAdapter, CARD_NAME_COLUMN);
            }

            if (updated) ReadTables();



        }

        protected void DeleteType_Click(object sender, EventArgs e)
        {
            bool deleted = false;

            if (ActionOnSegment)
            {
                string segmentId = ProfileTypeList.SelectedValue;
                deleted = DeleteType(segmentId, SegmentAdapter, ProfileTable);
                if (deleted) SegmentSelectedValue = null;
            }
            if (ActionOnAccount)
            {
                string accountId = AccountTypeList.SelectedValue;
                deleted = DeleteType(accountId, AccountAdapter, AccountTable);
                if (deleted) AccountSelectedValue = null;
            }
            if (ActionOnCard)
            {
                string cardId = CardTypeList.SelectedValue;
                deleted = DeleteType(cardId, CardAdapter, CardTable);
                if (deleted) CardSelectedValue = null;
            }

            if (deleted)
                ReadTables();


        }

        protected void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            SegmentSelectedValue = ProfileTypeList.SelectedValue;
            AccountSelectedValue = AccountTypeList.SelectedValue;
            CardSelectedValue = CardTypeList.SelectedValue;
        }
        protected SqlDataAdapter CreateAdapter(SqlConnection connection, string selectCommand, string insertCommand, string updateCommand, string nameColumn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            // Create the SelectCommand.
            SqlCommand command = new SqlCommand(selectCommand, connection);
            command.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = command;

            // Create the UpdateCommand.
            command = new SqlCommand(updateCommand, connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add the parameters for the UpdateCommand.
            command.Parameters.Add($"@{TYPE_ID_COLUMN}", SqlDbType.Int);
            command.Parameters.Add($"@{nameColumn}", SqlDbType.VarChar);
            command.Parameters.Add($"@{IS_ACTIVE_COLUMN}", SqlDbType.Bit);
            adapter.UpdateCommand = command;

            // Create the InsertCommand.
            command = new SqlCommand(insertCommand, connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add the parameters for the InsertCommand.
            command.Parameters.Add($"@{nameColumn}", SqlDbType.VarChar);
            SqlParameter idParam = new SqlParameter($"@{NEW_ID_PARAM}", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            command.Parameters.Add(idParam);
            adapter.InsertCommand = command;

            return adapter;
        }

        private void ReadTables()
        {
            ProfileTable = new DataTable();
            BindDropDown(SegmentAdapter, ProfileTable, ProfileTypeList, TYPE_ID_COLUMN, PROFILE_NAME_COLUMN);
            if (SegmentSelectedValue == null) SegmentSelectedValue = ProfileTypeList.SelectedValue;
            else ProfileTypeList.SelectedValue = SegmentSelectedValue;

            AccountTable = new DataTable();
            BindDropDown(AccountAdapter, AccountTable, AccountTypeList, TYPE_ID_COLUMN, ACCOUNT_NAME_COLUMN);
            if (AccountSelectedValue == null) AccountSelectedValue = AccountTypeList.SelectedValue;
            else AccountTypeList.SelectedValue = AccountSelectedValue;

            CardTable = new DataTable();
            BindDropDown(CardAdapter, CardTable, CardTypeList, TYPE_ID_COLUMN, CARD_NAME_COLUMN);
            if (CardSelectedValue == null) CardSelectedValue = CardTypeList.SelectedValue;
            else CardTypeList.SelectedValue = CardSelectedValue;
        }

        private void BindDropDown(SqlDataAdapter DataAdapter, DataTable Table, DropDownList DropDown, string ValueField, string TextField)
        {
            DataAdapter.Fill(Table);

            DropDown.DataSource = Table;
            DropDown.DataValueField = ValueField;
            DropDown.DataTextField = TextField;
            DropDown.DataBind();

        }

        private string InsertType(string name, DataTable table, SqlDataAdapter adapter, string parameter)
        {
            if (name.Equals("")) return null;


            // insert to datatable
            MarkTableInserted(table);
            adapter.InsertCommand.Parameters[$"@{parameter}"].Value = name;
            adapter.Update(table);
            object newIdParam = adapter.InsertCommand.Parameters[$"@{NEW_ID_PARAM}"].Value;

            if (newIdParam == null)
                return null;

            return newIdParam.ToString();
        }
        private bool UpdateType(string name, string id, DataTable table, SqlDataAdapter adapter, string parameter)
        {
            if (name.Equals("")) return false;

            MarkTableUpdated(table);
            adapter.UpdateCommand.Parameters[$"@{TYPE_ID_COLUMN}"].Value = id;
            adapter.UpdateCommand.Parameters[$"@{parameter}"].Value = name;
            int effectedRows = adapter.Update(table);

            return effectedRows > 0;
        }

        private bool DeleteType(string id, SqlDataAdapter adapter, DataTable table)
        {

            MarkTableUpdated(table);
            adapter.UpdateCommand.Parameters[$"@{TYPE_ID_COLUMN}"].Value = id;
            adapter.UpdateCommand.Parameters[$"@{IS_ACTIVE_COLUMN}"].Value = 0;
            int effectedRows = adapter.Update(table);

            return effectedRows > 0;
        }

        private void MarkTableUpdated(DataTable Table)
        {
            if (Table.Rows.Count != 0) Table.Rows[0].SetModified();
        }
        private void MarkTableInserted(DataTable Table)
        {
            Table.Rows.Add(Table.NewRow());
        }
    }
}