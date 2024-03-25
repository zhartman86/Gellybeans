namespace Gellybeans.Expressions
{
    public class EventValue
    {
        public dynamic[] Data { get; set; }

        public EventValue(dynamic data) =>
            Data = data;       
    
        public override string ToString() => 
            "EVENT";
    }
}
