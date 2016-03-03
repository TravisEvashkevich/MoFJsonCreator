using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MoFJsonEditor.Models
{

    public class Equipment : Loot
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EquipmentType Slot { get; set; }
        public int LowMelee { get; set; }
        public int MaxMelee { get; set; }
        public int LowMagic { get; set; }
        public int MaxMagic { get; set; }
        public int LowMagicResistance { get; set; }
        public int MaxMagicResistance { get; set; }
        public int LowMeleeResistance { get; set; }
        public int MaxMeleeResistance { get; set; }
        public int LowGold { get; set; }
        public int MaxGold { get; set; }
        public int CurrentUpgradeLevel { get; set; }
        public int MaxUpgradeLevel { get; set; }

        public Equipment()
        {
            Type = LootType.Equipment;
        }
    }
}
