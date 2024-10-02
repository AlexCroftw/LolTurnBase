using AutoMapper;
using LolTurnBase.Data;
using LolTurnBase.Models;
using LolTurnBase.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace LolTurnBase.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ChampionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ChampionController> _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        public ChampionController(ApplicationDbContext db, ILogger<ChampionController> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllChampions()
        {

            try
            {
                List<Champion> champList = await _db.Champion.ToListAsync();

                _response.Result = _mapper.Map<List<Champion>>(champList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _logger.LogInformation("The number of champions that are listed is: " + champList.Count);
               
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpGet("{id:int}", Name = "Get Champion")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetOneChampion(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    ModelState.AddModelError("ErrorMessages", "The Id of the Champion is invalid");

                    return NotFound(ModelState);
                }
                var champ = await _db.Champion.FirstOrDefaultAsync(x => x.Id == id);
                _response.Result = _mapper.Map<Champion>(champ);

                if (_response.Result == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Champion does not exist");
                    return NotFound(ModelState);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                _response.IsSuccess = false;
            }

           
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateChampion([FromBody] ChampionCreateDTO championCreateDTO)
        {

            try
            {

                if (championCreateDTO == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Item is null and void");
                    _response.IsSuccess=false;
                    return BadRequest(ModelState);
                }

                var champion = _mapper.Map<Champion>(championCreateDTO);

                await _db.Champion.AddAsync(champion);
                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.OK;
               
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                _response.IsSuccess = false;
            }

            return _response;


        }

        [HttpPut("{id:int}", Name = "UpdateChampion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> UpdateChampion([FromBody] ChampionDTO championDTO, int? id)
        {
            if (championDTO == null || id != championDTO.Id)
            {
                return BadRequest();
            }

            var champ = _mapper.Map<Champion>(championDTO);
            _db.Champion.Update(champ);
            _db.SaveChanges();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialChampion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialChamp(int id, [FromBody] JsonPatchDocument<ChampionDTO> championPatch)
        {
            if (championPatch == null || id == 0)
            {
                return BadRequest();
            }
            var champ = await _db.Champion.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            ChampionDTO champDTO = _mapper.Map<ChampionDTO>(champ);

            if (champ == null)
            {
                return BadRequest();
            }

            championPatch.ApplyTo(champDTO);

            Champion model = _mapper.Map<Champion>(champDTO);

            _db.Champion.Update(model);
            _db.SaveChanges();


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
        [HttpDelete("{id:int}", Name = "DeleteChampion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteChampion(int? id) 
        {
            if(id == 0 || id == null) 
            {
                ModelState.AddModelError("ErrorMessages", "This Champion does not exist");
                return BadRequest(ModelState);
            }
            var model = await _db.Champion.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) 
            {
                ModelState.AddModelError("ErrorMessages", "This Champion does not exist");
                return BadRequest(ModelState);
            }
            _db.Champion.Remove(model);
            _db.SaveChanges();
            return NoContent();
            
        }
       
    }
}
