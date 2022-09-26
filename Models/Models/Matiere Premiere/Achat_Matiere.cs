using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Achat_Matiere")]
    public class Achat_Matiere
    {
        public Guid Id { get; set; }
        public Matière_Premiere Matière { get; set; }
        [ForeignKey(nameof(Matière))]
        public Guid? MatiereId { get; set; }

        [Required(ErrorMessage = "La quantité est requise")]
        [Column(TypeName = "decimal (18, 0)")]
        public decimal Quantité { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Unité))]
        public Guid? UnitéMatièreId { get; set; }
        public UnitéMatière Unité { get; set; }
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
