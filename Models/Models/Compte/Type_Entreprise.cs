using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Models
{
    [Table("Type_Entreprise")]
    public class Type_Entreprise
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le type est requis")]
        [StringLength(60, ErrorMessage = "La taille du type ne peut dépasser 60 characters")]
        public string Type { get; set; }
        [Required(ErrorMessage = "La description est requise")]
        [StringLength(150, ErrorMessage = "La taille de la description ne peut dépasser 150 characters")]
        public string Description { get; set; }

        [StringLength(150, ErrorMessage = "La taille de l'url ne peut dépasser 150 characters")]
        public string Url { get; set; }
        public DateTime DateOfCreation { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
