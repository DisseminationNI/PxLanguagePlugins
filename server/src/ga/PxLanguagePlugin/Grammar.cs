using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PxLanguagePlugin
{
    internal class Grammar
    {
        public string[] plural { get; set; }
        public string[] eclipsis { get; set; }
        public string[] aspiration { get; set; }
        public Dictionary<string, string> irregularPlural { get; set; } 
    }
}
