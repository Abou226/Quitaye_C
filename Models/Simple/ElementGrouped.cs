using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Text;

namespace Models
{
    public class ElementGrouped<TType, TName> : ObservableCollection<TType> where TType : class
    {
        public TName Name { get; set; }
        public decimal Montant { get; set; }
        public ElementGrouped(TName name, decimal montant, ObservableCollection<TType> animals) : base(animals)
        {
            Name = name;
            Montant = montant;
        }
    }
}
