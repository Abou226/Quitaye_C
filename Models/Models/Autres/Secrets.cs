using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Secrets
    {
        public string Token { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
        public string Username { get; set; }
        public string BucketName { get; set; }
        public bool Success { get; set; }
    }
}
