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
        public static string MovePath;
        public static string ActorPath;
        public static string EnemyPath;

        static User()
        {
            SchematicPath = @"D:\Documentos\Battle TB";
            MovePath = @$"{SchematicPath}\moves";
            ActorPath = @$"{SchematicPath}\actors";
            EnemyPath = @$"{SchematicPath}\enemies";
        }
    }
}
