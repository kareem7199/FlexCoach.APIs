using System.ComponentModel.DataAnnotations;

namespace FlexCoach.APIs.Dtos.Coach.Certificate
{
	public class CertificateDto
	{
		[Required]
		public IFormFile Certificate { get; set; }
	}
}
