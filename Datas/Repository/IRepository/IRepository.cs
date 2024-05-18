using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T-Category
		IEnumerable<T> GetAll();
		//General syantax for linq operation
		T Get(Expression<Func<T,bool>> filter);
		void Add(T entity);	
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
	}
}
