
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    
    public class Telephone
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le numero est requis")]
        [StringLength(15, ErrorMessage = "La taille du numero ne peut dépasser 15 characters")]
        public string Number { get; set; }
        public Guid? OwnerId { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
