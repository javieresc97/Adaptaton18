using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdaptatonApi.Controllers.Helpers
{
    public class BlobStorage
    {
        // Parse the connection string and return a reference to the storage account.
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("adaptaton18_AzureStorageConnectionString"));
        
        public async Task<string> UploadImageToStorage(byte[] array)
        {

            var containerName = "adaptaton";
            var fileName = Guid.NewGuid().ToString("D");
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync();

            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            using (var stream = new MemoryStream(array, writable: false))
            {
                blockBlob.UploadFromStream(stream);
                // Upload the file
                await blockBlob.UploadFromStreamAsync(stream);
            }
            return $"https://adaptaton18.blob.core.windows.net/{containerName}/{fileName}";
          
        }
    }
}