using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestBooks.BookMicroservice.BusinessEntity;
using BestBooks.BookMicroservice.DataAccess;
using BestBooks.BookMicroService.BLRepository.OutputObject;

namespace BestBooks.BookMicroService.BLRepository.BookService
{
    public interface IBookService
    {
        public OutputResult<List<Book>> GetListOfAvailableBooks();

        public OutputResult<List<Book>> GetListOfAvailableBooks(string Category, string Publisher);

        public OutputResult<List<string>> GetListOfPublishers();

        public OutputResult<List<string>> GetListOfBookCategories();

        public OutputResult<string> SaveBook(BestBooks.BookMicroservice.BusinessEntity.Book NewBook);
    }
}
