using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBooks.ServiceGateway
{
    public class ReturnResult<T> where T : class
    {
        public bool Success { get; set; } = false;

        public int StatusCode { get; set; } = 400;

        public T RetuenMessage { get; set; }
    }
}
