using System;
using System.Collections.Generic;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using System.ComponentModel.DataAnnotations;

namespace AWSServiceGateway.S3ServiceGateway
{
    public class S3InputUploadObject
    {
        [Required (ErrorMessage ="S3Bucket is required")]
        public string S3Bucket { get; set; }

        [Required(ErrorMessage = "Region is required")]
        public RegionEndpoint Region { get; set; } = RegionEndpoint.USEast1;

        public string Prefix { get; set; }

        [Required(ErrorMessage = "InpuObject is required")]
        public byte[] InputObject { get; set; }

        [Required(ErrorMessage = "FileName is required")]
        public string ObjectName { get; set; }

        [Required(ErrorMessage = "FileExtension is required")]
        public string ObjectExtension { get; set; }

        public S3StorageClass ObjectStorageClass { get; set; } = S3StorageClass.IntelligentTiering;

        public List<Tag> Tags { get; set; }

        public Dictionary<string, string> Metadata { get; set; }

        public bool RandomizeFileName { get; set; } = false;

        public S3CannedACL AccessToObject { get; set; } = S3CannedACL.BucketOwnerFullControl;
    }
}
