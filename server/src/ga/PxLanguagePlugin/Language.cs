using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace PxLanguagePlugin 
{
    public class Language: ILanguagePlugin
    {
        public string LngIsoCode { get; set; } = "ga";
        public bool IsLive { get; set; } = true;
        public dynamic labelValues;
        readonly KeywordMetadata keywordMeta;
        readonly string synonymsResource;
        readonly List<Synonym> synonymsList;
        readonly Dictionary<string, string> morphology;
        public string lngFile = "";

        public Language(string translationData=null)
        {
            if (translationData != null)
                labelValues = JsonConvert.DeserializeObject<dynamic>(translationData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
            else
            {
                var lngFile = Properties.Resources.ResourceManager.GetString("language");
                labelValues = JsonConvert.DeserializeObject<dynamic>(lngFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

            }

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
        public IEnumerable<string> GetDoNotAmend()
        {
            return keywordMeta.doNotAmend;
        }
    }
}
