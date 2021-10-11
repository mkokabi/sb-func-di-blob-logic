using System.Threading.Tasks;

namespace TM.NDVR
{
  public interface ICsvHelper
  {
    Task<PunchTime[]> Extract(string content);
  }
}
