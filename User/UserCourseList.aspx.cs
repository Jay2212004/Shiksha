using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.User
{
    public partial class UserCourseList : System.Web.UI.Page
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
                LoadCourseList();
            }
            }


      
        private void LoadCourseList()
        {

            string q = $"exec  getMasterCourseList";

            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            DropDownList1.DataSource = rd;
            DropDownList1.DataTextField = "course_name";
            DropDownList1.DataValueField = "course_name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--Select Course--"));
            rd.Close();

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourseName = DropDownList1.SelectedValue;

            if (!string.IsNullOrEmpty(selectedCourseName) && selectedCourseName != "--Select Course--")
            {
                string sortOrder = DropDownList2.SelectedValue == "High to Low" ? "DESC" : "ASC";
                LoadSubCourses(selectedCourseName, sortOrder);
            }
            else
            {
                rptSubCourses.DataSource = null;
                rptSubCourses.DataBind();
            }
        }
        private void LoadSubCourses(string courseName, string sortOrder = "ASC")
        {
            string q = $"exec GetSubCoursesByCourseName2 @course_name"; // updated proc name

            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@course_name", courseName);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (sortOrder == "DESC")
            {
                dt.DefaultView.Sort = "subcourses_price DESC";
            }
            else
            {
                dt.DefaultView.Sort = "subcourses_price ASC";
            }

            rptSubCourses.DataSource = dt;
            rptSubCourses.DataBind();
        }


        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourseName = DropDownList1.SelectedValue;

            if (!string.IsNullOrEmpty(selectedCourseName) && selectedCourseName != "--Select Course--")
            {
                string sortOrder = DropDownList2.SelectedValue == "High to Low" ? "DESC" : "ASC";
                LoadSubCourses(selectedCourseName, sortOrder);
            }


        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            string[] data = ((Button)sender).CommandArgument.Split('|');


            DataTable cart = Session["Cart"] as DataTable ?? new DataTable();
            if (cart.Columns.Count == 0)
            {
                cart.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SubcourseId"),
                    new DataColumn("SubcourseName"),
                    new DataColumn("SubcoursePrice"),
                    new DataColumn("Stars"),
                    new DataColumn("Thumbnail")
                });
            }

            cart.Rows.Add(data[0], data[1], data[2], data[3], data[4]);
            Session["Cart"] = cart;

            lblCartCount.Text = cart.Rows.Count.ToString();
        }
        protected void Cart_Click(object sender, EventArgs e)
        {
            Response.Redirect("userCart.aspx");

        }
    }
}