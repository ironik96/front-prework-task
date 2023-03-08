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
        private const string IS_ACTIVE_COLUMN = "is_active";

        // Procedures
        private const string GET_PROFILE_PROCEDURE = "GetProfileTypes";
        private const string CREATE_PROFILE_PROCEDURE = "CreateProfileType";
        private const string UPDATE_PROFILE_PROCEDURE = "UpdateProfileType";


        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerAccount"].ConnectionString;
        private SqlDataAdapter SegmentAdapter;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            SegmentAdapter = CreateSegmentAdapter(Connection);

            if (!IsPostBack)
                ReadTables();
        }
        protected void AddSegment_Click(object sender, EventArgs e)
        {
            string newSegmentName = SegmentInputBox_Add.Text;
            if (newSegmentName.Equals("")) return;


            // insert to datatable
            MarkTableInserted(ProfileTable);
            SegmentAdapter.InsertCommand.Parameters[$"@{PROFILE_NAME_COLUMN}"].Value = newSegmentName;
            int effectedRows = SegmentAdapter.Update(ProfileTable);

            if (effectedRows > 0)
                ReadTables();



        }

        protected void UpdateSegment_Click(object sender, EventArgs e)
        {
            string updatedSegmentName = SegmentInputBox_Edit.Text;
            if (updatedSegmentName.Equals("")) return;
            string segmentID = ProfileTypeList.SelectedValue;

            MarkTableUpdated(ProfileTable);
            SegmentAdapter.UpdateCommand.Parameters[$"@{TYPE_ID_COLUMN}"].Value = segmentID;
            SegmentAdapter.UpdateCommand.Parameters[$"@{PROFILE_NAME_COLUMN}"].Value = updatedSegmentName;
            int effectedRows = SegmentAdapter.Update(ProfileTable);

            if (effectedRows > 0)
                ReadTables();



        }

        protected void DeleteSegment_Click(object sender, EventArgs e)
        {
            string segmentID = ProfileTypeList.SelectedValue;

            MarkTableUpdated(ProfileTable);
            SegmentAdapter.UpdateCommand.Parameters[$"@{TYPE_ID_COLUMN}"].Value = segmentID;
            SegmentAdapter.UpdateCommand.Parameters[$"@{IS_ACTIVE_COLUMN}"].Value = 0;
            int effectedRows = SegmentAdapter.Update(ProfileTable);

            if (effectedRows > 0)
                ReadTables();

        }

        protected SqlDataAdapter CreateSegmentAdapter(SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            // Create the SelectCommand.
            SqlCommand command = new SqlCommand(GET_PROFILE_PROCEDURE, connection);
            command.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = command;

            // Create the UpdateCommand.
            command = new SqlCommand(UPDATE_PROFILE_PROCEDURE, connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add the parameters for the UpdateCommand.
            command.Parameters.Add($"@{TYPE_ID_COLUMN}", SqlDbType.Int);
            command.Parameters.Add($"@{PROFILE_NAME_COLUMN}", SqlDbType.VarChar);
            command.Parameters.Add($"@{IS_ACTIVE_COLUMN}", SqlDbType.Bit);
            adapter.UpdateCommand = command;

            // Create the InsertCommand.
            command = new SqlCommand(CREATE_PROFILE_PROCEDURE, connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add the parameters for the InsertCommand.
            command.Parameters.Add($"@{PROFILE_NAME_COLUMN}", SqlDbType.VarChar);
            adapter.InsertCommand = command;

            return adapter;
        }

        private void ReadTables()
        {
            ProfileTable = new DataTable();
            BindDropDown(SegmentAdapter, ProfileTable, ProfileTypeList, TYPE_ID_COLUMN, PROFILE_NAME_COLUMN);
        }

        private void BindDropDown(SqlDataAdapter DataAdapter, DataTable Table, DropDownList DropDown, string ValueField, string TextField)
        {
            DataAdapter.Fill(Table);

            DropDown.DataSource = Table;
            DropDown.DataValueField = ValueField;
            DropDown.DataTextField = TextField;
            DropDown.DataBind();

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