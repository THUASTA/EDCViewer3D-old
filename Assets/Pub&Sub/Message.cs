using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}

namespace NovelCraft.Utilities.Messages
{

    internal abstract record Message : IMessage
    {
        [JsonProperty("messageType")]
        public abstract IMessage.MessageType Type { get; }
    }
}