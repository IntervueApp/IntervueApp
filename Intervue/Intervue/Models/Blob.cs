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

        /// <summary>
        /// This is for configuration for where the blob is to be created and accessed. Specifically, using CloudStorageAccount. The API key is noted as BlobStorage, which can be found in User Secrets.
        /// </summary>
        /// <param name="configuration"></param>
        public Blob(IConfiguration configuration)
        {
            _configuration = configuration;
            CloudStorageAccount = CloudStorageAccount.Parse(_configuration["BlobStorage"]);
            CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();
        }

        /// <summary>
        /// This will find the specific container assigned for the user. If the caontainer isn't found, a blob is created. Permissions are then created for this specific blob and will be publicly accessible.
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
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