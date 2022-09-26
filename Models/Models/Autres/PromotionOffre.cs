using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class PromotionOffre
    {
        public Guid Id { get; set; }
        public Guid? OffreId { get; set; }
    }
}
