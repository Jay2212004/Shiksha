using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
namespace SikshaNew.User
{
    public partial class UserDashboard : System.Web.UI.Page
    {

        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                LoadAssignmentStatus();
                LoadUserProgress();
                LoadUserDashboardData();
                LoadEnrolledCourses();
            }
        }

        public void LoadAssignmentStatus()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            SqlCommand cmd = new SqlCommand("sp_GetUserAssignments", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@user_email", userEmail);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvAssignments.DataSource = dt;
            gvAssignments.DataBind();
        }

        public void LoadUserProgress()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            SqlCommand cmd = new SqlCommand("sp_GetCourseProgress", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@user_email", userEmail);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int totalTopics = 0;
            int totalCompleted = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalTopics += Convert.ToInt32(row["TotalTopics"]);
                totalCompleted += Convert.ToInt32(row["CompletedTopics"]);
            }

            int progressPercent = totalTopics == 0 ? 0 : (int)Math.Round((totalCompleted * 100.0) / totalTopics);
            ViewState["ProgressPercent"] = progressPercent;
        }
        private void LoadEnrolledCourses()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            SqlCommand cmd = new SqlCommand(@"
        SELECT SC.subcourse_name
        FROM SubscribedUsers S
        INNER JOIN Users U ON U.UserID = S.UserID
        INNER JOIN subcourses SC ON SC.subcourse_id = S.SubCourseID
        WHERE U.Email = @Email", conn);

            cmd.Parameters.AddWithValue("@Email", userEmail);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptCoursesEnrolled.DataSource = dt;
            rptCoursesEnrolled.DataBind();
        }

        private void LoadUserDashboardData()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            SqlCommand cmd = new SqlCommand("sp_GetUserDashboardMetrics", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@user_email", userEmail);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblUserName.Text = reader["UserName"].ToString();
                lblCoursesSubscribed.Text = reader["CoursesSubscribed"].ToString();
                lblActiveCourses.Text = reader["ActiveCourses"].ToString();
                lblAmountSpent.Text = "₹" + Convert.ToDecimal(reader["AmountSpent"]).ToString("F2");
                lblAllSubCourses.Text = reader["AllSubCourses"].ToString();
            
            }
            reader.Close();
        }
    }
}
