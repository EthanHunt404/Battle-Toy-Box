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
            MoveSchematic movereference = new MoveSchematic();
            SchematicHandler.SaveSchematic(movereference);

            Directory.CreateDirectory(User.ActorPath);
            ActorSchematic actorreference = new ActorSchematic();
            SchematicHandler.SaveSchematic(actorreference);

            Directory.CreateDirectory(User.EnemyPath);
            EnemySchematic enemyreference = new EnemySchematic();
            SchematicHandler.SaveSchematic(enemyreference);
        }
    }
}
