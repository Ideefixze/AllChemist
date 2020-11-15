using AllChemist.Cells.Ruleset;
using AllChemist.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllChemistUnitTests
{
    [TestClass]
    public class ConwayRulesetCreatorTests
    {
        private Ruleset defaultRuleset;

        public ConwayRulesetCreatorTests()
        {
            defaultRuleset = new ConwayRulesetCreator("23/3").CreateRuleset();
        }
        [TestMethod]
        public void WrongInput1()
        {
            Assert.AreEqual(JSONHandler.Save(defaultRuleset), JSONHandler.Save(new ConwayRulesetCreator("abc/de").CreateRuleset()));

        }
        [TestMethod]
        public void WrongInput2()
        {
            Assert.AreEqual(JSONHandler.Save(defaultRuleset), JSONHandler.Save(new ConwayRulesetCreator("123/45/67").CreateRuleset()));
        }
        [TestMethod]
        public void WrongInput3()
        {
            Assert.AreEqual(JSONHandler.Save(defaultRuleset), JSONHandler.Save(new ConwayRulesetCreator("HEHEHEHE :))").CreateRuleset()));
        }
        [TestMethod]
        public void WrongInput4()
        {
            Assert.AreEqual(JSONHandler.Save(defaultRuleset), JSONHandler.Save(new ConwayRulesetCreator("").CreateRuleset()));
        }

        [TestMethod]
        public void WrongInput5()
        {
            Assert.AreEqual(JSONHandler.Save(defaultRuleset), JSONHandler.Save(new ConwayRulesetCreator("91/63").CreateRuleset()));
        }
    }
}
