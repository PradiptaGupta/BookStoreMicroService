using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Amazon;

namespace AWSServiceGateway.S3ServiceGateway
{
    public class S3InputDeleteObject
    {
        [Required(ErrorMessage = "S3Bucket is required")]
        public string S3Bucket { get; set; }

        [Required(ErrorMessage = "Region is required")]
        public RegionEndpoint Region { get; set; } = RegionEndpoint.USEast1;

        [Required(ErrorMessage = "S3 Object full name is required")]
        public string ObjectFullName { get; set; }
    }
}
