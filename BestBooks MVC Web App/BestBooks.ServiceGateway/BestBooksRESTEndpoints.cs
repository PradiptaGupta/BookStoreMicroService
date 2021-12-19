using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestBooks.ServiceGateway
{
    public class BestBooksRESTEndpoints
    {
        public string RESTApiGetAllBooksServiceEndpoint { get; set; }

        public string RESTApiSearchBooksServiceEndpoint { get; set; }

        public string RESTApiGetAllBookCategoriesServiceEndpoint { get; set; }

        public string RESTApiGetAllBooksPublishersServiceEndpoint { get; set; }

        public string RESTApiSaveBookPublishersServiceEndpoint { get; set; }
    }
}
