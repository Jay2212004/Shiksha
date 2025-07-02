using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
        }
    }
}