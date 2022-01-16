using System.Xml.Serialization;

namespace InvoiceEngine.Core.XTO
{
    public class TransactionXto
    {
        [XmlAttribute("id")]
        public string TransactionID { get; set; }

        [XmlElement("TransactionDate")]
        public string TransactionDate { get; set; }

        [XmlElement("PaymentDetails")]
        public PaymentDetailXto PaymentDetails { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }
    }
}
