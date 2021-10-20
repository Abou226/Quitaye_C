using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Models
{
    [Table("Pays")]
    public class Pays
    {
        public Guid Id { get; set; }
        //[Required(ErrorMessage = "Le nom est requis")]
        [StringLength(60, ErrorMessage = "La taille du nom ne peut dépasser 60 characters")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Le nom_En est requis")]
        [StringLength(60, ErrorMessage = "La taille du nom_En ne peut dépasser 60 characters")]
        public string NameEn { get; set; }
        public int Indicatif { get; set; }
        //[Required(ErrorMessage = "Le code Alpha_2 est requis")]
        [StringLength(2, ErrorMessage = "La taille du code Alpha_2 ne peut dépasser 2 characters")]
        public string Alpha_2 { get; set; }
        //[Required(ErrorMessage = "Le code Alpha_3 est requis")]
        [StringLength(3, ErrorMessage = "La taille du Alpha_3 ne peut dépasser 3 characters")]
        public string Alpha_3 { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
