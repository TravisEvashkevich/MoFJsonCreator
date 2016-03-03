using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoFJsonEditor.Models;
using MoFJsonEditor.Utility;
using Newtonsoft.Json;
using WPFFramework;

namespace MoFJsonEditor.ViewModels
{
    public class EquipmentVm : ViewModelBase
    {
        private ObservableCollection<Loot> _loot;
        private int _selectedItem = -1;
        private int _highestLootId = 0;
        private SortBy _sortByType;
        private ObservableCollection<Loot> _sortedLoot;

        public SortBy SortByType
        {
            get { return _sortByType; }
            set
            {
                Set(ref _sortByType,value);
                SetNewSort();
            }
        }

        public int SelectedItem
        {
            get { return _selectedItem; } 
            set { Set(ref _selectedItem, value); }
        }

        public ObservableCollection<Loot> SortedLoot { get { return _sortedLoot; } set{Set(ref _sortedLoot,value);} }

        public ObservableCollection<Loot> Loot
        {
            get { return _loot; }
            set { Set(ref _loot, value); }
        }

        public DelegateCommand CmdNewEquipment { get; private set; }
        public DelegateCommand CmdNewPotion { get; private set; }
        public DelegateCommand CmdLoadLoot { get; private set; }
        public DelegateCommand CmdSaveEquipment { get; private set; }
        public DelegateCommand CmdAddLoot { get; private set; }

        public EquipmentVm()
        {
            _loot = new ObservableCollection<Loot>();
            _sortedLoot = new ObservableCollection<Loot>();
            CmdNewEquipment = new DelegateCommand(NewEquipment);
            CmdNewPotion = new DelegateCommand(NewPotion);
            CmdLoadLoot = new DelegateCommand(LoadLoot);
            CmdSaveEquipment = new DelegateCommand(SaveLoot);
            CmdAddLoot = new DelegateCommand(AddLoot);
            SortByType = SortBy.All;
        }

        private void AddLoot()
        {
            if(SelectedItem != -1)
                MainVm.Instance.MonsterVm.AddLoot(SortedLoot[SelectedItem]);
        }

        private void SetNewSort()
        {
            SortedLoot.Clear();
            switch (SortByType)
            {
                case SortBy.All:
                    foreach (var loot in Loot)
                    {
                        SortedLoot.Add(loot);
                    }
                    break;
                case SortBy.Equipment:
                {
                    var list = Loot.OfType<Equipment>().ToList();
                    foreach (var equipment in list)
                    {
                        SortedLoot.Add(equipment);
                    }
                }
                    break;
                case SortBy.Potion:
                {
                    var list = Loot.OfType<Potion>().ToList();
                    foreach (var pot in list)
                    {
                        SortedLoot.Add(pot);
                    }
                }
                    break;
                case SortBy.Head:
                {
                    foreach (var loot in Loot)
                    {
                        if (loot is Equipment && ((Equipment)loot).Slot == EquipmentType.Head)
                        {
                            SortedLoot.Add(loot);
                        }
                    }
                }
                    break;
                case SortBy.Chest:
                {
                    foreach (var loot in Loot)
                    {
                        if (loot is Equipment && ((Equipment)loot).Slot == EquipmentType.Chest)
                        {
                            SortedLoot.Add(loot);
                        }
                    }
                }
                    break;
                case SortBy.Pants:
                {
                    foreach (var loot in Loot)
                    {
                        if (loot is Equipment && ((Equipment)loot).Slot == EquipmentType.Pants)
                        {
                            SortedLoot.Add(loot);
                        }
                    }
                }
                    break;
                case SortBy.Boots:
                {
                    foreach (var loot in Loot)
                    {
                        if (loot is Equipment && ((Equipment)loot).Slot == EquipmentType.Boots)
                        {
                            SortedLoot.Add(loot);
                        }
                    }
                }
                    break;
                case SortBy.MainHand:
                {
                    foreach (var loot in Loot)
                    {
                        if (loot is Equipment && ((Equipment)loot).Slot == EquipmentType.MainHand)
                        {
                            SortedLoot.Add(loot);
                        }
                    }
                }
                    break;
                case SortBy.OffHand:
                {
                    foreach (var loot in Loot)
                    {
                        if (loot is Equipment && ((Equipment)loot).Slot == EquipmentType.OffHand)
                        {
                            SortedLoot.Add(loot);
                        }
                    }
                }
                    break;

            }
        }

        public void NewEquipment()
        {
            _highestLootId += 1;
            Loot.Add(new Equipment());
            SetNewSort();
        }

        public void NewPotion()
        {
            _highestLootId += 1;
            Loot.Add(new Potion());
            SetNewSort();
        }

        public void LoadLoot()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".LootJSON";
            dlg.InitialDirectory = @"E:\BitBucket\JsonEditor\MoF_JsonEditor\MoFJsonEditor\MoFJsonEditor\bin\Debug";
            dlg.Filter = "LootJSON (*.LootJSON)|*.LootJSON";
            var result = dlg.ShowDialog();
            if (result == true)
            {

                var json = File.ReadAllText(dlg.FileName);
                var output = JsonConvert.DeserializeObject<List<Loot>>(json, new LootCreationConverter());
                Loot.Clear();
                if(!String.IsNullOrEmpty(json))
                {
                    foreach (var loot in output)
                    {
                        Loot.Add(loot);
                    }
                }
            }
            SetNewSort();
        }

        public void SaveLoot()
        {
            //save WITH typing for loading
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All, Formatting = Formatting.Indented };
            string json = JsonConvert.SerializeObject(Loot, settings);
            File.WriteAllText(@"Loot.LootJSON", json);

            //save without extra typing ($id etc.)
            var serverSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            string serverJson = "";
            foreach (var loot in Loot)
            {
                /*if(loot is Equipment)
                    serverJson += "\""+ ((Equipment)loot).EquipmentName + "\":\n" + JsonConvert.SerializeObject(loot, Formatting.Indented);
                else
                {*/
                    serverJson += "\"" + loot.Name+ "\":\n" + JsonConvert.SerializeObject(loot, Formatting.Indented);
                /*}*/
                if (Loot.IndexOf(loot) != Loot.Count - 1)
                    serverJson += ",\n";
            }
            File.WriteAllText(@"Loot.ServerJSON", serverJson);
        
            MessageBox.Show("Saved!");
        }

        public void DeleteSomething()
        {
            if (SelectedItem >= 0)
            {
                var loot = SortedLoot[SelectedItem];
                Loot.Remove(loot);
                SortedLoot.RemoveAt(SelectedItem);
            }
        }

        public void ReplaceLootWithItem(string type)
        {
            if(SelectedItem == -1)
                return;
            //replace the Loot class with the actual class the user wants.
            switch (type)
            {
                case "Equipment":
                {
                    SortedLoot[SelectedItem] = new Equipment();
                }
                break;

                case "Potion":
                {
                    SortedLoot[SelectedItem] = new Potion();
                }
                break;
            }
            
            SelectedItem = SelectedItem;
        }

        public void SetImagePathAndSpriteName(string path, string name)
        {
            if (SelectedItem != -1)
            {
                Loot[SelectedItem].ImagePath = path;
                Loot[SelectedItem].SpriteName = name;
            }
        }

        public string GetSpriteName()
        {
            if (SelectedItem != -1)
            {
                return Loot[SelectedItem].SpriteName;
            }
            return "NoNameFound";
        }
    }
}
