using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Heure
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? EntrepriseId { get; set; }
    }
}
