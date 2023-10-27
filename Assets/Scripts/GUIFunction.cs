using UnityEngine;
using System.Collections.Concurrent;
using EDCViewer.Messages;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using EDCViewer.Client;
using UnityEditor.Experimental.GraphView;

public class GUIFuntion : MonoBehaviour
{
    private readonly ConcurrentQueue<IMessage> _messageQueue = new();

    private Button _startButton;
    private Button _endButton;
    private Button _resetButton;
    private Button _connectButton;
    private Button _settingsButton;

    /// <summary>
    /// Button sound controller
    /// </summary>
    private AudioSource _buttonSound;

    /// <summary>
    /// Add external server
    /// </summary>
    private GameObject _addExternalServerWindow;
    private TMP_InputField _addExternalServerIPText;
    private TMP_InputField _addExternalServerPortText;
    private Button _addExternalServerConfirmButton;
    private Button _addExternalServerCancelButton;

    private Client _client;

    private void Start()
    {

        _buttonSound = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
        _buttonSound.clip ??= Resources.Load<AudioClip>("Music/ButtonClick");

        _startButton =GameObject.Find("Canvas/StartButton").GetComponent<Button>();
        _endButton = GameObject.Find("Canvas/EndButton").GetComponent<Button>();
        _resetButton = GameObject.Find("Canvas/ResetButton").GetComponent<Button>();
        _connectButton = GameObject.Find("Canvas/ConnectButton").GetComponent<Button>();
        _settingsButton = GameObject.Find("Canvas/SettingsButton").GetComponent<Button>();

        _startButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Send packet to the server
            CompetitionControlCommand competitionControlCommand = new()
            {
                command = CompetitionControlCommand.Command.Start
            };
            _client.Send(competitionControlCommand);
        });

        _endButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Send packet to the server
            CompetitionControlCommand competitionControlCommand = new()
            {
                command = CompetitionControlCommand.Command.End
            };
            _client.Send(competitionControlCommand);
        });

        _resetButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Send packet to the server
            CompetitionControlCommand competitionControlCommand = new()
            {
                command = CompetitionControlCommand.Command.Reset
            };
            _client.Send(competitionControlCommand);
        });

        _settingsButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
        });

        _connectButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            _addExternalServerWindow.SetActive(true);
        });

        // Add server buttons
        _addExternalServerWindow = GameObject.Find("Canvas/ConnectServer/");
        _addExternalServerConfirmButton = GameObject.Find("Canvas/ConnectServer/ConnectConfirmButton").GetComponent<Button>();
        _addExternalServerCancelButton = GameObject.Find("Canvas/ConnectServer/ConnectCancelButton").GetComponent<Button>();
        _addExternalServerIPText = GameObject.Find("Canvas/ConnectServer/Input/IP").GetComponent<TMP_InputField>();
        _addExternalServerPortText = GameObject.Find("Canvas/ConnectServer/Input/Port").GetComponent<TMP_InputField>();

        _addExternalServerConfirmButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Get ip and port from the input field
            string ip = _addExternalServerIPText.text;
           if(!int.TryParse(_addExternalServerPortText.text,out int port))
            {
                UnityEngine.Debug.Log("Cannot parse <port> to int");
            };
            _client = new(ip, port);

            // TODO: Add the event handler
            //_client.AfterMessageReceiveEvent += OnAfterMessageReceiveEvent;

        });

        _addExternalServerCancelButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Clear text
            _addExternalServerIPText.text="";
            _addExternalServerPortText.text="";
        });
    }

    void Send(IMessage message)
    {
        _messageQueue.Enqueue(message);
    }
    private void Update()
    {
        
    }
}
