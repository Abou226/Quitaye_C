using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Models
{
    public class GammeList
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public int Heigth { get; set; }
        public int Width { get; set; } = 130;
        public ObservableCollection<Offre> Offres { get; set; }
        public GammeList()
        {
            Offres = new ObservableCollection<Offre>();
        }
    }
}
