
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using GaeilgePlugin;

namespace TestGaPlugin
{
    [TestClass]
    public class TestGa
    {
        private TestContext testContextInstance;


        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public TestGa() { }

        [TestMethod]
        public void TestGetLabelValuesBasic()
        {
            GaPlugin glp = new GaPlugin();
            dynamic result = glp.GetLabelValues();
            Assert.IsFalse(result.Equals(null));
        }


        [TestMethod]
        public void SanitizeBasicNoSanitize()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "Is teist é seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals(testWordInput));
        }

        [TestMethod]
        public void SanitizeBasicRemoveCurlyBraces()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "Is teist {é} seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("Is teist é seo"));
        }

        [TestMethod]
        public void SanitizeBasicRemoveHtmlStuff()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "Is teist <é> seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("Is teist é seo"));
        }

        [TestMethod]
        public void SingularizeBasic()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "tithe";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeLenition()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "dteach";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeAspiration()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "theach";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeIrregular()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "mná";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("bean"));
        }

        [TestMethod]
        public void SingularizeNotFound()
        {
            GaPlugin glp = new GaPlugin();
            string testWordInput = "xxxx";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("xxxx"));
        }

        [TestMethod]
        public void GetLabelsBasic()
        {
            GaPlugin glp = new GaPlugin();
            var result = glp.GetLabelValues();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void SynonymBasic()
        {
            GaPlugin glp = new GaPlugin();
            var result = glp.GetSynonyms("sochaí");
            Assert.IsTrue(result.Contains("slua"));
        }

        [TestMethod]
        public void SynonymNotFound()
        {
            GaPlugin glp = new GaPlugin();
            var result = glp.GetSynonyms("xxxxx");
            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public void ExcludedTerms()
        {
            GaPlugin glp = new GaPlugin();
            var result = glp.GetExcludedTerms();
            Assert.IsTrue(result.Contains("agus"));
            Assert.IsTrue(result.Contains("do"));
        }
    }
}
