using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BattleEngine.common
{
    public static class IdHandler
    {
        public static List<int> MoveIdList { get; private set; }
        public static List<int> ActorIdList { get; private set; }

        public static Dictionary<string, string> InvalidMoves { get; private set; }
        public static Dictionary<string, string> InvalidActors { get; private set; }

        public static int MoveTotalIDs { get; private set; }
        public static int ActorTotalIDs { get; private set; }

        static IdHandler()
        {
            MoveIdList = new List<int>();
            ActorIdList = new List<int>();

            InvalidActors = new Dictionary<string, string>();
            InvalidMoves = new Dictionary<string, string>();

            MoveTotalIDs = 0;
            ActorTotalIDs = 0;

            if (File.Exists(User.SchematicPath + @"/base.json"))
            {
                string JsonString = File.ReadAllText(User.SchematicPath + @"/base.json");
                IdSchematic basefile = JsonSerializer.Deserialize<IdSchematic>(JsonString, Global.SchemaFormatter);

                MoveTotalIDs = basefile.MoveTotalIDs;
                ActorTotalIDs = basefile.ActorTotalIDs;
            }
            else
            {
                ResetIDs();
            }
        }

        private static void SaveIDs()
        {
            IdSchematic idschema = new IdSchematic();
            idschema.MoveTotalIDs = MoveTotalIDs;
            idschema.ActorTotalIDs = ActorTotalIDs;

            string Intermediary = JsonSerializer.Serialize(idschema, Global.SchemaFormatter);

            File.WriteAllText(User.SchematicPath + $@"\base.json", Intermediary);
        }

        public static void SortIDs()
        {
            foreach (string movepath in SchematicHandler.MoveFileList)
            {
                Move currentmove = new Move(movepath, true);

                if (MoveIdList.Contains(currentmove.ID))
                {
                    InvalidMoves.Add(currentmove.FileName, movepath);
                    SchematicHandler.ActorList.Remove(currentmove.FileName);
                }
                else
                {
                    MoveIdList.Add(currentmove.ID);
                }
            }
            foreach (string actorpath in SchematicHandler.ActorFileList)
            {
                Actor currentactor = new Actor(actorpath, true);

                if (ActorIdList.Contains(currentactor.ID))
                {
                    InvalidActors.Add(currentactor.FileName, actorpath);
                    SchematicHandler.ActorList.Remove(currentactor.FileName);
                }
                else
                {
                    ActorIdList.Add(currentactor.ID);
                }
            }

            MoveTotalIDs = MoveIdList.Max();
            ActorTotalIDs = ActorIdList.Max();

            SaveIDs();
        }

        public static void ResetIDs()
        {
            IdSchematic idschema = new IdSchematic();
            idschema.ActorTotalIDs = 0;
            idschema.MoveTotalIDs = 0;

            string Intermediary = JsonSerializer.Serialize(idschema, Global.SchemaFormatter);

            File.WriteAllText(User.SchematicPath + $@"\base.json", Intermediary);
        }

        public static int GetID(Move asker)
        {
            if (MoveIdList != null)
            {
                int current = MoveTotalIDs;

                while (MoveIdList.Contains(current))
                {
                    current++;
                }

                MoveIdList.Add(current);
                MoveTotalIDs = current + 1;

                SaveIDs();

                return current;
            }
            else
            {
                int current = MoveTotalIDs;

                MoveIdList.Add(current);
                MoveTotalIDs = current + 1;

                SaveIDs();

                return current;
            }
        }
        public static int GetID(Actor asker)
        {
            if (ActorIdList != null)
            {
                int current = ActorTotalIDs;

                while (ActorIdList.Contains(current))
                {
                    current++;
                }

                ActorIdList.Add(current);
                ActorTotalIDs = current + 1;

                SaveIDs();

                return current;
            }
            else
            {
                int current = ActorTotalIDs;

                ActorIdList.Add(current);
                ActorTotalIDs = current + 1;

                SaveIDs();

                return current;
            }
        }
    }

    public record struct IdSchematic
    {
        public string Version;
        public int MoveTotalIDs;
        public int ActorTotalIDs;
        public string Message;

        public IdSchematic()
        {
            Version = Global.Version;
            MoveTotalIDs = 0;
            ActorTotalIDs = 0;
            Message = "Do not delete this, it may cause problems in the indexing of actors and moves alike";
        }
    }
}
