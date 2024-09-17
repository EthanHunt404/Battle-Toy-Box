using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleEngine.main.Schematics;

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
            SchematicHandler.SaveSchematic(movereference);

            ActorSchematic actorreference = new ActorSchematic();
            SchematicHandler.SaveSchematic(actorreference);

            EnemySchematic enemyreference = new EnemySchematic();
            SchematicHandler.SaveSchematic(enemyreference);
        }
    }
}
