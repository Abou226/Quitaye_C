using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Promotion
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "L'url est requis")]
        [StringLength(100, ErrorMessage = "La taille de l'url ne peut dépaser 100 characters")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(60, ErrorMessage = "La taille du nom ne peut dépaser 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La description est requise")]
        [StringLength(160, ErrorMessage = "La taille de la description ne peut dépaser 160 characters")]
        public string Description { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(Type))]
        public Guid? TypeId { get; set; }
        public PromotionType Type { get; set; }

        [Column(TypeName = "decimal (18, 2)")]
        public decimal? QMin { get; set; }

        [Column(TypeName = "decimal (18, 2)")]
        public decimal? Reduction { get; set; }

        [Column(TypeName ="decimal (18, 2)")]
        public decimal? Quantité { get; set; }
        public List<PromotionOffre> OffreList { get; set; } = new List<PromotionOffre>();
        public bool AllProducts { get; set; } = true;
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public IFormFile Image { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
    }
}
