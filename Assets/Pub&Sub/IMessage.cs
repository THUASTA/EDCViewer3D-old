using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NovelCraft.Utilities.Messages
{
    public interface IMessage
    {

        public enum MessageType
        {
            CompetitionControlCommand,

            CompetitionUpdate,

            Error,

            HostConfigurationFromClient,

            HostConfigurationFromServer
        }

        public MessageType Type { get; }
    }
}