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
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name.
                    sqlBulkCopy.DestinationTableName = "dbo.Invoices";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dataTable);
                    con.Close();
                }
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
