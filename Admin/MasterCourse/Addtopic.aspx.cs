using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SikshaNew.Admin.MasterCourse
{
    public partial class Addtopic : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["ShikshaCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();
            fetchcourse();
            fetchsubcourse();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string mcoursename = DropDownList1.SelectedValue;
            string scoursename=DropDownList2.SelectedValue,videourl=TextBox2.Text;
            string topic = TextBox1.Text, status = DropDownList3.SelectedValue;
            string q = $"exec addtopic '{mcoursename}','{scoursename}','{topic}','{videourl}','{status}'";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('New Subcourse course added');</script>");
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
            string query = "SELECT DISTINCT subcourse_name FROM subcourses where status='Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DropDownList2.DataSource = rdr;
            DropDownList2.DataTextField = "subcourse_name";
            DropDownList2.DataValueField = "subcourse_name";
            DropDownList2.DataBind();

        }
    }
}