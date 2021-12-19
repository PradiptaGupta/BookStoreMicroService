using System;
using System.Linq;
using BestBooks.ViewModel;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using BestBooks.ServiceGateway;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace BestBooks.ServiceGateway.BookServiceGateway
{
    public class BookServiceGateway : IBookServiceGateway
    {
        BestBooksRESTEndpoints _BestBooksRESTServiceEndpoints;

        public BookServiceGateway(IOptions<BestBooksRESTEndpoints> BestBooksRESTServiceEndpoints)
        {
            _BestBooksRESTServiceEndpoints = BestBooksRESTServiceEndpoints.Value;
        }
        List<string> IBookServiceGateway.GetBookCategoryList()
        {
            List<string> BookCategoryList = new List<string>();

            using (var HttpBestBooksRESTClient = new HttpClient())
            {
                using (var BooksRESTResult = HttpBestBooksRESTClient.GetAsync(_BestBooksRESTServiceEndpoints.RESTApiGetAllBookCategoriesServiceEndpoint).Result)
                {
                    string apiResponse = BooksRESTResult.Content.ReadAsStringAsync().Result.ToString();
                    ReturnResult<List<string>> Result = JsonConvert.DeserializeObject<ReturnResult<List<string>>>(apiResponse);
                    BookCategoryList = Result.RetuenMessage;
                }
            }

            return BookCategoryList;
        }

        List<Book> IBookServiceGateway.GetBookList(string Category, string Publisher)
        {
            List<Book> BookList = new List<Book>();

            using (var HttpBestBooksRESTClient = new HttpClient())
            {
                using (var BooksRESTResult = HttpBestBooksRESTClient.GetAsync(_BestBooksRESTServiceEndpoints.RESTApiGetAllBooksServiceEndpoint).Result)
                {
                    string apiResponse = BooksRESTResult.Content.ReadAsStringAsync().Result.ToString();
                    ReturnResult<List<Book>> Result= JsonConvert.DeserializeObject<ReturnResult<List<Book>>>(apiResponse);
                    BookList = Result.RetuenMessage;
                }
            }

            return BookList;
        }

        List<string> IBookServiceGateway.GetLBookPublisherList()
        {
            List<string> BookPublisherList = new List<string>();

            using (var HttpBestBooksRESTClient = new HttpClient())
            {
                using (var BooksRESTResult = HttpBestBooksRESTClient.GetAsync(_BestBooksRESTServiceEndpoints.RESTApiGetAllBooksPublishersServiceEndpoint).Result)
                {
                    string apiResponse = BooksRESTResult.Content.ReadAsStringAsync().Result.ToString();
                    ReturnResult<List<string>> Result = JsonConvert.DeserializeObject<ReturnResult<List<string>>>(apiResponse);
                    BookPublisherList = Result.RetuenMessage;
                }
            }

            return BookPublisherList;
        }

        string IBookServiceGateway.SaveBook(Book NewBook)
        {
            string BookID = string.Empty;

            var Book = JsonConvert.SerializeObject(NewBook);

            var RequestContent = new StringContent(Book, Encoding.UTF8, "application/json");

            using (var HttpBestBooksRESTClient = new HttpClient())
            {
                using (var BooksRESTResult = HttpBestBooksRESTClient.PostAsync(_BestBooksRESTServiceEndpoints.RESTApiSaveBookPublishersServiceEndpoint, RequestContent).Result)
                {
                    string apiResponse = BooksRESTResult.Content.ReadAsStringAsync().Result.ToString();
                    ReturnResult<string> Result = JsonConvert.DeserializeObject<ReturnResult<string>>(apiResponse);
                    BookID = Result.RetuenMessage;
                }
            }

            return BookID;
        }
    }
}
