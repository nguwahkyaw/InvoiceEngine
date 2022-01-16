using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEngine.Data.DataAccess
{
    public class InvoiceDAL
    {
        string consString = ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString;
        public void InsertBulkInvoices(DataTable dataTable)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(consString))
            {
                sqlBulkCopy.ColumnMappings.Clear();

                sqlBulkCopy.ColumnMappings.Add("TransactionID", "TransactionID");
                sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
                sqlBulkCopy.ColumnMappings.Add("Currency", "Currency");
                sqlBulkCopy.ColumnMappings.Add("TransactionDate", "TransactionDate");
                sqlBulkCopy.ColumnMappings.Add("Status", "Status");

                //Set the database table name.
                sqlBulkCopy.DestinationTableName = "dbo.Invoices";
                
                sqlBulkCopy.WriteToServer(dataTable);
            }
        }

        public void InsertBulkInvoices(string xmlString)
        {       
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertInvoice_xml"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@xml", xmlString);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
