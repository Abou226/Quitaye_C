using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class EditObject
    {
        public string Op { get; set; } = "Replace";
        public string Value { get; set; }
        public string Path { get; set; }
    }
}