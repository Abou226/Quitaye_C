using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Produit_Fini")]
    public class Produit_Fini
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Offre))]
        public Guid? OffreId { get; set; }
        public Offre Offre { get; set; }
        [Required(ErrorMessage = "La quantité est requise")]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Quantité { get; set; }
        public DateTime Date { get; set; }
        public Entreprise Entreprise { get; set; }
        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Guid? UserId { get; set; }

        [StringLength(120, ErrorMessage = "La taille de l'url ne peut dépasser 120 characters")]
        public string Url { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
