using System.Text;

namespace Gellybeans.Expressions
{
    public class KeyValuePairValue
    {
        public string Key { get; set; }
        public dynamic Value { get; set; }

        public KeyValuePairValue(StringValue key, dynamic value)
        {
            Console.WriteLine("creating");
            Key = key.String;
            Value = value;
        }

        public override string ToString() =>
            $"{Key}: {Value}";
        
    }
}
