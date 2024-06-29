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
        public static List<string> MoveList { get; private set; }
        public static List<string> InvalidMoves { get; private set; }

        public static List<string> ActorList { get; private set; }
        public static List<string> InvalidActors { get; private set; }

        static SchematicHandler()
        {
            MoveList = new List<string>();
            InvalidMoves = new List<string>();

            ActorList = new List<string>();
            InvalidActors = new List<string>();

            MoveList.AddRange(Directory.EnumerateFiles(User.MovePath));
            ActorList.AddRange(Directory.EnumerateFiles(User.ActorPath));
        }

        public static void SaveSchema(MoveSchematic mov)
        {
            if (Directory.Exists(User.MovePath))
            {
                string Intermediary = JsonSerializer.Serialize(mov, Global.SchemaFormatter);

                File.WriteAllText(User.MovePath + $@"\{mov.FileName.ToLower()}.json", Intermediary);
            }
            else
            {
                FolderStructurer.CreateStructure(1);

                string Intermediary = JsonSerializer.Serialize(mov, Global.SchemaFormatter);

                File.WriteAllText(User.MovePath + $@"\{mov.FileName.ToLower()}.json", Intermediary);
            }
        }
        public static void SaveSchema(ActorSchematic schema)
        {
            if (Directory.Exists(User.ActorPath))
            {
                string Intermediary = JsonSerializer.Serialize(schema, Global.SchemaFormatter);

                File.WriteAllText(User.ActorPath + $@"\{schema.FileName.ToLower()}.json", Intermediary);
            }
            else
            {
                FolderStructurer.CreateStructure(2);

                string Intermediary = JsonSerializer.Serialize(schema, Global.SchemaFormatter);

                File.WriteAllText(User.ActorPath + $@"\{schema.FileName.ToLower()}.json", Intermediary);
            }
        }
    }
}
