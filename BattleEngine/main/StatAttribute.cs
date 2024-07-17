using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.main
{
    public record struct StatAttribute
    {
        public string Name { get; set; }
        public string Acronym { get; set; }

        private int _value;
        public int Value {
            get 
            { 
                return _value; 
            } 
            set 
            {
                if (value > (int)Global.Values.STATCAP)
                {
                    _value = (int)Global.Values.STATCAP;
                }
                else if (value < 0)
                {
                    _value = 0;
                }
                else
                {
                    _value = value;
                }
            } 
        }

        public StatAttribute(string name, string acronym, int value)
        {
            Name = name;
            Acronym = acronym;
            Value = value;
        }
    }
}
