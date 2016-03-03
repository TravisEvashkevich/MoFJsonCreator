using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using MoFJsonEditor.Models;
using MoFJsonEditor.Utility;
using Newtonsoft.Json;
using WPFFramework;
using Monster = MoFJsonEditor.Models.Monster;

namespace MoFJsonEditor.ViewModels
{
    public class MonsterVm :ViewModelBase
    {
        private string _serverJson;
        private ObservableCollection<Monster> _monsters;
        private int _selectedItem = -1;
        private int _selectedMonster = -1;

        public ObservableCollection<Monster> Monsters
        {
            get { return _monsters; }
            set { Set(ref _monsters, value); }
        }

        public int SelectedMonster
        {
            get { return _selectedMonster; }
            set { Set(ref _selectedMonster,value);}
        }

        public int SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem,value); }
        }
        public DelegateCommand CmdNewMonster { get; private set; }
        public DelegateCommand CmdLoadMonsters { get; private set; }
        public DelegateCommand CmdSaveMonsters { get; set; }
        public DelegateCommand CmdExit { get; set; }

        public MonsterVm()
        {
            //make the Equipment Collections
            _monsters = new ObservableCollection<Monster>();
            Monsters = new ObservableCollection<Monster>();
            //Commands
            CmdNewMonster = new DelegateCommand(NewMonster);
            CmdLoadMonsters = new DelegateCommand(LoadMonsters);
            CmdSaveMonsters = new DelegateCommand(SaveMonsters);
        }

        public void NewMonster()
        {
            var mob = new Monster();
            Monsters.Add(mob);
        }

        public void SaveMonsters()
        {
            //save WITH typing for loading
            var settings = new JsonSerializerSettings{PreserveReferencesHandling = PreserveReferencesHandling.All,Formatting = Formatting.Indented};
            string json = JsonConvert.SerializeObject(Monsters,settings);
            File.WriteAllText(@"Monster.MonsterJSON",json);

            //save without extra typing ($id etc.)
            var serverSettings = new JsonSerializerSettings {Formatting = Formatting.Indented};
            string serverJson = "";
            foreach (var monster in Monsters)
            {
                serverJson += "\"" + monster.Name + "\":\n" + JsonConvert.SerializeObject(monster, Formatting.Indented);
                if(Monsters.IndexOf(monster) != Monsters.Count-1)
                    serverJson += ",\n";
            }
            File.WriteAllText(@"Monster.ServerJSON",serverJson);
            _serverJson = serverJson;
            MessageBox.Show("Saved!");

            //var platform = new DefaultPlatform();
            //GS.Initialise(platform);
            //
            //GS.GameSparksAvailable += Connected; 
            

        }

        public void LoadMonsters()
        {
            if (MainVm.Instance.EquipmentVm.Loot.Count == 0)
            {
                MainVm.Instance.EquipmentVm.LoadLoot();
            }

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".MonsterJSON";
            dlg.InitialDirectory = @"E:\BitBucket\JsonEditor\MoF_JsonEditor\MoFJsonEditor\MoFJsonEditor\bin\Debug";
            dlg.Filter = "MonsterJSON (*.MonsterJSON)|*.MonsterJSON";
            var result = dlg.ShowDialog();
            if (result == true)
            {

                var json = File.ReadAllText(dlg.FileName);
                var output = JsonConvert.DeserializeObject<List<Monster>>(json,new LootCreationConverter());
                Monsters.Clear();
                
                foreach (var monster in output)
                {
                    foreach (var i in monster.Inventory)
                    {
                        var temp = MainVm.Instance.EquipmentVm.Loot.First(x => x.Name == i);
                        monster.InventoryView.Add(temp);
                    }
                    Monsters.Add(monster);
                }
            }
        }

        public void DeleteSomething()
        {
            //if the item is not -1 then we have an item selected and we are wanting to delete it
            //else we want to delete the Monster itself
            if (SelectedItem != -1)
            {
                Monsters[SelectedMonster].Inventory.RemoveAt(SelectedItem);
                SelectedItem = -1;
            }
            else
            {
                Monsters.RemoveAt(SelectedMonster);
                SelectedMonster = 0;
            }
        }

        public void SetImagePathAndSpriteName(string path,string name)
        {
            if(SelectedItem != -1 && SelectedMonster != -1)
            {
                Monsters[SelectedMonster].InventoryView[SelectedItem].ImagePath = path;
                Monsters[SelectedMonster].InventoryView[SelectedItem].SpriteName = name;
            }
        }

        public string GetSpriteName()
        {
            if (SelectedItem != -1 && SelectedMonster != -1)
            {
                return Monsters[SelectedMonster].InventoryView[SelectedItem].SpriteName;
            }
            return "";
        }

        public void AddLoot(Loot loot)
        {
            if (SelectedMonster != -1)
            {
                Monsters[SelectedMonster].InventoryView.Add(loot);
                Monsters[SelectedMonster].Inventory.Add(loot.Id);
            }
        }
    }
}
