using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BattleEngine.common.Global;
using static BattleEngine.main.Move;
using static BattleEngine.main.Schematics;
using static BattleEngine.main.TurnHandler;

namespace BattleEngine.main
{
    public class Enemy : Actor
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

        public Enemy()
        {
            ID = IdHandler.GetID(this);

            FileName = $"enemy{ID}";
            DisplayName = $"Enemy {ID}";

            AiType = EnemyAITypes.WILD;
            OnTurn += Think;

            Level = 5;

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
                ComponentRatios.Add(ListOfComponents[i], 1.0);
            }

            MaxHealth = 500 * Attributes[0].Value;
            Health = MaxHealth;

            MoveSet = [new Move()];

            MitigationValue = 0;
            IsHurt += Mitigate;
        }
        public Enemy(EnemyAITypes type)
        {
            ID = IdHandler.GetID(this);

            FileName = $"enemy{ID}";
            DisplayName = $"Enemy {ID}";

            AiType = type;
            OnTurn += Think;

            Level = 5;

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
                ComponentRatios.Add(ListOfComponents[i], 1.0);
            }

            MaxHealth = 500 * Attributes[0].Value;
            Health = MaxHealth;

            MoveSet = [new Move()];

            MitigationValue = 0;
            IsHurt += Mitigate;
        }
        public Enemy(string filename, string displayname, EnemyAITypes type, int lvl, double[] ratios, params Move[] moves)
        {
            ID = IdHandler.GetID(this);

            FileName = filename;
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

            MitigationValue = 0;
            IsHurt += Mitigate;
        }
        public Enemy(string name, bool isfile)
        {
            string JsonString;
            EnemySchematic origin;

            if (isfile == true)
            {
                if (name.Contains(".json"))
                {
                    JsonString = File.ReadAllText(name);
                    origin = JsonSerializer.Deserialize<EnemySchematic>(JsonString, SchemaFormatter);
                }
                else
                {
                    JsonString = File.ReadAllText($@"{User.EnemyPath}\{name}.json");
                    origin = JsonSerializer.Deserialize<EnemySchematic>(JsonString, SchemaFormatter);
                }

                ID = origin.ID;

                FileName = origin.FileName;
                DisplayName = origin.DisplayName;

                AiType = origin.AiType;
                OnTurn += Think;

                Level = origin.Level;

                MaxHealth = origin.MaxHealth;
                Health = MaxHealth;

                MitigationValue = 0;
                IsHurt += Mitigate;

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
            Enemy enemy = new Enemy();

            enemy.ID = schema.ID;
            enemy.FileName = schema.FileName;
            enemy.DisplayName = schema.DisplayName;
            enemy.AiType = schema.AiType;
            enemy.Level = schema.Level;
            enemy.MaxHealth = schema.MaxHealth;
            enemy.Attributes = schema.Attributes;
            enemy.ComponentRatios = schema.ComponentRatios;
            enemy.MoveSet = schema.MoveSet;

            return enemy;
        }

        public void Think(Enemy target, Actor[] party, Enemy[] enemies)
        {
            if (target != this)
            {
                return;
            }

            Random rand = new Random();

            if (AiType == EnemyAITypes.WILD)
            {
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
        }
    }
}
