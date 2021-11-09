using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class File
    {
        [StringLength(150, ErrorMessage = "La taille de l'url ne peut dépasser 150 characters")]
        public string Url { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
