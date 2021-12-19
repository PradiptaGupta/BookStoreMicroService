using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestBooks.BookMicroservice.BusinessEntity
{
    public class BookPrice
    {
        public long BookID { get; set; }

        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "Datetime")]
        public DateTime EffectiveFrom { get; set; }
    }
}
