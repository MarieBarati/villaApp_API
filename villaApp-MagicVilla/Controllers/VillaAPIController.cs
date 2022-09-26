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
    }
}
