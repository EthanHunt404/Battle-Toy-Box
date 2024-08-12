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

            Move move1 = new Move("katana_slash", "Katana Slash", "A Cool Slash", 50, Categories.MELEE, [0, 1.0, 0, 0 ,1.0], ListOfComponents[1]);
            Actor actor1 = new Actor("deadpool", "Deadpool", 20, [1.0, 1.0, 1.0, 1.0], move1);

            Actor actor2 = new Actor();

            Console.WriteLine(actor2.Health);

            actor1.Attack(0, actor2);

            Console.WriteLine(actor2.Health);

            SchematicHandler.SaveSchematic(actor1);
        }
    }
}
