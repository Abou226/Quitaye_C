using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Table("ModePayement")]
    public class ModePayement
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(60, ErrorMessage ="La taille du nom ne peut dépasser 60 characters")]
        public string Name { get; set; }
        public bool MadeForAgent { get; set; } = true;
        public bool MadeForAdmin { get; set; } = true;

        [StringLength(50, ErrorMessage = "La taille du nom ne peut dépasser 50 characters")]
        public string RefCompte { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
