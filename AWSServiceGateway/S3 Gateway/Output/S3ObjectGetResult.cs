using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSServiceGateway.S3ServiceGateway
{
    public class S3ObjectGetResult
    {
        public string InputObjectName { get; set; }

        public string S3ObjectExtension { get; set; }

        public byte[] S3ObjectData { get; set; }
    }
}
