using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLanguagePlugin
{
    public interface ILanguagePlugin
    {
        string LngIsoCode { get; }
        dynamic GetLabelValues();
        string Sanitize(string words);
        string Singularize(string word);
        IEnumerable< string> GetSynonyms(string word);
        IEnumerable<string> GetExcludedTerms();
    }
}
