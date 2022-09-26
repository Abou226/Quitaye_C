using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SubscriptionDuration
    {
        public Guid Id { get; set; }
        public int Months { get; set; } = 3;
    }
}
