using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
namespace SikshaNew.User
{
    public partial class paymentsuccess : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {

            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                string paymentId = Request.QueryString["payment_id"];
                if (!string.IsNullOrEmpty(paymentId))
                {
                    SaveTransaction(paymentId);
                    Response.Redirect("invoice.aspx?payment_id=" + paymentId);
                }
            }
        }

        private void SaveTransaction(string paymentId)
        {
            if (Session["Cart"] != null)
            {
                DataTable cart = Session["Cart"] as DataTable;

                foreach (DataRow row in cart.Rows)
                {
                    string subcourseName = row["SubcourseName"].ToString();
                    decimal price = Convert.ToDecimal(row["SubcoursePrice"]);
                    string status = "Success";

                    string q = $"exec InsertTransaction1 '{paymentId}','{subcourseName}','{price}','{status}'";
                    SqlCommand cmd = new SqlCommand(q, conn);
                    cmd.ExecuteNonQuery();

                }

                Session["Cart"] = null;
            }
        }
    }
}


