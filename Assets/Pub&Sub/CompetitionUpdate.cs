using Newtonsoft.Json;
using System.Collections.Generic;

namespace NovelCraft.Utilities.Messages
{

    internal record CompetitionUpdate : Message, Event
    {

        [JsonProperty("messageType")]
        public override IMessage.MessageType Type => IMessage.MessageType.CompetitionUpdate;

        [JsonProperty("cameras")]
        public List<Cameras> cameras { get; init; } = new();

        [JsonProperty("chunks")]
        public List<Chunks> chunks { get; init; } = new();

        [JsonProperty("events")]
        public List<Events> events { get; init; } = new();

        [JsonProperty("info")]
        public Info info { get; init; } = new(); 

        [JsonProperty("mines")]
        public List<Mines> mines { get; init; } = new();

        [JsonProperty("players")]
        public List<Players> players { get; init; } = new();  
        
        public record Cameras
        {
            [JsonProperty("cameraId")]
            public int cameraId { get; init; }

            [JsonProperty("frameData")]
            public string frameData { get; init; }

            [JsonProperty("height")]
            public int height { get; init; }

            [JsonProperty("width")]
            public int width { get; init; }   

        }

        public record Chunks
        {
            [JsonProperty("chunkId")]
            public int chunkId { get; init; }

            [JsonProperty("height")]
            public int height { get; init; }

            [JsonProperty("position")]
            public Position position { get; init; }      

            public record Position
            {
                [JsonProperty("x")]
                public int x { get; init; }

                [JsonProperty("y")]
                public int y { get; init; }

            }     
        }     

        public record Events//存在问题
        {
            [JsonProperty("PlayerAttackEvent")]
            public PlayerAttackEvent PlayerAttackEvent { get; init; }

            [JsonProperty("PlayerDigEvent")]
            public PlayerDigEvent PlayerDigEvent { get; init; }

            [JsonProperty("PlayerPickUpEvent")]
            public PlayerPickUpEvent PlayerPickUpEvent { get; init; }

            [JsonProperty("PlayerPlaceBlockEvent")]
            public PlayerPlaceBlockEvent PlayerPlaceBlockEvent { get; init; }   

            [JsonProperty("PlayerTryAttackEvent")]
            public PlayerTryAttackEvent PlayerTryAttackEvent { get; init; }

            [JsonProperty("PlayerTryUseEvent")]
            public PlayerTryUseEvent PlayerTryUseEvent { get; init; }   

            public record PlayerAttackEvent
            {
                [JsonProperty("eventType")]
                public override IEvent.EventType Type => IEvent.EventType.PlayerAttack;

                [JsonProperty("playerId")]
                public int playerId { get; init; }

                [JsonProperty("targetPlayerId")]
                public int targetPlayerId { get; init; }                                  
            }               

            public record PlayerDigEvent
            {
                [JsonProperty("eventType")]
                public override IEvent.EventType Type => IEvent.EventType.PlayerDig;

                [JsonProperty("playerId")]
                public int playerId { get; init; }

                [JsonProperty("targetChunk")]
                public int targetChunk { get; init; }                
                }                   
            }    

            public record PlayerPickUpEvent
            {
                [JsonProperty("eventType")]
                public override IEvent.EventType Type => IEvent.EventType.PlayerPickUp;

                [JsonProperty("playerId")]
                public int playerId { get; init; }

                [JsonProperty("itemType")]
                public ItemType itemType { get; init; }                

                [JsonProperty("itemCount")]
                public int itemCount { get; init; }        

                public enum ItemType       
                {
                    IronIngot,

                    GoldIngot,

                    Diamond

                }                
            }    

            public record PlayerPlaceBlockEvent
            {
                [JsonProperty("eventType")]
                public override IEvent.EventType Type => IEvent.EventType.PlayerPlaceBlock;

                [JsonProperty("playerId")]
                public int playerId { get; init; }

                [JsonProperty("blockType")]
                public BlockType blockType { get; init; }                      

                public enum BlockType       
                {
                   Wool 
                }                
            }    

            public record PlayerTryAttackEvent
            {
                [JsonProperty("eventType")]
                public override IEvent.EventType Type => IEvent.EventType.PlayerTryAttack;

                [JsonProperty("playerId")]
                public int playerId { get; init; }

                [JsonProperty("targetChunk")]
                public int targetChunk { get; init; }                      

            } 

            public record PlayerTryUseEvent
            {
                [JsonProperty("eventType")]
                public override IEvent.EventType Type => IEvent.EventType.PlayerTryUse;

                [JsonProperty("playerId")]
                public int playerId { get; init; }

                [JsonProperty("targetChunk")]
                public int targetChunk { get; init; }                      
                
            }             

        }

        public record Info
        {
            [JsonProperty("elapsedTicks")]
            public int elapsedTicks { get; init; }

            [JsonProperty("stage")]
            public Stage stage { get; init; }

            public enum Stage
            {
                Ready,

                Running,

                Battling,

                Finished

            }
        }     

        public record Mines
        {
            [JsonProperty("mineId")]
            public int mineId { get; init; }

            [JsonProperty("accumulatedOreCount")]
            public int accumulatedOreCount { get; init; }

            [JsonProperty("oreType")]
            public OreType oreType { get; init; }

            public enum OreType
            {
                IronOre,

                GoldOre,

                DiamondOre

            }

            [JsonProperty("position")]
            public Position position { get; init; }      

            public record Position
            {
                [JsonProperty("x")]
                public int x { get; init; }

                [JsonProperty("y")]
                public int y { get; init; }
                }                   
            }     
        }     

        public record Players
        {
            [JsonProperty("playerId")]
            public int playerId { get; init; }

            [JsonProperty("attributes")]
            public Attributes attributes { get; init; }

            public record Attributes
            {
                [JsonProperty("agility")]
                public int agility { get; init; }

                [JsonProperty("maxHealth")]
                public int maxHealth { get; init; }

                [JsonProperty("strength")]
                public int strength { get; init; }                
                }                   
            }     

            [JsonProperty("health")]
            public int health { get; init; }                  

            [JsonProperty("homePosition")]
            public HomePosition homePosition { get; init; }      

            public record HomePosition
            {
                [JsonProperty("x")]
                public number x { get; init; }

                [JsonProperty("y")]
                public number y { get; init; }
                }                   
            }     

            [JsonProperty("inventory")]
            public Inventory inventory { get; init; }      

            public record Inventory
            {
                [JsonProperty("emerald")]
                public int emerald { get; init; }

                [JsonProperty("wool")]
                public int wool { get; init; }
                }                   
            }  

            [JsonProperty("position")]
            public Position position { get; init; }      

            public record Position
            {
                [JsonProperty("x")]
                public number x { get; init; }

                [JsonProperty("y")]
                public number y { get; init; }
                }                   
            }     
        }     
    }
}