using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SikshaNew.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        SqlConnection conn;
        public string monthlySalesJson;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                LoadMastercourses();
                ddlYear.SelectedValue = "2025";
            }

     
            LoadTotalUsers();
            LoadActiveInactiveUsers();
            LoadCourseCounts();
            LoadSubCourseCounts();
            LoadSoldCoursesCount();
            LoadYearlyTransactions();
        }


        private void LoadTotalUsers()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users", conn);
                lblTotalUsers.Text = cmd.ExecuteScalar().ToString();
            }
            catch
            {
                lblTotalUsers.Text = "Error";
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

        private void LoadMastercourses()
        {
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT Mastercourse FROM subcourses WHERE Mastercourse IS NOT NULL", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            ddlMastercourse.Items.Clear();
            ddlMastercourse.Items.Add(new ListItem("-- Select --", ""));
            while (reader.Read())
            {
                ddlMastercourse.Items.Add(new ListItem(reader["Mastercourse"].ToString()));
            }
            reader.Close();
        }

        protected void ddlMastercourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ddlMastercourse.SelectedValue;
            SqlCommand cmd = new SqlCommand("SELECT subcourse_name, subcourses_price, status, created_at FROM subcourses WHERE Mastercourse=@master", conn);
            cmd.Parameters.AddWithValue("@master", selected);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            gvSubcourses.DataSource = dt;
            gvSubcourses.DataBind();
                
            LoadTotalUsers();
            LoadActiveInactiveUsers();
            LoadCourseCounts();
            LoadSubCourseCounts();
            LoadSoldCoursesCount();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadYearlyTransactions();
        }

        private void LoadYearlyTransactions()
        {
            LoadTotalUsers();
            LoadActiveInactiveUsers();
            LoadCourseCounts();
            LoadSubCourseCounts();
            LoadSoldCoursesCount();
            string year = ddlYear.SelectedValue;
            decimal[] monthly = new decimal[12];

            SqlCommand cmd = new SqlCommand(@"
                SELECT MONTH(PaymentDate) as MonthNo, SUM(SubcoursePrice) as Total
                FROM Transactions 
                WHERE Status='Success' AND YEAR(PaymentDate)=@year
                GROUP BY MONTH(PaymentDate)", conn);
            cmd.Parameters.AddWithValue("@year", year);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int month = Convert.ToInt32(reader["MonthNo"]);
                decimal total = Convert.ToDecimal(reader["Total"]);
                monthly[month - 1] = total;
            }
            reader.Close();

            monthlySalesJson = new JavaScriptSerializer().Serialize(monthly);
        }

        protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                string email = e.CommandArgument.ToString();

                SqlCommand cmd1 = new SqlCommand("UPDATE Users SET status='Active' WHERE Email=@Email", conn);
                cmd1.Parameters.AddWithValue("@Email", email);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM loginlogs WHERE user_id = (SELECT UserID FROM Users WHERE Email=@Email)", conn);
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