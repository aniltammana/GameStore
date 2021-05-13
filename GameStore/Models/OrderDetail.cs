using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string GameId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Game Game { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<ActivationCode> ActivationCodes { get; set; }

    }
}
