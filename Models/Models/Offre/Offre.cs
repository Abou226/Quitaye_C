using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Offre")]
    public class Offre
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Gamme))]
        public Guid? GammeId { get; set; }
        public Gamme Gamme { get; set; }
        [ForeignKey(nameof(Model))]
        public Guid? ModelId { get; set; }
        public Model Model { get; set; }
        public Taille Taille { get; set; }
        [ForeignKey(nameof(Taille))]
        public Guid? TailleId { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
