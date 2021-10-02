using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class Secure : ISecure
    {
        public string Encryption(string text)
        {
            return StringSha256Hash(text);
        }

        public string StringSha256Hash(string text) =>
        string.IsNullOrEmpty(text) ? string.Empty :
            BitConverter.ToString(
                new System.Security.Cryptography.SHA256Managed().
                ComputeHash(System.Text.Encoding.UTF8.
                    GetBytes(text))).Replace("-", string.Empty);
    }
}
