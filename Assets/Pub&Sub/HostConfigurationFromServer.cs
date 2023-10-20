using Newtonsoft.Json;
using System.Collections.Generic;

namespace NovelCraft.Utilities.Messages
{

    internal record HostConfigurationFromServer : Message
    {
        public record PlayerInfo
        {

            [JsonProperty("playerId")]
            public int PlayerId { get; init; }

            [JsonProperty("camera")]
            public Camera Camera { get; init; } = new();

            [JsonProperty("serialPort")]
            public SerialPort SerialPort { get; init; } = new();

            public record Camera
            {
                [JsonProperty("cameraId")]
                public int CameraId { get; init; }

                [JsonProperty("calibration")]
                public Calibration Calibration { get; init; } = new();

                [JsonProperty("recognition")]
                public Recognition Recognition { get; init; } = new();       

                public record Calibration
                {
                    [JsonProperty("topLeft")]
                    public TopLeft TopLeft { get; init; } = new();

                    [JsonProperty("topRight")]
                    public TopRight TopRight { get; init; } = new();    

                    [JsonProperty("bottomLeft")]
                    public BottomLeft BottomLeft { get; init; } = new();

                    [JsonProperty("bottomRight")]
                    public BottomRight BottomRight { get; init; } = new();    

                    public record TopLeft      
                    {
                        [JsonProperty("x")]
                        public number x { get; init; }

                        [JsonProperty("y")]
                        public number y { get; init; }
                    }        

                    public record TopRight      
                    {
                        [JsonProperty("x")]
                        public number x { get; init; }

                        [JsonProperty("y")]
                        public number y { get; init; }
                    }          

                    public record BottomLeft      
                    {
                        [JsonProperty("x")]
                        public number x { get; init; }

                        [JsonProperty("y")]
                        public number y { get; init; }
                    }   

                    public record BottomRight      
                    {
                        [JsonProperty("x")]
                        public number x { get; init; }

                        [JsonProperty("y")]
                        public number y { get; init; }
                    }                                                                                  
                }    

                public record Recognition
                {
                    [JsonProperty("hueCenter")]
                    public number HueCenter { get; init; }   

                    [JsonProperty("hueRange")]
                    public number HueRange { get; init; }  

                    [JsonProperty("saturationCenter")]
                    public number SaturationCenter { get; init; }       

                    [JsonProperty("saturationRange")]
                    public number SaturationRange { get; init; }    

                    [JsonProperty("valueCenter")]
                    public number ValueCenter { get; init; }    

                    [JsonProperty("valueRange")]
                    public number ValueRange { get; init; }    

                    [JsonProperty("minArea")]
                    public number MinArea { get; init; }    

                    [JsonProperty("showMask")]
                    public bool ShowMask { get; init; }                                                                                                                                                           
                }             
            }

            public record SerialPort
            {
                [JsonProperty("portName")]
                public string PortName { get; init; }

                [JsonProperty("baudRate")]
                public int BaudRate { get; init; }
            }
        }


        public record availableCameras
        {
            [JsonProperty("Items:")]
            public int Items { get; init; }
        }

        public record availableSerialPorts
        {
            [JsonProperty("Items:")]
            public int Items { get; init; }
        }        

        [JsonProperty("messageType")]
        public override IMessage.MessageType Type => IMessage.MessageType.HostConfigurationFromServer;

        [JsonProperty("availableCameras")]
        public List<availableCameras> AvailableCameras { get; init; } = new();

        [JsonProperty("availableSerialPorts")]
        public List<availableSerialPorts> AvailableSerialPorts { get; init; } = new(); 

        [JsonProperty("players")]
        public List<PlayerInfo> PlayerInfo { get; init; } = new();
    }
}