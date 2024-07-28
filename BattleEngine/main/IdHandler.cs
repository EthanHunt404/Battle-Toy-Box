using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BattleEngine.main.Schematics;

namespace BattleEngine.main
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

            InvalidMoves = new Dictionary<string, string>();
            InvalidActors = new Dictionary<string, string>();

            MoveTotalIDs = 0;
            ActorTotalIDs = 0;

            if (File.Exists(User.SchematicPath + @"/base.json"))
            {
                string JsonString = File.ReadAllText(User.SchematicPath + @"/base.json");
                IdFileSchematic basefile = JsonSerializer.Deserialize<IdFileSchematic>(JsonString, Global.SchemaFormatter);

                MoveTotalIDs = basefile.MoveTotalIDs;
                ActorTotalIDs = basefile.ActorTotalIDs;

                SortIDs();
            }
            else
            {
                ResetIDs();
            }
        }

        private static void SaveIDs()
        {
            IdFileSchematic idschema = new IdFileSchematic();
            idschema.MoveTotalIDs = MoveTotalIDs;
            idschema.ActorTotalIDs = ActorTotalIDs;

            string Intermediary = JsonSerializer.Serialize(idschema, Global.SchemaFormatter);

            File.WriteAllText(User.SchematicPath + $@"\base.json", Intermediary);
        }

        public static void SortIDs()
        {
            MoveIdList.Clear();
            ActorIdList.Clear();

            InvalidMoves.Clear();
            InvalidActors.Clear();

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
            ActorTotalIDs = 0;
            MoveTotalIDs = 0;
            SaveIDs();
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
}
