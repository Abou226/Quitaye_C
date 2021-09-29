using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Email
    {
        public Guid Id { get; set; }

        [StringLength(200, ErrorMessage = "L'adresse email ne peut dépasser 200 characters")]
        [Required(ErrorMessage = "L'adresse email est requis")]
        public string Adresse { get; set; }
        public Guid? OwnerId { get; set; }
        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
    }
}
