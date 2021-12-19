using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Extensions;

namespace AWSServiceGateway.S3ServiceGateway
{
    public interface IS3ServiceGateway
    {
        public Task<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>> UploadObjectAsync(S3InputUploadObject InputObject);

        public Task<List<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>>> UploadObjectsAsync(List<S3InputUploadObject> InputObject);

        public string GetS3ObjectURL(string S3Bucket, string Region, string ObjectName);

        public Task<List<S3OperationResult<S3ObjectGetResult, S3ErrorResult>>> GetObjectsDataAsync(List<S3InputGetObject> S3DeleteObject);

        public Task<S3OperationResult<S3ObjectGetResult, S3ErrorResult>> GetObjectDataAsync(S3InputGetObject S3GetObject);

        public Task<bool> DeleteObjectAsync(S3InputDeleteObject S3DeleteObject);

        public Task<List<S3OperationResult<string, string>>> DeleteObjectsAsync(List<S3InputDeleteObject> S3DeleteObject);

    }
}
