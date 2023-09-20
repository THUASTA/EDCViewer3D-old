using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents common interfaces for all messages sent from the client to the server.
/// </summary>
public interface IClientMessage : ICompetitionControlCommand
{
    /// <summary>
    /// Gets the token of the client.
    /// </summary>
    public string Token { get; }
}
