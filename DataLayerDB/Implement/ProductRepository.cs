using DataLayerDB.DataBaseScaffold;
using DataLayerDB.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerDB.Implement
{
    public class ProductRepository : IProductRepository
    {

        private eStoreContext _dbContext;
        DbSet<Product> _dbSet { get; set; }

        public ProductRepository(eStoreContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Product>();
        }

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public IQueryable<Product> GetProducts()
        {
            var result = _dbContext.Products;
            return result;
        }

        public Product GetProductById(int? id)
        {
            var product = _dbContext.Products.FirstOrDefault(p=>p.ProductId==id);
            return product;
        }
    }
}
