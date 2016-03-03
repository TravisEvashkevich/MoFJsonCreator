using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFFramework;

namespace MoFJsonEditor.ViewModels
{
    //ViewModel to handle the creation of all other viewmodels and passing refs of VM's to others if they need them.
    public class MainVm:ViewModelBase
    {
        //make a static 
        private static MainVm _mainVm;
        public static MainVm Instance
        {
            get
            {
                if (_mainVm == null)
                {
                    _mainVm = new MainVm();
                }
                return _mainVm;
            }
        }

        public EditorVm EditorVm { get; set; }
        public MonsterVm MonsterVm { get; set; }
        public EquipmentVm EquipmentVm { get; set; }

        private MainVm()
        {
            EditorVm = new EditorVm();
            MonsterVm = new MonsterVm();
            EquipmentVm = new EquipmentVm();
        }
    }
}
