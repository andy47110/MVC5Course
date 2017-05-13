using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All()
        {
            return base.All().Where(x => !x.Is刪除);
        }

        public Product Get單筆資料ByPrdouctId(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }
        public IQueryable<Product> GetProduct列表頁所有資料(bool Active, bool showAll =false)
        {

            IQueryable<Product> all = this.All();
            if (showAll)
            {
                all = base.All();
            }
            else
            {
                all = all
                            .Where(p =>
                            p.Active.HasValue
                            && p.Active.Value == Active)
                            .OrderByDescending(p => p.ProductId).Take(10);
            }
            return all;
        }
        public void Add單筆資料(Product product)
        {
            this.Add(product);
        }
        public void Update(Product product)
       {
           this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
       }
}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}