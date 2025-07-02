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
    public partial class UserAssignments : System.Web.UI.Page
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
                LoadUserAssignments();
            }
        }
        private void LoadUserAssignments()
        {
            SqlCommand cmd = new SqlCommand("select * from UserAssignment order by uploaded_on DESC", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvUserAssignments.DataSource = dt;
            gvUserAssignments.DataBind();
        }
        protected void gvUserAssignments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveScore")
            {
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                TextBox txtScore = (TextBox)gvUserAssignments.Rows[rowIndex].FindControl("txtScore");

                if (int.TryParse(txtScore.Text.Trim(), out int score))
                {
                    int uid = Convert.ToInt32(e.CommandArgument);

                   
                   
                        SqlCommand cmd = new SqlCommand("update UserAssignment set score = @score where Uid = @uid", conn);
                        cmd.Parameters.AddWithValue("@score", score);
                        cmd.Parameters.AddWithValue("@uid", uid);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    

                    LoadUserAssignments();
                }
            }
        }
        protected void gvUserAssignments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblScore = e.Row.FindControl("txtScore") as Label;
                if (lblScore != null && !string.IsNullOrEmpty(lblScore.Text))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }
    }
}


    
