using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC_Framework_Azure_BlobStorage_1
{
    public class AzureBlobStorageService
    {
            private readonly string _connectionString;
            private readonly string _containerName;

            public AzureBlobStorageService(string connectionString, string containerName)
            {
                _connectionString = connectionString;
                _containerName = containerName;
            }

            public async Task UploadFileAsync(string fileName, Stream fileStream)
            {
                var containerClient = new BlobContainerClient(_connectionString, _containerName);

                await containerClient.CreateIfNotExistsAsync();

                var blobClient = containerClient.GetBlobClient(fileName);

                await blobClient.UploadAsync(fileStream, true);
            }







    }
}