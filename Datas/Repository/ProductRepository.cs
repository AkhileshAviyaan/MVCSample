using Datas.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
	{
		public ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		void IProductRepository.Update(Product category)
		{
			_db.Update(category);
		}
	}
}
