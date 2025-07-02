using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.Admin
{
    public partial class ApproveRating : System.Web.UI.Page
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
                LoadReviewGrid();
            }
        }

        public void LoadReviewGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM CourseReviews WHERE is_approved = 0", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvReviews.DataSource = dt;
            gvReviews.DataBind();
        }


        protected void gvReviews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reviewId = Convert.ToInt32(e.CommandArgument);
            Session["ReviewID"] = reviewId;

            SqlCommand cmd = null;

            if (e.CommandName == "Approve")
            {
                cmd = new SqlCommand("sp_ApproveReview", conn);
            }
            else if (e.CommandName == "Reject")
            {
                cmd = new SqlCommand("sp_RejectReview", conn);
            }

            if (cmd != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", reviewId);
                cmd.ExecuteNonQuery();
                LoadReviewGrid();
            }
        }




    }








}