using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SocialWall.Models;

namespace SocialWall
{
	public class Post
	{
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
        [Required]
        [MaxLength(240)]
		public string content { get; set; }
		[Required,DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime createdOn { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual ICollection<Like> Likes { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public Post()
		{
		}

		public bool isLikedByMe()
        {
			return Likes.Any(like => like.ApplicationUser == this.ApplicationUser);
        }
	
	}
}

