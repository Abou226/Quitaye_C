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
        public Unité Unité { get; set; } = Unité.Pièce;

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [StringLength(120, ErrorMessage = "La taille de l'url ne peut dépasser 120 characters")]
        public string Url { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
