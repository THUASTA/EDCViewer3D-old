using UnityEngine;
using System.Collections.Concurrent;
using EDCViewer.Messages;
using UnityEngine.UI;

public class GUIFuntion : MonoBehaviour
{
    private readonly ConcurrentQueue<IMessage> _messageQueue = new();
    private Button _startButton;
    private Button _endButton;
    private Button _resetButton;
    private Button _connectButton;
    private Button _settingsButton;

    private void Start()
    {
       _startButton=GameObject.Find("Canvas/StartButton").GetComponent<Button>();
        _endButton = GameObject.Find("Canvas/EndButton").GetComponent<Button>();
        _resetButton = GameObject.Find("Canvas/ResetButton").GetComponent<Button>();
        _connectButton = GameObject.Find("Canvas/ConnectButton").GetComponent<Button>();
        _settingsButton = GameObject.Find("Canvas/SettingsButton").GetComponent<Button>();

    }

    void Send(IMessage message)
    {
        _messageQueue.Enqueue(message);
    }
    private void Update()
    {
        
    }
}
