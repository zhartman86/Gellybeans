using Gellybeans.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


static int Main(string[] args)
{
    var test = new UnitTests();
    
    test.TokenizerTest();
    test.UnaryTest();
    test.AddSubtractTest();
    test.MultiplyDivideTest();
    test.OrderOfOperation();
    test.Variables();
    test.Functions();


    return -1;
}


[TestClass]
public class UnitTests
{
    
    public void TokenizerTest()
    {
        var testString = "10 + 20 - 30.123";
        var t = new Tokenizer(new StringReader(testString));

        // "10"
        Assert.AreEqual(t.Token, TokenType.Number,"YOO");
        Assert.AreEqual(t.Number, 10);
        t.NextToken();

        // "+"
        Assert.AreEqual(t.Token, TokenType.Add);
        t.NextToken();

        // "20"
        Assert.AreEqual(t.Token, TokenType.Number);
        Assert.AreEqual(t.Number, 20);
        t.NextToken();

        // "-"
        Assert.AreEqual(t.Token, TokenType.Sub);
        t.NextToken();

        // "30.123"
        Assert.AreEqual(t.Token, TokenType.Number);
        Assert.AreEqual(t.Number, 30.123);
        t.NextToken();
    }

    
    public void AddSubtractTest()
    {
        // Add 
        Assert.AreEqual(Parser.Parse("10 + 20").Eval(null), 30);

        // Subtract 
        Assert.AreEqual(Parser.Parse("10 - 20").Eval(null), -10);

        // Sequence
        Assert.AreEqual(Parser.Parse("10 + 20 - 40 + 100").Eval(null), 90);
    }

    
    public void UnaryTest()
    {
        // Negative
        Assert.AreEqual(Parser.Parse("-10").Eval(null), -10);

        // Positive
        Assert.AreEqual(Parser.Parse("+10").Eval(null), 10);

        // Negative of a negative
        Assert.AreEqual(Parser.Parse("--10").Eval(null), 10);

        // Woah!
        Assert.AreEqual(Parser.Parse("--++-+-10").Eval(null), 10);

        // All together now
        Assert.AreEqual(Parser.Parse("10 + -20 - +30").Eval(null), -40);
    }

    
    public void MultiplyDivideTest()
    {
        // Add 
        Assert.AreEqual(Parser.Parse("10 * 20").Eval(null), 200);

        // Subtract 
        Assert.AreEqual(Parser.Parse("10 / 20").Eval(null), 0.5);

        // Sequence
        Assert.AreEqual(Parser.Parse("10 * 20 / 50").Eval(null), 4);
    }

    
    public void OrderOfOperation()
    {
        // No parens
        Assert.AreEqual(Parser.Parse("10 + 20 * 30").Eval(null), 610);

        // Parens
        Assert.AreEqual(Parser.Parse("(10 + 20) * 30").Eval(null), 900);

        // Parens and negative
        Assert.AreEqual(Parser.Parse("-(10 + 20) * 30").Eval(null), -900);

        // Nested
        Assert.AreEqual(Parser.Parse("-((10 + 20) * 5) * 30").Eval(null), -4500);
    }

    class MyContext : IContext
    {
        public MyContext(int r)
        {
            _r = r;
        }

        int _r;

        public int Resolve(string name)
        {
            switch(name)
            {
                case "pi": return 3;
                case "r": return _r;
            }

            throw new InvalidDataException($"Unknown variable: '{name}'");
        }

        public int Call(string name, int[] arguments)
        {
            throw new NotImplementedException();
        }
    }

    
    public void Variables()
    {
        var ctx = new MyContext(10);

        Assert.AreEqual(Parser.Parse("2 * pi * r").Eval(ctx), 2 * Math.PI * 10);
    }

    class MyFunctionContext : IContext
    {
        public MyFunctionContext()
        {
        }

        public int Resolve(string name)
        {
            throw new InvalidDataException($"Unknown variable: '{name}'");
        }

        public int Call(string name, int[] arguments)
        {
            if(name == "rectArea")
            {
                return arguments[0] * arguments[1];
            }

            if(name == "rectPerimeter")
            {
                return (arguments[0] + arguments[1]) * 2;
            }

            throw new InvalidDataException($"Unknown function: '{name}'");
        }
    }

    
    public void Functions()
    {
        var ctx = new MyFunctionContext();
        Assert.AreEqual(Parser.Parse("rectArea(10,20)").Eval(ctx), 200);
        Assert.AreEqual(Parser.Parse("rectPerimeter(10,20)").Eval(ctx), 60);
    }

    class MyLibrary
    {
        public MyLibrary()
        {
            pi = 3;
        }

        public int pi { get; private set; }
        public int r { get; set; }

        public int rectArea(int width, int height)
        {
            return width * height;
        }

        public int rectPerimeter(int width, int height)
        {
            return (width + height) * 2;
        }
    }
}