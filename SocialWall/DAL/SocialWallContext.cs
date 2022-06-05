using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialWall.Models;

namespace SocialWall.DAL
{
	public class SocialWallContext: IdentityDbContext
	{

		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Post> Posts { get; set; }
		public SocialWallContext(DbContextOptions<SocialWallContext> options)
			: base(options)
		{
		}

		

	}
}

