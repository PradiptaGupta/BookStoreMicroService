using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBooks.BookMicroservice.BusinessEntity
{
    public class BookDiscount
    {
        public long BookID { get; set; }

        public decimal Discount { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }
    }
}
