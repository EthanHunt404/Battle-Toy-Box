using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BattleEngine.main.Schematics;
using static BattleEngine.common.Global;

namespace BattleEngine.common
{
    public static class IdHandler
    {
        public static int MoveCurrentID { get; private set; }
        public static int ActorCurrentID { get; private set; }
        public static int EnemyCurrentID { get; private set; }

        private static List<string> MoveNamesLookUp;
        private static List<string> ActorNamesLookUp;
        private static List<string> EnemyNamesLookUp;

        static IdHandler()
        {
            MoveCurrentID = 0;
            ActorCurrentID = 0;
            EnemyCurrentID = 0;

            MoveNamesLookUp = new List<string>();
            ActorNamesLookUp = new List<string>();
            EnemyNamesLookUp = new List<string>();
        }

        public static void ResetIDs()
        {
            ActorCurrentID = 0;
            MoveCurrentID = 0;
            EnemyCurrentID = 0;

            MoveNamesLookUp.Clear();
            ActorNamesLookUp.Clear();
            EnemyNamesLookUp.Clear();
        }

        public static int GetID(Move asker)
        {
            if (MoveNamesLookUp.Contains(asker.FileName))
            {
                int current = MoveCurrentID;
                MoveCurrentID = current + 1;
                return current;
            }
            else
            {
                MoveNamesLookUp.Add(asker.FileName);
                MoveCurrentID = 0;
                return 0;
            }
        }
        public static int GetID(Actor asker)
        {
            if (ActorNamesLookUp.Contains(asker.FileName))
            {
                int current = ActorCurrentID;
                ActorCurrentID = current + 1;
                return current;
            }
            else
            {
                ActorNamesLookUp.Add(asker.FileName);
                ActorCurrentID = 0;
                return 0;
            }
            
        }
        public static int GetID(Enemy asker)
        {
            if (EnemyNamesLookUp.Contains(asker.FileName))
            {
                int current = EnemyCurrentID;
                EnemyCurrentID = current + 1;
                return current;
            }
            else
            {
                EnemyNamesLookUp.Add(asker.FileName);
                EnemyCurrentID = 0;
                return 0;
            }
        }
    }
}
