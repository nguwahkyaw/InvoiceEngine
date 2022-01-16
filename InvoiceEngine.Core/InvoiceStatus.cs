using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEngine.Core
{
    public enum CSVInvoiceStatus
    {
        Approved,

        Failed,

        Finished
    }

    public enum XMLInvoiceStatus
    {
        Approved,

        Rejected,

        Done
    }
}
