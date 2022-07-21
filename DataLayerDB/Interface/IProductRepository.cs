using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerDB.DataBaseScaffold;

namespace DataLayerDB.Interface
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts();
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        Product GetProductById(int? id);
        bool UpdateProduct(Product product);
    }
}
