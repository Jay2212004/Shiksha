using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
namespace SikshaNew.Admin
{
    public partial class trans : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);

            if (!IsPostBack)
                LoadTransaction();
        }

        private void LoadTransaction()
        {
            using (SqlCommand cmd = new SqlCommand("getTransaction", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvTransactions.DataSource = dt;
                gvTransactions.DataBind();
            }
        }
    }
}