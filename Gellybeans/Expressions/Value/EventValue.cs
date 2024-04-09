namespace Gellybeans.Expressions
{
    public class EventValue
    {
        public ArrayValue Data { get; set; }

        public EventValue(ArrayValue data) =>
            Data = data;       
    
        public override string ToString() => 
            "EVENT";
    }
}
