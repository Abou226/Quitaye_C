using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Avarier")]
    public class Avarier
    {
        public Guid Id { get; set; }

        public Offre Offre { get; set; }
        [ForeignKey(nameof(Offre))]
        public Guid? OffreId { get; set; }
        [Required(ErrorMessage = "La quantité est requise")]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Quantité { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
    }
}
