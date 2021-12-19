using System;

namespace AWSServiceGateway.S3ServiceGateway
{
    public class S3OperationResult<T,S> where T : class where S : class
    {
        public bool Success { get; set; } = false;

        public int StatusCode { get; set; } = 400;

        public T RetrnMessage { get; set; }

        public S ErrorMessage { get; set; }
    }
}
