using BattleEngine.common;
using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using static BattleEngine.common.Global;

namespace BattleRunner
{
    public class Program
    {
        public static void Main()
        {
            FolderStructurer.CreateStructure();

            double[] moveratios = [0, 1.0, 0, 0, 0];
            double[] actorratios = [1.0, 1.0, 1.0, 1.0];

            Move move = new Move("testiculartorsion", "Torçao Testicular", "Ouch", 120, Categories.RANGED, moveratios, ListOfComponents[1]);
            
            Actor actor = new Actor("sansadventuretime", "Sans Adventure Time", 10, [1.0, 1.0, 1.0, 1.0], move);
            
            Actor victim = new Actor("garpend", "Garpend The Spaghetti Devourer", 100, actorratios, new Move());

            Console.WriteLine(victim.Health);

            actor.Attack(0, victim);

            Console.WriteLine(victim.Health);

            SchematicHandler.SaveSchema(actor);
            SchematicHandler.SaveSchema(move);

            IdHandler.ResetIDs();
        }
    }
}
