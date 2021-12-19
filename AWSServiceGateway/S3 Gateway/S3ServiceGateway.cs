using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using System.Net;
using System.IO;
using HelperUtility;
using System.ComponentModel.DataAnnotations;
using Amazon;

namespace AWSServiceGateway.S3ServiceGateway
{
    public class S3ServiceGateway : IS3ServiceGateway
    {
        async Task<S3OperationResult<S3ObjectUploadResult,S3ErrorResult>> IS3ServiceGateway.UploadObjectAsync(S3InputUploadObject InputObject)
        {
            S3OperationResult<S3ObjectUploadResult, S3ErrorResult> OperationResult = await UploadObjectToS3Async(InputObject);

            return OperationResult;
        }

        async Task<List<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>>> IS3ServiceGateway.UploadObjectsAsync(List<S3InputUploadObject> InputObjects)
        {

            List<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>> OperationResult = await UploadObjectsToS3(InputObjects);

            return OperationResult;
        }

        async Task<S3OperationResult<S3ObjectGetResult,S3ErrorResult>> IS3ServiceGateway.GetObjectDataAsync(S3InputGetObject S3GetObject)
        {
            ICollection<ValidationResult> Results = null;

            if (!Utility.Validate<S3InputGetObject>(S3GetObject, out Results))
            {
                return new S3OperationResult<S3ObjectGetResult, S3ErrorResult>()
                {
                    Success = false,
                    StatusCode = 400,
                    ErrorMessage = new S3ErrorResult()
                    {
                        ErroeMessage= GetValidationErrorMessage(Results.Select(E => E.ErrorMessage).ToList<string>())
                    }
                };
            }
            else
            {
                IAmazonS3 AmazonS3Client = new AmazonS3Client(S3GetObject.Region);

                if (S3GetObject.ObjectFullName.IndexOf("https://") >= 0)
                {
                    S3GetObject.ObjectFullName = S3GetObject.ObjectFullName.Substring(S3GetObject.ObjectFullName.IndexOf("https://") + 8);
                    S3GetObject.ObjectFullName = S3GetObject.ObjectFullName.Substring(S3GetObject.ObjectFullName.IndexOf("/") + 1);
                }

                GetObjectResponse S3ObjectResponse = await AmazonS3Client.GetObjectAsync(S3GetObject.S3Bucket, S3GetObject.ObjectFullName);
                MemoryStream S3ObjectMemoryStream = new MemoryStream();

                using (Stream ResponseStream = S3ObjectResponse.ResponseStream)
                {
                    ResponseStream.CopyTo(S3ObjectMemoryStream);
                }

                return new S3OperationResult<S3ObjectGetResult,S3ErrorResult>()
                {
                    Success = true,
                    StatusCode = 200,
                    RetrnMessage = new S3ObjectGetResult()
                    {
                        InputObjectName = S3GetObject.ObjectFullName,
                        S3ObjectExtension = S3GetObject.ObjectFullName.Substring(S3GetObject.ObjectFullName.LastIndexOf(".")),
                        S3ObjectData = S3ObjectMemoryStream.ToArray()
                    }
                };
            }
        }

