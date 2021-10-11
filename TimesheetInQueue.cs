using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TM.NDVR
{
  public class TimesheetInQueue
  {
    private readonly IBlobHelper blobHelper;
    private readonly ICsvHelper csvHelper;
    private readonly ILogger<TimesheetInQueue> log;
    private readonly HttpClient httpClient;

    public TimesheetInQueue(IHttpClientFactory httpClientFactory,
      IBlobHelper blobHelper,
      ICsvHelper csvHelper,
      ILogger<TimesheetInQueue> log)
    {
      this.blobHelper = blobHelper;
      this.csvHelper = csvHelper;
      this.log = log;
      this.httpClient = httpClientFactory.CreateClient();
    }

    [FunctionName("TimesheetInQueue")]
    public async Task Run(
      [ServiceBusTrigger("timesheets", Connection = "ndvrcltimesheet_SERVICEBUS")] string myQueueItem
    )
    {
      log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

      var punchItemContent = await blobHelper.ReadBlob(myQueueItem);
      log.LogInformation($"punchItemContent: {punchItemContent}");

      var punchItems = await csvHelper.Extract(punchItemContent);
      log.LogInformation($"punchItems.rows: {punchItems.Length}");

      var j = System.Text.Json.JsonSerializer.Serialize(punchItems);

      var psApiUrl = GetEnvironmentVariable("PSApiURL");
      var response = await httpClient.PostAsJsonAsync(psApiUrl, punchItems);
      response.EnsureSuccessStatusCode();
    }

    private string GetEnvironmentVariable(string name)
    {
      return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
  }
}
