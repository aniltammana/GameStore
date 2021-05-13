using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Models;

namespace GameStore.DB
{
	public class GameContext: DbContext
	{
		protected IConfiguration configuration;

		public GameContext(DbContextOptions<GameContext> options)
			: base(options)
		{
		}

		public GameContext()
		{
		}
		protected override void OnModelCreating(ModelBuilder model)
		{
			model.Entity<Cart>().HasKey(tbl => new { tbl.SessionId, tbl.GameId });
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				IConfigurationRoot configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json")
					.Build();
				var connectionString = configuration.GetConnectionString("DbConn");
				optionsBuilder.UseSqlServer(connectionString);
			}
		}

		public DbSet<Game> Games { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<ActivationCode> ActivationCodes { get; set; }
		public DbSet<Session> Sessions { get; set; }

	}
}
