using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// *** TODO: Rename the "GenericLanguagePlugin" namespace for your language
namespace GenericLanguagePlugin
{
    // *** TODO Rename the "GenericPlugin" class for your language
    public class GenericPlugin : ILanguagePlugin
    {   
        public string LngIsoCode { get; } = "";
        readonly dynamic labelValues;
        readonly KeywordMetadata keywordMeta;
        readonly string synonymsResource;
        readonly List<Synonym> synonymsList;

        // *** TODO: Rename the "GenericPlugin" class constructor for your language
        public GenericPlugin()
        {
            var lngFile = Properties.Resources.ResourceManager.GetString("language");
            labelValues = JsonConvert.DeserializeObject<dynamic>(lngFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

            var keywordMetaFile = Properties.Resources.ResourceManager.GetString("keywordMetadata");
            keywordMeta = JsonConvert.DeserializeObject<KeywordMetadata>(keywordMetaFile, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

            synonymsResource = Properties.Resources.ResourceManager.GetString("synonym");
            synonymsList = JsonConvert.DeserializeObject<List<Synonym>>(synonymsResource, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

        }

        //*** TODO: Translate all of the terms in language.json to your language
        //*** These are the messages you need to return to users and insert in diagnostic logs
        public dynamic GetLabelValues()
        {
            return labelValues;
        }

        //*** TODO: Prepare a comprehensive list of synonyms for your language and insert them into the synonym.json file
        //*** Some examples from the english language are already in the synonym.json. The term "lemma" is the word you are trying to match
        //*** and "match" are the synonyms for this word. You should remove the examples when you're finished.
        public List<string> GetSynonyms(string word)
        {
            var synonym = synonymsList.Where(x => x.lemma.Equals(word));
            return synonym.Select(x => x.match).ToList<string>();
        }

        //*** This function simply removes any non-alpha-numeric characters from a string
        //*** The relevant regex is included in the keywordMetadata.json file. You may amend this entry as you see fit.
        public string Sanitize(string words)
        {
            return Regex.Replace(words, keywordMeta.regex, " ");
        }

        //*** TODO: Write the code necessary to produce a single word to represent all inflections of that word
        //*** For example, in the english language, we ensure that "children" returns "child" and "dogs" returns "dog"
        //*** The complexity of this task depends on (a) how highly inflected the language is and (b) the resources available to you,
        //*** i.e. .Net libraries, databases, public APIs etc.
        //*** At minimum, this task can be restricted to nouns, however you should apply your own judgement for this.
        public string Singularize(string word)
        {
            return word;
        }

        //*** TODO: Amend keywordMetadata.json to include a list of words that will be ignored in a search
        //*** Typically, these will include articles (e.g. "the", "a"), prepositions (e.g, "with", "of", "by"), interrogatives
        //*** (e.g. "which", "how many", "what") and any other words that might be relevant
        public IEnumerable<string> GetExcludedTerms()
        {
            return keywordMeta.excludedWords;
        }

    }
}
