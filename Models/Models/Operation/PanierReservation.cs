using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("PanierReservation")]
    public class PanierReservation
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Gamme))]
        public Guid? GammeId { get; set; }
        public Gamme Gamme { get; set; }

        [ForeignKey(nameof(Taille))]
        public Guid TailleId { get; set; }
        public Taille Taille { get; set; }

        [ForeignKey(nameof(Model))]
        public Guid ModelId { get; set; }
        public Model Model { get; set; }
        public DateTime DateOfCreation { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "La quantité est requise")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantité { get; set; }

        [StringLength(120, ErrorMessage = "La taille du designation ne peut dépasser 120 characters")]
        public string Designation { get; set; }
        [StringLength(120, ErrorMessage = "La taille de Autres_Info ne peut dépasser 120 characters")]
        public string Autres_Info { get; set; }

        [Required(ErrorMessage = "Le prix de vente unitaire est requis")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Prix_Vente_Unité { get; set; }

        [Required(ErrorMessage = "L'adresse de livraison est requise")]
        [StringLength(120, ErrorMessage = "La taille de l'adresse de livraison ne peut dépasser 120 characters")]
        public string Adresse_Livraison { get; set; }

        [Required(ErrorMessage = "L'heure de livraison est requise")]
        [StringLength(20, ErrorMessage = "La taille de l'heure de livraison ne peut dépasser 20 characters")]
        public string Heure_Livraison { get; set; }

        [Required(ErrorMessage = "La date de livraison est requise")]
        public DateTime Date_Livraison { get; set; }

        [Required(ErrorMessage = "Le temps du serveur est requis")]
        public DateTime ServerTime { get; set; }

        [Required(ErrorMessage = "La mention est requise")]
        [StringLength(100, ErrorMessage = "La taille de la mention ne peut dépasser 100 characters")]
        public string Mention { get; set; }

        public Client Client { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid? ClientId { get; set; }

        [StringLength(60, ErrorMessage = "La taille du details de livraison ne peut dépasser 60 characters")]
        public string Details_Adresse { get; set; }
        [ForeignKey(nameof(QuartierId))]
        public Guid? QuartierId { get; set; }
        public Quartier Quartier { get; set; }

        [StringLength(15, ErrorMessage = "La taille du contact client ne peut dépasser 15 charaters")]
        public string Contact_Client { get; set; }

        [Required(ErrorMessage = "Le contact de livraison est requis")]
        [StringLength(15, ErrorMessage = "La taille du contact livraison ne peut dépasser 15 characters")]
        public string Contact_Livraison { get; set; }

        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        public bool Annulée { get; set; }

        [ForeignKey(nameof(Marque))]
        public Guid? MarqueId { get; set; }
        public Marque Marque { get; set; }
    }
}
