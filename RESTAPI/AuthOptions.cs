using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RESTAPI
{
	public class AuthOptions
	{
		public const string ISSUER = "AgentServer";
		public const string AUDIENCE = "HTTP://localhost:44366";
		const string KEY = "Ibah0d@ybt!123";

		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
