using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class User
    {
        public static string SchematicPath;
        public static string ActorPath;
        public static string MovePath;

        static User()
        {
            SchematicPath = @"D:\Documentos\BattleSimulator";
            ActorPath = @$"{SchematicPath}\actors";
            MovePath = @$"{SchematicPath}\moves";
        }
    }
}
