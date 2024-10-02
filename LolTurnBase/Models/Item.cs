using System.ComponentModel.DataAnnotations.Schema;

namespace LolTurnBase.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HealthGained { get; set; }
        public int ManaGained { get; set; }
        public int ArmorGained { get; set; }
        public int MagicResitGained { get; set; }
        public int AttackDamageGained { get; set; }
        public int AbillityPowerGained { get; set; }
        public int Cost { get; set; }
        public int? ChampionID { get; set; }
        [ForeignKey("ChampionID")]
        public Champion Champion { get; set; }

    }
}
