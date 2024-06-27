using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEngine.common
{
    public static class FolderStructurer
    {
        static FolderStructurer()
        {
            CreateStructure();
        }

        public static void CreateStructure()
        {
            if (!Directory.Exists(User.SchematicPath))
            {
                Directory.CreateDirectory(User.SchematicPath);
            }
            if (!Directory.Exists(User.ActorPath))
            {
                Directory.CreateDirectory(User.ActorPath);
                ActorSchematic actorreference = new ActorSchematic();
                SchematicHandler.SaveSchema(actorreference);
            }
            if(!Directory.Exists(User.MovePath))
            {
                Directory.CreateDirectory(User.MovePath);
                MoveSchematic movereference = new MoveSchematic();
                SchematicHandler.SaveSchema(movereference);
            }
        }
    }
}
