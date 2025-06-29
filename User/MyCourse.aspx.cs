using Microsoft.Win32;
using SikshaNew.Admin.MasterCourse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SikshaNew.User
{
    public partial class MyCourse : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["ShikshaCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                LoadAllSubCourses();
                LoadTopics();

            }
        }


        private string GetUserIdentifier()
        {
            // This method will retrieve either the email or fullname from session
            if (Session["userEmail"] != null)
                return Session["userEmail"].ToString();
            else if (Session["userFullname"] != null)
                return Session["userFullname"].ToString();
            return null;
        }


        public void LoadTopicsListOnly(string subcourseName)
        {
            //string userEmail = Session["userEmail"]?.ToString();
            //if (string.IsNullOrEmpty(userEmail)) return;

            string userIdentifier = GetUserIdentifier();
            if (string.IsNullOrEmpty(userIdentifier)) return;

            //SqlCommand cmd = new SqlCommand("sp_GetAllowedTopics", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@user_email", userEmail);
            //cmd.Parameters.AddWithValue("@subcourse_name", subcourseName);

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

            //string userEmail = Session["userEmail"]?.ToString();
            //if (string.IsNullOrEmpty(userEmail)) return;

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
                        //MarkTopicCompleted(userEmail, subcourseName, topicId);
                        MarkTopicCompleted(userIdentifier, subcourseName, topicId);
                        return;
                    }
                }

                // All completed
                var lastWatched = dt.AsEnumerable().LastOrDefault(r => Convert.ToBoolean(r["is_completed"]));
                if (lastWatched != null)
                {
                    litVideo.Text = $"<iframe width='100%' height='400px' src='{ConvertToEmbedUrl(lastWatched["video_url"].ToString())}' frameborder='0' allowfullscreen></iframe>";
                    ViewState["CurrentTopicId"] = lastWatched["topic_id"].ToString();
                }
            }

        }


        //public void MarkTopicCompleted(string user, string subcourse, int topicId)
        public void MarkTopicCompleted(string userIdentifier, string subcourse, int topicId)
        {
            SqlCommand cmd = new SqlCommand("sp_MarkTopicCompleted", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@user_email", user);
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
            
                SqlCommand cmd = new SqlCommand("sp_GetAllActiveSubCourses", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptSubCourseCard.DataSource = dt;
                rptSubCourseCard.DataBind();
            
        }

        protected void rptSubCourseCard_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            //Response.Write("<script>alert('Clicked');</script>");
            if (e.CommandName == "Play")
            {
                ViewState["SubcourseName"] = e.CommandArgument.ToString();
                pnlCourseCards.Visible = false;
                pnlVideoTopics.Visible = true;
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

                //string userEmail = Session["userEmail"]?.ToString();
                //if (string.IsNullOrEmpty(userEmail)) return;
                string userIdentifier = GetUserIdentifier();
                if (string.IsNullOrEmpty(userIdentifier)) return;

                DataTable allTopics = ViewState["AllTopics"] as DataTable;
                if (allTopics == null) return;

                var topicList = allTopics.AsEnumerable().ToList();
                int topicIndex = topicList.FindIndex(r => Convert.ToInt32(r["topic_id"]) == topicId);

                bool canWatch = isCompleted || topicList.Take(topicIndex).All(r => Convert.ToBoolean(r["is_completed"]));

                if (canWatch)
                {
                    litVideo.Text = $"<iframe width='100%' height='400px' src='{ConvertToEmbedUrl(url)}' frameborder='0' allowfullscreen></iframe>";

                    if (!isCompleted)
                    {
                        //MarkTopicCompleted(userEmail, ViewState["SubcourseName"].ToString(), topicId);
                        MarkTopicCompleted(userIdentifier, ViewState["SubcourseName"].ToString(), topicId);
                        ViewState["CurrentTopicId"] = topicId.ToString();
                        LoadTopicsListOnly(ViewState["SubcourseName"].ToString());
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

        private void LoadTopics()
        {
            string loadTopics = $"exec LoadTopics";
            SqlCommand cmd = new SqlCommand(loadTopics, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList1.DataSource = rdr;
            DropDownList1.DataTextField = "Topic";
            DropDownList1.DataValueField = "topic_id";
            DropDownList1.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMCQs(int.Parse(DropDownList1.SelectedValue));
        }
        private void LoadMCQs(int topicId)
        {
            string mcqbytopic = $"exec fetch_mcqs_by_topicId'{topicId}'";
            SqlCommand cmd = new SqlCommand(mcqbytopic, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (!dt.Columns.Contains("correct_answer"))
            {
                throw new Exception("CorrectAnswer column is missing in the result of fetch_mcqs_by_topic.");
            }
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            Session["MCQs"] = dt;
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


    }

}