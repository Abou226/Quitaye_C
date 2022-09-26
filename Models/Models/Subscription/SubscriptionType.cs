using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class SubscriptionType
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le type est requis")]
        [StringLength(60, ErrorMessage = "La taille du type ne peux dépasser 60 charactères")]
        public string Type { get; set; }

        public Guid? EntrepriseId { get; set; }
    }
}
