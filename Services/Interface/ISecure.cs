using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ISecure
    {
        string Encryption(string text);
        string StringSha256Hash(string text);
    }
}
