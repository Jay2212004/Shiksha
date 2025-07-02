using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.Admin.Subscription
{
    public partial class Subscription_List : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            conn.Open();
            if (!IsPostBack)
            {
                LoadSubscriptions();
            }
        }

        private void LoadSubscriptions()
        {
            SqlCommand cmd = new SqlCommand("sp_GetAllSubscriptions", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Add hardcoded Validity
            dt.Columns.Add("Validity", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["Validity"] = "30 Days";
            }

            GridViewSubscriptions.DataSource = dt;
            GridViewSubscriptions.DataBind();
        }

        protected void GridViewSubscriptions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSubscriptions.EditIndex = e.NewEditIndex;
            LoadSubscriptions();
        }

        protected void GridViewSubscriptions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSubscriptions.EditIndex = -1;
            LoadSubscriptions();
        }

        private DataTable GetSubscriptionTypes()
        {
            SqlCommand cmd = new SqlCommand("exec Fetch_Active_MasterSubscriptions", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        protected void GridViewSubscriptions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSubscriptionType");
                if (ddl != null)
                {
                    ddl.DataSource = GetSubscriptionTypes();
                    ddl.DataTextField = "Sname";
                    ddl.DataValueField = "Sname";
                    ddl.DataBind();

                    string currentValue = DataBinder.Eval(e.Row.DataItem, "SubscriptionType").ToString();
                    if (ddl.Items.FindByValue(currentValue) != null)
                    {
                        ddl.SelectedValue = currentValue;
                    }
                }
            }
        }

        protected void GridViewSubscriptions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewSubscriptions.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridViewSubscriptions.Rows[e.RowIndex];

            string status = ((TextBox)row.FindControl("txtStatus")).Text.Trim();
            string price = ((TextBox)row.FindControl("txtPrice")).Text.Trim();
            string duration = ((TextBox)row.FindControl("txtDuration")).Text.Trim();

            DropDownList ddlType = (DropDownList)row.FindControl("ddlSubscriptionType");
            string subscriptionType = ddlType.SelectedValue;

            string query = $"exec Update_Subscription {id}, '{status}', '{subscriptionType}', '{price}', '{duration}'";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.ExecuteNonQuery();


            GridViewSubscriptions.EditIndex = -1;
            LoadSubscriptions();
        }


        protected void GridViewSubscriptions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridViewSubscriptions.DataKeys[e.RowIndex].Value);
            SqlCommand cmd = new SqlCommand($"exec Delete_Subscription {id}", conn);
            cmd.ExecuteNonQuery();
            LoadSubscriptions();
        }
    }
}