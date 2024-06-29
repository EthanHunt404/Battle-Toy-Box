using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class FolderStructurer
    {
        public static void CreateStructure(int level)
        {
            if (level == 0)
            {
                Directory.CreateDirectory(User.SchematicPath);

                Directory.CreateDirectory(User.MovePath);
                MoveSchematic movereference = new MoveSchematic();
                SchematicHandler.SaveSchema(movereference);

                Directory.CreateDirectory(User.ActorPath);
                ActorSchematic actorreference = new ActorSchematic();
                SchematicHandler.SaveSchema(actorreference);
            }
            else if (level == 1)
            {
                Directory.CreateDirectory(User.MovePath);
                MoveSchematic movereference = new MoveSchematic();
                SchematicHandler.SaveSchema(movereference);
            }
            else if (level == 2)
            {
                Directory.CreateDirectory(User.ActorPath);
                ActorSchematic actorreference = new ActorSchematic();
                SchematicHandler.SaveSchema(actorreference);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{level} number is not valid");
            }
        }
    }
}
