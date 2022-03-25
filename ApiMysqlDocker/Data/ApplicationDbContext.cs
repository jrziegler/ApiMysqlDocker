using System;
using ApiMysqlDocker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiMysqlDocker.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			this.Database.EnsureCreated();
		}

		public DbSet<Product> Products { get; set; }
	}
}

