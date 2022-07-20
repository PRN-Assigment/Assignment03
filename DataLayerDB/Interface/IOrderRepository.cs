using DataLayerDB.DataBaseScaffold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerDB.Interface
{
    public interface IOrderRepository
    {
        bool UpdateOrder(Order order);
        IQueryable<Order> GetAll();
    }
}
