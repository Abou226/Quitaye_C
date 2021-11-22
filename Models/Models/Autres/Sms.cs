using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Sms
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Le senderId est requis")]
        [StringLength(20, ErrorMessage = "La taille du senderId ne peut dépaser 20 characters")]
        public string SenderId { get; set; }

        [Required(ErrorMessage = "Le telephone est requis")]
        [StringLength(20, ErrorMessage = "La taille du numero de telephone ne peut dépaser 20 characters")]
        public string Telephone { get; set; }

        [StringLength(60, ErrorMessage = "La taille du message ne peut dépaser 60 characters")]
        public string Message { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime SendDate { get; set; }

        [StringLength(20, ErrorMessage = "La taille du numero de telephone ne peut dépaser 20 characters")]
        public string Type { get; set; }
    }
}
