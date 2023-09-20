using UnityEngine;
using System.Collections.Concurrent;

public class UI : MonoBehaviour
{
    private readonly ConcurrentQueue<ICompetitionControlCommand> _messageQueue = new();

    private void Start()
    {
        
    }
    void Send(ICompetitionControlCommand message)
    {
        _messageQueue.Enqueue(message);
    }

    public void StartMessage()
    {
        StartCommand startCommand = new StartCommand();
        Send(startCommand);
    }

    public void EndMessage()
    {
        EndCommand endCommand = new EndCommand();
        Send(endCommand);
    }

    public void ResetMessage()
    {
        ResetCommand resetCommand = new ResetCommand();
        Send(resetCommand);
    }

    public void GetHostConfigurationMessage()
    {
        GetHostConfigurationCommand getHostConfigurationCommand = new GetHostConfigurationCommand();
        Send(getHostConfigurationCommand);
    }
}
