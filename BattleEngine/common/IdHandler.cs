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
        public static int MoveTotalIDs { get; private set; }
        public static int ActorTotalIDs { get; private set; }

        public static List<int> MoveIdList { get; private set; }
        public static List<int> ActorIdList { get; private set; }


        static IdHandler()
        {
            ActorIdList = new List<int>();
            MoveIdList = new List<int>();

            ActorTotalIDs = 0;
            MoveTotalIDs = 0;

            if (File.Exists(User.SchematicPath + @"/base.json"))
            {
                string JsonString = File.ReadAllText(User.SchematicPath + @"/base.json");
                IdSchematic basefile = JsonSerializer.Deserialize<IdSchematic>(JsonString, Global.SchemaFormatter);

                ActorTotalIDs = basefile.ActorTotalIDs;
                MoveTotalIDs = basefile.MoveTotalIDs;

                foreach (string move in SchematicHandler.MoveList)
                {
                    Move currentmove = new Move(move, true);

                    if (MoveIdList.Contains(currentmove.ID))
                    {
                        SchematicHandler.InvalidMoves.Add(move);
                        SchematicHandler.ActorList.Remove(move);
                    }
                    else
                    {
                        MoveIdList.Add(currentmove.ID);
                    }
                }
                foreach (string actor in SchematicHandler.ActorList)
                {
                    Actor currentactor = new Actor(actor, true);

                    if (ActorIdList.Contains(currentactor.ID))
                    {
                        SchematicHandler.InvalidActors.Add(actor);
                        SchematicHandler.ActorList.Remove(actor);
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

        private static void ResetIDs()
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

    public record IdSchematic
    {
        public string Version;
        public int MoveTotalIDs;
        public int ActorTotalIDs;
        public string Message;

        public IdSchematic()
        {
            Version = "0.0.1";
            MoveTotalIDs = 0;
            ActorTotalIDs = 0;
            Message = "Do not delete this, it may cause problems in the indexing of actors and moves alike";
        }
    }
}
