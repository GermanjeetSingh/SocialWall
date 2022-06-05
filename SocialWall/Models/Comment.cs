using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialWall.Models
{
	public class Comment
	{
		[Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		[MaxLength(120)]
		public string content { get; set; }
		[Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime createdOn { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public Comment()
		{

		}
	}
}

