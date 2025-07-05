using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
namespace SikshaNew.Admin.MasterCourse
{
    public partial class Addtopic : System.Web.UI.Page
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
                fetchcourse();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string mcoursename = DropDownList1.SelectedValue;
            string scoursename = DropDownList2.SelectedValue, videourl = TextBox2.Text;
            string topic = TextBox1.Text, status = DropDownList3.SelectedValue;
            string q = $"exec addtopic '{mcoursename}','{scoursename}','{topic}','{videourl}','{status}'";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('New Topic added');</script>");
            SendEmailToAllUsers(topic);
        }
        public void fetchcourse()
        {
            string query = "SELECT DISTINCT course_name FROM mastercourse where status='Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList1.DataSource = rdr;
            DropDownList1.DataTextField = "course_name";
            DropDownList1.DataValueField = "course_name";
            DropDownList1.DataBind();

        }
        public void fetchsubcourse()
        {
            string query = $"SELECT DISTINCT subcourse_name FROM subcourses WHERE mastercourse = '{DropDownList1.SelectedValue}' AND status = 'Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList2.DataSource = rdr;
            DropDownList2.DataTextField = "subcourse_name";
            DropDownList2.DataValueField = "subcourse_name";
            DropDownList2.DataBind();

        }


        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {

            fetchsubcourse();
        }
        public void SendEmailToAllUsers(string topic)
        {
            SqlCommand getEmailsCmd = new SqlCommand("select Email from Users where status = 'Active'", conn);
            SqlDataReader rdr = getEmailsCmd.ExecuteReader();

            while (rdr.Read())
            {
                string toEmail = rdr["Email"].ToString();
                string subject = "📚 New Topic Added: " + topic;
                string body = $"Hello Learner,\n\nA new topic \"{topic}\" has been added to the Shiksha Academy platform.\n\n" +
                              "👉 Explore the course in your dashboard and start learning today!\n\n" +
                              "Happy Learning!\nTeam Shiksha Academy.";

                SendEmail(toEmail, subject, body);
            }

            rdr.Close();
        }


        private void SendEmail(string toEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("rrai07505@gmail.com");
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("rrai07505@gmail.com", "bprbcsejgyqgudls");
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}