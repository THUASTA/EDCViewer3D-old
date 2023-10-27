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
    private GameObject _connectServerWindow;
    private TMP_InputField _connectServerIPText;
    private TMP_InputField _connectServerPortText;
    private Button _connectServerConfirmButton;
    private Button _connectServerCancelButton;

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


        // Add server buttons
        _connectServerWindow = GameObject.Find("Canvas/ConnectServer/");
        _connectServerConfirmButton = GameObject.Find("Canvas/ConnectServer/ConnectConfirmButton").GetComponent<Button>();
        _connectServerCancelButton = GameObject.Find("Canvas/ConnectServer/ConnectCancelButton").GetComponent<Button>();
        _connectServerIPText = GameObject.Find("Canvas/ConnectServer/Input/IP").GetComponent<TMP_InputField>();
        _connectServerPortText = GameObject.Find("Canvas/ConnectServer/Input/Port").GetComponent<TMP_InputField>();

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
            _connectServerWindow.SetActive(true);
            Debug.Log("connect");

        });


        _connectServerConfirmButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Get ip and port from the input field
            string ip = _connectServerIPText.text;
           if(!int.TryParse(_connectServerPortText.text,out int port))
            {
                UnityEngine.Debug.Log("Cannot parse <port> to int");
            };
            _client = new(ip, port);

            // TODO: Add the event handler
            //_client.AfterMessageReceiveEvent += OnAfterMessageReceiveEvent;
            Debug.Log("confirm");

        });

        _connectServerCancelButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Clear text
            _connectServerIPText.text="";
            _connectServerPortText.text="";

            _connectServerWindow.SetActive(false);
            Debug.Log("cancel");
        });

        _connectServerWindow.SetActive(false);
    }

    void Send(IMessage message)
    {
        _messageQueue.Enqueue(message);
    }
    private void Update()
    {
        
    }
}
