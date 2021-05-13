using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class Game
    {
        [MaxLength(36)]
        public string Id { get; set; }
        public string GenreId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageURL { get; set; }

        public string TrailerURL { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
