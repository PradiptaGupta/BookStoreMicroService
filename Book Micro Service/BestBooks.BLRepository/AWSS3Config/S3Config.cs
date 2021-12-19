using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestBooks.BookMicroService.BLRepository.AWSS3Config
{
    public class S3Config
    {
        public string S3BucketName { get; set; }
        public string S3BucketRegion { get; set; }
    }
}
