using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class Order
    {     
        public string OrderId { get; set; }
        public string Userusername { get; set; }
        public virtual User user { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }        

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
