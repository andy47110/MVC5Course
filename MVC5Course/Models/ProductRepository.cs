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
            return base.All().Where(x => !x.Is�R��);
        }

        public Product Get�浧���ByPrdouctId(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }
        public IQueryable<Product> GetProduct�C���Ҧ����(bool Active, bool showAll =false)
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
        public void Add�浧���(Product product)
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