﻿using System;
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
            MoveSchematic movereference = new MoveSchematic();
            SchematicHandler.SaveSchema(movereference);

            Directory.CreateDirectory(User.ActorPath);
            ActorSchematic actorreference = new ActorSchematic();
            SchematicHandler.SaveSchema(actorreference);
        }
    }
}
