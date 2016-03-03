using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoFJsonEditor.Models
{
    public class Stats
    {
        private int _hp;
        public int HP
        {
            get { return _hp; }
            set
            {
                _hp = value;
                CurrentHp = value;
            }
        }
        public int CurrentHp { get; set; }
        public int Melee { get; set; }
        public int Magic { get; set; }
        public int MeleeRes { get; set; }
        public int MagicRes { get; set; }
    }

}
