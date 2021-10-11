using System.Threading.Tasks;

namespace TM.NDVR
{
  public interface IBlobHelper
  {
    Task<string> ReadBlob(string blobName);
  }
}
