using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datas.Repository.IRepository;
using Models;
namespace Datas.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}

		void ICategoryRepository.Update(Category category)
		{
			_db.Update(category);
		}
	}
}
