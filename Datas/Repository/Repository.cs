using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Datas.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace Datas.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> _dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			_dbSet=_db.Set<T>();
			//_db.Categories = dbSet;
		}
		void IRepository<T>.Add(T entity)
		{
			_dbSet.Add(entity);
		}

		T IRepository<T>.Get(Expression<Func<T, bool>> filter)
		{
			var result=_dbSet.FirstOrDefault(filter);
			return result;
		}
		IEnumerable<T> IRepository<T>.GetAll()
		{
			var result = _dbSet.ToList();
			return result;
		}
		void IRepository<T>.Remove(T entity)
		{
			_dbSet.Remove(entity);
		}
		void IRepository<T>.RemoveRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}
	}
}
