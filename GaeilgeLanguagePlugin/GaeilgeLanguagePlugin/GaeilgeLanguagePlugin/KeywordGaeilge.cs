using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaeilgePlugin
{
    internal static class KeywordGaeilge
    {
        static Grammar grammar;
        internal static void initiateGrammarFile()
        {
            if (grammar == null)
            {
                var grammarFile = Properties.Resources.ResourceManager.GetString("grammar");
                grammar = JsonConvert.DeserializeObject<Grammar>(grammarFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
            }
        }
        internal static string RemoveEclipsis(string word)
        {
            initiateGrammarFile();
            string outWord = word;
            List<string> eclipsisList = grammar.eclipsis.ToList();
            foreach (string s in eclipsisList)
            {
                if (word.StartsWith(s) && s.Length == 2)
                {
                    outWord = word.Substring(s.Length - (s.Length - 1), word.Length - (s.Length - (s.Length - 1)));
                }
                else if (word.StartsWith(s) && s.Length == 3)
                    outWord = word.Substring(s.Length - (s.Length - 2), word.Length - (s.Length - (s.Length - 2)));
            }

            return outWord;
        }

        internal static string RemoveAspiration(string word)
        {
            initiateGrammarFile();
            string outWord = word;
            List<string> aspirationList = grammar.aspiration.ToList();
            foreach (string s in aspirationList)
            {
                if (word.StartsWith(s) && s.Length >= 2 && word.Length >= 3)
                {
                    outWord = word.Substring(0, 1) + word.Substring(2, word.Length - 2);
                }
            }

            return outWord;
        }
    }
}
