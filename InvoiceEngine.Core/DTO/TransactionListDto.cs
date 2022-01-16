using System.Collections.Generic;
using System.Xml.Serialization;

namespace InvoiceEngine.Core.DTO
{
    [XmlRoot("Transactions")]
    public class TransactionListDto
    {
        [XmlElement("Transaction")]
        public List<TransactionDto> Transactions { get; set; }
    }
}
