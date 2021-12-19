using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestBooks.BookMicroservice.BusinessEntity
{
    public class Book
    {
        public long? ID { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public DateTime DatePublished { get; set; }

        public string Category { get; set; }

        public string Publisher { get; set; }

        public decimal BasePrice { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalPrice { get; set; }

        public Byte[] CoverImage { get; set; }

        public string CoverImageURL { get; set; }

        public string CoverImageFileName { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
