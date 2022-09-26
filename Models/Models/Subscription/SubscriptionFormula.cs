using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class SubscriptionFormula
    {
        public Guid? Id { get; set; }
        public SubscriptionType Type { get; set; }

        [ForeignKey(nameof(Type))]
        public Guid? TypeId { get; set; }

        public SubscriptionDuration Duration { get; set; }
        [ForeignKey(nameof(Duration))]
        public Guid? DurantionId { get; set; }

        [Column(TypeName = "decimal (18, 2)")]
        public decimal Montant { get; set; }

        public Guid? EntrepriseId { get; set; }
    }
}
