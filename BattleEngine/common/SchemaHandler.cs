using BattleEngine.main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class SchemaHandler
    {
        public static List<string> ActorList { get; private set; }
        public static List<string> InvalidActors { get; private set; }

        public static List<string> MoveList { get; private set; }
        public static List<string> InvalidMoves { get; private set; }

        static SchemaHandler()
        {
            ActorList = new List<string>();
            InvalidActors = new List<string>();

            MoveList = new List<string>();
            InvalidMoves = new List<string>();

            if (Directory.Exists(User.SchemaPath) & Directory.Exists(User.ActorPath) & Directory.Exists(User.MovePath))
            {
                ActorList.AddRange(Directory.EnumerateFiles(User.ActorPath));
                MoveList.AddRange(Directory.EnumerateFiles(User.MovePath));

                ActorList.Remove("base.json");
                MoveList.Remove("base.json");
            }
            else
            {
                Directory.CreateDirectory(User.SchemaPath);
                Directory.CreateDirectory(User.ActorPath);
                Directory.CreateDirectory(User.MovePath);

                ActorList.AddRange(Directory.EnumerateFiles(User.ActorPath));
                MoveList.AddRange(Directory.EnumerateFiles(User.MovePath));

                ActorList.Remove("base.json");
                MoveList.Remove("base.json");
            }
        }

        public static void SaveSchema(ActorSchema schema)
        {
            string Intermediary = JsonSerializer.Serialize(schema, Global.SchemaFormatter);

            File.WriteAllText(User.ActorPath + $@"\{schema.InternalName.ToLower()}.json", Intermediary);
        }
        public static void SaveSchema(MoveSchema mov)
        {
            string Intermediary = JsonSerializer.Serialize(mov, Global.SchemaFormatter);

            File.WriteAllText(User.MovePath + $@"\{mov.InternalName.ToLower()}.json", Intermediary);
        }
    }
}
