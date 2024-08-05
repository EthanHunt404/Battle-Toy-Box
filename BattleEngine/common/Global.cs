using System.Reflection.Emit;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace BattleEngine.common
{
    public static class Global
    {
        public static JsonSerializerOptions SchemaFormatter = new JsonSerializerOptions();

        public static List<string> ListOfComponents { get; set; }

        public static List<StatAttribute> ListOfAttributes { get; set; }

        public static string Version { get; set; }

        static Global()
        {
            Version = "0.0.1";

            SchemaFormatter.WriteIndented = true;
            SchemaFormatter.IncludeFields = true;
            SchemaFormatter.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            ListOfComponents = ["NEUTRAL", "PHYSICAL", "SPECIAL", "MIXED"];

            ListOfAttributes =
            [
                new StatAttribute("Vitality", 0),
                new StatAttribute("Strenght", 0),
                new StatAttribute("Defense", 0),
                new StatAttribute("Inteligence", 0),
                new StatAttribute("Dexterity", 0)
            ];
        }

        public enum Limits
        {
            HEALTHCAP = 50000,
            ENEMYHEALTHCAP = 200000,
            STATCAP = 100,
            ENEMYSTATCAP = 200,
            POWERCAP = 200,
            LEVELCAP = 100
        }
        public enum Categories
        {
            EFFECT = 0,
            MELEE = 1,
            RANGED = 2,
            AOE = 3
        }
        public enum EnemyAITypes
        {
            WILD = 0,
            AGRESSIVE = 1,
            SURVIVALIST = 2,
            TATICAL = 3
        }
    }
}
