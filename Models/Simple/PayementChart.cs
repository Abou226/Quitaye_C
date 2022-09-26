using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PayementChart
    {
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Nature { get; set; }
    }
}
