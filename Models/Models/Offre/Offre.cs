using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Offre")]
    public class Offre
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Niveau))]
        public Guid? NiveauId { get; set; }

        //[ForeignKey(nameof(Occasion))]
        //public Guid? OccasionId { get; set; }
        public List<OccasionList> Occasionss { get; set; } = new List<OccasionList>();
        public Niveau Niveau { get; set; }

        [ForeignKey(nameof(Marque))]
        public Guid? MarqueId { get; set; }
        public Marque Marque { get; set; }

        [ForeignKey(nameof(Style))]
        public Guid? StyleId { get; set; }
        public Style Style { get; set; }

        [ForeignKey(nameof(Categorie))]
        public Guid? CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        [ForeignKey(nameof(Model))]
        public Guid? ModelId { get; set; }
        public Model Model { get; set; }
        public Taille Taille { get; set; }
        [ForeignKey(nameof(Taille))]
        public Guid? TailleMinId { get; set; }

        [StringLength(60, ErrorMessage = "La taille du nom ne peut dépasser 60 characters")]
        public string Nom { get; set; }
        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Le prix unité est requis")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Prix_Unité { get; set; }

        [StringLength(120, ErrorMessage = "La taille de l'url ne peut dépasser 120 characters")]
        public string Url { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
