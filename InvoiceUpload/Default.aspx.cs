using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InvoiceUpload
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Upload(object sender, EventArgs e)
        {
            //Upload and save the file.
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string extension = Path.GetExtension(filename);
            string filePath = Server.MapPath("~/UploadFiles/") + filename; ;
            FileUpload1.SaveAs(filePath);
                      
            if (extension == ".csv")
            {
                UploadCSV(filePath);
            }
            else if (extension == ".xml")
            {
                UploadXML(filePath);
            }
            else
            {
                //  file is Invalid  
                Response.Write("Unknown Format");
                return;
            }

        }

        protected void UploadCSV(string filePath)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5]
                { new DataColumn("TransactionID", typeof(string)),
                new DataColumn("Amount", typeof(decimal)),
                new DataColumn("Currency",typeof(string)),
                new DataColumn("TransactionDate",typeof(DateTime)),
                new DataColumn("Status",typeof(string))
                }
                );


            string csvData = File.ReadAllText(filePath);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }

            string consString = ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name.
                    sqlBulkCopy.DestinationTableName = "dbo.Invoice";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
        }

        protected void UploadXML(string filePath)
        {
            string xml = File.ReadAllText(filePath);
            string constr = ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertInvoice_xml"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@xml", xml);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}