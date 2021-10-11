using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace TM.NDVR
{
  public class BlobHelper : IBlobHelper
  {
    private readonly string storageConnectionString;
    private readonly string containerName;

    public BlobHelper(string storageConnectionString, string containerName)
    {
      this.storageConnectionString = storageConnectionString;
      this.containerName = containerName;
    }
    public async Task<string> ReadBlob(string blobName)
    {
      var blobServiceClient = new BlobServiceClient(storageConnectionString);
      var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
      var blobClient = blobContainerClient.GetBlobClient(blobName);
      var content = await blobClient.DownloadContentAsync();
      var value = content.Value;
      var str = value.Content.ToString();
      return str;
    }
  }
}
