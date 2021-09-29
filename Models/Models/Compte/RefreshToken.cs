using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public Guid RefreshTokenId { get; set; }

        [Required(ErrorMessage = "Le token est requis")]
        public string Token { get; set; }

        [Required(ErrorMessage = "La data de l'expiration est requise")]
        public DateTime DateOfExpiry { get; set; }

        [Required(ErrorMessage = "La date de création est requise")]
        public DateTime DateOfIssue { get; set; }

        [Required(ErrorMessage = "Refreshable requis")]
        [StringLength(60, ErrorMessage = "La taille de refreshable ne peut dépasser 60 characters")]
        public string Refreshable { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Le temps du serveur est requis")]
        public DateTime ServerTime { get; set; }
    }
}
