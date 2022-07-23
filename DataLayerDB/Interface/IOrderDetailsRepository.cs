using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerDB.DataBaseScaffold;
namespace DataLayerDB.Interface
{
    public interface IOrderDetailsRepository
    {
        void AddOrdersDetails(OrderDetail orderDetails);
        void DeleteOrdersDetails(OrderDetail orderDetails);
        OrderDetail GetOrderDetailsByID(int? id);
        IQueryable<OrderDetail> GetAllOrdersDetailsByOrderId(int orderId);
    }
}
