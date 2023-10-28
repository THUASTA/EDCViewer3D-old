using UnityEngine;
using System.Collections.Concurrent;
using EDCViewer.Messages;
using UnityEngine.UI;
using TMPro;
using System.Net.WebSockets;
using System.Threading;
using Client = EDCViewer.Client.Client;
using System.Collections.Generic;
using static EDCViewer.Messages.CompetitionControlCommand;
using System;

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

    /// <summary>
    /// Settings Window
    /// </summary>
    private GameObject _settingsWindow;
    private Button _settingsConfirmButton;
    private Button _settingsCancelButton;

    /// <summary>
    /// The slider which can change the record playing rate
    /// </summary>
    private List<TMP_Text> _hueMinText;
    private List<TMP_Text> _hueMaxText;
    private List<TMP_Text> _saturationMinText;
    private List<TMP_Text> _saturationMaxText;
    private List<TMP_Text> _valueMinText;
    private List<TMP_Text> _valueMaxText;

    private List<TMP_Dropdown> _cameraDropdown;

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

        // Settings window
        _settingsWindow = GameObject.Find("Canvas/SettingsWindow/");
        _settingsConfirmButton = GameObject.Find("Canvas/SettingsWindow/ConfirmButton").GetComponent<Button>();
        _settingsCancelButton = GameObject.Find("Canvas/SettingsWindow/CancelButton").GetComponent<Button>();

        _hueMinText = new() {
            GameObject.Find("Canvas/SettingsWindow/Player1/Player1Color/Hue/Min/Text Area/Text").GetComponent<TMP_Text>(),
            GameObject.Find("Canvas/SettingsWindow/Player2/Player2Color/Hue/Min/Text Area/Text").GetComponent<TMP_Text>()
        };
        _hueMaxText = new() {
            GameObject.Find("Canvas/SettingsWindow/Player1/Player1Color/Hue/Max/Text Area/Text").GetComponent<TMP_Text>(),
            GameObject.Find("Canvas/SettingsWindow/Player2/Player2Color/Hue/Max/Text Area/Text").GetComponent<TMP_Text>()
        };
        _saturationMinText = new() {
            GameObject.Find("Canvas/SettingsWindow/Player1/Player1Color/Saturation/Min/Text Area/Text").GetComponent<TMP_Text>(),
            GameObject.Find("Canvas/SettingsWindow/Player2/Player2Color/Saturation/Min/Text Area/Text").GetComponent<TMP_Text>()
        };
        _saturationMaxText = new() {
            GameObject.Find("Canvas/SettingsWindow/Player1/Player1Color/Saturation/Max/Text Area/Text").GetComponent<TMP_Text>(),
            GameObject.Find("Canvas/SettingsWindow/Player2/Player2Color/Saturation/Max/Text Area/Text").GetComponent<TMP_Text>()
        };
        _valueMinText = new() {
            GameObject.Find("Canvas/SettingsWindow/Player1/Player1Color/Value/Min/Text Area/Text").GetComponent<TMP_Text>(),
            GameObject.Find("Canvas/SettingsWindow/Player2/Player2Color/Value/Min/Text Area/Text").GetComponent<TMP_Text>()
        };
        _valueMaxText = new() {
            GameObject.Find("Canvas/SettingsWindow/Player1/Player1Color/Value/Max/Text Area/Text").GetComponent<TMP_Text>(),
            GameObject.Find("Canvas/SettingsWindow/Player2/Player2Color/Value/Max/Text Area/Text").GetComponent<TMP_Text>()
        };

        _cameraDropdown = new()
        {
            GameObject.Find("Canvas/SettingsWindow/Player1/Player1Camera").GetComponent<TMP_Dropdown>(),
            GameObject.Find("Canvas/SettingsWindow/Player2/Player2Camera").GetComponent<TMP_Dropdown>(),
        };

        _startButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            if (Client is not null)
            {
                // Send packet to the server
                CompetitionControlCommand competitionControlCommand = new()
                {
                    command = CompetitionControlCommand.Command.Start
                };
                Client.Send(competitionControlCommand);
            }
        });

        _endButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            if (Client is not null)
            {
                // Send packet to the server
                CompetitionControlCommand competitionControlCommand = new()
                {
                    command = CompetitionControlCommand.Command.End
                };
                Client.Send(competitionControlCommand);
            }
        });

        _resetButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            if (Client is not null)
            {
                // Send packet to the server
                CompetitionControlCommand competitionControlCommand = new()
                {
                    command = CompetitionControlCommand.Command.Reset
                };
                Client.Send(competitionControlCommand);
            }
        });

        _settingsButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            if (Client is not null)
            {
                CompetitionControlCommand getConfigMessage = new CompetitionControlCommand()
                {
                    command = CompetitionControlCommand.Command.GetHostConfiguration
                };
                Client.Send(getConfigMessage);

                _settingsWindow.SetActive(true);
            }
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
            _frameDataWindow = _cameraDisplay.CameraListObj;
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

        _settingsConfirmButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();
            // TODO
            // Send packet
            if(Client is not null)
            {


                _settingsWindow.SetActive(false);
                Debug.Log("confirm settings");
            }

        });

        _settingsCancelButton.onClick.AddListener(() =>
        {
            // Play sound
            _buttonSound.Play();

            _settingsWindow.SetActive(false);
            Debug.Log("cancel settings");
        });

        _settingsWindow.SetActive(false);
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
        UnityMainThreadDispatcher.Enqueue(() =>
        {
            if (message is CompetitionUpdate competitionUpdate)
            {
                _cameraDisplay.DisplayCameraImage(competitionUpdate);
            }
            if (message is HostConfigurationFromServer configMessage)
            {
                // Update info
                for (int i = 0; i < Math.Min(2, configMessage.playerInfo.Count); i++)
                {
                    int hueCenter = configMessage.playerInfo[i].camera.recognition.HueCenter;
                    int hueRange = configMessage.playerInfo[i].camera.recognition.HueRange;
                    int SaturationCenter = configMessage.playerInfo[i].camera.recognition.SaturationCenter;
                    int SaturationRange = configMessage.playerInfo[i].camera.recognition.SaturationRange;
                    int ValueCenter = configMessage.playerInfo[i].camera.recognition.ValueCenter;
                    int ValueRange = configMessage.playerInfo[i].camera.recognition.ValueCenter;

                    _hueMinText[i].text = Math.Max(0, hueCenter - hueRange / 2).ToString();
                    _hueMaxText[i].text = Math.Min(255, hueCenter + hueRange / 2).ToString();
                    _saturationMinText[i].text = Math.Max(0, SaturationCenter - SaturationRange / 2).ToString();
                    _saturationMaxText[i].text = Math.Min(255, SaturationCenter + SaturationRange / 2).ToString();
                    _valueMinText[i].text = Math.Max(0, ValueCenter - ValueRange / 2).ToString();
                    _valueMaxText[i].text = Math.Min(255, ValueCenter + ValueRange / 2).ToString();


                }

                // Update camera
                List<string> cameraList = new();
                foreach (int camera in configMessage.AvailableCameras)
                {
                    cameraList.Add(camera.ToString());
                }
                for (int i = 0; i < 2; i++)
                {
                    _cameraDropdown[i].ClearOptions();
                    _cameraDropdown[i].AddOptions(cameraList);
                }
                // Update Port

            }
        });
    }
    private void Update()
    {
        
    }
}
