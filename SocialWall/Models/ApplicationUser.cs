using System;
using Microsoft.AspNetCore.Identity;

namespace SocialWall.Models
{
	public class ApplicationUser:IdentityUser
	{
		public virtual ICollection<Post> Posts { get; set; }
	}
}

