using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Framework_Azure_BlobStorage_1.Controllers
{
    public class AzureUploadsController : Controller
    {
        private string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        private string containerName = "blobcontainer1";


        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var blobService = new AzureBlobStorageService(connectionString, containerName);
                using (var stream = file.InputStream)
                {
                    await blobService.UploadFileAsync(file.FileName, stream);
                }
            }

            return RedirectToAction("Index");
        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}
