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
        public string PhoneNumber { get; set; }

        private string area;
        private string block;
        private string street;
        private string house;
        private bool gender;
        public string Address
        {
            get
            {
                return $"{area}, block: {block}, st: {street}, house: {house}";
            }
        }
        
        public string Gender
        {
            get
            {
                return gender ? "MALE" : "FEMALE";
            }
        }
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
                PhoneNumber = table.Rows[0]["phone_number"].ToString();
                area = table.Rows[0]["area"].ToString();
                block = table.Rows[0]["block_number"].ToString();
                street = table.Rows[0]["street"].ToString();
                house = table.Rows[0]["house"].ToString();
                gender = Convert.ToBoolean(table.Rows[0]["gender"]);


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