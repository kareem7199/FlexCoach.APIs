namespace FlexCoach.APIs.Dtos.Account
{
	public class AccountDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Token { get; set; } = null!;
		public string Role { get; set; } = null!;
    }
}
