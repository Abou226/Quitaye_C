using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Models
{
    [Table("Matiere_Premiere")]
    public class Matière_Premiere
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(60, ErrorMessage = "La taille du nom ne peut dépasser 60 characters")]
        public string Name { get; set; }

        [StringLength(60, ErrorMessage = "La taille de la reference ne peut dépasser 60 characters")]
        public string Reference { get; set; }

        [ForeignKey(nameof(Unité))]
        public Guid? UnitéMatièreId { get; set; }
        public UnitéMatière Unité { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantité { get; set; }

        public string UnitéName { get; set; }

        public bool Active { get; set; } = true;

        [StringLength(120, ErrorMessage = "La taille de l'url ne peut dépasser 120 characters")]
        public string Url { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
