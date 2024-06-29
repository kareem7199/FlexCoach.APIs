using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace FlexCoach.Repository
{
	internal class SpecificationsEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
		{
			var query = inputQuery;

			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria);

			if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);

			else if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);

			if (spec.IsPaginationEnabled)
				query = query.Skip(spec.Skip).Take(spec.Take);

			query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

			return query;
		}
	}
}
