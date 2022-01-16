using System.Xml.Serialization;

namespace InvoiceEngine.Core.XTO
{    
    public class PaymentDetailXto
    {
        [XmlElement("Amount")]
        public string Amount { get; set; }

        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}
