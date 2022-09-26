using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Livraison")]
    public class Livraison
    {
        public Guid  Id { get; set; }
        [ForeignKey(nameof(Vente))]
        public Guid? VenteId { get; set; }
        public Vente Vente { get; set; }
        public DateTime Date { get; set; }
        public bool Livré { get; set; }
        [StringLength(60, ErrorMessage ="La taille de raison ne peut dépasser 60 characters")]
        public string Raison { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
    }
}
