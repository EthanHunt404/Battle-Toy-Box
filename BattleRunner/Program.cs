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
            IdHandler.ResetIDs();

            Move moveset = new Move("meatballsburp", "Meat Balls Burp", "I Shouldn't have put those onions in", 120, Categories.AOE, DefaultComponents[3]);
            Move moveset2 = new Move("testiculartorsion", "Torçao Testicular", "Ouch", 200, Categories.RANGED, DefaultComponents[2]);

            Actor actor = new Actor("garpend", "Garpend The Spaghetti Devourer", 100, moveset);
            Actor actor2 = new Actor("sansadventuretime", "Sans Adventure Time", 100, moveset2);

            SchematicHandler.SaveSchema(actor);
            SchematicHandler.SaveSchema(moveset);

            SchematicHandler.SaveSchema(actor2);
            SchematicHandler.SaveSchema(moveset2);
        }
    }
}
