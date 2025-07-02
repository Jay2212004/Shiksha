using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SikshaNew
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            con.Open();
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            string query = $"exec Register '{fullName}', '{email}', '{password}' ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Registered Successfully!')</script>");
        }


    }
}