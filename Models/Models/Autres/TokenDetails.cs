using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TokenDetails
    {
        public string Token { get; set; }
        public DateTime IssueAt { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
    }
}
