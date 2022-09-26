using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class GatoniniUpdate
    {
        public Guid Id { get; set; }
        public Plateforms Plateforms { get; set; } = Plateforms.Android;

        [Required(ErrorMessage = "La VersionCode est requis")]
        public int VersionCode { get; set; }

        [Required(ErrorMessage = "La VersionName est requis")]
        [StringLength(100, ErrorMessage = "La taille de la VersionName ne peut dépaser 100 characters")]
        public string VersionName { get; set; }

        public DateTime? DateOfExpiry { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public bool IsExpired { get; set; }
    }
}
