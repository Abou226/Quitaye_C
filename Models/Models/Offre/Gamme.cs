using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Gamme")]
    public class Gamme
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Categorie))]
        public Guid? CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        [ForeignKey(nameof(Marque))]
        public Guid? MarqueId { get; set; }
        public Marque Marque { get; set; }

        [ForeignKey(nameof(Style))]
        public Guid? StyleId { get; set; }
        public Style Style { get; set; }

        [Required(ErrorMessage = "Le prix unité est requis")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Prix_Unité { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
