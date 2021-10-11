
using FluentCsv.FluentReader;
using System.Linq;
using System.Threading.Tasks;

namespace TM.NDVR
{
  public class CsvHelper : ICsvHelper
  {
    public async Task<PunchTime[]> Extract(string content)
    {
      var csv = Read.Csv.FromString(content)
              .With.ColumnsDelimiter(",").And.EndOfLineDelimiter("\n")
              .ThatReturns.ArrayOf<PunchTime>()
              .Put.Column(0).As<int>().Into(a => a.SessionNumber)
              .Put.Column(1).Into(a => a.EmployeeID)
              .Put.Column(2).As<int>().Into(a => a.EmployeeRecord)
              .Put.Column(3).Into(a => a.PunchType)
              .GetAll();
      if (csv.Errors.Any())
      {
        // log.LogError("Error in parsing: {errors}", csv.Errors);
      }
      return await Task.FromResult(csv.ResultSet);
    }
  }
}
