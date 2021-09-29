using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Num_Payement")]
    public class Num_Payement
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "La reference est requise")]
        [StringLength(60, ErrorMessage = "La taille de la reference ne peut dépasser 60 characters")]
        public string Reference { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
