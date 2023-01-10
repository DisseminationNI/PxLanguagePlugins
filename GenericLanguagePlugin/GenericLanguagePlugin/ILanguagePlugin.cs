using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLanguagePlugin
{
    public interface ILanguagePlugin
    {
        string LngIsoCode { get; }
        dynamic GetLabelValues();
        string Sanitize(string words);
        string Singularize(string word);
        List<string> GetSynonyms(string word);
    }
}
