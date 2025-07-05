using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
namespace SikshaNew.Admin.Material
{
    public partial class AddMaterial : System.Web.UI.Page
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
            string scoursename = DropDownList2.SelectedValue;
            string topicname = DropDownList3.SelectedValue;
            string filename = Path.GetFileName(FileUpload1.FileName);


            string savePath = Server.MapPath("~/Admin/Material/AdminAssignment/") + filename;
            FileUpload1.SaveAs(savePath);
            string filePath = filename;
            string q = $"exec AddAssignment '{mcoursename}','{scoursename}','{topicname}','{filePath}'";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
            SendEmailToAllUsers(mcoursename, scoursename, topicname, filename);
            Response.Write("<script>alert('New Material added');</script>");
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

        public void FetchTopic()
        {
            string query = $"SELECT DISTINCT Topic  FROM Topic  where subcourse = '{DropDownList2.SelectedValue}' AND status='Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList3.DataSource = rdr;
            DropDownList3.DataTextField = "Topic";
            DropDownList3.DataValueField = "Topic";
            DropDownList3.DataBind();
            //Session["TopicName"] = rdr["Topic"].ToString();
        }
        private void SendEmailToAllUsers(string course, string subcourse, string topic, string filename)
        {
            string subject = $"📄 New Study Material Uploaded: {topic}";
            string body = $"Hello Learner,\n\nA new study material has been uploaded on the Shiksha Academy platform.\n\n" +
                          $"📚 Course: {course}\n" +
                          $"📘 Subcourse: {subcourse}\n" +
                          $"🔖 Topic: {topic}\n" +
                          $"📁 File: {filename}\n\n" +
                          $"👉 Log in to your dashboard and download the material now!\n\n" +
                          $"Happy Learning!\nTeam Shiksha Academy.";

            SqlCommand getEmailsCmd = new SqlCommand("SELECT Email FROM Users WHERE status = 'Active'", conn);
            SqlDataReader rdr = getEmailsCmd.ExecuteReader();

            while (rdr.Read())
            {
                string toEmail = rdr["Email"].ToString();
                SendEmail(toEmail, subject, body);
            }

            rdr.Close();
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress("rrai07505@gmail.com");
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("rrai07505@gmail.com", "bprbcsejgyqgudls");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchsubcourse();
        }

        protected void DropDownList2_SelectedIndexChanged1(object sender, EventArgs e)
        {
            FetchTopic();
        }
    }
}