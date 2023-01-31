using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;


namespace EnglishLanguagePlugin
{
    public class Language : ILanguagePlugin
    {
        public string LngIsoCode { get; set; }
        public bool IsLive { get; set; } = true;
        string ILanguagePlugin.LngIsoCode { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        readonly PluralizationService pluralService;
        readonly dynamic labelValues;
        readonly KeywordMetadata keywordMeta;
        readonly string synonymsResource;
        readonly List<Synonym> synonymsList;

        public Language()
        {
            LngIsoCode = "en";

            pluralService = PluralizationService.CreateService(new CultureInfo(LngIsoCode));

            var lngFile = Properties.Resources.ResourceManager.GetString("language");
            labelValues= JsonConvert.DeserializeObject<dynamic>(lngFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

            var keywordMetaFile = Properties.Resources.ResourceManager.GetString("keywordMetadata");
            keywordMeta = JsonConvert.DeserializeObject<KeywordMetadata>(keywordMetaFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
            
            synonymsResource = Properties.Resources.ResourceManager.GetString("synonym");
            synonymsList = JsonConvert.DeserializeObject<List<Synonym>>(synonymsResource, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

        }
        public dynamic  GetLabelValues()
        {
            return labelValues;
        }

        public string Sanitize(string words)
        {
            return Regex.Replace(words, keywordMeta.regex, " ");
        }

        public string Singularize(string word)
        {
           if (pluralService.IsPlural(word))
            {
                return pluralService.Singularize(word);
            }
            else return word;
        }

        public IEnumerable<string> GetSynonyms(string word)
        {
            var synonym = synonymsList.Where(x=>x.lemma.Equals(word));
            return synonym.Select(x=>x.match).ToList<string>(); 
            
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
