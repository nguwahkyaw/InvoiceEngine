using System.Collections.Generic;
using System.Xml.Serialization;

namespace InvoiceEngine.Core.XTO
{
    [XmlRoot("Transactions")]
    public class TransactionListXto
    {
        [XmlElement("Transaction")]
        public List<TransactionXto> Transactions { get; set; }
    }
}
