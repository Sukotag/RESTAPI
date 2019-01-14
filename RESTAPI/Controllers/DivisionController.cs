using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using RESTAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace RESTAPI.Controllers
{
	[Route("api/[controller]")]
	public class DivisionController : Controller
	{
		private DivisionContext db;
		public DivisionController(DivisionContext context)
		{
			this.db = context;
			
			if (db.Divisions.Any())
			{
				
			}
		}

		[Authorize]
		[HttpGet]
		public async Task<IEnumerable<Division>> Get()
		{
			return await db.Divisions.ToListAsync();
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			Division division = await db.Divisions.FirstOrDefaultAsync(x => x.Id == id);
			if (division == null)
				return NotFound();
			return new ObjectResult(division);
		}

		[Authorize]
		[HttpGet]
		[Route("name")]
		public async Task<IEnumerable<Division>> GetLikeName([FromQuery]string name)
		{			
			return await db.Divisions.Where(x => EF.Functions.Like(x.Name, "%" + name + "%")).ToListAsync();
		}

		//Удаление Подразделения	
		[Authorize]
		[HttpDelete("id")]
		public async Task<IActionResult> Delete(int id)
		{
			Division division = await db.Divisions.FirstOrDefaultAsync(x => x.Id == id);

			if (division == null)
			{
				return NotFound();
			}

			db.Divisions.Remove(division);
			await db.SaveChangesAsync();
			return Ok(division);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]Division division)
		{
			if (division == null)
			{
				return BadRequest();
			}

			db.Divisions.Add(division);
			await db.SaveChangesAsync();
			return Ok(division);
		}

		[Authorize]
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Division division)
		{
			if (division == null)
			{
				return BadRequest();
			}

			if (!db.Divisions.Any(x => x.Id == division.Id))
			{
				return NotFound();
			}

			db.Divisions.Update(division);
			await db.SaveChangesAsync();
			return Ok(division);
		}
	}
}
