using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BattleEngine.main
{
    public static class IdHandler
    {
        public static int ActorTotalIDs { get; private set; }
        public static int MoveTotalIDs { get; private set; }

        public static List<int> ActorIdList { get; private set; }
        public static List<int> MoveIdList { get; private set; }


        static IdHandler()
        {
            ActorIdList = new List<int>();
            MoveIdList = new List<int>();

            ActorTotalIDs = 0;
            MoveTotalIDs = 0;

            if (File.Exists(User.SchemaPath + @"/base.json"))
            {
                string JsonString = File.ReadAllText(User.SchemaPath + @"/base.json");
                IdSchema basefile = JsonSerializer.Deserialize<IdSchema>(JsonString, Global.SchemaFormatter);

                ActorTotalIDs = basefile.ActorTotalIDs;
                MoveTotalIDs = basefile.MoveTotalIDs;

                foreach (string actor in SchemaHandler.ActorList)
                {
                    Actor currentactor = new Actor(actor, true);

                    if (ActorIdList.Contains(currentactor.ID))
                    {
                        SchemaHandler.InvalidActors.Add(actor);
                        SchemaHandler.ActorList.Remove(actor);
                    }
                    else
                    {
                        ActorIdList.Add(currentactor.ID);
                    }
                }
                foreach (string move in SchemaHandler.MoveList)
                {
                    Move currentmove = new Move(move, true);

                    if (MoveIdList.Contains(currentmove.ID))
                    {
                        SchemaHandler.InvalidMoves.Add(move);
                        SchemaHandler.ActorList.Remove(move);
                    }
                    else
                    {
                        MoveIdList.Add(currentmove.ID);
                    }
                }

                ActorTotalIDs = ActorIdList.Max();
                MoveTotalIDs = MoveIdList.Max();

                SaveIDs();
            }
            else
            {
                SaveIDs(true);
            }
        }

        private static void SaveIDs()
        {
            IdSchema idschema = new IdSchema();
            idschema.ActorTotalIDs = ActorTotalIDs;
            idschema.MoveTotalIDs = MoveTotalIDs;

            string Intermediary = JsonSerializer.Serialize(idschema, Global.SchemaFormatter);

            File.WriteAllText(User.SchemaPath + $@"\base.json", Intermediary);
        }
        private static void SaveIDs(bool init)
        {
            IdSchema idschema = new IdSchema();
            idschema.ActorTotalIDs = 0;
            idschema.MoveTotalIDs = 0;

            string Intermediary = JsonSerializer.Serialize(idschema, Global.SchemaFormatter);

            File.WriteAllText(User.SchemaPath + $@"\base.json", Intermediary);
        }

        public static int GetID(Actor asker)
        {
            int current = ActorTotalIDs;
            ActorTotalIDs += 1;

            SaveIDs();

            return current;

        }
        public static int GetID(Move asker)
        {
            int current = MoveTotalIDs;
            MoveTotalIDs += 1;

            SaveIDs();

            return current;
        }
    }
    public record IdSchema
    {
        public string Version;
        public int ActorTotalIDs;
        public int MoveTotalIDs;

        public IdSchema()
        {
            Version = "0.0.1";
            ActorTotalIDs = 0;
            MoveTotalIDs = 0;
        }
    }
}
