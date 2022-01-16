using System.Xml.Serialization;

namespace InvoiceEngine.Core.DTO
{
    public class TransactionDto
    {
        [XmlAttribute("id")]
        public string TransactionID { get; set; }

        [XmlElement("TransactionDate")]
        public string TransactionDate { get; set; }

        [XmlElement("PaymentDetails")]
        public PaymentDetailDto PaymentDetails { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }
    }
}
