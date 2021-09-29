﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Panier")]
    public class Panier
    {
        public Guid Id { get; set; }
        public Guid? OffreId { get; set; }
        public Offre Offre { get; set; }
        [Required(ErrorMessage = "La quantité est requise")]
        [Column(TypeName = "decimal (18, 0)")]
        public decimal Quantité { get; set; }

        [Required(ErrorMessage = "Le prix unité est requise")]
        [Column(TypeName = "decimal (18, 0)")]
        public decimal Prix_Unité { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid? ClientId { get; set; }
        public Client Client { get; set; }
        public Guid? UserId { get; set; }

        [StringLength(60, ErrorMessage = "La taille du details de livraison ne peut dépasser 60 characters")]
        public string Details_Adresse { get; set; }
        [ForeignKey(nameof(QuartierId))]
        public Guid? QuartierId { get; set; }
        public Quartier Quartier { get; set; }
        public DateTime Date_Livraison { get; set; }
        [StringLength(5, ErrorMessage = "La taille de l'heure ne peut dépasser 5 characters")]
        public string Heure_Livraison { get; set; }

        [StringLength(15, ErrorMessage = "La taille du contact de livraison ne peut dépasser 15 characters")]
        public string Contact_Livraison { get; set; }
    }
}