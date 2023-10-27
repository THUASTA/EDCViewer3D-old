using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System;
using EDCViewer.Messages;

namespace EDCViewer.Messages
{
    /// <summary>
    /// Parses messages from JSON.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parses a message from JSON.
        /// </summary>
        public static IMessage Parse(JToken json)
        {
            return Parse(json.ToString()!);
        }

        /// <summary>
        /// Parses a message from JSON.
        /// </summary>
        public static IMessage Parse(string jsonString)
        {
            EmptyMessage result = JsonConvert.DeserializeObject<EmptyMessage>(jsonString)!;
            IMessage.MessageType type = (IMessage.MessageType)(int)result.Type!;
            return type switch
            {
                IMessage.MessageType.CompetitionControlCommand =>
                  JsonConvert.DeserializeObject<CompetitionControlCommand>(jsonString)!,
                IMessage.MessageType.CompetitionUpdate =>
                  JsonConvert.DeserializeObject<CompetitionUpdate>(jsonString)!,
                IMessage.MessageType.Error =>
                  JsonConvert.DeserializeObject<Error>(jsonString)!,
                IMessage.MessageType.HostConfigurationFromClient =>
                  JsonConvert.DeserializeObject<HostConfigurationFromClient>(jsonString)!,
                IMessage.MessageType.HostConfigurationFromServer =>
                  JsonConvert.DeserializeObject<HostConfigurationFromServer>(jsonString)!,

                _ => throw new Exception("The message is not supported"),
            };
        }
    }
}
