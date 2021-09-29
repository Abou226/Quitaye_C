using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("User")]
    public class User
    {
        public Guid Id { get; set; }

        [StringLength(60, ErrorMessage = "La taille du prenom ne peut depasser 60 characters")]
        public string Prenom { get; set; }

        [StringLength(60, ErrorMessage = "La taille du nom de famille de peut dépasser 60 characters")]
        public string Nom { get; set; }

        [StringLength(60, ErrorMessage = "La taille du nom d'utilisateur ne peut dépasser 60 characters")]
        public string Username { get; set; }

        [StringLength(100, ErrorMessage = "La taille du mot de passe ne peut dépasser 100 characters")]
        public string Password { get; set; }

        [StringLength(60, ErrorMessage = "La taille de 'Active' ne peut dépasser 60 characters")]
        public string Active { get; set; }
        public DateTime DateOfCreation { get; set; }

        [StringLength(100, ErrorMessage = "La taille de l'email ne peut dépasser 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le temps du serveur est requis")]
        public DateTime ServerTime { get; set; }

        [StringLength(400, ErrorMessage = "La taille de l'url ne peut dépasser 400 characters")]
        public string PhotoUrl { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Telephone> Telephones { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Email> Emails { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrperiseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
