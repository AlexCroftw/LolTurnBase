

using Microsoft.AspNetCore.Mvc.Rendering;

namespace LolTurnBase.Models.DTO
{
    public class ItemDetailsDTO
    {
        public ItemDTO Item { get; set; }
        public IEnumerable<SelectListItem> ChampionList { get; set; }
    }
}
