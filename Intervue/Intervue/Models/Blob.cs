using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace Intervue.Models
{
    public class Blob
    {
        public CloudStorageAccount CloudStorageAccount { get; set; }
        public CloudBlobClient CloudBlobClient { get; set; }
        public IConfiguration _configuration { get; set; }

        public Blob(IConfiguration configuration)
        {
            _configuration = configuration;
            CloudStorageAccount = CloudStorageAccount.Parse(_configuration["ConnectionStrings:BlobStorage"]);
            CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();
        }

        // GetContainer
        public async Task<CloudBlobContainer> GetContainer(string containerName)
        {
            CloudBlobContainer cbc = CloudBlobClient.GetContainerReference(containerName);
            await cbc.CreateIfNotExistsAsync();

            await cbc.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            return cbc;
        }
    }
}
