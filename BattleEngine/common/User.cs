using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class User
    {
        public static string JsonPath;
        public static string ActorPath;
        public static string MovePath;

        static User()
        {
            JsonPath = @"D:\Documentos\BattleSimulator";
            ActorPath = @$"{JsonPath}\actors";
            MovePath = @$"{JsonPath}\moves";
        }
    }
}
