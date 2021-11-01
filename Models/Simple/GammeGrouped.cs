using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Models
{
    public class GammeGrouped : ObservableCollection<Gamme>
    {
        public string Name { get; set; }
        public Guid StyleId { get; set; }
        public ObservableCollection<Gamme> Gammes { get; set; }
        public GammeGrouped(string name, Guid styleId, ObservableCollection<Gamme> gammes) : base(gammes)
        {
            Name = name;
            StyleId = styleId;
            Gammes = gammes;
        }
    }
}
