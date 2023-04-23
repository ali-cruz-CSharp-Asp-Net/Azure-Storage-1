using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_Framework_Azure_BlobStorage_1.Controllers
{
    public class AzureDownloadsController : Controller
    {
        string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        string containerName = "blobcontainer1";
        private BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;

        public AzureDownloadsController()
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        }

        // GET: AzureDownloads
        public async Task<ActionResult> Index()
        {
            List<BlobItem> blobItems = new List<BlobItem>();
            await foreach (BlobItem blobItem in _blobContainerClient.GetBlobsAsync())
            {
                blobItems.Add(blobItem);
            }
            return View(blobItems);
        }

        public async Task<ActionResult> DownloadFile(string fileName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            return File(download.Content, download.ContentType, fileName);
        }

        public async Task<ActionResult> DownloadBlob(string blobName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);
            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();
            MemoryStream memoryStream = new MemoryStream();
            await blobDownloadInfo.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, blobDownloadInfo.ContentType, blobName);
        }
    }
}