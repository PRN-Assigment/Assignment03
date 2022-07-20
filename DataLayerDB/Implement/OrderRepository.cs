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
    public class OrderRepository : IOrderRepository
    {
        private eStoreContext _dbContext;
        DbSet<Order> _dbSet { get; set; }
        public OrderRepository(eStoreContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Orders;
        }
        public bool UpdateOrder(Order order)
        {
            var target = _dbSet.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (target != null)
            {
                target.RequireDate = order.RequireDate;
                target.OrderDate = order.OrderDate;
                target.ShippedDate = order.ShippedDate;
                target.Freight = order.Freight;
                _dbSet.Update(target);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Order> GetAll()
        {
            return _dbSet;
        }
    }
}
