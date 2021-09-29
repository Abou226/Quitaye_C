using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    [Table("Client")]
    public class Client
    {
        public Guid Id { get; set; }
        [StringLength(60, ErrorMessage = "Le prenom ne peut dépasser 60 characters")]
        [Required(ErrorMessage = "Le prenom est requis")]
        public string Prenom { get; set; }

        [StringLength(60, ErrorMessage = "Le nom ne peut dépasser 60 characters")]
        [Required(ErrorMessage = "Le nom est requis")]
        public string Nom { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public Genre Genre { get; set; } = Genre.Femme;
    }
}
