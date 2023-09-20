using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class StartCommand : ICompetitionControlCommand
{
    [JsonProperty("command")]
    public string command { get => "START"; }
}
