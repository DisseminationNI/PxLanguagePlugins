using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Pluralize.NET;
using System.Globalization;
using System.Net;
using Pluralize.NET.Core;

namespace PxLanguagePlugin
{
    public class Language : ILanguagePlugin
    {
        public string LngIsoCode { get; set; }
        public bool IsLive { get; set; } = true;
        string ILanguagePlugin.LngIsoCode { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        readonly Pluralizer pluralService;// pluralService;
        internal dynamic labelValues;
        readonly KeywordMetadata keywordMeta;
        readonly string synonymsResource;
        readonly List<Synonym> synonymsList;
        public string lngFile = "";

        public Language(string translationData = null)
        {


            LngIsoCode = "en";
            pluralService = new Pluralizer();// PluralizationService.CreateService(new CultureInfo(LngIsoCode));

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

        }



        public dynamic GetLabelValues()
        {
            return labelValues;
        }

        public string Sanitize(string words)
        {
            return Regex.Replace(words, keywordMeta.regex, " ");
        }

        public string Singularize(string word)
        {

            return pluralService.Singularize(word);

        }

        public IEnumerable<string> GetSynonyms(string word)
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
