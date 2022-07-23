using DataLayerDB.DataBaseScaffold;

namespace eStore.Models
{
    public class OrderStatisticViewModel
    {
        public Order? Order { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
