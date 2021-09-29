using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    [Table("Num_Vente")]
    public class Num_Vente
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le numero de reference est requis")]
        [StringLength(60, ErrorMessage = "La taille de la reference ne peut dépasser 60 characters")]
        public string Name { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
