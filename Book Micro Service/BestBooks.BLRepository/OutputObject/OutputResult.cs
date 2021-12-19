using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBooks.BookMicroService.BLRepository.OutputObject
{
    public class OutputResult<T> where T : class
    {
        public bool Success { get; set; } = false;

        public int StatusCode { get; set; } = 400;

        public T RetuenMessage { get; set; }

        public string S3ObjectName { get; set; }

        public T RetuenMessageShort { get; set; }

        public T RetuenMessageDetailed { get; set; }
    }
}
