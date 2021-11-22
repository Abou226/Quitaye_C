using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    [Table("EntrepriseUser")]
    public class EntrepriseUser
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Entreprise))]
        public Guid? EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public DateTime DateOfAdd { get; set; }

        [ForeignKey(nameof(Departement))]
        public Guid? DepartementId { get; set; }
        public Departement Departement { get; set; }
        public UserRole Role { get; set; } = UserRole.Admin;
    }
}
