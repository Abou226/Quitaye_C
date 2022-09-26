using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ChartData
    {
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Quantité { get; set; }
        public string Marque { get; set; }
        public string Style { get; set; }
        public string Categorie { get; set; }
        public string Taille { get; set; }
        public string Model { get; set; }
    }
}
