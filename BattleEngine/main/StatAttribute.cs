using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.main
{
    public record struct StatAttribute
    {
        public string Name { get; private set; }
        public string Acronym { get; private set; }

        private int _value;
        public int Value {
            get 
            { 
                return _value; 
            } 
            set 
            {
                if (value > (int)Global.Limits.STATCAP)
                {
                    _value = (int)Global.Limits.STATCAP;
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

        public StatAttribute(string name, int value)
        {
            Name = name;
            Acronym = name.Substring(0, 3).ToUpper();
            Value = value;
        }
    }
}
