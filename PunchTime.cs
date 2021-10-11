using System.Text.Json.Serialization;

namespace TM.NDVR
{
  public class PunchTime
  {
    [JsonPropertyName("ST_INSTANCE")]
    public int SessionNumber { get; set; }

    [JsonPropertyName("EMPLID")]
    public string EmployeeID { get; set; }

    [JsonPropertyName("EMPL_RCD")]
    public int EmployeeRecord { get; set; }

    [JsonPropertyName("PUNCH_TYPE")]
    public string PunchType { get; set; }
  }
}
