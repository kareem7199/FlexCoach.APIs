using System.ComponentModel.DataAnnotations;

namespace FlexCoach.APIs.Dtos.Register
{
	public class TraineeRegisterDto
	{

		[Required]
		public string Name { get; set; } = null!;
		[Required]
		public string TraineeInfo { get; set; } = null!;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
		public string Password { get; set; } = null!;
	}
}
