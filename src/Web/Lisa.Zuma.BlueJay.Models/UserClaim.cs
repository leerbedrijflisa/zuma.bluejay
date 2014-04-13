using System;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.Models
{
    public class UserClaim
    {
        public Dictionary<string, string> Properties { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }
}
