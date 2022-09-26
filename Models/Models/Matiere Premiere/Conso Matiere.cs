using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Conso_Matiere")]
    public class Conso_Matiere
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Matière))]
        public Guid? MatiereId { get; set; }
        public Matière_Premiere Matière { get; set; }

        [Required(ErrorMessage = "La quantité est requise")]
        [Column(TypeName = "decimal (18, 0)")]
        public decimal Quantité { get; set; }
        public DateTime Date { get; set; }
        public Unité Unité { get; set; } = Unité.Pièce;
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
