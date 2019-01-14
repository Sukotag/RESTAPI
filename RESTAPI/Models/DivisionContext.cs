using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RESTAPI.Models
{
	public class DivisionContext : DbContext
	{
		public DbSet<Division> Divisions { get; set; }

		public DivisionContext(DbContextOptions<DivisionContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var bId = modelBuilder.Entity<Division>().HasKey(b => b.Id);

			modelBuilder.Entity<Division>().Property(p => p.Name).HasMaxLength(128);
						
		}
	}
}
