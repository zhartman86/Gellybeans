using Gellybeans.Expressions;

namespace Gellybeans.Test
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Ops_Basic()
        {
            var result = Parser.Parse("1+1", this).Eval(0, this, null);
            Assert.AreEqual(result, 2);
        }    

        [TestMethod]
        public void Ops_Order1()
        {
            var result = Parser.Parse("2 + 3 * 4", this).Eval(0, this, null);
            Assert.AreEqual(result, 14);
        }

        [TestMethod]
        public void Ops_Order2()
        {
            var result = Parser.Parse("(2 + 3) * 4", this).Eval(0, this, null);
            Assert.AreEqual(result, 20);          
        }
    }

}
