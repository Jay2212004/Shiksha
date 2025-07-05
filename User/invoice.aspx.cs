using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SikshaNew.User
{
    public partial class invoice : System.Web.UI.Page
    {
        SqlConnection conn;
        decimal grandTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            if (!IsPostBack)
            {
                string paymentId = Request.QueryString["payment_id"];

                if (!string.IsNullOrEmpty(paymentId))
                {
                    LoadInvoice(paymentId);
                }
                else
                {
                    Response.Write("<script>alert('Payment ID not found.');</script>");
                }
            }
        }

        private void LoadInvoice(string paymentId)
        {
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cnf))
            {
                conn.Open();
                string q = "SELECT SubcourseName, SubcoursePrice, PaymentId, Status, PaymentDate " +
                           "FROM Transactions WHERE PaymentId = @pid";

                using (SqlCommand cmd = new SqlCommand(q, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", paymentId);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rptInvoice.DataSource = rdr;
                        rptInvoice.DataBind();
                    }
                }

         
                string q2 = "SELECT TOP 1 PaymentId, Status, PaymentDate FROM Transactions WHERE PaymentId = @pid";
                using (SqlCommand cmd2 = new SqlCommand(q2, conn))
                {
                    cmd2.Parameters.AddWithValue("@pid", paymentId);
                    using (SqlDataReader reader = cmd2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblPaymentId.Text = reader["PaymentId"].ToString();
                            lblStatus.Text = reader["Status"].ToString();
                            lblDate.Text = Convert.ToDateTime(reader["PaymentDate"]).ToString("dd-MM-yyyy");
                        }
                    }
                }
            }
        }

        protected void rptInvoice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblPrice = (Label)e.Item.FindControl("lblPrice");
                if (lblPrice != null && decimal.TryParse(lblPrice.Text, out decimal price))
                {
                    grandTotal += price;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lblTotal.Text = "Total Amount: ₹" + grandTotal.ToString("F2");
            }
        }

        protected void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph title = new Paragraph("Payment Invoice", FontFactory.GetFont("Arial", 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Chunk("\n"));

                doc.Add(new Paragraph("Payment ID: " + lblPaymentId.Text));
                doc.Add(new Paragraph("Status: " + lblStatus.Text));
                doc.Add(new Paragraph("Payment Date: " + lblDate.Text));
                doc.Add(new Chunk("\n"));

                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.AddCell("Subcourse Name");
                table.AddCell("Price (₹)");

                foreach (RepeaterItem item in rptInvoice.Items)
                {
                    string name = ((Label)item.FindControl("lblSubcourseName")).Text;
                    string price = ((Label)item.FindControl("lblPrice")).Text;

                    table.AddCell(name);
                    table.AddCell(price);
                }

                doc.Add(table);
                doc.Add(new Chunk("\n"));

                string totalText = lblTotal.Text.Replace("Total Amount: ₹", "").Trim();
                doc.Add(new Paragraph("Total Amount: ₹" + totalText, FontFactory.GetFont("Arial", 12, Font.BOLD)));

                doc.Close();

                byte[] bytes = ms.ToArray();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Invoice.pdf");
                Response.BinaryWrite(bytes);
                Response.End();
            }
        }
    
}

