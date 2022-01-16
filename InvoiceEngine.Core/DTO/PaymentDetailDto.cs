using System.Xml.Serialization;

namespace InvoiceEngine.Core.DTO
{    
    public class PaymentDetailDto
    {
        [XmlElement("Amount")]
        public string Amount { get; set; }

        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}
