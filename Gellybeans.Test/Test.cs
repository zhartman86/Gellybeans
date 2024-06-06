using Gellybeans.Expressions;
using System.Text;

namespace Gellybeans.Test
{
    [TestClass]
    public class Test
    {

        [TestMethod]
        public void Ops()
        {
            var sb = new StringBuilder();

            var result = Parser.Parse("1+1", this).Eval(0, this, sb);
            Assert.AreEqual(result, 2);

            result = Parser.Parse("2 + 3 * 4", this).Eval(0, this, sb);
            Assert.AreEqual(result, 14);

            result = Parser.Parse("(2 + 3) * 4", this).Eval(0, this, sb);
            Assert.AreEqual(result, 20);
        }
    }

}
