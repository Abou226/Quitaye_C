using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LogInModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Nom { get; set; }
        public string PhotoUrl { get; set; }
    }
}
