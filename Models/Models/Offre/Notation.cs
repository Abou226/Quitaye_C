using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Notation
    {
        public Guid Id { get; set; }
        public Guid? ElementId { get; set; }

        [Column(TypeName = "decimal (18, 2)")]
        public decimal Note { get; set; }
        public Guid? UserId { get; set; }
    }
}
