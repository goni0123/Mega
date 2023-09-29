using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaNew.Models
{
    public class IncomingModel
    {
            public int NalogNr { get; set; }
            public string Truck { get; set; }
            public string Rit { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int KM { get; set; }
            public int WorkDays { get; set; }
            public string ExtraCosts { get; set; }
            public string ExtraCostsAttachment { get; set; }
            public string Invoice { get; set; }
            public string InvoiceAttachment { get; set; }
            public string Comment { get; set; }
            public string CommentAttachment { get; set; }
            public bool Check { get; set; }
            public string Driver { get; set; }
        }
}
