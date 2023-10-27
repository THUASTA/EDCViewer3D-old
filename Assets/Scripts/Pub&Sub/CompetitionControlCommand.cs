using Newtonsoft.Json;
using System.Collections.Generic;

namespace EDCViewer.Messages
{
    internal record CompetitionControlCommand : Message
    {
        public enum Command
        {
            Start,

            End,

            Reset,

            GetHostConfiguration
        }

        [JsonProperty("messageType")]
        public override IMessage.MessageType Type => IMessage.MessageType.CompetitionControlCommand;

        [JsonProperty("token")]
        public string Token { get; init; } = string.Empty;

        [JsonProperty("command")]
        public Command command { get; init; } = new();
    }
}
