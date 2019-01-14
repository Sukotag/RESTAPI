using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace RESTAPI.Service
{
	public class ErrorHandlingMiddleware
	{
		private RequestDelegate _next;		

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await _next.Invoke(context);
			if (context.Response.StatusCode == 403)
			{
				context.Response.ContentType = "text/html;charset=utf-8";
				await context.Response.WriteAsync("доступ отсутствует!");
			}
			else if (context.Response.StatusCode == 404)
			{
				context.Response.ContentType = "text/html;charset=utf-8";
				await context.Response.WriteAsync("Путь ненайден!");
			}
			else if (context.Response.StatusCode == 401)
			{
				context.Response.ContentType = "text/html;charset=utf-8";
				await context.Response.WriteAsync("Токен устарел!");
			}


		}



	}	
}
