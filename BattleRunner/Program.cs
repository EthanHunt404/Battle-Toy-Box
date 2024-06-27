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
            FolderStructurer.CreateStructure();
            Move[] movelist = [new Move("reference", true)];

            Actor a = new Actor();
            Actor b = new Actor("Kinigame", "The Ninja Kinigami", 100, movelist);

            SchematicHandler.SaveSchema((ActorSchematic)a);
            SchematicHandler.SaveSchema((ActorSchematic)b);
        }
    }
}
