using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using static BattleEngine.main.Schematics;

namespace BattleEngine.common
{
    public class SchematicHandler
    {
        public List<string> MoveFileList { get; private set; }
        public Dictionary<string, string> MoveList { get; private set; }

        public List<string> ActorFileList { get; private set; }
        public Dictionary<string, string> ActorList { get; private set; }

        public List<string> EnemyFileList { get; private set; }
        public Dictionary<string, string> EnemyList { get; private set; }

        public SchematicHandler()
        {
            MoveList = new Dictionary<string, string>();
            MoveFileList = new List<string>();

            ActorFileList = new List<string>();
            ActorList = new Dictionary<string, string>();

            EnemyFileList = new List<string>();
            EnemyList = new Dictionary<string, string>();
        }

        public void RefreshSchematics()
        {
            MoveFileList.Clear();
            MoveList.Clear();

            ActorFileList.Clear();
            ActorList.Clear();

            EnemyFileList.Clear();
            EnemyList.Clear();

            FolderStructurer.CreateStructure();

            MoveFileList.AddRange(Directory.EnumerateFiles(User.MovePath));
            ActorFileList.AddRange(Directory.EnumerateFiles(User.ActorPath));
            EnemyFileList.AddRange(Directory.EnumerateFiles(User.EnemyPath));

            foreach (string movepath in MoveFileList)
            {
                Move currentmove = new Move(movepath, true);

                MoveList.Add(currentmove.InternalName, movepath);
            }
            foreach (string actorpath in ActorFileList)
            {
                Actor currentactor = new Actor(actorpath, true);

                ActorList.Add(currentactor.InternalName, actorpath);
            }
            foreach (string enemypath in EnemyFileList)
            {
                Enemy currentenemy = new Enemy(enemypath, true);

                EnemyList.Add(currentenemy.InternalName, enemypath);
            }
        }
        public void SaveSchematic<T>(T asker) where T: IFileInfo
        {
            string Intermediary;
            switch (asker)
            {
                case Enemy:
                    Intermediary = JsonSerializer.Serialize(asker, Global.SchemaFormatter);
                    File.WriteAllText(User.EnemyPath + $@"\{asker.InternalName}.json", Intermediary);
                    break;
                case Actor:
                    Intermediary = JsonSerializer.Serialize(asker, Global.SchemaFormatter);
                    File.WriteAllText(User.ActorPath + $@"\{asker.InternalName}.json", Intermediary);
                    break;
                case Move:
                    Intermediary = JsonSerializer.Serialize(asker, Global.SchemaFormatter);
                    File.WriteAllText(User.MovePath + $@"\{asker.InternalName}.json", Intermediary);
                    break;
            }
        }
    }
}
