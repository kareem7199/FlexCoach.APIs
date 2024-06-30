using System.ComponentModel.DataAnnotations;

namespace FlexCoach.APIs.Dtos.Register
{
	public class CoachRegisterDto
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
		public string Password { get; set; } = null!;

		[Required]
		public IFormFile Picture { get; set; }

		[Required]
		[RegularExpression(@"^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
		public string Gender { get; set; }
        [Required]
		[Range(1 , 50)]
        public int Experience { get; set; }
        [Required]
        public string AboutMe { get; set; }
    }
}
