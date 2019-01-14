using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Models
{
	public class Realtor
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public int Division { get; set; }
		public DateTime CreatedDateTime { get; set; }
	}
}
