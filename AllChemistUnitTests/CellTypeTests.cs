using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllChemist.Cells;

namespace AllChemistUnitTests
{
    [TestClass]
    public class CellTypeTests
    {
        [TestMethod]
        public void InitID()
        {
            CellType.ResetCounter();

            CellType ct1 = new CellType("test", 0, 0, 0);

            Assert.AreEqual(ct1.Id, 0);

            CellType ct2 = new CellType("test2", 0, 0, 0);

            Assert.AreEqual(ct2.Id, 1);

            CellType.ResetCounter();

            CellType ct3 = new CellType("test3", 0, 0, 0);

            Assert.AreEqual(ct3.Id, 0);

        }

        [TestMethod]
        public void Equality()
        {
            CellType.ResetCounter();

            CellType ct1 = new CellType("test", 128, 0, 128);
            CellType ct2 = new CellType("test", 128, 0, 128);

            Assert.AreNotEqual(ct1, ct2);

            CellType.ResetCounter();

            CellType ct3 = new CellType("test", 128, 0, 128);

            Assert.AreEqual(ct1, ct3);

            CellType.ResetCounter();

            CellType ct4 = new CellType("test", 99, 99, 99);

            Assert.AreNotEqual(ct1, ct4);
        }

    }
}
