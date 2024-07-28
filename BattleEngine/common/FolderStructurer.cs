using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class FolderStructurer
    {
        public static void CreateStructure()
        {
            Directory.CreateDirectory(User.SchematicPath);

            Directory.CreateDirectory(User.MovePath);
            Schematics.MoveSchematic movereference = new Schematics.MoveSchematic();
            SchematicHandler.SaveSchema(movereference);

            Directory.CreateDirectory(User.ActorPath);
            Schematics.ActorSchematic actorreference = new Schematics.ActorSchematic();
            SchematicHandler.SaveSchema(actorreference);
        }
    }
}
