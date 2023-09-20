using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ResetCommand : ICompetitionControlCommand
{
    [JsonProperty("command")]
    public string command { get => "RESET"; }
}
