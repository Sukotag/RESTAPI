using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RESTAPI.Models; 

namespace RESTAPI.Controllers
{
	public class AccountController : Controller
	{
		private List<Person> people = new List<Person>
		{
			new Person {Login = "admin", Password = "66666", Role = "admin"},
			new Person {Login = "agent", Password = "12345", Role = "user" }
		};

		[HttpPost("/token")]
		public async Task Token()
		{
			var username = Request.Form["username"];
			var password = Request.Form["password"];

			var identity = GetIdentity(username, password);
			if (identity == null)
			{
				Response.StatusCode = 400;
				await Response.WriteAsync("Неправильный Логин или Пароль.");
				return;
			}

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					notBefore: now,
					claims: identity.Claims,
					expires: now.Add(TimeSpan.FromMinutes(1)),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new
			{
				access_token = encodedJwt,
				username = identity.Name
			};

			// сериализация ответа
			Response.ContentType = "application/json";
			await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
		}

		private ClaimsIdentity GetIdentity(string username, string password)
		{
			Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
			if (person != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
				};
				ClaimsIdentity claimsIdentity =
					new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

				return claimsIdentity;
			}

			// если пользователя не найдено
			return null;
		}
	}
}
