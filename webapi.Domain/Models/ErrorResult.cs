namespace Domain.Models;
using Newtonsoft.Json;

public class ErrorResult
{
  public ErrorResult()
  { }

  public ErrorResult(int status, string error, string description = "")
  {
    Status = status;
    Description = description;
  }

  [JsonProperty(PropertyName = "title")]
  public string Title { get; set; }

  [JsonProperty(PropertyName = "status")]
  public int Status { get; set; }

  [JsonProperty(PropertyName = "description")]
  public string Description { get; set; }

  [JsonProperty(PropertyName = "modelState")]
  public IEnumerable<ModelStateVM> ModelState { get; set; } = Enumerable.Empty<ModelStateVM>();

  public class ModelStateVM
  {
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }
  }
}