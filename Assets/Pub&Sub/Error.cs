using Newtonsoft.Json;
using System.Collections.Generic;

namespace NovelCraft.Utilities.Messages
{

    internal record Error : Message
    {

        [JsonProperty("messageType")]
        public override IMessage.MessageType Type => IMessage.MessageType.Error;

        [JsonProperty("errorCode")]
        public int ErrorCode { get; init; } = string.Empty;

        [JsonProperty("message")]
        public string Message { get; init; };
    }
}