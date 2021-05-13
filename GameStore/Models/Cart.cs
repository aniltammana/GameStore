using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class Cart
    {
        [Required]
        public string SessionId { get; set; }
        public virtual Session Session { get; set; }
        [Required]
        public string GameId { get; set; }
        public int Quantity { get; set; }
        public virtual Game Game { get; set; }
    }
}
