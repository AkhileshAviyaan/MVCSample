﻿using Bulky.Models;
using Microsoft.EntityFrameworkCore;
namespace Bulky.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Item1", DisplayOrder = 1 },
				new Category { Id = 2, Name = "Item2", DisplayOrder = 2 },
				new Category { Id = 3, Name = "Item3", DisplayOrder = 3 });
		}
	}
}