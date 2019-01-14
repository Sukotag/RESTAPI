using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RESTAPI.Models
{
	public class RealtorListViewModel
	{
		public IEnumerable<Realtor> Realtors { get; set; }		
		public SelectList Division { get; set; }
		public string LastName { get; set; }
	}
}
