using System.ComponentModel.DataAnnotations;

namespace FlexCoach.APIs.Dtos.Coach.Plan
{
	public class PlanDto
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[Range(30 , 365)]
		public int Duration { get; set; }
	}
}
