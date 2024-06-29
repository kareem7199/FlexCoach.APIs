﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Repositories.Contract;
using FlexCoach.Core.Specifications;
using FlexCoach.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexCoach.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
		{
			return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
		}

		public async Task<int> GetCountAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}

		public void Add(T entity)
			=> _dbContext.Set<T>().Add(entity);

		public void Update(T entity)
			=> _dbContext.Set<T>().Update(entity);

		public void Delete(T entity)
			=> _dbContext.Set<T>().Remove(entity);
	}
}
