using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoFJsonEditor.Views;
using WPFFramework;

namespace MoFJsonEditor.ViewModels
{
    public class EditorVm : ViewModelBase
    {
        //We'll use the tab index to change Hotkeys?
        private int _selectedTabIndex = 0;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                Set(ref _selectedTabIndex, value);
            }
        }

        public DelegateCommand CmdNew { get; private set; }
        public DelegateCommand CmdLoad { get; private set; }
        public DelegateCommand CmdSave { get; private set; }
        public DelegateCommand CmdDelete { get; private set; }

        public EditorVm()
        {
            CmdNew = new DelegateCommand(NewThing);
            CmdLoad = new DelegateCommand(LoadThings);
            CmdSave = new DelegateCommand(SaveThings);
            CmdDelete = new DelegateCommand(DeleteThings);
        }

        private void DeleteThings()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                {
                    MainVm.Instance.MonsterVm.DeleteSomething();
                }
                    break;
                case 1:
                {
                    MainVm.Instance.EquipmentVm.DeleteSomething();
                }
                    break;
            }
        }

        //For hotkeying on the fly (depending on what tab is selected) we will
        //decide what thing should be be done
        private void NewThing()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                {
                    MainVm.Instance.MonsterVm.NewMonster();
                }
                    break;
                case 1:
                {
                    MainVm.Instance.EquipmentVm.NewEquipment();
                }
                    break;
            }
        }

        private void LoadThings()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                    {
                        MainVm.Instance.MonsterVm.LoadMonsters();
                    }
                    break;
                case 1:
                    {
                        MainVm.Instance.EquipmentVm.LoadLoot();
                    }
                    break;
            }
        }

        private void SaveThings()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                    {
                        MainVm.Instance.MonsterVm.SaveMonsters();
                    }
                    break;
                case 1:
                    {
                        MainVm.Instance.EquipmentVm.SaveLoot();
                    }
                    break;
            }
        }
    }
}
