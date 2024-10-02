using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LolTurnBase.Models.DTO
{
    public class ItemDTO
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
        public int Cost {  get; set; }
        [ValidateNever]
        public ChampionDTO Champion { get; set; }
    }
}
