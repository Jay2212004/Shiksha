using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.User
{
    public partial class User_MCQ : System.Web.UI.Page
    {
        SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            con.Open();
            if (!IsPostBack)
            {
                LoadTopics();
            }
        }
        private void LoadTopics()
        {
            string q = $"exec LoadTopics";
            SqlCommand cmd = new SqlCommand(q, con);
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
            string query = $"exec fetch_mcqs_by_topicId'{topicId}'";
            SqlCommand cmd = new SqlCommand(query, con);
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
        protected void Button1_Click(object sender, EventArgs e)
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
            Label1.Text = $"Your Score: {score} / {total}";
            Label1.Visible = true;
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