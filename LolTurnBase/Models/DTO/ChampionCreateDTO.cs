namespace LolTurnBase.Models.DTO
{
    public class ChampionCreateDTO
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Armor { get; set; }
        public int MagicResist { get; set; }
        public int AttackDamage { get; set; }
        public int AbillityPower { get; set; }
        public int Level { get; set; } = 1;
    }
}
