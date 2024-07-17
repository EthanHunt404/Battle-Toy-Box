using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattleEngine.main
{
    public static class SchematicHandler
    {
        public static List<string> MoveFileList { get; private set; }
        public static Dictionary<string, string> MoveList { get; private set; }

        public static List<string> ActorFileList { get; private set; }
        public static Dictionary<string, string> ActorList { get; private set; }

        static SchematicHandler()
        {
            MoveList = new Dictionary<string, string>();
            ActorList = new Dictionary<string, string>();

            MoveFileList = new List<string>();
            ActorFileList = new List<string>();

            FolderStructurer.CreateStructure();

            RefreshSchemas();

            IdHandler.SortIDs();
        }

        public static void RefreshSchemas()
        {
            MoveFileList.Clear();
            ActorFileList.Clear();

            MoveFileList.AddRange(Directory.EnumerateFiles(User.MovePath));
            ActorFileList.AddRange(Directory.EnumerateFiles(User.ActorPath));

            foreach (string movepath in MoveFileList)
            {
                Move currentmove = new Move(movepath, true);

                MoveList.Add(currentmove.FileName, movepath);
            }
            foreach (string actorpath in ActorFileList)
            {
                Actor currentactor = new Actor(actorpath, true);

                ActorList.Add(currentactor.FileName, currentactor.FileName);
            }
        }
        public static void SaveSchema(MoveSchematic mov)
        {
            string Intermediary = JsonSerializer.Serialize(mov, Global.SchemaFormatter);

            File.WriteAllText(User.MovePath + $@"\{mov.FileName.ToLower()}.json", Intermediary);
        }
        public static void SaveSchema(ActorSchematic schema)
        {
            string Intermediary = JsonSerializer.Serialize(schema, Global.SchemaFormatter);

            File.WriteAllText(User.ActorPath + $@"\{schema.FileName.ToLower()}.json", Intermediary);
        }
    }
}
