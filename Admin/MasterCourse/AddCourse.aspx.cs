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
    public partial class AddCourse : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {

            string cnf = ConfigurationManager.ConnectionStrings["ShikshaCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cname = TextBox1.Text,status = DropDownList1.SelectedValue;
            string filename = Path.GetFileName(FileUpload1.FileName);
            string savePath = Server.MapPath("~/Admin/MasterCourse/MasterThumbnail/") + filename;
            FileUpload1.SaveAs(savePath);
            string filePath = $"{cname} " + filename;
            string q = $"exec addmastercourse '{cname}','{filePath}','{status}'";
            SqlCommand cmd = new SqlCommand(q,conn);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('New Master course added');</script>");
        }
    }
}