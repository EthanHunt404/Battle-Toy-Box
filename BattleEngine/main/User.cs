using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.main
{
    public static class User
    {
        public static string SchematicPath;
        public static string MovePath;
        public static string ActorPath;

        static User()
        {
            SchematicPath = @"D:\Documentos\Battle Toy Box";
            MovePath = @$"{SchematicPath}\moves";
            ActorPath = @$"{SchematicPath}\actors";
        }
    }
}
