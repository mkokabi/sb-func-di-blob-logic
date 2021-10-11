using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using System;

[assembly: FunctionsStartup(typeof(TM.NDVR.Startup))]
namespace TM.NDVR
{
  public class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddHttpClient();

      builder.Services.AddSingleton<IBlobHelper>((s) =>
      {
        var storageConnectionString =
          Environment.GetEnvironmentVariable("ndvrcltimesheet_STORAGE", EnvironmentVariableTarget.Process);

        return new BlobHelper(storageConnectionString, "clexport");
      });

      builder.Services.AddSingleton<ICsvHelper, CsvHelper>();
      // builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
    }
  }
}
