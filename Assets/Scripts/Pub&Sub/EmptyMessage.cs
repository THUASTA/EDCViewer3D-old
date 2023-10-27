using System.Collections.Generic;
using EDCViewer.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EDCViewer.Messages
{

    internal record EmptyMessage : Message
    {

        [JsonProperty("type")]
        public override IMessage.MessageType Type { get; }
    }
}
