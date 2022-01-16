using InvoiceEngine.Core;
using InvoiceEngine.Core.Constants;
using InvoiceEngine.Core.DTO;
using InvoiceEngine.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace InvoiceEngine.Services.Services
{
    public class FileUploadService
    {
        InvoiceDAL invoiceDAL = new InvoiceDAL();
        CultureInfo provider = CultureInfo.InvariantCulture;

        public void UploadCSV(string filePath)
        {
            string csvString = File.ReadAllText(filePath);
            string noQuotesCsvString = csvString.Replace("\"", "");
            DataTable dt = ConvertCSVToDatatable(noQuotesCsvString);
            invoiceDAL.InsertBulkInvoices(dt);
        }

        public void UploadXML(string filePath)
        {
            var transactionList = new TransactionListDto();
            XmlSerializer serializer = new XmlSerializer(typeof(TransactionListDto));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                transactionList = (TransactionListDto)serializer.Deserialize(fileStream);
            }



            //string xml = File.ReadAllText(filePath);
            //invoiceDAL.InsertBulkInvoices(xml);
        }

        private DataTable ConvertCSVToDatatable(string csvString)
        {

            DataTable dt = GetInvoiceTempDataTable();

            foreach (string row in csvString.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    DataRow dr = dt.NewRow();
                    string[] fieldArr = row.Split(',');

                    dr["TransactionID"] = fieldArr[0];

                    if (double.TryParse(fieldArr[1], out double amount))
                    {
                        dr["Amount"] = amount;
                    }
                    else
                    {
                        throw new FormatException($"Amount [{fieldArr[1]}]");
                    }

                    if (ISO._4217.CurrencyCodesResolver.Codes.Any(c => c.Code == fieldArr[2]))
                    {
                        dr["Currency"] = fieldArr[2];
                    }
                    else
                    {
                        throw new FormatException($"Currency Code [{fieldArr[2]}]");
                    }

                    if (DateTime.TryParseExact(
                        fieldArr[3],
                        Constants.CSVDateFormat,
                        provider,
                        DateTimeStyles.None,
                        out DateTime transactionDate))
                    {
                        dr["TransactionDate"] = transactionDate;
                    }
                    else
                    {
                        throw new FormatException($"Transaction Date [{fieldArr[3]}]");
                    }

                    if (Enum.IsDefined(typeof(CSVInvoiceStatus), fieldArr[4]))
                    {
                        dr["Status"] = fieldArr[4];
                    }
                    else
                    {
                        throw new FormatException($"Status [{fieldArr[4]}]");
                    }
                }
            }
            return dt;
        }

        private DataTable GetInvoiceTempDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TransactionID", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Currency", typeof(string));
            dt.Columns.Add("TransactionDate", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));
            return dt;
        }
    }
}
