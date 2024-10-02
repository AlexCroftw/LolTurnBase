using AutoMapper;
using LolTurnBase.Data;
using LolTurnBase.Models;
using LolTurnBase.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LolTurnBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemController> _logger;
        protected APIResponse _response;
        public ItemController(ApplicationDbContext db, IMapper mapper, ILogger<ItemController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _response = new();
        }

        [HttpGet(Name = "Get all Items")]
        public async Task<IActionResult> GetAllItems()
        {
            List<Item> items = await _db.Items.ToListAsync();
            List<ItemDTO> itemDTOs = _mapper.Map<List<ItemDTO>>(items);

            _logger.LogInformation("This response has :" + itemDTOs.Count + "items");

            return Ok(itemDTOs);
        }


        [HttpGet("{id:int}", Name = "Get Item by Id")]
        public async Task<IActionResult> GetItem(int? id)
        {
            if (id == null || id == 0)
            {
                ModelState.AddModelError("ErrorMessages", "This Id is Invalid");
                return BadRequest(ModelState);
            }
            var item = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                ModelState.AddModelError("ErrorMessages", "This Item does not exist");
                return NotFound(ModelState);
            }
            ItemDTO itemDTO = _mapper.Map<ItemDTO>(item);
            return Ok(itemDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreateDTO itemCreateDTO)
        {
            if (ModelState.IsValid)
            {
                var item = _mapper.Map<Item>(itemCreateDTO);
                if (item == null)
                {
                    return BadRequest(ModelState);
                }
                if (await _db.Items.AnyAsync(x => x.Name == item.Name))
                {
                    ModelState.AddModelError("ErrorMessages", "This name is already taken");
                    return BadRequest(ModelState);
                }
                await _db.Items.AddAsync(item);
                _db.SaveChanges();
                return Ok(ModelState);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
        [HttpPut("{id:int}", Name = "Update Item")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDTO itemDTO, int? id)
        {
            if (id == 0 || id == null) 
            {
                ModelState.AddModelError("ErrorMessages", "The Id is invalid");
                return BadRequest(ModelState);
            }
            if (itemDTO == null || itemDTO.Id != id) 
            {
                ModelState.AddModelError("ErrorMessages", "This item does not exist");
                return NotFound(ModelState);
            }

            var item = _mapper.Map<Item>(itemDTO);
            _db.Items.Update(item);
            _db.SaveChanges();
            return Ok(ModelState);
            

        }
        [HttpDelete("{id:int}", Name = "Delete Item")]

        public async Task<IActionResult> DeleteItem(int? id) 
        {
            if (id == 0 || id == null) 
            {
                ModelState.AddModelError("ErrorMessages", "This Id is invalid");
                return BadRequest(ModelState);
            }
            var item =  await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) 
            {
                ModelState.AddModelError("ErrorMessages", "This item cannot be found");
                return NotFound(ModelState);
            }
            _db.Items.Remove(item);
            _db.SaveChanges();
            return Ok(ModelState);
        }

    }
}
