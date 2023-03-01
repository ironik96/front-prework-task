using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Task1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CustomerAccount"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetProfileTypes", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    


                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    
                    ProfileTypeList.DataSource = table;
                    ProfileTypeList.DataValueField = "id";
                    ProfileTypeList.DataTextField = "profile_type_name";
                    ProfileTypeList.DataBind();
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
}