using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerDB.DataBaseScaffold;
using DataLayerDB.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayerDB.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private eStoreContext _dbContext;
        DbSet<Order> _dbSet { get; set; }

        public OrderRepository(eStoreContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Order>();
        }

        public void AddOrders(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public void DeleteOrders(Order order)
        {
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }

        public IQueryable<Order> GetOrders()
        {
            var result = _dbContext.Orders;
            return result;
        }

        Order GetOrdersByID(int? id)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId== id);
            return order;
        }
        public List<Order> GetListReport(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Orders.Where(m => m.OrderDate >= startDate && m.OrderDate <= endDate).OrderByDescending(m => m.OrderDate).ToList();
        }

        Order IOrderRepository.GetOrdersByID(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
