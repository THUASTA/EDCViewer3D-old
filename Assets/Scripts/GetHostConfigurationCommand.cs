using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GetHostConfigurationCommand : ICompetitionControlCommand
{
    [JsonProperty("command")]
    public string command { get => "GET_HOST_CONFIGURATION"; }
}

