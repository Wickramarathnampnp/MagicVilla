using MagicVilla.Data;
using MagicVilla.Logging;
using MagicVilla.Model;
using MagicVilla.Model.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MagicVilla.Controllers
{
	[Route("api/MagicVilla")]
	[ApiController]
	public class MagicVillaController : ControllerBase
	{
		private readonly	ILogging _Logger;
        public MagicVillaController(ILogging Logger)
        {
			_Logger = Logger;
        }
        [HttpGet]
		[ProducesResponseType(200)]
		//[ProducesResponseType(StatusCodes.Satus200OK)]
		public IEnumerable<VillaDto> GetVillas()
		{
			_Logger.Log("Getting all Villas","error");
			return DataBase.VillaList;
			
		}

		[HttpGet("{id:int}",Name= "GetVilla")]
		//[HttpGet("id")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public ActionResult<VillaDto> GetVilla(int id)
		{
			if(id == 0)
			{
				_Logger.Log("ID can not be zero","error");
				return BadRequest();
			}
			var villa = DataBase.VillaList.FirstOrDefault(u => u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			return Ok(villa);
		}

		//[HttpPost("{id:int},{name:String}")]
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public ActionResult<VillaDto> PostVilla([FromBody] VillaDto villa)
		{
			if (villa == null)
			{
				return BadRequest(villa);
			}
			if(DataBase.VillaList.FirstOrDefault(u=> u.Name.ToLower() == villa.Name.ToLower()) == null)
			{
				ModelState.AddModelError("CustomError", "Villa already exists!");
			}
			if (villa.Id < 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			villa.Id = DataBase.VillaList.Count + 1;
			DataBase.VillaList.Add(villa);
			//return Ok(villa);
			return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
		}

		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult DeleteVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = DataBase.VillaList.FirstOrDefault(u => u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			DataBase.VillaList.Remove(villa);
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult UpdateVilla(int id,[FromBody]VillaDto villa) 
		{
			if (villa == null || id != villa.Id)
			{
				return BadRequest();
			}
			var villachange = DataBase.VillaList.FirstOrDefault(u =>u.Id == id);
			if(villachange != null)
			{
				villachange.Name = villa.Name;
				villachange.Sqrft = villa.Sqrft;
				villachange.Occupency = villa.Occupency;
			}
			return Ok();
		}
		[HttpPatch("{id:int}", Name = "PatchVilla")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult PatchVilla(int id,JsonPatchDocument<VillaDto> patchVilla)
		{
			if(id == 0 || patchVilla == null)
			{
				return BadRequest();
			}
			var villa = DataBase.VillaList.FirstOrDefault(u => u.Id == id);
			if(villa == null)
			{
				return BadRequest();
			}
			patchVilla.ApplyTo(villa, ModelState);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}

	}
}
	