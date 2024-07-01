﻿using BattleEngine.common;
using System;
using System.Runtime;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BattleRunner
{
    public class Program
    {
        public static void Main()
        {
            FolderStructurer.CreateStructure(0);

            Actor a = new Actor(SchematicHandler.ActorList["kinigami"], true);
            Console.WriteLine(a.DisplayName);
        }
    }
}
