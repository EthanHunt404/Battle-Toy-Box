using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattleEngine.main
{
    public static class SchematicHandler
    {
        public static List<string> ActorList { get; private set; }
        public static List<string> InvalidActors { get; private set; }

        public static List<string> MoveList { get; private set; }
        public static List<string> InvalidMoves { get; private set; }

        static SchematicHandler()
        {
            ActorList = new List<string>();
            InvalidActors = new List<string>();

            MoveList = new List<string>();
            InvalidMoves = new List<string>();

            ActorList.AddRange(Directory.EnumerateFiles(User.ActorPath));
            MoveList.AddRange(Directory.EnumerateFiles(User.MovePath));
        }

        public static void SaveSchema(ActorSchematic schema)
        {
            string Intermediary = JsonSerializer.Serialize(schema, Global.SchemaFormatter);

            File.WriteAllText(User.ActorPath + $@"\{schema.FileName.ToLower()}.json", Intermediary);
        }
        public static void SaveSchema(MoveSchematic mov)
        {
            string Intermediary = JsonSerializer.Serialize(mov, Global.SchemaFormatter);

            File.WriteAllText(User.MovePath + $@"\{mov.FileName.ToLower()}.json", Intermediary);
        }
    }
}
