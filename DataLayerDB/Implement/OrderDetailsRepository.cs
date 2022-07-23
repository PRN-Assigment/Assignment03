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
            _dbSet = dbContext.OrderDetails;
        }

        public void AddOrdersDetails(OrderDetail orderDetails)
        {
            var last = _dbSet.Count();
            orderDetails.Id = last + 1;

            _dbContext.OrderDetails.Add(orderDetails);
            _dbContext.SaveChanges();
        }

        public void DeleteOrdersDetails(OrderDetail orderDetails)
        {
            _dbContext.OrderDetails.Remove(orderDetails);
            _dbContext.SaveChanges();
        }

        public OrderDetail GetOrderDetailsByID(int id)
        {
            var orderDT = _dbContext.OrderDetails.FirstOrDefault(x => x.OrderId == id);
            return orderDT;
        }

        public IQueryable<OrderDetail> GetAllDetails()
        {
            return _dbSet;
        }

        public IQueryable<OrderDetail> GetAllOrdersDetailsByOrderId(int orderId)
        {
            var result = _dbContext.OrderDetails.Where(x => x.OrderId == orderId);
            return result;
        }

    }
}
