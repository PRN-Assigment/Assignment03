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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private eStoreContext _dbContext;
        DbSet<OrderDetail> _dbSet { get; set; }

        public OrderDetailsRepository(eStoreContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<OrderDetail>();
        }

        public void AddOrdersDetails(OrderDetail orderDetails)
        {
            _dbContext.OrderDetails.Add(orderDetails);
            _dbContext.SaveChanges();
        }

        public void DeleteOrdersDetails(OrderDetail orderDetails)
        {
            _dbContext.OrderDetails.Remove(orderDetails);
            _dbContext.SaveChanges();
        }

        public OrderDetail GetOrderDetailsByID(int? id)
        {
            var orderDT = _dbContext.OrderDetails.FirstOrDefault(o => o.OrderId == id);
            return orderDT;
        }

        public IQueryable<OrderDetail> GetAllOrdersDetailsByOrderId(int orderId)
        {
            var result = _dbContext.OrderDetails.Where(x => x.OrderId == orderId);
            return result;
        }
    }
}
