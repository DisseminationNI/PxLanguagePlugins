using EnglishLanguagePlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTestEnPlugin
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestEnglish
    {
        public TestEnglish()
        {
            
        }

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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SanitizeBasicNoSanitize()
        {
            EnPlugin elp = new EnPlugin ();
            string testWordInput = "this is a test";
            string testWordsResult = elp.Sanitize (testWordInput);
            Assert.IsTrue(testWordsResult.Equals(testWordInput));
        }

        [TestMethod]
        public void SanitizeBasicRemoveCurlyBraces()
        {
            EnPlugin elp = new EnPlugin();
            string testWordInput = "this is{a}test";
            string testWordsResult = elp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("this is a test"));
        }

        [TestMethod]
        public void SanitizeBasicRemoveHtmlStuff()
        {
            EnPlugin elp = new EnPlugin();
            string testWordInput = "this is<a>test";
            string testWordsResult = elp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("this is a test"));
        }

        [TestMethod]
        public void SingularizeBasic()
        {
            EnPlugin elp = new EnPlugin();
            string testWordInput = "dogs";
            string testWordsResult = elp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("dog"));
        }

        [TestMethod]
        public void SingularizeLessBasic()
        {
            EnPlugin elp = new EnPlugin();
            string testWordInput = "children";
            string testWordsResult = elp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("child"));
        }

        [TestMethod]
        public void SingularizeNotFound()
        {
            EnPlugin elp = new EnPlugin();
            string testWordInput = "xxxx";
            string testWordsResult = elp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("xxxx"));
        }

        [TestMethod]
        public void GetLabelsBasic()
        {
            EnPlugin elp = new EnPlugin();
            var result = elp.GetLabelValues();
            Assert.IsTrue(result!=null);
        }

        [TestMethod]
        public void SynonymBasic()
        {
            EnPlugin elp = new EnPlugin();
            var result = elp.GetSynonyms ("car");
            Assert.IsTrue(result.Contains("auto"));
        }

        [TestMethod]
        public void SynonymNotFound()
        {
            EnPlugin elp = new EnPlugin();
            var result = elp.GetSynonyms("carxxx");
            Assert.IsTrue(result.Count()==0);
        }

        [TestMethod]
        public void ExcludedTerms()
        {
            EnPlugin elp = new EnPlugin();
            var result = elp.GetExcludedTerms();
            Assert.IsTrue(result.Contains("which"));
            Assert.IsTrue(result.Contains("and"));
        }

        
    }
}
