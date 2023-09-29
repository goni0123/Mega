using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaNew.Models
{
    public class OrderInModel
    {
        public int Nalog{ get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal FreightPrice { get; set; }
        public bool Check { get; set; }
    }
}
