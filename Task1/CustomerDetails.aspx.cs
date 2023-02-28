using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Task1.Services;

namespace Task1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string Name
        {
            get; set;
        }
        public string CivilId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count>0)
            {
                EncryptionService service = new EncryptionService();
                CivilId = service.Decrypt(Request.QueryString["civil_id"]);
            }
            var connectionString = ConfigurationManager.ConnectionStrings["CustomerAccount"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetCustomerDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@civil_id", SqlDbType.Decimal).Value = Decimal.Parse(CivilId);


                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable();

                adapter.Fill(table);

                Name = table.Rows[0]["customer_name"].ToString();

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

                CustomerDetail.DataSource = table;
                CustomerDetail.DataBind();
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
    }
}