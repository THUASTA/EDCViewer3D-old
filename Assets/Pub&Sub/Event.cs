using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}

namespace NovelCraft.Utilities.Messages
{

    internal abstract record Event : IEvent
    {
        [JsonProperty("eventType")]
        public abstract IEvent.EventType Type { get; }
    }
}