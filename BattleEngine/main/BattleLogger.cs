using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.main
{
    public static class BattleLogger
    {
        public delegate void TextDelegate(string text);
        public static event TextDelegate TextBus;

        public static void Log(string text)
        {
            TextBus.Invoke(text);
        }
    }
}
