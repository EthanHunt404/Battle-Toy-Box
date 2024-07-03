using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace BattleEngine.common
{
    public static class Global
    {
        public static JsonSerializerOptions SchemaFormatter = new JsonSerializerOptions();

        static Global()
        {
            SchemaFormatter.WriteIndented = true;
            SchemaFormatter.IncludeFields = true;
            SchemaFormatter.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
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
            EFFECT = -1,
            MELEE = 0,
            RANGED = 1,
            AOE = 2
        }
        public enum Components
        {
            NONE = -1,
            NEUTRAL = 0,
            PHYSICAL = 1,
            SPECIAL = 2
        }
    }
}
