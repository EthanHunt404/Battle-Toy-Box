using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class User
    {
        public static string SchemaPath;
        public static string ActorPath;
        public static string MovePath;

        static User()
        {
            SchemaPath = @"D:\Documentos\BattleSimulator";
            ActorPath = @$"{SchemaPath}\actors";
            MovePath = @$"{SchemaPath}\moves";
        }
    }
}
