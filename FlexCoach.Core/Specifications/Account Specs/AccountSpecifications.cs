using FlexCoach.Core.Entities;
using FlexCoach.Core.Specifications;

public class AccountSpecifications<T> : BaseSpecifications<T> where T : Account
{
	public AccountSpecifications(string email)
		: base(A => A.Email == email)
	{
	}
}
