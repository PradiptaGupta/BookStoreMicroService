using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSServiceGateway.S3ServiceGateway
{
    public class S3ObjectUploadResult
    {
        public string InputObjectName { get; set; }

        public string S3ObjectName { get; set; }
    }
}
