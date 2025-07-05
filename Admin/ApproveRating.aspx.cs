using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SikshaNew.Admin
{
    public partial class ApproveRating : System.Web.UI.Page
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
                LoadReviewGrid();
            }
        }

        public void LoadReviewGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM CourseReviews WHERE is_approved = 0", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvReviews.DataSource = dt;
            gvReviews.DataBind();
        }

        protected void gvReviews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reviewId = Convert.ToInt32(e.CommandArgument);
            Session["ReviewID"] = reviewId;

            SqlCommand cmd = null;
            string action = "";

            if (e.CommandName == "Approve")
            {
                cmd = new SqlCommand("sp_ApproveReview", conn);
                action = "approved";
            }
            else if (e.CommandName == "Reject")
            {
                cmd = new SqlCommand("sp_RejectReview", conn);
                action = "rejected";
            }

            if (cmd != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", reviewId);
                cmd.ExecuteNonQuery();

                // Fetch user email & course name from review
                SqlCommand fetch = new SqlCommand("SELECT UserEmail, CourseName FROM CourseReviews WHERE id = @id", conn);
                fetch.Parameters.AddWithValue("@id", reviewId);
                SqlDataReader rdr = fetch.ExecuteReader();
                if (rdr.Read())
                {
                    string email = rdr["UserEmail"].ToString();
                    string course = rdr["CourseName"].ToString();
                    rdr.Close();

                    SendEmailToUser(email, course, action);
                }
                else
                {
                    rdr.Close();
                }

                LoadReviewGrid();
            }
        }

        private void SendEmailToUser(string email, string courseName, string status)
        {
            string subject = $"📢 Your Review has been {status}";
            string body = $"Hello Learner,\n\nYour review for the course \"{courseName}\" has been {status} by the admin.\n\n" +
                          $"Thank you for sharing your feedback!\n\nBest,\nTeam Shiksha Academy";

            MailMessage mail = new MailMessage("rrai07505@gmail.com", email, subject, body);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("rrai07505@gmail.com", "bprbcsejgyqgudls");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}