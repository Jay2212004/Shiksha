using System.Web.UI;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SikshaNew.User
{
    public partial class userCart : System.Web.UI.Page
    {
        SqlConnection conn;
        DataTable cart;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();
            if (!IsPostBack)
            {
                BindCart();
            }

        }
        private void BindCart()
        {
            cart = Session["Cart"] as DataTable;

            if (cart != null)
            {
                gvCartItems.DataSource = cart;
                gvCartItems.DataBind();
            }
        }
        protected void btnAddMoreCourses_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserCourseList.aspx");
        }
        protected void gvCartItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            cart = Session["Cart"] as DataTable;
            if (cart == null) return;

            string id = gvCartItems.DataKeys[e.RowIndex].Value.ToString();

            foreach (DataRow dr in cart.Select("SubcourseId = '" + id + "'"))
            {
                cart.Rows.Remove(dr);
                break;
            }

            Session["Cart"] = cart;
            BindCart();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("check.aspx");

        }
    }
}