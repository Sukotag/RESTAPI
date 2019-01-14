using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RESTAPI.Controllers
{	
	public class HomeController : Controller
	{
		private readonly RealtorContext db;
		public HomeController(RealtorContext context)
		{
			db = context;
		}


		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SettingsDb()
		{
			ViewData["Message"] = "Администрирование БД";

			return View();
		}				

		public IActionResult Auth()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
