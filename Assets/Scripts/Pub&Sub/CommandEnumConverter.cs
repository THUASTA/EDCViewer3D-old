using EDCViewer.Messages;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using static EDCViewer.Messages.CompetitionControlCommand;

public class CommandEnumConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Command) || objectType == typeof(IMessage.MessageType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            string value = reader.Value.ToString();

            if (objectType == typeof(Command))
            {
                if (Enum.TryParse(typeof(Command), ToCamelCase(value), out object command))
                {
                    return command;
                }
            }
            else if (objectType == typeof(IMessage.MessageType))
            {
                if (Enum.TryParse(typeof(IMessage.MessageType), ToCamelCase(value), out object messageType))
                {
                    return messageType;
                }
            }
        }

        return null;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is Command command)
        {
            writer.WriteValue(ToSnakeCase(command.ToString()));
        }
        else if (value is IMessage.MessageType messageType)
        {
            writer.WriteValue(ToSnakeCase(messageType.ToString()));
        } 
    }

    // Helper method to convert from CamelCase to snake_case
    private string ToSnakeCase(string input)
    {
        return Regex.Replace(input, "(?<=.)([A-Z])", "_$1", RegexOptions.Compiled).TrimStart('_').ToUpper();
    }

    // Helper method to convert from snake_case to CamelCase
    private string ToCamelCase(string input)
    {
        string[] parts = input.Split('_');
        return string.Join("", parts.Select(p => char.ToUpper(p[0]) + p[1..].ToLower()));
    }
}
