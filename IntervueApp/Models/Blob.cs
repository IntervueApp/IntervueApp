using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntervueApp.Models
{
    public class Blob
    {
        public CloudStorageAccount CloudStorageAccount { get; set; }
        public CloudBlobClient CloudBlobClient { get; set; }

        public Blob(string storageAccountName, string accessKey)
        {
            CloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=intervuestorage;AccountKey=1IGtYb6i5OeqQ6CJHR2U0IW7dQq72doLPxSFp2oNLvOgMFAiCiI5OX3rXK0EYaYQiLl/U6tIqpkwJTICsio56A==;EndpointSuffix=core.windows.net");
            CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();
        }
        
    }
}
