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

        [ForeignKey(nameof(Quartier))]
        public Guid? QuartierId { get; set; }
        public Quartier Quartier { get; set; }
        public DateTime DateOfCreation { get; set; }

        [ForeignKey(nameof(Type))]
        public Guid? Type_Id { get; set; }
        public Type_Entreprise Type { get; set; } 
        public int Nb_Employés { get; set; }

        //[ForeignKey(nameof(Owner))]
        [StringLength(60, ErrorMessage = "La taille de l'heure ne peut dépasser 60 characters")]
        public string TypeHeure { get; set; }
        public Guid? OwnerId { get; set; }
        //public User Owner { get; set; }
    }
}
