using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Task1.Services;

namespace Task1
{
    public partial class _Default : Page
    {
        readonly private string connectionString = ConfigurationManager.ConnectionStrings["CustomerAccount"].ConnectionString;

        protected string displayAlert
        {
            get {
                return SuccessMessage.Text == String.Empty ? "none" : "block";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindCustomers();

        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string input = inputTextBox.Text;
            BindCustomers(input);

        }

        protected void DetailsLink_Click(object sender, EventArgs e)
        {
            EncryptionService service = new EncryptionService();
            string maskedCivilID = service.Encrypt(inputTextBox.Text);
            Response.Redirect($"CustomerDetails.aspx?civil_id={maskedCivilID}");
        }

        protected void CustomersGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bindLists = IsEditState(e.Row) && IsDataType(e.Row);
            if (!bindLists) return;

            BindSegmentList(e.Row);
            BindGenderList(e.Row);

        }
        protected void CustomersGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CustomersGrid.EditIndex = e.NewEditIndex;
            BindCustomers();
        }
        protected void CustomersGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CustomersGrid.EditIndex = -1;
            BindCustomers();
        }
        protected void CustomersGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = CustomersGrid.Rows[e.RowIndex];
            if (!IsDataType(row)) return;
            string civilId = LabelControlText("CivilIdLabel", row);
            string customerName = TextBoxControlText("TxtName", row);
            bool gender = bool.Parse(DropDownControlValue("GenderList", row));
            string phoneNumber = TextBoxControlText("TxtPhone", row);
            string area = TextBoxControlText("TxtArea", row);
            int blockNumber = int.Parse(TextBoxControlText("TxtBlockNumber", row));
            string street = TextBoxControlText("TxtStreet", row);
            string house = TextBoxControlText("TxtHouse", row);
            int profileTypeId = int.Parse(DropDownControlValue("SegmentList", row));

            UpdateCustomer(civilId, customerName, gender, phoneNumber, area, blockNumber, street, house, profileTypeId);
            CustomersGrid.EditIndex = -1;
            BindCustomers();
            SetSuccessMessage("Customer Updated Successfully!");
        }
        protected void CustomersGrid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            SetSuccessMessage("Customer Added Successfully!");
        }
        protected void CustomersGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string civilId = LabelControlText("CivilIdLabel", CustomersGrid.Rows[e.RowIndex]);
            DeleteCustomer(civilId);
            BindCustomers();

        }

        private void BindCustomers(string civilId = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomerProfile", connection);
                command.CommandType = CommandType.StoredProcedure;
                if (civilId.Length != 0)
                    command.Parameters.AddWithValue("@civil_id", SqlDbType.Decimal).Value = Decimal.Parse(civilId);


                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                DataColumn gender = new DataColumn("genderString", typeof(string));
                table.Columns.Add(gender);
                foreach (DataRow row in table.Rows)
                {
                    bool isMale = Convert.ToBoolean(row["gender"]);
                    string genderString = isMale ? "Male" : "Female";

                    row["genderString"] = genderString;
                }


                CustomersGrid.DataSource = table;
                CustomersGrid.DataBind();
            }
            ResetMessages();
        }
        private string TextBoxControlText(string controlId, GridViewRow row)
        {
            return (row.FindControl(controlId) as TextBox).Text.Trim();
        }
        private string DropDownControlValue(string controlId, GridViewRow row)
        {
            return (row.FindControl(controlId) as DropDownList).SelectedValue;
        }
        private string LabelControlText(string controlId, GridViewRow row)
        {
            return (row.FindControl(controlId) as Label).Text.Trim();
        }
        private void UpdateCustomer(string civilId, string customerName, bool gender, string phoneNumber, string area, int blockNumber, string street, string house, int profileTypeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateCustomerProfile", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@civil_id", SqlDbType.Decimal).Value = Decimal.Parse(civilId);
                command.Parameters.AddWithValue("@customer_name", SqlDbType.VarChar).Value = customerName;
                command.Parameters.AddWithValue("@gender", SqlDbType.Bit).Value = gender;
                command.Parameters.AddWithValue("@phone_number", SqlDbType.Decimal).Value = Decimal.Parse(phoneNumber);
                command.Parameters.AddWithValue("@area", SqlDbType.VarChar).Value = area;
                command.Parameters.AddWithValue("@block_number", SqlDbType.Int).Value = blockNumber;
                command.Parameters.AddWithValue("@street", SqlDbType.VarChar).Value = street;
                command.Parameters.AddWithValue("@house", SqlDbType.VarChar).Value = house;
                command.Parameters.AddWithValue("@profile_type_id", SqlDbType.Int).Value = profileTypeId;
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        private void DeleteCustomer(string civilId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteCustomerProfile", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@civil_id", SqlDbType.Decimal).Value = Decimal.Parse(civilId);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        private void BindSegmentList(GridViewRow row)
        {
            string selectedProfileTypeId = (row.DataItem as DataRowView)["profile_type_id"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetProfileTypes", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable segmentTable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(segmentTable);
                var segmentList = row.FindControl("SegmentList") as DropDownList;
                segmentList.DataSource = segmentTable;
                segmentList.DataValueField = "id";
                segmentList.DataTextField = "profile_type_name";
                segmentList.DataBind();
                segmentList.SelectedValue=selectedProfileTypeId;
            }
        }
        private void BindGenderList(GridViewRow row)
        {
            string selectedGender = (row.DataItem as DataRowView)["gender"].ToString();
            var genderList = row.FindControl("GenderList") as DropDownList;
            
            genderList.DataSource = GenderTable;
            genderList.DataValueField = "Value";
            genderList.DataTextField = "Text";
            genderList.DataBind();
            genderList.SelectedValue = selectedGender;

        }
        private void SetSuccessMessage(string message)
        {
            SuccessMessage.Text = message;
        }
        private void ResetMessages()
        {
            SuccessMessage.Text = String.Empty;
        }
        private bool IsEditState(GridViewRow row)
        {
            return row.RowState.HasFlag(DataControlRowState.Edit);
        }
        private bool IsDataType(GridViewRow row)
        {
            return row.RowType == DataControlRowType.DataRow;
        }

        private DataTable GenderTable
        {
            get
            {
                DataTable genderTable = new DataTable();

                genderTable.Columns.Add(new DataColumn("Value", typeof(string)));
                genderTable.Columns.Add(new DataColumn("Text", typeof(String)));

                var femaleRow = genderTable.NewRow();
                femaleRow[0] = Boolean.FalseString;
                femaleRow[1] = "Female";
                genderTable.Rows.Add(femaleRow);
                var maleRow = genderTable.NewRow();
                maleRow[0] = Boolean.TrueString;
                maleRow[1] = "Male";
                genderTable.Rows.Add(maleRow);
                return genderTable;
            }
        }
    }
}