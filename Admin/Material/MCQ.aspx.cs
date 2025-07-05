using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SikshaNew.Admin.Material
{
    public partial class MCQ : System.Web.UI.Page
    {
        SqlConnection con;
        DataTable mcqTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            Repeater1.ItemCommand += Repeater1_ItemCommand;
            Repeater1.ItemDataBound += Repeater1_ItemDataBound;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            con.Open();
            if (!IsPostBack)
            {
                LoadCourses();
                InitializeMCQTable();
                Session["MCQData"] = mcqTable;
                BindRepeater();
                LoadAllMCQs();

            }
            else
            {
                mcqTable = (DataTable)Session["MCQData"];

            }
        }


        private void LoadCourses()
        {
            string q = $"exec fetch_courses";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList1.DataSource = rdr;
            DropDownList1.DataTextField = "course_name";
            DropDownList1.DataValueField = "course_name";
            DropDownList1.DataBind();
            DropDownList1_SelectedIndexChanged(null, null);
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string course_name = DropDownList1.SelectedValue;
            string q = $"exec fetch_sub_courses '{course_name}'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList2.DataSource = rdr;
            DropDownList2.DataTextField = "subcourse_name";
            DropDownList2.DataValueField = "subcourse_id";
            DropDownList2.DataBind();
            rdr.Close();

            DropDownList2_SelectedIndexChanged(null, null);
        }


        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string subcourseId = DropDownList2.SelectedValue;
            string q = $"exec fetch_topics_by_subcourse_id '{subcourseId}'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList3.DataSource = rdr;
            DropDownList3.DataTextField = "Topic";
            DropDownList3.DataValueField = "topic_id";
            DropDownList3.DataBind();
            rdr.Close();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void InitializeMCQTable()
        {
            mcqTable = new DataTable();
            mcqTable.Columns.Add("Question");
            mcqTable.Columns.Add("OptionA");
            mcqTable.Columns.Add("OptionB");
            mcqTable.Columns.Add("OptionC");
            mcqTable.Columns.Add("OptionD");
            mcqTable.Columns.Add("CorrectAnswer");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveCurrentRepeaterValues();
            mcqTable.Rows.Add(mcqTable.NewRow());
            Session["MCQData"] = mcqTable;
            BindRepeater();
        }
        private void BindRepeater()
        {
            Repeater1.DataSource = mcqTable;
            Repeater1.DataBind();
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int indexToDelete = Convert.ToInt32(e.CommandArgument);
                SaveCurrentRepeaterValues();
                mcqTable.Rows.RemoveAt(indexToDelete);
                Session["MCQData"] = mcqTable;
                BindRepeater();
            }
        }
        private void SaveCurrentRepeaterValues()
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem item = Repeater1.Items[i];
                TextBox txtQuestion = (TextBox)item.FindControl("TextBox1");
                TextBox txtA = (TextBox)item.FindControl("TextBox2");
                TextBox txtB = (TextBox)item.FindControl("TextBox3");
                TextBox txtC = (TextBox)item.FindControl("TextBox4");
                TextBox txtD = (TextBox)item.FindControl("TextBox5");
                DropDownList ddlCorrect = (DropDownList)item.FindControl("DropDownList4");

                mcqTable.Rows[i]["Question"] = txtQuestion.Text;
                mcqTable.Rows[i]["OptionA"] = txtA.Text;
                mcqTable.Rows[i]["OptionB"] = txtB.Text;
                mcqTable.Rows[i]["OptionC"] = txtC.Text;
                mcqTable.Rows[i]["OptionD"] = txtD.Text;
                mcqTable.Rows[i]["CorrectAnswer"] = ddlCorrect.SelectedValue;
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item)
            { return; }

            ((DropDownList)e.Item.FindControl("DropDownList4")).SelectedValue =
                Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CorrectAnswer"));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SaveCurrentRepeaterValues();
            int subCourseId = int.Parse(DropDownList2.SelectedValue);
            int topicId = int.Parse(DropDownList3.SelectedValue);

            foreach (DataRow row in mcqTable.Rows)
            {
                string question = row["Question"].ToString().Replace("'", "''");
                string optionA = row["OptionA"].ToString().Replace("'", "''");
                string optionB = row["OptionB"].ToString().Replace("'", "''");
                string optionC = row["OptionC"].ToString().Replace("'", "''");
                string optionD = row["OptionD"].ToString().Replace("'", "''");
                string correct = row["CorrectAnswer"].ToString();

                string query = $"exec add_mcq {subCourseId}, {topicId}, '{question}', '{optionA}', '{optionB}', '{optionC}', '{optionD}', '{correct}'";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }

            int count = mcqTable.Rows.Count;
            string course = DropDownList1.SelectedItem.Text;
            string subcourse = DropDownList2.SelectedItem.Text;
            string topic = DropDownList3.SelectedItem.Text;

            SendEmailToAllUsers(course, subcourse, topic, count);

            Response.Write("<script>alert('MCQs saved successfully.');</script>");
            mcqTable.Clear();
            Session["MCQData"] = mcqTable;
            LoadAllMCQs();
            BindRepeater();
        }

        private void SendEmailToAllUsers(string course, string subcourse, string topic, int mcqCount)
        {
            string subject = $"📝 New MCQs Added in {subcourse}";
            string body = $"Hello Learner,\n\nNew MCQs have been added under:\n\n" +
                          $"📚 Course: {course}\n" +
                          $"📘 Subcourse: {subcourse}\n" +
                          $"🔖 Topic: {topic}\n" +
                          $"❓ Total MCQs: {mcqCount}\n\n" +
                          $"👉 Go to your dashboard and test your knowledge now!\n\n" +
                          $"Happy Learning!\nTeam Shiksha Academy.";

            SqlCommand getEmailsCmd = new SqlCommand("SELECT Email FROM Users WHERE status = 'Active'", con);
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
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("rrai07505@gmail.com");
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("rrai07505@gmail.com", "bprbcsejgyqgudls");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        private void LoadAllMCQs()
        {
            string query = "exec fetch_mcqs";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Repeater1_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}