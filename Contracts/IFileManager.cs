using Amazon;
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFileManager
    {
        Task<UploadResult> Upload(string accessKey, string secretkey, string bucketName, RegionEndpoint region, IFormFile file);
        Task Download(string accessKey, string secretkey, string bucketName, RegionEndpoint region, string fileName);
    }
}
