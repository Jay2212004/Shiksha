using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.Admin
{
    public partial class Users_List : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            string q = $"exec fetch_users";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (row["CreatedBy"] == DBNull.Value || string.IsNullOrWhiteSpace(row["CreatedBy"].ToString()))
                {
                    row["CreatedBy"] = "Admin";
                }
            }

            GridViewUsers.DataSource = dt;
            GridViewUsers.DataBind();
        }

        protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUsers.EditIndex = e.NewEditIndex;
            LoadUsers();
        }

        protected void GridViewUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUsers.EditIndex = -1;
            LoadUsers();
        }

        protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userId = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridViewUsers.Rows[e.RowIndex];

            string fullName = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string email = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string role = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();
            string status = ((TextBox)row.Cells[4].Controls[0]).Text.Trim();
            string modifiedBy = "Admin";

            string query = $"exec sp_UpdateUser '{userId}', '{fullName}', '{email}', '{role}', '{status}', '{modifiedBy}'";

            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            GridViewUsers.EditIndex = -1;
            LoadUsers();
        }

        protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);
            
                string query = $"exec sp_DeleteUser '{userId}'";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception) {
                Response.Write("<script>alert('Can't as the user has performed a transaction')</script>");
            }

            LoadUsers();
        }
    }
}