using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleEngine.common.Global;

namespace BattleEngine.main
{
    public static class Schematics
    {
        public interface IFileInfo
        {
            string InternalName { get; protected set; }
        }

        public static string JsonVersion = "1.0.0";

        //Move Schematic
        public record MoveSchematic: IFileInfo
        {
            public string Version;
            public string InternalName { get; set; }

            public string DisplayName;
            public string Description;

            public int Power;
            public Categories Category;
            public List<string> Components;
            public Dictionary<string, double> AttributeRatioes;

            public MoveSchematic()
            {
                Version = JsonVersion;

                InternalName = "reference";
                DisplayName = "Move Schematic";
                Description = "Empty";

                Power = -1;
                Category = Categories.EFFECT;
                Components = new List<string>();
                AttributeRatioes = new Dictionary<string, double>();
            }
            public static implicit operator MoveSchematic(Move move)
            {
                MoveSchematic schema = new MoveSchematic();

                schema.InternalName = move.InternalName;
                schema.DisplayName = move.DisplayName;
                schema.Description = move.Description;
                schema.Power = move.Power;
                schema.Category = move.Category;
                schema.Components = move.Components;
                schema.AttributeRatioes = move.AttributeRatioes;

                return schema;
            }
        }

        //Actor Schematic
        public record ActorSchematic: IFileInfo
        {
            public string Version;

            public string InternalName { get; set; }
            public string DisplayName;

            public double MaxHealth;

            public int Level;

            public List<StatAttribute> Attributes;
            public Dictionary<string, double> ComponentRatios;
            public List<Move> MoveSet;

            public ActorSchematic()
            {
                Version = JsonVersion;

                InternalName = $"reference";
                DisplayName = "Actor Schematic";

                MaxHealth = -1;

                Level = -1;

                Attributes = new List<StatAttribute>();
                ComponentRatios = new Dictionary<string, double>();
                MoveSet = new List<Move>();
            }

            public static implicit operator ActorSchematic(Actor actor)
            {
                ActorSchematic schema = new ActorSchematic();

                schema.InternalName = actor.InternalName;
                schema.DisplayName = actor.DisplayName;
                schema.Level = actor.Level;
                schema.MaxHealth = actor.MaxHealth;
                schema.Attributes = actor.Attributes;
                schema.ComponentRatios = actor.ComponentRatios;

                foreach (Move move in actor.MoveSet)
                {
                    schema.MoveSet.Add(move);
                }

                return schema;
            }
        }

        //Enemy Schematic
        public record EnemySchematic: IFileInfo
        {
            public string Version;

            public string InternalName { get; set; }
            public string DisplayName;

            public EnemyAITypes AiType;

            public double MaxHealth;

            public int Level;

            public List<StatAttribute> Attributes;
            public Dictionary<string, double> ComponentRatios;
            public List<Move> MoveSet;

            public EnemySchematic()
            {
                Version = JsonVersion;

                InternalName = $"reference";
                DisplayName = "Enemy Schematic";

                MaxHealth = -1;

                AiType = EnemyAITypes.WILD;

                Level = -1;

                Attributes = new List<StatAttribute>();
                ComponentRatios = new Dictionary<string, double>();
                MoveSet = new List<Move>();
            }

            public static implicit operator EnemySchematic(Enemy enemy)
            {
                EnemySchematic schema = new EnemySchematic();

                schema.InternalName = enemy.InternalName;
                schema.DisplayName = enemy.DisplayName;
                schema.AiType = enemy.AiType;
                schema.Level = enemy.Level;
                schema.MaxHealth = enemy.MaxHealth;
                schema.Attributes = enemy.Attributes;
                schema.ComponentRatios = enemy.ComponentRatios;

                foreach (Move move in enemy.MoveSet)
                {
                    schema.MoveSet.Add(move);
                }

                return schema;
            }
        }
    }
}
