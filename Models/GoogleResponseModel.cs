using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GoogleResponseModel
    {
        public string azp { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string scope { get; set; }
        public string exp { get; set; }
        public string expires_in { get; set; }
        public string email { get; set; }
        public string email_verified { get; set; }
        public string access_type { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
