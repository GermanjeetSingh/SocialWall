using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialWall.Models
{
	public class Like
	{
		[Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public Like()
		{
		}
	}
}

