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

        public static List<string> DefaultComponents { get; set; }

        public static List<StatAttribute> DefaultAttributes { get; set; }

        public static string Version { get; set; }

        static Global()
        {
            Version = "0.0.1";

            SchemaFormatter.WriteIndented = true;
            SchemaFormatter.IncludeFields = true;
            SchemaFormatter.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            DefaultComponents = ["NONE", "NEUTRAL", "PHYSICAL", "SPECIAL"];

            DefaultAttributes =
            [
                new StatAttribute("Vitality", "VIT", 0),
                new StatAttribute("Strenght", "STR", 0),
                new StatAttribute("Defense", "DEF", 0),
                new StatAttribute("Inteligence", "INT", 0),
                new StatAttribute("Dexterity", "DEX", 0)
            ];
        }

        public enum Values
        {
            HEALTHCAP = 75000,
            POWERCAP = 200,
            LEVELCAP = 100,
            STATCAP = 500
        }
        public enum Categories
        {
            EFFECT = 0,
            MELEE = 1,
            RANGED = 2,
            AOE = 3
        }
    }
}