        async Task<List<S3OperationResult<S3ObjectGetResult, S3ErrorResult>>> IS3ServiceGateway.GetObjectsDataAsync(List<S3InputGetObject> S3GetObjects)
        {
            ICollection<ValidationResult> Results = null;

            List<S3OperationResult<S3ObjectGetResult, S3ErrorResult>> S3GetObjectResult = new List<S3OperationResult<S3ObjectGetResult, S3ErrorResult>>();

            foreach (S3InputGetObject S3GetObject in S3GetObjects)
            {
                if (!Utility.Validate<S3InputGetObject>(S3GetObject, out Results))
                {
                    S3GetObjectResult.Add(new S3OperationResult<S3ObjectGetResult, S3ErrorResult>()
                    {
                        Success = false,
                        StatusCode = 400,
                        ErrorMessage = new S3ErrorResult()
                        {
                            ErroeMessage = GetValidationErrorMessage(Results.Select(E => E.ErrorMessage).ToList<string>())
                        }
                    });
                }
                else
                {
                    IAmazonS3 AmazonS3Client = new AmazonS3Client(S3GetObject.Region);

                    if (S3GetObject.ObjectFullName.IndexOf("https://") >= 0)
                    {
                        S3GetObject.ObjectFullName = S3GetObject.ObjectFullName.Substring(S3GetObject.ObjectFullName.IndexOf("https://") + 8);
                        S3GetObject.ObjectFullName = S3GetObject.ObjectFullName.Substring(S3GetObject.ObjectFullName.IndexOf("/") + 1);
                    }

                    GetObjectResponse S3ObjectResponse = await AmazonS3Client.GetObjectAsync(S3GetObject.S3Bucket, S3GetObject.ObjectFullName);
                    MemoryStream S3ObjectMemoryStream = new MemoryStream();

                    using (Stream ResponseStream = S3ObjectResponse.ResponseStream)
                    {
                        ResponseStream.CopyTo(S3ObjectMemoryStream);
                    }

                    S3GetObjectResult.Add(new S3OperationResult<S3ObjectGetResult, S3ErrorResult>()
                    {
                        Success = true,
                        StatusCode = 200,
                        RetrnMessage = new S3ObjectGetResult()
                        {
                            InputObjectName = S3GetObject.ObjectFullName,
                            S3ObjectExtension = S3GetObject.ObjectFullName.Substring(S3GetObject.ObjectFullName.LastIndexOf(".")),
                            S3ObjectData = S3ObjectMemoryStream.ToArray()
                        }
                    });
                }
            }

            return S3GetObjectResult;
        }

        async Task<bool> IS3ServiceGateway.DeleteObjectAsync(S3InputDeleteObject S3DeleteObject)
        {
            bool IsSuccess = await DeleteObject(S3DeleteObject);

            return IsSuccess;
        }

        async Task<List<S3OperationResult<string, string>>> IS3ServiceGateway.DeleteObjectsAsync(List<S3InputDeleteObject> S3DeleteObjects)
        {
            List<S3OperationResult<string, string>> S3DeleteObjectResult = new List<S3OperationResult<string, string>>();

            S3OperationResult<string, string> DeleteResult = null;

            foreach (S3InputDeleteObject S3DeleteObject in S3DeleteObjects)
            {
                bool IsSuccess = await DeleteObject(S3DeleteObject);
                if (IsSuccess)
                {
                    DeleteResult = new S3OperationResult<string, string>();
                    DeleteResult.Success = true;
                    DeleteResult.StatusCode = 200;
                    DeleteResult.RetrnMessage = S3DeleteObject.ObjectFullName;

                    S3DeleteObjectResult.Add(DeleteResult);
                }
                else
                {
                    DeleteResult = new S3OperationResult<string, string>();
                    DeleteResult.Success = true;
                    DeleteResult.StatusCode = 200;
                    DeleteResult.RetrnMessage = S3DeleteObject.ObjectFullName;

                    S3DeleteObjectResult.Add(DeleteResult);
                }
            }

            return S3DeleteObjectResult;

        }

        string IS3ServiceGateway.GetS3ObjectURL(string S3Bucket, string Region, string ObjectName)
        {
            return GetS3FileURL(S3Bucket, Region, ObjectName);
        }

