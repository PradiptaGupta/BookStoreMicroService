using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BestBooks.ViewModel
{
    public class Book
    {
        public long? ID { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Name is required")] 
        [MaxLength(200, ErrorMessage ="Name should be <=200 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Author is required")]
        [MaxLength(500, ErrorMessage = "Author should be <=500 characters")]
        public string Author { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ISBN is required")]
        [MaxLength(30, ErrorMessage = "ISBN should be <=30 characters")]
        public string ISBN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date Published is required")]
        public DateTime DatePublished { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category is required")]
        [MaxLength(200, ErrorMessage = "Category should be <=200 characters")]
        public string Category { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Publisher is required")]
        [MaxLength(200, ErrorMessage = "Publisher should be <=200 characters")]
        public string Publisher { get; set; }

        public IFormFile CoverImageFile { get; set; }

        public string CoverImageFileName { get; set; }

        public byte[] CoverImage { get; set; }
    }
}
