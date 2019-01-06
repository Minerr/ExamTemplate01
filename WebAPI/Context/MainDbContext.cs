using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebAPI.Context
{
	public class MainDbContext : DbContext
	{
		public DbSet<AuctionItem> AuctionItems { get; set; }

		public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
		{
			
		}

		// Fluent API
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AuctionItem>()
				.HasKey(i => i.ItemNumber);

			modelBuilder.Entity<AuctionItem>()
				.Property(i => i.ItemDescription)
				.IsRequired();

			modelBuilder.Entity<AuctionItem>()
				.Property(i => i.BidCustomName)
				.IsRequired();

			modelBuilder.Entity<AuctionItem>()
				.Property(i => i.BidCustomPhone)
				.IsRequired();

			modelBuilder.Entity<AuctionItem>()
				.Property(i => i.BidPrice)
				.IsRequired();

			modelBuilder.Entity<AuctionItem>()
				.Property(i => i.RatingPrice)
				.IsRequired();

			modelBuilder.Entity<AuctionItem>()
				.Property(i => i.BidTimestamp)
				.HasColumnType("datetime2");
		}
	}
}

