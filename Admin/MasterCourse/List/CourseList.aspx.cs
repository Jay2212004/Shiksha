using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SikshaNew.Admin.MasterCourse.List
{
    public partial class CourseList : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();
            // bindgrid();

        }

        public void bindgrid()
        {
            string q = "AllMasterCourse";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);



            foreach (DataRow row in dt.Rows)
            {
                if (row["course_thumbnail"] != DBNull.Value)
                {
                    string filename = row["course_thumbnail"].ToString();
                    row["course_thumbnail"] = ResolveUrl("~/Admin/MasterCourse/MasterThumbnail/") + filename;
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

    }
}