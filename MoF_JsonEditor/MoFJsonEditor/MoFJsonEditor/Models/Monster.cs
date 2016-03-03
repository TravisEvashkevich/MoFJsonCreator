using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using Newtonsoft.Json;
using WPFFramework;

namespace MoFJsonEditor.Models
{
    public class Monster
    {
        public string Name { get; set; }
        public Stats Stats { get; set; }

        //this inventory is for visual purposes
        [JsonIgnore]
        public ObservableCollection<Loot> InventoryView { get; set; }

        //this is used to actually house the ID's of the inventory items for serialization
        public ObservableCollection<string> Inventory { get; set; } 

        public Monster()
        {
            Inventory = new ObservableCollection<string>();
            InventoryView = new ObservableCollection<Loot>();
            Stats= new Stats();
        }
    }
}
