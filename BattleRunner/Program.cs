using BattleEngine.common;
using Spectre.Console;
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

            Move slash = new Move("katana_slash", "Katana Slash", "A Cool Slash", 50, Categories.MELEE, [0, 1.0, 0, 0, 1.0], ListOfComponents[1]);
            Actor deadpool = new Actor("deadpool", "Deadpool", 20, [1.0, 1.0, 1.0, 1.0], slash);

            Actor[] party = [deadpool, new Actor()];
            Enemy[] enemies = [new Enemy(), new Enemy()];

            TurnHandler test = new BattleTest(party, enemies);
        }
    }
}
