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
            Actor a = new Actor(SchemaHandler.ActorList[0], true);

            Console.WriteLine(a.DisplayName);
        }
    }
}
