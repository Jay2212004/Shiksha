using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SikshaNew.Admin
{
    public partial class ApproveRatings : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["ShikshaCon"].ConnectionString;
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