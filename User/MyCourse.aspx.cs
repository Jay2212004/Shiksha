using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
namespace SikshaNew.User
{
    public partial class MyCourse : System.Web.UI.Page
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
                LoadAllSubCourses();
                pnlNavBar.Visible = false; 
            }
        }
        private string GetUserIdentifier()
        {
            
            if (Session["userEmail"] != null)
                return Session["userEmail"].ToString();
            else if (Session["userFullname"] != null)
                return Session["userFullname"].ToString();
            return null;
        }


        public void LoadTopicsListOnly(string subcourseName)
        {
            

            string userIdentifier = GetUserIdentifier();
            if (string.IsNullOrEmpty(userIdentifier)) return;

           

            SqlCommand cmd = new SqlCommand("sp_GetAllowedTopics", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_email", userIdentifier);
            cmd.Parameters.AddWithValue("@subcourse_name", subcourseName);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptTopics.DataSource = dt;
            rptTopics.DataBind();
            ViewState["AllTopics"] = dt;
        }

        public void LoadTopicsAndVideo(string subcourseName, bool playFirst = false)
        {

            LoadTopicsListOnly(subcourseName);


            string userIdentifier = GetUserIdentifier();
            if (string.IsNullOrEmpty(userIdentifier)) return;

            DataTable dt = ViewState["AllTopics"] as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                litVideo.Text = "<div class='alert alert-warning'>No topics found for this course.</div>";
                return;
            }

            if (playFirst)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!Convert.ToBoolean(row["is_completed"]))
                    {
                        int topicId = Convert.ToInt32(row["topic_id"]);
                        string videoUrl = row["video_url"].ToString();

                        ViewState["CurrentTopicId"] = topicId.ToString();
                        litVideo.Text = $"<iframe width='100%' height='400px' src='{ConvertToEmbedUrl(videoUrl)}' frameborder='0' allowfullscreen></iframe>";
                        
                        MarkTopicCompleted(userIdentifier, subcourseName, topicId);
                        return;
                    }
                }

                
                var lastWatched = dt.AsEnumerable().LastOrDefault(r => Convert.ToBoolean(r["is_completed"]));
                if (lastWatched != null)
                {
                    int topicId = Convert.ToInt32(lastWatched["topic_id"]);
                    litVideo.Text = $"<iframe width='100%' height='400px' src='{ConvertToEmbedUrl(lastWatched["video_url"].ToString())}' frameborder='0' allowfullscreen></iframe>";
                    ViewState["CurrentTopicId"] = lastWatched["topic_id"].ToString();
                    LoadMCQs(topicId);

                }
            }

        }


        
        public void MarkTopicCompleted(string userIdentifier, string subcourse, int topicId)
        {
            SqlCommand cmd = new SqlCommand("sp_MarkTopicCompleted", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@user_email", userIdentifier);
            cmd.Parameters.AddWithValue("@subcourse_name", subcourse);
            cmd.Parameters.AddWithValue("@topic_id", topicId);
            cmd.ExecuteNonQuery();
        }

        public string ConvertToEmbedUrl(string url)
        {
            if (url.Contains("watch?v="))
                return "https://www.youtube.com/embed/" + url.Split('=')[1];
            if (url.Contains("youtu.be/"))
                return "https://www.youtube.com/embed/" + url.Split('/').Last();
            return url;
        }

        public void LoadAllSubCourses()
        {
            string userEmail = Session["userEmail"]?.ToString();

            if (string.IsNullOrEmpty(userEmail))
            {
               
                Response.Redirect("~/LoginPage.aspx");
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_GetAllActiveSubCourses", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userEmail", userEmail);

                SqlDataReader rdr = cmd.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(rdr);

                rptSubCourseCard.DataSource = dt;
                rptSubCourseCard.DataBind();
            }
        }


        protected void rptSubCourseCard_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Play")
            {
                ViewState["SubcourseName"] = e.CommandArgument.ToString();
                pnlCourseCards.Visible = false;
                pnlVideoTopics.Visible = true;
                pnlNavBar.Visible = true;
                LoadTopicsAndVideo(ViewState["SubcourseName"].ToString(), playFirst: true);
            }
        }

        protected void rptTopics_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "PlayTopic")
            {
                string[] args = e.CommandArgument.ToString().Split(',');
                int topicId = int.Parse(args[0]);
                string url = args[1];
                bool isCompleted = args[2] == "1";

                
                string userIdentifier = GetUserIdentifier();
                if (string.IsNullOrEmpty(userIdentifier)) return;

                DataTable allTopics = ViewState["AllTopics"] as DataTable;
                if (allTopics == null) return;

                var topicList = allTopics.AsEnumerable().ToList();
                int topicIndex = topicList.FindIndex(r => Convert.ToInt32(r["topic_id"]) == topicId);
                var topicRow = allTopics.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["topic_id"]) == topicId);
                if (topicRow != null)
                {
                    Session["TopicName"] = topicRow["Topic"].ToString();  // ✅ Set topic name here
                }

                bool canWatch = isCompleted || topicList.Take(topicIndex).All(r => Convert.ToBoolean(r["is_completed"]));

                if (canWatch)
                {
                    litVideo.Text = $"<iframe width='100%' height='400px' src='{ConvertToEmbedUrl(url)}' frameborder='0' allowfullscreen></iframe>";

                    if (!isCompleted)
                    {
                        
                        MarkTopicCompleted(userIdentifier, ViewState["SubcourseName"].ToString(), topicId);
                        ViewState["CurrentTopicId"] = topicId.ToString();
                        LoadTopicsListOnly(ViewState["SubcourseName"].ToString());
                        LoadMCQs(topicId);
                    }
                }
                else
                {
                    litVideo.Text = "<div class='alert alert-danger'>You must complete the previous topic(s) first.</div>";
                }
            }
        }

        protected void btnBackToCourses_Click(object sender, EventArgs e)
        {
            pnlNavBar.Visible = false;
            pnlCourseCards.Visible = true;
            pnlVideoTopics.Visible = false;
            LoadAllSubCourses();
        }



        //------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------
        //         NAVBAR ACTIONS
        //--------------------------------------------------------------------------------

        protected void lnkMCQ_Click(object sender, EventArgs e)
        {
            pnlMCQ.Visible = true;
            pnlAssignments.Visible = false;
            pnlReviews.Visible = false;
        }

        protected void lnkAssignments_Click(object sender, EventArgs e)
        {
            pnlMCQ.Visible = false;
            pnlAssignments.Visible = true;
            pnlReviews.Visible = false;
        }

        protected void lnkReviews_Click(object sender, EventArgs e)
        {
            pnlMCQ.Visible = false;
            pnlAssignments.Visible = false;
            pnlReviews.Visible = true;
        }


        public string GetStars(double rateValue)
        {
            if (rateValue == 5)
                return "★★★★★";
            else if (rateValue == 4.5)
                return "★★★★½";
            else if (rateValue == 4)
                return "★★★★☆";
            else if (rateValue == 3.5)
                return "★★★½☆";
            else if (rateValue == 3)
                return "★★★☆☆";
            else if (rateValue == 2.5)
                return "★★½☆☆";
            else if (rateValue == 2)
                return "★★☆☆☆";
            else if (rateValue == 1.5)
                return "★½☆☆☆";
            else if (rateValue == 1)
                return "★☆☆☆☆";
            else if (rateValue == 0.5)
                return "½☆☆☆☆";
            else
                return "☆☆☆☆☆";
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            double rateValue;
            if (double.TryParse(TextBox1.Text.Trim(), out rateValue))
            {
                Label1.Text = GetStars(rateValue);
            }
            else
            {
                Label1.Text = "Invalid rating value.";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string rating = TextBox1.Text.Trim();
            string comment = TextBox2.Text.Trim();
            string stars = Label1.Text;

            if (Session["userEmail"] == null || ViewState["SubcourseName"] == null)
            {
                Response.Write("<script>alert('Session expired or invalid. Please re-login or reselect the course.');</script>");
                return;
            }

            string userEmail = Session["userEmail"].ToString();
            string subcourseName = ViewState["SubcourseName"].ToString();

            SqlCommand cmd = new SqlCommand("add_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@subcourse_name", subcourseName);
            cmd.Parameters.AddWithValue("@user_email", userEmail);
            cmd.Parameters.AddWithValue("@rating", Convert.ToDouble(rating));
            cmd.Parameters.AddWithValue("@comment", comment);
            cmd.Parameters.AddWithValue("@stars", stars);
            cmd.ExecuteNonQuery();

            Response.Write("<script>alert('Thank you for your feedback!');</script>");
            TextBox1.Text = "";
            TextBox2.Text = "";
            Label1.Text = "";
        }


        ///////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        ///MCQS



        private void LoadMCQs(int topicId)
        {
            string query = $"exec fetch_mcqs_by_topicId @topic_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@topic_id", topicId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            Session["MCQs"] = dt;

            btn.Visible = dt.Rows.Count > 0;
            lblScore.Visible = false;
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            int score = 0;
            int total = Repeater1.Items.Count;
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButtonList rbl = (RadioButtonList)(item.FindControl("rblOptions") ?? item.FindControl("RadioButtonList1"));
                HiddenField hf = (HiddenField)(item.FindControl("hfCorrect") ?? item.FindControl("HiddenField1"));
                if (rbl != null && hf != null)
                {
                    string userAns = rbl.SelectedValue;
                    string correctAns = hf.Value;
                    if (!string.IsNullOrEmpty(userAns))
                    {
                        if (userAns == correctAns)
                        {
                            rbl.ForeColor = Color.Green;
                            score = score + 1;
                        }
                        else
                        {
                            rbl.ForeColor = Color.Red;
                        }
                    }
                }
            }
            Button1.Enabled = false;
            lblScore.Text = $"Your Score: {score} / {total}";
            lblScore.Visible = true;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButtonList rbl = (RadioButtonList)(e.Item.FindControl("rblOptions") ?? e.Item.FindControl("RadioButtonList1"));
                HiddenField hfA = (HiddenField)(e.Item.FindControl("hfOptionA") ?? e.Item.FindControl("HiddenField2"));
                HiddenField hfB = (HiddenField)(e.Item.FindControl("hfOptionB") ?? e.Item.FindControl("HiddenField3"));
                HiddenField hfC = (HiddenField)(e.Item.FindControl("hfOptionC") ?? e.Item.FindControl("HiddenField4"));
                HiddenField hfD = (HiddenField)(e.Item.FindControl("hfOptionD") ?? e.Item.FindControl("HiddenField5"));
                if (rbl != null && hfA != null && hfB != null && hfC != null && hfD != null)
                {
                    rbl.Items.Add(new ListItem("A. " + hfA.Value, "A"));
                    rbl.Items.Add(new ListItem("B. " + hfB.Value, "B"));
                    rbl.Items.Add(new ListItem("C. " + hfC.Value, "C"));
                    rbl.Items.Add(new ListItem("D. " + hfD.Value, "D"));
                }
            }
        }

        protected void btnDownloadAssignment_Click(object sender, EventArgs e)
        {

            if (ViewState["CurrentTopicId"] == null)
            {
                Response.Write("<script>alert('No topic selected.');</script>");
                return;
            }

            int topicId = Convert.ToInt32(ViewState["CurrentTopicId"]);

 
            string topicName = null;
            SqlCommand getTopicNameCmd = new SqlCommand("select Topic FROM topic WHERE topic_id = @topicId", conn);
            getTopicNameCmd.Parameters.AddWithValue("@topicId", topicId);
            SqlDataReader reader = getTopicNameCmd.ExecuteReader();

            if (reader.Read())
            {
                topicName = reader["Topic"].ToString();
            }
            reader.Close();

            if (string.IsNullOrEmpty(topicName))
            {
                Response.Write("<script>alert('Topic name not found for the current topic ID.');</script>");
                return;
            }

            SqlCommand getAssignmentCmd = new SqlCommand("select assignment FROM Assignments where topicname = @topicname", conn);
            getAssignmentCmd.Parameters.AddWithValue("@topicname", topicName);

            string fileName = getAssignmentCmd.ExecuteScalar()?.ToString();

       
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Server.MapPath("~/Admin/Material/AdminAssignment/") + Path.GetFileName(fileName);
                if (File.Exists(filePath))
                {
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(fileName));
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    Response.Write("<script>alert('Assignment file not found on server.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('No assignment available for this topic.');</script>");
            }

        }

        protected void btnUploadAssignment_Click(object sender, EventArgs e)
        {
            if (!fuAssignment.HasFile)
            {
                lblUploadStatus.Text = "Please select a PDF file.";
                return;
            }


            if (Session["userEmail"] == null || ViewState["SubcourseName"] == null || ViewState["CurrentTopicId"] == null)
            {
                lblUploadStatus.Text = "Session or topic info missing.";
                return;
            }

            string userEmail = Session["userEmail"].ToString();
            string subcourseName = ViewState["SubcourseName"].ToString();
            int topicId = Convert.ToInt32(ViewState["CurrentTopicId"]);

            SqlCommand cmdTopic = new SqlCommand("select Topic from topic where topic_id = @tid", conn);
            cmdTopic.Parameters.AddWithValue("@tid", topicId);
            string topicName = cmdTopic.ExecuteScalar()?.ToString();

            if (string.IsNullOrEmpty(topicName))
            {
                lblUploadStatus.Text = "Topic name not found.";
                return;
            }

            string fileName = $"{userEmail}_{DateTime.Now.Ticks}_{Path.GetFileName(fuAssignment.FileName)}";
            string savePath = Server.MapPath("~/Assignments/") + fileName;
            fuAssignment.SaveAs(savePath);

            
            SqlCommand insertCmd = new SqlCommand(@"insert into UserAssignment
(user_email, subcoursename, topicname, filename) 
values (@user_email, @sub, @topic, @file)", conn);
            insertCmd.Parameters.AddWithValue("@user_email", userEmail);
            insertCmd.Parameters.AddWithValue("@sub", subcourseName);
            insertCmd.Parameters.AddWithValue("@topic", topicName);
            insertCmd.Parameters.AddWithValue("@file", fileName);
            insertCmd.ExecuteNonQuery();

            Response.Write("<script>alert('Assignment uploaded successfully')</script>");
        }

    }
}