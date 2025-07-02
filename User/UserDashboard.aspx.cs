using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
namespace SikshaNew.User
{
    public partial class UserDashboard : System.Web.UI.Page
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
                LoadUserDashboard();
            }
        }

        private void LoadUserDashboard()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            SqlCommand cmdName = new SqlCommand("SELECT FullName FROM Users WHERE UserID=@uid", conn);
            cmdName.Parameters.AddWithValue("@uid", userId);
            object result = cmdName.ExecuteScalar();
            lblUserName.Text = result != null ? result.ToString() : "User";
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Transactions WHERE UserID=@uid AND Status='Success'", conn);
            cmd1.Parameters.AddWithValue("@uid", userId);
            lblCoursesSubscribed.Text = cmd1.ExecuteScalar().ToString();

            SqlCommand cmd2 = new SqlCommand(@"
                SELECT COUNT(*) 
                FROM Transactions t 
                JOIN subcourses s ON t.Status='Success' AND t.UserID=@uid 
                WHERE s.status='Active'", conn);
            cmd2.Parameters.AddWithValue("@uid", userId);
            lblActiveCourses.Text = cmd2.ExecuteScalar().ToString();

            SqlCommand cmd3 = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM Transactions WHERE UserID=@uid AND Status='Success'", conn);
            cmd3.Parameters.AddWithValue("@uid", userId);
            lblAmountSpent.Text = "₹ " + cmd3.ExecuteScalar().ToString();

            SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM subcourses WHERE status='Active'", conn);
            lblAllSubCourses.Text = cmd4.ExecuteScalar().ToString();

            SqlCommand cmd5 = new SqlCommand(@"
                SELECT TOP 1 s.subcourse_name, t.PaymentDate 
                FROM Transactions t 
                JOIN subcourses s ON t.Status='Success' AND t.UserID=@uid 
                ORDER BY t.PaymentDate DESC", conn);
            cmd5.Parameters.AddWithValue("@uid", userId);
            SqlDataReader rdr = cmd5.ExecuteReader();
            if (rdr.Read())
            {
                lblLatestCourse.Text = rdr["subcourse_name"].ToString() + " on " + Convert.ToDateTime(rdr["PaymentDate"]).ToString("dd MMM yyyy");
            }
            else
            {
                lblLatestCourse.Text = "No purchases yet";
            }
            rdr.Close();
        }
    }
}