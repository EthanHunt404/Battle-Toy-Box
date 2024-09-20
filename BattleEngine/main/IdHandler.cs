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

        static IdHandler()
        {
            MoveCurrentID = 0;
            ActorCurrentID = 0;
            EnemyCurrentID = 0;
        }

        public static void ResetIDs()
        {
            ActorCurrentID = 0;
            MoveCurrentID = 0;
            EnemyCurrentID = 0;
        }

        public static int GetID(Move asker)
        {
            if (Schematiker.ActorList.ContainsKey(asker.FileName))
            {
                int current = MoveCurrentID;
                MoveCurrentID = current + 1;
                return current;
            }
            else
            {
                MoveCurrentID = 0;
                return 0;
            }
        }
        public static int GetID(Actor asker)
        {
            if (Schematiker.ActorList.ContainsKey(asker.FileName))
            {
                int current = ActorCurrentID;
                ActorCurrentID = current + 1;
                return current;
            }
            else
            {
                ActorCurrentID = 0;
                return 0;
            }
            
        }
        public static int GetID(Enemy asker)
        {
            if (Schematiker.EnemyList.ContainsKey(asker.FileName))
            {
                int current = EnemyCurrentID;
                EnemyCurrentID = current + 1;
                return current;
            }
            else
            {
                EnemyCurrentID = 0;
                return 0;
            }
        }
    }
}
