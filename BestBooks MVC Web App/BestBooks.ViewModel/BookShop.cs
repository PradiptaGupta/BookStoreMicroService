using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestBooks.ViewModel
{
    public class BookShop
    {
        public List<Book> Books { get; set; }
        public List<string> BookCategories { get; set; }
        public List<string> BookPublishers {get;set;}
    }
}
