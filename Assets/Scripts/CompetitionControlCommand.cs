using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.VersionControl;

public interface ICompetitionControlCommand 
{
    [JsonIgnore]
    public JToken Json
    {
        get => JToken.Parse(JsonConvert.SerializeObject((object)this))!;
    }

    [JsonIgnore]
    public string JsonString
    {
        get => JsonConvert.SerializeObject((object)this);
    }
    [JsonProperty("messageType")]
    public string messageType => "COMPETITION_CONTROL_COMMAND";

    [JsonProperty("command")]
    public string command { get; }

}
