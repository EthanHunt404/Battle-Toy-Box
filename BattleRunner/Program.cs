using BattleEngine.common;
using BattleEngine.main;
using System.Runtime;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BattleRunner
{
    public class Program
    {
        public static void Main()
        {
            Move[] movelist = new Move[2];

            movelist[0] = new Move("Mega Blast", "Mega Omega Blast!", "Very awesome POWERFUL dead ray!", 666, Categories.AOE, Components.MAGICAL);
            movelist[1] = new Move("Super Punch", "Super PLUS ULTRA Punch!", "The mostest powerfullest punch EVER!", 200, Categories.MELEE, Components.PHYSICAL);

            foreach (Move m in movelist)
            { 
                SaveJson(m);
            }

            Actor Shinigami = new Actor("Shinigami", "The God Of Death Shinigame", 999, movelist);
            SaveJson((ActorSchema)Shinigami);

            Actor Shinji = new Actor("Shinigami");
            Console.WriteLine(Shinji);
        }
    }
}
