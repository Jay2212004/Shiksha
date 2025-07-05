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
    public partial class LoginLogs : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM loginlogs", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMessage.Text = ""; // Clear previous messages

            int userId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string username = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string userRole = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string lastLoginText = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            if (DateTime.TryParse(lastLoginText, out DateTime lastLogin))
            {
                string formattedDate = lastLogin.ToString("yyyy-MM-dd HH:mm:ss");
                string query = $"UPDATE loginlogs SET username='{username}', user_role='{userRole}', lastlogin='{formattedDate}' WHERE user_id={userId}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                GridView1.EditIndex = -1;
                BindGrid();
            }
            else
            {
                lblMessage.Text = "⚠ Invalid datetime format. Please use 'yyyy-MM-dd HH:mm:ss' format.";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string query = $"DELETE FROM loginlogs WHERE user_id={userId}";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();

            BindGrid();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}