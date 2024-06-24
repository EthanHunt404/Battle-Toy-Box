using BattleEngine.main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class JsonHandler
    {
        public static void SaveJson(ActorSchema schema)
        {
            string Intermediary = JsonSerializer.Serialize(schema, Global.JsonFormatter);

            File.WriteAllText(User.ActorPath + $@"\{schema.InternalName.ToLower()}.json", Intermediary);
        }
        public static void SaveJson(Move mov)
        {
            string Intermediary = JsonSerializer.Serialize(mov, Global.JsonFormatter);

            File.WriteAllText(User.MovePath + $@"\{mov.InternalName.ToLower()}.json", Intermediary);
        }
    }
}
