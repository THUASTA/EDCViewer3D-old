using Newtonsoft.Json;
using System.Collections.Generic;

namespace NovelCraft.Utilities.Messages
{

    internal record CompetitionControlCommand : Message
    {
stage


        [JsonProperty("messageType")]
        public override IMessage.MessageType Type => IMessage.MessageType.CompetitionControlCommand;

        [JsonProperty("token")]
        public string Token { get; init; } = string.Empty;

        [JsonProperty("command")]
        public Command Command { get; init; } = new();
    }
}