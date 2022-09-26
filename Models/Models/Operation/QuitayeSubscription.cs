using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class QuitayeSubscription
    {
        public Guid Id { get; set; }

        public SubscriptionType Type { get; set; }

        [ForeignKey(nameof(Type))]
        public Guid? TypeId { get; set; }

    }
}
