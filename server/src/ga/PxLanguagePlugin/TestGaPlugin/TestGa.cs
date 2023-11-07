
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using PxLanguagePlugin;

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
            Language glp = new Language();
            dynamic result = glp.GetLabelValues();
            Assert.IsFalse(result.Equals(null));
        }


        [TestMethod]
        public void SanitizeBasicNoSanitize()
        {
            Language glp = new Language();
            string testWordInput = "Is teist é seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals(testWordInput));
        }

        [TestMethod]
        public void SanitizeWithDiacritics()
        {
            Language elp = new Language();
            string testWordInput = "Bhí mé anseo oiche aréir";
            string testWordsResult = elp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals(testWordInput));
            testWordInput = "Škoda Enyaq iV";
            testWordsResult = elp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals(testWordInput));
        }

        [TestMethod]
        public void SanitizeBasicRemoveCurlyBraces()
        {
            Language glp = new Language();
            string testWordInput = "Is teist {é} seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("Is teist é seo"));
        }

        [TestMethod]
        public void SanitizeBasicRemoveHtmlStuff()
        {
            Language glp = new Language();
            string testWordInput = "Is teist <é> seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("Is teist é seo"));
        }

        [TestMethod]
        public void SingularizeBasic()
        {
            Language glp = new Language();
            string testWordInput = "tithe";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeLenition()
        {
            Language glp = new Language();
            string testWordInput = "dteach";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeAspiration()
        {
            Language glp = new Language();
            string testWordInput = "theach";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeIrregular()
        {
            Language glp = new Language();
            string testWordInput = "mná";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("bean"));
        }

        [TestMethod]
        public void SingularizeNotFound()
        {
            Language glp = new Language();
            string testWordInput = "xxxx";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("xxxx"));
        }

        [TestMethod]
        public void GetLabelsBasic()
        {
            Language glp = new Language();
            var result = glp.GetLabelValues();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void SynonymBasic()
        {
            Language glp = new Language();
            var result = glp.GetSynonyms("sochaí");
            Assert.IsTrue(result.Contains("slua"));
        }

        [TestMethod]
        public void SynonymNotFound()
        {
            Language glp = new Language();
            var result = glp.GetSynonyms("xxxxx");
            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public void ExcludedTerms()
        {
            Language glp = new Language();
            var result = glp.GetExcludedTerms();
            Assert.IsTrue(result.Contains("agus"));
            Assert.IsTrue(result.Contains("do"));
        }
        [TestMethod]
        public void DoNotAmend()
        {
            Language glp = new Language();
            var result = glp.GetDoNotAmend();
            Assert.IsTrue(result.Contains("méid"));
            Assert.IsTrue(result.Contains("mhéid"));
        }
    }
}
