using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SikshaNew.Admin.Subscription
{
    public partial class AddSubscription : System.Web.UI.Page
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
                fetchSubscription();
                fetchCourseList();
                Session["SelectedSubcourses"] = null;
            }
        }
        public void SendEmailToAllUsers(string subscriptionName, string courseName, List<string> subcourses)

        {
            SqlCommand getEmailsCmd = new SqlCommand("select Email from Users where status = 'Active'", conn);
            SqlDataReader rdr = getEmailsCmd.ExecuteReader();

            while (rdr.Read())
            {
                string toEmail = rdr["Email"].ToString();
                string subject = "📚 New Subscription Added: " + courseName;
                string subList = string.Join(", ", subcourses);
                string body = $"Hello Learner,\n\nA new \"{subscriptionName}\" subscription has been added to the Shiksha Academy platform " +
                              $"for the Course \"{courseName}\".\n\n" +
                              $"📘 Subcourses included: {subList}\n\n" +
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

        private List<string> SelectedSubcourses
        {
            get
            {
                if (Session["SelectedSubcourses"] == null)
                    Session["SelectedSubcourses"] = new List<string>();
                return (List<string>)Session["SelectedSubcourses"];
            }
            set
            {
                Session["SelectedSubcourses"] = value;
            }
        }

        public void fetchSubscription()
        {
            string query = "SELECT DISTINCT Sname FROM MasterSubscribtion WHERE status = 'Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            DropDownList1.DataSource = rdr;
            DropDownList1.DataTextField = "Sname";
            DropDownList1.DataValueField = "Sname";
            DropDownList1.DataBind();
            rdr.Close();
        }

        public void fetchCourseList()
        {
            string query = "SELECT DISTINCT course_name FROM mastercourse WHERE status = 'Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            DropDownList2.DataSource = rdr;
            DropDownList2.DataTextField = "course_name";
            DropDownList2.DataValueField = "course_name";
            DropDownList2.DataBind();
            rdr.Close();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = DropDownList2.SelectedValue;
            string query = $"SELECT subcourse_name FROM subcourses WHERE Mastercourse = '{selectedCourse}'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            CheckBoxList1.Items.Clear();
            while (rdr.Read())
            {
                string subName = rdr["subcourse_name"].ToString();
                CheckBoxList1.Items.Add(new ListItem(subName));
            }

            rdr.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string subType = DropDownList1.SelectedValue;
            string courseName = DropDownList2.SelectedValue;
            string price = TextBox1.Text.Trim();
            string duration = TextBox2.Text.Trim();
            string iconPath = "";
            if (FileUpload1.HasFile)
            {
                string filename = System.IO.Path.GetFileName(FileUpload1.FileName);
                string folderPath = Server.MapPath("/Subscription_Icon/");
                System.IO.Directory.CreateDirectory(folderPath); 
                string filePath = folderPath + filename;

                FileUpload1.SaveAs(filePath);
                iconPath = "/Subscription_Icon/" + filename;
            }

            int count = 0;

            foreach (string subcourse in SelectedSubcourses)
            {
                string query = $"exec SC_subscription '{courseName}', '{subcourse}', '{subType}', '{price}', '{duration}', '{iconPath}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                count++;
            }

            LabelMessage.Text = count > 0 ? "Subscriptions added successfully!" : "Please select at least one subcourse.";
            SelectedSubcourses.Clear();
            lblCombinedSubcourses.Text = "";
            SendEmailToAllUsers(subType, courseName, SelectedSubcourses);

        }

        protected void btnAddToBundle_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected && !SelectedSubcourses.Contains(item.Text))
                {
                    SelectedSubcourses.Add(item.Text);
                }
            }

            
            lblCombinedSubcourses.Text = string.Join(", ", SelectedSubcourses);
        }
    }
}