        async private Task<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>> UploadObjectToS3Async(S3InputUploadObject InputObject)
        {
            ICollection<ValidationResult> Results = null;

            if (!Utility.Validate<S3InputUploadObject>(InputObject, out Results))
            {
                return new S3OperationResult<S3ObjectUploadResult, S3ErrorResult>()
                {
                    Success = false,
                    StatusCode = 400,
                    ErrorMessage = new S3ErrorResult()
                    {
                        ErroeMessage = GetValidationErrorMessage(Results.Select(E => E.ErrorMessage).ToList<string>())
                    }
                };
            }
            else
            {
                TransferUtility S3TransferUtility = new TransferUtility(InputObject.Region);

                string SourceObjectName = InputObject.ObjectName;
                string FinalObjectName = string.Empty;

                if (SourceObjectName.IndexOf(".") > 0)
                {
                    SourceObjectName = SourceObjectName.Substring(1, SourceObjectName.IndexOf(".") - 1);
                }

                SourceObjectName = WebUtility.HtmlEncode(SourceObjectName);
                SourceObjectName = SourceObjectName + "." + InputObject.ObjectExtension.ToLower();

                if (InputObject.RandomizeFileName)
                {

                    FinalObjectName = Path.GetRandomFileName();
                    FinalObjectName = FinalObjectName + "." + InputObject.ObjectExtension.ToLower();
                }
                else
                {
                    FinalObjectName = SourceObjectName;
                }

                if (!string.IsNullOrEmpty(InputObject.Prefix))
                {
                    FinalObjectName = InputObject.Prefix + FinalObjectName;
                }

                var S3TransferUtilityRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = new MemoryStream(InputObject.InputObject),
                    Key = FinalObjectName,
                    BucketName = InputObject.S3Bucket,
                    CannedACL = InputObject.AccessToObject,
                    PartSize = 6291456,
                    StorageClass = InputObject.ObjectStorageClass,
                    TagSet = InputObject.Tags
                };

                // Add metatags which can include the original file name and other decriptions
                var ObjectMetaData = InputObject.Metadata;
                if (ObjectMetaData != null && ObjectMetaData.Count() > 0)
                {
                    foreach (var Tag in ObjectMetaData)
                    {
                        S3TransferUtilityRequest.Metadata.Add(Tag.Key, Tag.Value);
                    }
                }

                S3TransferUtilityRequest.Metadata.Add("OriginalFileName", SourceObjectName);

                await S3TransferUtility.UploadAsync(S3TransferUtilityRequest);

                return new S3OperationResult<S3ObjectUploadResult, S3ErrorResult>()
                {
                    Success = true,
                    StatusCode = 200,
                    RetrnMessage = new S3ObjectUploadResult()
                    {
                        InputObjectName = InputObject.ObjectName,
                        S3ObjectName = GetS3FileURL(InputObject.S3Bucket, InputObject.Region.ToString(), FinalObjectName)
                    }
                };
            }
        }

        private Task<List<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>>> UploadObjectsToS3(List<S3InputUploadObject> InputObjects)
        {
            S3OperationResult<S3ObjectUploadResult, S3ErrorResult> OpearationResult;
            List<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>> OperationResults = new List<AWSServiceGateway.S3ServiceGateway.S3OperationResult<S3ObjectUploadResult, S3ErrorResult>>();

            foreach (S3InputUploadObject InputObject in InputObjects)
            {
                try
                {
                    OpearationResult = UploadObjectToS3Async(InputObject).Result;
                    OperationResults.Add(new S3OperationResult<S3ObjectUploadResult, S3ErrorResult>()
                    {
                        Success = true,
                        StatusCode = 200,
                        RetrnMessage = new S3ObjectUploadResult()
                        {
                            InputObjectName = InputObject.ObjectName,
                            S3ObjectName = OpearationResult.RetrnMessage.S3ObjectName
                        }
                    });
                }
                catch (Exception ex)
                {
                    OperationResults.Add(new S3OperationResult<S3ObjectUploadResult, S3ErrorResult>
                    {
                        Success = false,
                        StatusCode = 400,
                        ErrorMessage = new S3ErrorResult()
                        {
                            ErroeMessage=ex.Message,
                            ErrorStackTrace=ex.StackTrace
                        }
                    });
                }
            }

            return Task.FromResult<List<S3OperationResult<S3ObjectUploadResult, S3ErrorResult>>>(OperationResults);
        }

        private Task<bool> DeleteObject(S3InputDeleteObject S3DeleteObject)
        {
            IAmazonS3 AmazonS3Client = new AmazonS3Client(S3DeleteObject.Region);

            DeleteObjectRequest S3DeleteRequest = new DeleteObjectRequest();
            S3DeleteRequest.BucketName = S3DeleteObject.S3Bucket;
            S3DeleteRequest.Key = S3DeleteObject.ObjectFullName;

            AmazonS3Client.DeleteObjectAsync(S3DeleteRequest);

            return Task.FromResult<bool>(true);
        }

        private string GetValidationErrorMessage(List<string> ErrorMessages)
        {
            string ErrorMessage = string.Empty;

            foreach (string Error in ErrorMessages)
            {
                ErrorMessage = ErrorMessage + Error + "\\n ";
            }

            return ErrorMessage;
        }

        private string GetS3FileURL(string S3Bucket, string Region, string FileName)
        {
            return "https://" + S3Bucket + ".s3" + Region + ".amazonaws.com/" + FileName;
        }

    }
}
