using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class EndCommand : ICompetitionControlCommand
{
    [JsonProperty("command")]
    public string command { get => "END"; }
}
