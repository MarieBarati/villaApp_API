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
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        }
        [HttpGet("id")]
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
    }
}
