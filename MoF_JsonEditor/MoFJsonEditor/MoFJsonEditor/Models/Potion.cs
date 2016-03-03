using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MoFJsonEditor.Models
{
    public enum PotionType
    {
        HP,
        XP,
    }

    public class Potion : Loot
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PotionType PotionType { get; set; }
        public int Amount { get; set; }

        public Potion()
        {
            Type = LootType.Potion;
        }
    }
}
