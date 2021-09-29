using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Quartier")]
    public class Quartier
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(60, ErrorMessage = "La taille du nom ne peut dépasser 60 characters")]
        public string Name { get; set; }

        [ForeignKey(nameof(Commune))]
        public Guid? CommuneId { get; set; }
        public Commune Commune { get; set; }

        [ForeignKey(nameof(Zone))]
        public Guid? ZoneId { get; set; }
        public Zone Zone { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
