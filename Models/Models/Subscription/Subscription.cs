using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public SubscriptionFormula Formula { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Guid? EntrepriseId { get; set; }
        public DateTime? DateOfIssue { get; set; }
    }
}
