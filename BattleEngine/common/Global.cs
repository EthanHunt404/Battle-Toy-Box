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

        public static Dictionary<string, int> DefaultAttributes { get; set; }

        static Global()
        {            
            SchemaFormatter.WriteIndented = true;
            SchemaFormatter.IncludeFields = true;
            SchemaFormatter.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            DefaultComponents = ["NONE", "NEUTRAL", "PHYSICAL", "SPECIAL"];

            DefaultAttributes = new Dictionary<string, int>
            {
                ["ATK"] = 5 * 5,
                ["VIT"] = 5 * 5,
                ["INT"] = 5 * 5,
                ["DEF"] = 5 * 5,
                ["DGE"] = 5 * 5
            };
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
