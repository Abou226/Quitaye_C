using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Payement")]
    public class Payement
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Le montant est requis")]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }
        public DateTime Date_Payement { get; set; }

        [StringLength(60, ErrorMessage = "La taille de la réference ne peut dépasser 60 characters")]
        public string Reference { get; set; }
        public string Num_Operation { get; set; }
        public Guid? Num_PayementId { get; set; }
        public Num_Payement Num_Payement { get; set; }
        [Required(ErrorMessage = "Le type est requis")]
        [StringLength(60, ErrorMessage = "La taille du type ne peut dépasser 60 characters")]
        public string Type { get; set; }

        [Required(ErrorMessage = "La nature est requis")]
        [StringLength(60, ErrorMessage = "La taille de la nature ne peut dépasser 60 characters")]
        public string Nature { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid? ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(ModePayement))]
        public Guid? ModePayementId { get; set; }
        public ModePayement ModePayement { get; set; }
        [NotMapped]
        public bool Selected { get; set; }
    }
}
