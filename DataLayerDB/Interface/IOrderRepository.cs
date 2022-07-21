using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerDB.DataBaseScaffold;
namespace DataLayerDB.Interface
{
    interface IOrderRepository
    {
        IQueryable<Order> GetOrders();
        void AddOrders(Order order);
        void DeleteOrders(Order order);
        Order GetOrdersByID(int? id);
    }
}
