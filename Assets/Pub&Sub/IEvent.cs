using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NovelCraft.Utilities.Messages
{
    public interface IEvent
    {

        public enum EventType
        {
            PlayerAttack,

            PlayerDig,

            PlayerPickUp,

            PlayerPlaceBlock,

            PlayerTryAttack,

            PlayerTryUse
        }

        public EventType Type { get; }
    }
}