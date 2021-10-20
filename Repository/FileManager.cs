using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Contracts;
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FileManager : IFileManager
    {
        public async Task Download(string accessKey, string secretkey, string bucketName, RegionEndpoint region, string fileName)
        {
            var S3Client = new AmazonS3Client(accessKey, secretkey, region);
            var fileTransferUtility = new TransferUtility(S3Client);

            // Use TransferUtilityUploadRequest to configure options.
            // In this example we subscribe to an event.

            var downloadRequest = new TransferUtilityDownloadRequest
            {
                BucketName = bucketName,
                Key = fileName,
            };

            //uploadRequest.UploadProgressEvent += new EventHandler<UploadProgressArgs>(uploadRequest_UploadPartProgressEvent);

            await fileTransferUtility.DownloadAsync(downloadRequest);
        }

        public async Task<UploadResult> Upload(string accessKey, string secretkey, string bucketName, RegionEndpoint region, IFormFile file)
        {
            var S3Client = new AmazonS3Client(accessKey, secretkey, region);
            var fileTransferUtility = new TransferUtility(S3Client);

            // Use TransferUtilityUploadRequest to configure options.
            // In this example we subscribe to an event.
            var day = DateTime.Now.ToString("dd");
            var month = DateTime.Now.ToString("MM");
            var year = DateTime.Now.ToString("yyyy");
            var heure = DateTime.Now.ToString("HH");
            var min = DateTime.Now.ToString("m");
            var second = DateTime.Now.ToString("ss");
            var filename = day + "_" + month + "_" + year + "_" + heure + "_" + min + "_" + second;
            var uploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = bucketName,
                InputStream = file.OpenReadStream(),
                Key = filename,
                StorageClass = S3StorageClass.Standard,
                CannedACL = S3CannedACL.PublicRead,
                PartSize = 6291456,
            };

            //uploadRequest.UploadProgressEvent += new EventHandler<UploadProgressArgs>(uploadRequest_UploadPartProgressEvent);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return new UploadResult() { Url = "https://" + bucketName + ".s3.amazonaws.com/" + filename };
        }
    }
}
