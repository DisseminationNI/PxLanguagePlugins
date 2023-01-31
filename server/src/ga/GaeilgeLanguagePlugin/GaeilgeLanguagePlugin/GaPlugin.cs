using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GaeilgePlugin 
{
    public class GaPlugin: ILanguagePlugin
    {
        public string LngIsoCode { get; } = "ga";
        readonly dynamic labelValues;
        readonly KeywordMetadata keywordMeta;
        readonly string synonymsResource;
        readonly List<Synonym> synonymsList;
        readonly Dictionary<string, string> morphology;

        public GaPlugin()
        {
            var lngFile = Properties.Resources.ResourceManager.GetString("language");
            labelValues = JsonConvert.DeserializeObject<dynamic>(lngFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

            var keywordMetaFile = Properties.Resources.ResourceManager.GetString("keywordMetadata");
            keywordMeta = JsonConvert.DeserializeObject<KeywordMetadata>(keywordMetaFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

            synonymsResource = Properties.Resources.ResourceManager.GetString("synonym");
            synonymsList = JsonConvert.DeserializeObject<List<Synonym>>(synonymsResource, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

            string dictionaryString = Properties.Resources.ResourceManager.GetString("dictionary");
            morphology = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionaryString);
        }
        public dynamic GetLabelValues()
        {
            return labelValues;
        }

        public string Sanitize(string words)
        {
            return Regex.Replace(words, keywordMeta.regex, "");
        }

        public string Singularize(string word)
        {
            word = KeywordGaeilge.RemoveEclipsis(word.ToLower());
            word = KeywordGaeilge.RemoveAspiration(word);

            return morphology.ContainsKey(word) ? morphology[word] : word;
        }

        public List<string> GetSynonyms(string word)
        {
            var synonym = synonymsList.Where(x => x.lemma.Equals(word));
            return synonym.Select(x => x.match).ToList<string>();
        }

        public IEnumerable<string> GetExcludedTerms()
        {
            return keywordMeta.excludedWords;
        }
    }
}
