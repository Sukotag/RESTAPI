using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RESTAPI.Models
{
	public class RealtorContext : DbContext
	{
		public DbSet<Realtor> Realtors { get; set; }

		public RealtorContext(DbContextOptions<RealtorContext> options) :  base(options)
		{
					   
		}
	}
}
