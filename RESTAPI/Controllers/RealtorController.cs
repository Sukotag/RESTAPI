using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace RESTAPI.Controllers
{
	[Route("api/[controller]")]
	public class RealtorController : Controller
	{
		private RealtorContext db;
		public RealtorController(RealtorContext context)
		{
			this.db = context;

			if (db.Realtors.Any())
			{
				/*
				db.Realtors.Add(new Realtor { Firstname = "Николай", Lastname = "Котенко", Division = 1, CreatedDateTime = DateTime.Now.Date });
				db.Realtors.Add(new Realtor { Firstname = "Елена", Lastname = "Котенко", Division = 2, CreatedDateTime = DateTime.Now.Date });
				db.SaveChanges();*/
			}

		}

		[Authorize]
		[HttpGet]
		public async Task<IEnumerable<Realtor>> Get()
		{
			return await db.Realtors.ToListAsync();
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			Realtor realtor = await db.Realtors.FirstOrDefaultAsync(x => x.Id == id);
			if (realtor == null)
				return NotFound();
			return new ObjectResult(realtor);
		}

		//Запрос с условием like LastName	
		//api/realtor/lastname?lastname=
		[HttpGet]		
		[Route("lastname")]
		public async Task<IEnumerable<Realtor>> GetLikeLastName([FromQuery]string lastname)
		{			
			return await db.Realtors.Where(x => EF.Functions.Like(x.Lastname, "%" + lastname + "%")).ToListAsync();
		}

		//Выбор всех записей где подразделение =
		[HttpGet]
		[Route("division")]		
		public async Task<IEnumerable<Realtor>> GetRealtorOnDivision([FromQuery]int id)		
		{			
			return await db.Realtors.Where(x => x.Division == id).ToListAsync();
		}

		[Authorize]		
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]Realtor realtor)
		{
			if(realtor == null)
			{
				return BadRequest();
			}

			db.Realtors.Add(realtor);
			await db.SaveChangesAsync();
			return Ok(realtor);
		}

		[Authorize]
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Realtor realtor)
		{
			if(realtor == null)
			{
				return BadRequest();
			}

			if(!db.Realtors.Any(x => x.Id == realtor.Id))
			{
				return NotFound();
			}

			db.Realtors.Update(realtor);
			await db.SaveChangesAsync();
			return Ok(realtor);
		}

		//Удаление		
		[Authorize]
		[HttpDelete("id")]
		public async Task<IActionResult> Delete(int id)
		{
			Realtor realtor = await db.Realtors.FirstOrDefaultAsync(x => x.Id == id);

			if(realtor == null)
			{
				return NotFound();
			}

			db.Realtors.Remove(realtor);
			await db.SaveChangesAsync();
			return Ok(realtor);
		}


		
	}
}
