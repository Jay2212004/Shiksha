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
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                string paymentId = Request.QueryString["payment_id"];
                if (!string.IsNullOrEmpty(paymentId))
                {
                    SaveTransaction(paymentId);
                    Response.Redirect("MyCourse.aspx");
                }
            }
        }

        private void SaveTransaction(string paymentId)
        {
            decimal amount = 0;
            string status = "Success";

            if (Session["Cart"] != null && Session["userEmail"] != null)
            {
                DataTable dt = Session["Cart"] as DataTable;
                string userEmail = Session["userEmail"].ToString();

                // Get UserID from email
                string userIdQuery = $"SELECT UserID FROM Users WHERE Email = '{userEmail}'";
                SqlCommand userCmd = new SqlCommand(userIdQuery, conn);
                object userIdObj = userCmd.ExecuteScalar();
                if (userIdObj == null) return;
                int userId = Convert.ToInt32(userIdObj);

                foreach (DataRow row in dt.Rows)
                {
                    if (decimal.TryParse(row["SubcoursePrice"].ToString(), out decimal price))
                    {
                        amount += price;
                    }
                }

                // Insert into Transactions
                string q = $"exec InsertTransaction '{paymentId}', '{status}', {amount}, {userId}";

                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.ExecuteNonQuery();

                // Insert each course into SubscribedUsers
                foreach (DataRow row in dt.Rows)
                {
                    int subcourseId = Convert.ToInt32(row["SubcourseId"]);
                    string checkDup = $"SELECT COUNT(*) FROM SubscribedUsers WHERE UserID = {userId} AND SubCourseID = {subcourseId}";
                    SqlCommand checkCmd = new SqlCommand(checkDup, conn);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0)
                    {
                        string insert = $"INSERT INTO SubscribedUsers (UserID, SubCourseID) VALUES ({userId}, {subcourseId})";
                        SqlCommand insertCmd = new SqlCommand(insert, conn);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                Session["Cart"] = null;
            }
        }


    }
}