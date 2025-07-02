using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SikshaNew
{
    public partial class LandingPage : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {

            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();
            if (!IsPostBack)
            {
                LoadSubcourses();
            }
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int subcourseId = Convert.ToInt32(btn.CommandArgument);

            Response.Write("<script>alert('Please Login First!!! ') ;</script>");
        }


        private void LoadSubcourses()
        {

            string q = $"exec Landing_Page";

            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rptSubcourses.DataSource = dt;
            rptSubcourses.DataBind();
        }
        private DataTable LoadComments(int subcourseId)
        {

            string q = $"exec feedback '{subcourseId}'";
            SqlCommand cmd = new SqlCommand(q, conn);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        protected void rptSubcourses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int subcourseId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "subcourse_id"));
                Repeater rptComments = (Repeater)e.Item.FindControl("rptComments");
                rptComments.DataSource = LoadComments(subcourseId);
                rptComments.DataBind();
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

    }


}


