using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Style
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(60, ErrorMessage = "La taille du nom ne peut dépaser 60 characters")]
        public string Name { get; set; }

        [StringLength(120, ErrorMessage = "La taille de la description ne peut dépaser 120 characters")]
        public string Description { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "L'url est requis")]
        [StringLength(120, ErrorMessage = "La taille de l'url ne peut dépaser 120 characters")]
        public string Url { get; set; }

        public bool Style_Special { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
