using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public DateTime SendDate { get; set; }

        [Required(ErrorMessage = "Le message est requis")]
        [StringLength(120, ErrorMessage = "La taille du message ne peut dépasser 120 characters")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        [StringLength(20, ErrorMessage = "La taille du titre ne peut dépasser 20 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Le topic est requis")]
        [StringLength(60, ErrorMessage = "La taille du topic ne peut dépasser 60 characters")]
        public string Topic { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
        public Guid? AuthorId { get; set; }

        [StringLength(120, ErrorMessage = "La taille du topic ne peut dépasser 120 characters")]
        public string Url { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
