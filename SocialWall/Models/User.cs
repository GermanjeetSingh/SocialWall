using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SocialWall.Models
{
	public class User
	{
		[Required(ErrorMessage ="You must enter a username")]
		public string Username { get; set; }

		[Required(ErrorMessage = "You must enter a password")]
		public string Password { get; set; }

		public string? ConfirmPassword { get; set; }

	}
}

