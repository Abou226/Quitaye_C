using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Models
{
    public class ReservationGroupedDays : ObservableCollection<Reservation>
    {
        public DateTime Name { get; set; }
        public decimal Montant { get; set; }
        public ReservationGroupedDays(DateTime name, decimal montant, ObservableCollection<Reservation> animals) : base(animals)
        {
            Name = name;
            Montant = montant;
        }
    }
}
