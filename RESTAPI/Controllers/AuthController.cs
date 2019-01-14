using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]    
    public class AuthController : Controller
    {
		[Authorize]
		[Route("getlogin")]
		public IActionResult GetLogin()
		{
			return Ok($"Ваш логин: {User.Identity.IsAuthenticated.ToString()}");
		}

		[Authorize(Roles = "admin")]
		[Route("getrole")]
		public IActionResult GetRole()
		{
			return Ok("Ваша роль: администратор");
		}
	}
}