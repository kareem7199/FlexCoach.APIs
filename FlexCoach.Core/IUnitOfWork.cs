using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Repositories.Contract;

namespace FlexCoach.Core
{
	public interface IUnitOfWork
	{
		IGenericRepository<T> Repository<T>() where T : BaseEntity;
		Task<int> CompleteAsync();
	}
}
