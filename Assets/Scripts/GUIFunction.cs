using UnityEngine;
using System.Collections.Concurrent;
using EDCViewer.Messages;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using EDCViewer.Client;
using UnityEditor.Experimental.GraphView;
using System.Net.WebSockets;
using System.Threading;
using UnityEditor.PackageManager;
using Client = EDCViewer.Client.Client;

public class GUIFuntion : MonoBehaviour
{
    private readonly ConcurrentQueue<IMessage> _messageQueue = new();

    private Button _startButton;
    private Button _endButton;
    private Button _resetButton;
    private Button _connectButton;
    private Button _settingsButton;
    private Button _calibrateButton;
    private Button _cameraButton;

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
    private CameraDisplay _cameraDisplay;


    /// <summary>
    /// Frame data
    /// </summary>
    private GameObject _frameDataWindow;

    public Client Client { get; private set; }

     
    private void Start()
    {
        _cameraDisplay= GetComponent<CameraDisplay>();

        _buttonSound = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
        _buttonSound.clip ??= Resources.Load<AudioClip>("Music/ButtonClick");

        _startButton =GameObject.Find("Canvas/StartButton").GetComponent<Button>();
        _endButton = GameObject.Find("Canvas/EndButton").GetComponent<Button>();
        _resetButton = GameObject.Find("Canvas/ResetButton").GetComponent<Button>();
        _connectButton = GameObject.Find("Canvas/ConnectButton").GetComponent<Button>();
        _settingsButton = GameObject.Find("Canvas/SettingsButton").GetComponent<Button>();
        _calibrateButton = GameObject.Find("Canvas/CalibrateButton").GetComponent<Button>();
        _cameraButton = GameObject.Find("Canvas/CameraButton").GetComponent<Button>();

        // Add server buttons
        _connectServerWindow = GameObject.Find("Canvas/ConnectServer/");
        _connectServerConfirmButton = GameObject.Find("Canvas/ConnectServer/ConnectConfirmButton").GetComponent<Button>();
        _connectServerCancelButton = GameObject.Find("Canvas/ConnectServer/ConnectCancelButton").GetComponent<Button>();
        _connectServerIPText = GameObject.Find("Canvas/ConnectServer/Input/IP").GetComponent<TMP_InputField>();
        _connectServerPortText = GameObject.Find("Canvas/ConnectServer/Input/Port").GetComponent<TMP_InputField>();
        _connectServerIPText.text = "127.0.0.1";
        _connectServerPortText.text = "8080";

        _frameDataWindow = _cameraDisplay.CameraListObj;

        _startButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // Send packet to the server
            CompetitionControlCommand competitionControlCommand = new()
            {
                command = CompetitionControlCommand.Command.Start
            };
            Client.Send(competitionControlCommand);
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
            Client.Send(competitionControlCommand);
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
            Client.Send(competitionControlCommand);
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

        _cameraButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();

            _frameDataWindow.SetActive(!_frameDataWindow.activeInHierarchy);
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
            Client = new Client(ip, port);

            Client.AfterMessageReceiveEvent += AfterMessageReceivedEventHandler;

            // TODO: Add the event handler
            //Client.AfterMessageReceiveEvent += OnAfterMessageReceiveEvent;
            _connectServerWindow.SetActive(false);
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
    public void QuitClient()
    {
        if (Client is not null)
        // Close websocket
        {
            Client.ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                                                "Closing",
                                                CancellationToken.None);
            Client.CloseReceiveMessageTask();
        } 
    }
    private void OnApplicationQuit()
    {
        QuitClient();
    }
    void Send(IMessage message)
    {
        _messageQueue.Enqueue(message);
    }
    public void AfterMessageReceivedEventHandler(object? sender, IMessage message)
    {
        if (message is CompetitionUpdate competitionUpdate)
        {
          _cameraDisplay.DisplayCameraImage(competitionUpdate);
        }
    }
    private void Update()
    {
        
    }
}
