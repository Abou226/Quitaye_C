using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("UserInvitation")]
    public class UserInvitation
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La quantité est requise")]
        [StringLength(100, ErrorMessage = "La taille de l'email ne peut dépasser 100 characters")]
        public string Email { get; set; }

        public DateTime DateOfInvitation { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? InviterId { get; set; }
        public User User { get; set; }

        public bool Accepted { get; set; }
        public bool Confirmed { get; set; }
    }

    
}
