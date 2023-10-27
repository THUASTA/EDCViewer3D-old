using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EDCViewer.Messages
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

        public abstract MessageType Type { get; }

        /// <summary>
        /// Gets the JSON representation of the message.
        /// </summary>
        public JToken Json { get; }

        /// <summary>
        /// Gets the JSON string representation of the message.
        /// </summary>
        public string JsonString { get; }
    }
}
