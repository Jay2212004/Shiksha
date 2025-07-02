using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.Admin.Subscription
{
    public partial class AddSubscription : System.Web.UI.Page
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
                fetchSubscription();
                fetchCourseList();
            }
        }

        public void fetchSubscription()
        {
            string query = "SELECT DISTINCT Sname FROM MasterSubscribtion WHERE status = 'Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            DropDownList1.DataSource = rdr;
            DropDownList1.DataTextField = "Sname";
            DropDownList1.DataValueField = "Sname";
            DropDownList1.DataBind();
            rdr.Close();
        }

        public void fetchCourseList()
        {
            string query = "SELECT DISTINCT course_name FROM mastercourse WHERE status = 'Active'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            DropDownList2.DataSource = rdr;
            DropDownList2.DataTextField = "course_name";
            DropDownList2.DataValueField = "course_name";
            DropDownList2.DataBind();
            rdr.Close();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = DropDownList2.SelectedValue;
            string query = $"SELECT subcourse_name FROM subcourses WHERE Mastercourse = '{selectedCourse}'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            CheckBoxList1.Items.Clear();
            while (rdr.Read())
            {
                string subName = rdr["subcourse_name"].ToString();
                CheckBoxList1.Items.Add(new ListItem(subName));
            }

            rdr.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string subType = DropDownList1.SelectedValue;
            string courseName = DropDownList2.SelectedValue;
            string price = TextBox1.Text.Trim();
            string duration = TextBox2.Text.Trim();
            string iconPath = "";
            if (FileUpload1.HasFile)
            {
                string filename = System.IO.Path.GetFileName(FileUpload1.FileName);
                string folderPath = Server.MapPath("~/Uploads/Icons/");
                System.IO.Directory.CreateDirectory(folderPath); // ensure folder exists
                string filePath = folderPath + filename;

                FileUpload1.SaveAs(filePath);
                iconPath = "~/Uploads/Icons/" + filename;
            }

            int count = 0;

            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    string subcourse = item.Text;

                    string query = $"exec SC_subscription '{courseName}', '{subcourse}', '{subType}', '{price}', '{duration}', '{iconPath}'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    count++;
                }
            }

            LabelMessage.Text = count > 0 ? "Subscriptions added successfully!" : "Please select at least one subcourse.";
        }

    }
}