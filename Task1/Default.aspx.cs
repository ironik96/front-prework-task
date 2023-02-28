using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Task1.Services;

namespace Task1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
      
           
        }

        private bool SearchIsEmpty()
        {
            return inputTextBox.Text.Length== 0;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string input = inputTextBox.Text;

            var connectionString = ConfigurationManager.ConnectionStrings["CustomerAccount"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetCustomerProfile", connection);
                command.CommandType = CommandType.StoredProcedure;
                if (!SearchIsEmpty())
                    command.Parameters.AddWithValue("@civil_id",SqlDbType.Decimal).Value = Decimal.Parse(input);
                

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable();
                
                adapter.Fill(table);

                DataColumn gender = new DataColumn("genderString", typeof(string));
                table.Columns.Add(gender);
                table.Columns["genderString"].SetOrdinal(2);
                foreach (DataRow row in table.Rows)
                {
                    bool isMale = Convert.ToBoolean(row["gender"]);
                    string genderString = isMale ? "Male" : "Female";
                    
                    row["genderString"] = genderString;
                }
                table.Columns.Remove("gender");

                GridView1.DataSource = table;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Console.Write("could not connect to database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void DetailsButton_DataBound(object sender, EventArgs e)
        {
            DetailsLink.Visible = GridView1.Rows.Count > 0 && !SearchIsEmpty();
        }

        protected void DetailsLink_Click(object sender, EventArgs e)
        {
            EncryptionService service = new EncryptionService();
            string maskedCivilID = service.Encrypt(inputTextBox.Text);
            Response.Redirect($"CustomerDetails.aspx?civil_id={maskedCivilID}");
        }

    }
}