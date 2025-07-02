using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
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
                LoadTotalUsers();
             
                LoadActiveInactiveUsers();
                LoadCourseCounts();
                LoadSubCourseCounts();
                LoadSoldCoursesCount();
            }


        }
        private void LoadActiveInactiveUsers()
        {
            SqlCommand cmdActive = new SqlCommand("SELECT COUNT(*) FROM Users WHERE status='Active'", conn);
            SqlCommand cmdInactive = new SqlCommand("SELECT COUNT(*) FROM Users WHERE status='Inactive'", conn);

            lblActiveUsers.Text = cmdActive.ExecuteScalar().ToString();
            lblInactiveUsers.Text = cmdInactive.ExecuteScalar().ToString();
        }

        private void LoadCourseCounts()
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Mastercourse", conn);
            lblCourses.Text = cmd.ExecuteScalar().ToString();
        }

        private void LoadSubCourseCounts()
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM subcourses", conn);
            lblSubCourses.Text = cmd.ExecuteScalar().ToString();
        }

        private void LoadSoldCoursesCount()
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Transactions WHERE Status='Success'", conn);
            lblSoldCourses.Text = cmd.ExecuteScalar().ToString();
        }

        private void LoadTotalUsers()
        {
            string query = "SELECT COUNT(*) FROM Users";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {

                int totalUsers = (int)cmd.ExecuteScalar();
                lblTotalUsers.Text = totalUsers.ToString();
            }
            catch (Exception ex)
            {
                lblTotalUsers.Text = "Error";


            }

        }
        
        protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                string email = e.CommandArgument.ToString();

               
                SqlCommand cmd1 = new SqlCommand("UPDATE Users SET status='Active' WHERE Email=@Email", conn);
                cmd1.Parameters.AddWithValue("@Email", email);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM loginlogs WHERE user_id = (SELECT UserID FROM Users WHERE Email=@Email)\r\n", conn);
                cmd2.Parameters.AddWithValue("@Email", email);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("DELETE FROM ReactivationRequests WHERE Email=@Email", conn);
                cmd3.Parameters.AddWithValue("@Email", email);
                cmd3.ExecuteNonQuery();

              
            }
            else if (e.CommandName == "Reject")
            {
                int reqId = Convert.ToInt32(e.CommandArgument);
                SqlCommand cmd = new SqlCommand("DELETE FROM ReactivationRequests WHERE RequestID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", reqId);
                cmd.ExecuteNonQuery();

          
            }
        }

    }
}