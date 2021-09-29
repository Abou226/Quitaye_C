using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Fournisseur
    {
        public Guid Id { get; set; }

        [StringLength(60, ErrorMessage = "Le nom ne peut dépasser 60 characters")]
        [Required(ErrorMessage = "Le nom est requis")]
        public string Nom { get; set; }
        public Genre Genre { get; set; } = Genre.Entreprise;

        [StringLength(150, ErrorMessage = "L'adresse ne peut dépasser 150 characters")]
        [Required(ErrorMessage = "L'adresse est requis")]
        public string Adresse { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
