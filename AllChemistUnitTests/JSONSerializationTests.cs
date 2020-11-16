using AllChemist.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
namespace AllChemistUnitTests
{
    [TestClass]
    public class JSONSerializationTests
    {
        struct TestStruct
        {
            public int a;
            public double b;
            public string c;
            public List<TestStruct> d;
            public Dictionary<string, int> e;
        }
        [TestMethod]
        public void SaveAndLoad()
        {
            TestStruct testObject1 = new TestStruct { a = 5, b = 9.9, c = "oh juliet!" };
            TestStruct testObject2 = new TestStruct { a = 1, b = 2.1, c = "oh romeo!", d = new List<TestStruct> { testObject1 }, e = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } } };

            string saved = JSONHandler.Save<TestStruct>(testObject2);

            TestStruct loaded = JSONHandler.Load<TestStruct>(saved);

            string saved2 = JSONHandler.Save<TestStruct>(loaded);
            Assert.AreEqual(saved2, saved);

            Assert.AreEqual(testObject2.a, loaded.a);
            Assert.AreEqual(testObject2.b, loaded.b);
            Assert.AreEqual(testObject2.c, loaded.c);
            Assert.IsTrue(testObject2.d.SequenceEqual(loaded.d));
            Assert.IsTrue(testObject2.e.SequenceEqual(loaded.e));
        }
    }
}
