using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
namespace SikshaNew.Admin.MasterCourse
{
    public partial class SubCourse : System.Web.UI.Page
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
            string subname = TextBox1.Text, status = DropDownList2.SelectedValue;
            double price = double.Parse(TextBox2.Text);
            string filename = Path.GetFileName(FileUpload1.FileName);
            string savePath = Server.MapPath("~/Admin/MasterCourse/Subcourse Thumb/") + filename;
            FileUpload1.SaveAs(savePath);
            string filePath = $"{subname} " + filename;
            string q = $"exec addsubcourse '{mcoursename}','{subname}','{filePath}','{price}','{status}'";
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
    }
}