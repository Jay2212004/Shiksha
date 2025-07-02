using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace SikshaNew.Admin.Subscription
{
    public partial class MasterSubscription : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string SubName = TextBox1.Text;
            string Status = DropDownList1.SelectedValue;
            string q = $"exec addSubscriptionType '{SubName}','{Status}'";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();

            Response.Write("<script>alert('New Subscription type added');</script>");

        }
    }
}