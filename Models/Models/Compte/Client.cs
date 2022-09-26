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

        [StringLength(60, ErrorMessage = "Le nom d'utilisateur ne peut dépasser 60 characters")]
        public string Username { get; set; }

        [StringLength(100, ErrorMessage = "Le mot de passe ne peut dépasser 100 characters")]
        public string Password { get; set; }

        [StringLength(120, ErrorMessage = "La PhotoUrl ne peut dépasser 120 characters")]
        public string PhotoUrl { get; set; }

        [StringLength(100, ErrorMessage = "L'email ne peut dépasser 100 characters")]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "Le numero de telephone ne peut dépasser 15 characters")]
        public string Telephone { get; set; }

        public DateTime DateOfCreation { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public Genre Genre { get; set; } = Genre.Femme;

        [NotMapped]
        public bool Selected { get; set; }
    }
}
