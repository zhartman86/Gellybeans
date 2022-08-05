namespace Gellybeans.RPG
{
    public class Stat
    {
        public int Value    { get { return Base + Bonuses.Total; } }
        public int Base     { get; set; } = 0;
        Bonuses Bonuses     { get; set; } = new Bonuses();
    
        public static implicit operator int(Stat stat)  => stat.Value + stat.Bonuses.Total;
        public static implicit operator Stat(int value) => new Stat { Base = value };
    }
}