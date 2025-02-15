﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BattleEngine.common.Global;
using static BattleEngine.main.Move;
using static BattleEngine.main.Schematics;
using static BattleEngine.main.TurnHandler;

namespace BattleEngine.main
{
    public class Enemy : Actor, IDisposable, IFileInfo
    {
        private double _maxhp;
        public override double MaxHealth
        {
            get { return _maxhp; }
            protected set
            {
                if (value <= 1)
                {
                    _maxhp = 1;
                }
                else if (value > (double)Limits.ENEMYHEALTHCAP)
                {
                    _maxhp = (double)Limits.ENEMYHEALTHCAP;
                }
                else
                {
                    _maxhp = value;
                }
            }
        }

        public EnemyAITypes AiType { get; set; }

        [JsonConstructor()]
        public Enemy(string internalname) : base(internalname)
        {
            InternalName = internalname.ToLower();
            ID = IdHandler.GetID(this);

            DisplayName = InternalName[0].ToString().ToUpper() + InternalName.Substring(1);

            AiType = EnemyAITypes.WILD;
            OnTurn += Think;

            MaxHealth = 500 * Attributes[0].Value;
            Health = MaxHealth;

            MoveSet = [new Move("move")];
        }
        public Enemy(string internalname, EnemyAITypes type) : base(internalname)
        {
            InternalName = $"enemy";
            ID = IdHandler.GetID(this);

            DisplayName = InternalName[0].ToString().ToUpper() + InternalName.Substring(1);

            AiType = type;
            OnTurn += Think;

            Level = 5;

            MaxHealth = 500 * Attributes[0].Value;
            Health = MaxHealth;

            MoveSet = [new Move("move")];
        }
        public Enemy(string internalname, string displayname, EnemyAITypes type, int lvl, double[] ratios, params Move[] moves) : base(internalname)
        {
            InternalName = internalname.ToLower();
            ID = IdHandler.GetID(this);

            DisplayName = displayname;

            AiType = type;
            OnTurn += Think;

            Level = lvl;

            Attributes = new List<StatAttribute>(ListOfAttributes);
            for (int i = 0; i < Attributes.Count; i++)
            {
                StatAttribute item = Attributes[i];
                item.Value = 5 * Level;
                Attributes[i] = item;
            }

            ComponentRatios = new Dictionary<string, double>();
            for (int i = 0; i < ListOfComponents.Count; i++)
            {
                ComponentRatios.Add(ListOfComponents[i], ratios[i]);
            }

            MaxHealth = 500 * Attributes[0].Value;
            Health = MaxHealth;

            MoveSet = new List<Move>(moves);
        }
        public Enemy(string path, bool isfile) : base(path)
        {
            string JsonString;
            EnemySchematic origin;

            if (isfile == true)
            {
                if (path.Contains(".json"))
                {
                    JsonString = File.ReadAllText(path);
                    origin = JsonSerializer.Deserialize<EnemySchematic>(JsonString, SchemaFormatter);
                }
                else
                {
                    JsonString = File.ReadAllText($@"{User.EnemyPath}\{path}.json");
                    origin = JsonSerializer.Deserialize<EnemySchematic>(JsonString, SchemaFormatter);
                }

                InternalName = origin.InternalName;
                ID = IdHandler.GetID(this);

                DisplayName = origin.DisplayName;

                AiType = origin.AiType;
                OnTurn += Think;

                Level = origin.Level;

                MaxHealth = origin.MaxHealth;
                Health = MaxHealth;

                Attributes = new List<StatAttribute>(origin.Attributes);
                ComponentRatios = new Dictionary<string, double>(origin.ComponentRatios);
                MoveSet = new List<Move>(origin.MoveSet);
            }
            else
            {
                throw new ArgumentNullException("isfile", "param was never confirmed");
            }
        }

        public static implicit operator Enemy(EnemySchematic schema)
        {
            Enemy enemy = new Enemy(schema.InternalName);

            enemy.DisplayName = schema.DisplayName;
            enemy.AiType = schema.AiType;
            enemy.Level = schema.Level;
            enemy.MaxHealth = schema.MaxHealth;
            enemy.Attributes = schema.Attributes;
            enemy.ComponentRatios = schema.ComponentRatios;
            enemy.MoveSet = schema.MoveSet;

            return enemy;
        }

        protected override void DeathCheck()
        {
            if (Health <= 0)
            {
                Alive = false;
            }
            else
            {
                return;
            }
        }

        public void Think(Enemy target, Actor[] party, Actor[] enemies)
        {
            if (target != this)
            {
                return;
            }

            if (AiType == EnemyAITypes.WILD)
            {
                WildBrain(target, party, enemies);
            }
        }

        private void WildBrain(Enemy target, Actor[] party, Actor[] enemies)
        {
            Random rand = new Random();

            int choice = rand.Next(0, MoveSet.Count);
            if (MoveSet[choice].Category == Categories.AOE)
            {
                foreach (Actor victim in party)
                {
                    Attack(choice, victim);
                }
            }
            else
            {
                int victim = rand.Next(0, party.Length);
                Attack(choice, party[victim]);
            }
        }

        public void Dispose()
        {
            InternalName = null;
            ID = null;
            DisplayName = null;

            MoveSet.Clear();
            Attributes.Clear();
            ComponentRatios.Clear();

            OnHurt -= Mitigate;
            OnTurn -= Think;
        }
    }
}
