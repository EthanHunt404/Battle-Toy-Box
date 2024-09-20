using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleEngine.main.Schematics;
using static BattleEngine.common.Global;

namespace BattleEngine.common
{
    public static class FolderStructurer
    {
        public static void CreateStructure()
        {
            Directory.CreateDirectory(User.SchematicPath);
            Directory.CreateDirectory(User.MovePath);
            Directory.CreateDirectory(User.ActorPath);
            Directory.CreateDirectory(User.EnemyPath);

            CreateJsonReferences();
        }
        public static void CreateJsonReferences()
        {
            MoveSchematic movereference = new MoveSchematic();
            Schematiker.SaveSchematic(movereference);

            ActorSchematic actorreference = new ActorSchematic();
            Schematiker.SaveSchematic(actorreference);

            EnemySchematic enemyreference = new EnemySchematic();
            Schematiker.SaveSchematic(enemyreference);
        }
    }
}
