using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestBooks.ViewModel;

namespace BestBooks.ServiceGateway.BookServiceGateway
{
    public interface IBookServiceGateway
    {
        public List<Book> GetBookList(string Category="",string Publisher="");

        public List<string> GetLBookPublisherList();

        public List<string> GetBookCategoryList();

        public string SaveBook(Book NewBook);
    }
}
