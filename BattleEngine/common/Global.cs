using System.Text.Json;

namespace BattleEngine.common
{
    public static class Global
    {
        public static JsonSerializerOptions JsonFormatter = new JsonSerializerOptions();

        static Global()
        {
            JsonFormatter.WriteIndented = true;
            JsonFormatter.IncludeFields = true;
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
            MAGICAL = 2
        }
    }
}
