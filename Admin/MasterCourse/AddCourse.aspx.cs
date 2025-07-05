using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
namespace SikshaNew.Admin.MasterCourse
{
    public partial class AddCourse : System.Web.UI.Page
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
            string cname = TextBox1.Text, status = DropDownList1.SelectedValue;
            string filename = Path.GetFileName(FileUpload1.FileName);
            string savePath = Server.MapPath("~/Admin/MasterCourse/MasterThumbnail/") + filename;
            FileUpload1.SaveAs(savePath);
            string filePath = $"{cname} " + filename;
            string q = $"exec addmastercourse '{cname}','{filePath}','{status}'";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('New Master course added');</script>");

            SendEmailToAllUsers(cname);
        }

        

        public void SendEmailToAllUsers(string courseName)
        {
            SqlCommand getEmailsCmd = new SqlCommand("select Email from Users where status = 'Active'", conn);
            SqlDataReader rdr = getEmailsCmd.ExecuteReader();

            while (rdr.Read())
            {
                string toEmail = rdr["Email"].ToString();
                string subject = "📚 New Course Added: " + courseName;
                string body = $"Hello Learner,\n\nA new course \"{courseName}\" has been added to the Shiksha Academy platform.\n\n" +
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