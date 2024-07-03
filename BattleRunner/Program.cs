using BattleEngine.common;
using System;
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
            FolderStructurer.CreateStructure(0);

            Move[] moveset = [new Move("meatballsburp", "Meat Balls Burp", "I Shouldn't have put those onions in", 120, Global.Categories.AOE, Global.Components.SPECIAL)];

            Actor actor = new Actor("garpend", "Garpend The Spaghetti Devourer", 2, moveset);

            SchematicHandler.SaveSchema((ActorSchematic)actor);
        }
    }
}
