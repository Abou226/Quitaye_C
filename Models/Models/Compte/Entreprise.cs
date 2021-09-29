using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Entreprise")]
    public class Entreprise
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(60, ErrorMessage = "La taille du nom ne peut dépasser 60 characters")]
        public string Name { get; set; }

        [ForeignKey(nameof(Ville))]
        public Guid VilleId { get; set; }
        public Ville Ville { get; set; }

        [StringLength(120, ErrorMessage = "L'adresse ne peut dépasser 120 characters")]
        public string Adresse { get; set; }
        
        public DateTime DateOfCreation { get; set; }

        public Type_Entreprise Type { get; set; } = Type_Entreprise.Education;

        [ForeignKey(nameof(Owner))]
        public Guid? OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
