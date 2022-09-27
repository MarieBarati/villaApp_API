using Microsoft.AspNetCore.Mvc;
using villaApp_MagicVilla.Data;
using villaApp_MagicVilla.Models;
using villaApp_MagicVilla.Models.Dto;

namespace villaApp_MagicVilla.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController>  _logger;
        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        }
        [HttpGet("id",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id == 0)
            {
                _logger.LogError("this id is invalid, id =", id);
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(villa => villa.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (VillaStore.VillaList.FirstOrDefault(villa => villa.Name.ToLower() == villaDto.Name.ToLower())!= null)
            {
                ModelState.AddModelError("customError", "villa is already Exists!");
                return BadRequest(ModelState);
            }
            if(villaDto == null)
            {
                return base.BadRequest();
            }
            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError); 
            }
            villaDto.Id = VillaStore.VillaList.OrderByDescending(villa => villa.Id).FirstOrDefault().Id+1;
            VillaStore.VillaList.Add(villaDto);
            return CreatedAtRoute("GetVilla",new { id = villaDto.Id },villaDto);
        }
        
        [HttpDelete("id",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.Find(villa => villa.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
                VillaStore.VillaList.Remove(villa);
            return Ok();
        }
        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id,[FromBody]VillaDto villaDto)
        {
            if (villaDto == null || villaDto.Id != id)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var villa = VillaStore.VillaList.FirstOrDefault(villa => villa.Id == id);
            if (villa == null)
            { 
                return NotFound();
            }
            villa.Name = villaDto.Name;

            return NoContent();
        }
    }
}
