using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Schema;
using static BattleEngine.common.Global;
using static BattleEngine.main.Move;
using static BattleEngine.main.Schematics;

namespace BattleEngine.main
{
    public record Actor
    {
        public int ID { get; protected set; }
        public string FileName { get; protected set; }
        public string DisplayName { get; set; }

        private double _maxhp;
        public virtual double MaxHealth
        {
            get { return _maxhp; }
            protected set
            {
                if (value <= 1)
                {
                    _maxhp = 1;
                }
                else if (value > (double)Limits.HEALTHCAP)
                {
                    _maxhp = (double)Limits.HEALTHCAP;
                }
                else
                {
                    _maxhp = value;
                }
            }
        }

        private double _health;
        public double Health
        {
            get { return _health; }
            protected set
            {
                if (value < 0)
                {
                    _health = 0;
                }
                else
                {
                    _health = value;
                }
            }
        }

        private double _mitigationvalue;
        public double MitigationValue
        {
            get { return _mitigationvalue; }
            protected set
            {
                if (_mitigationvalue < 0)
                {
                    _mitigationvalue = 0;
                }
                else if (_mitigationvalue > MaxHealth)
                {
                    _mitigationvalue = MaxHealth;
                }
                else
                {
                    _mitigationvalue = value;
                }
            }
        }

        private int _level;
        public int Level {
            get { return _level; }
            protected set
            {
                if (value <= 1)
                {
                    _level = 1;
                }
                else if (value > (int)Limits.LEVELCAP)
                {
                    _level = (int)Limits.LEVELCAP;
                }
                else
                {
                    _level = value;
                }
            }
        }

        public List<StatAttribute> Attributes { get; set; }
        public Dictionary<string, double> ComponentRatios { get; protected set; }
        public List<Move> MoveSet { get; set; }

        public Actor()
        {
            ID = IdHandler.GetID(this);

            FileName = $"Actor {ID}";
            DisplayName = "Placeholder";

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
        public Actor(string filename, string displayname, int lvl, double[] ratios, params Move[] moves)
        {
            ID = IdHandler.GetID(this);

            FileName = filename;
            DisplayName = displayname;

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
        public Actor(string name, bool isfile)
        {
            string JsonString;
            ActorSchematic origin;

            if (isfile == true)
            {
                if (name.Contains(".json"))
                {
                    JsonString = File.ReadAllText(name);
                    origin = JsonSerializer.Deserialize<ActorSchematic>(JsonString, SchemaFormatter);
                }
                else
                {
                    JsonString = File.ReadAllText($@"{User.ActorPath}\{name}.json");
                    origin = JsonSerializer.Deserialize<ActorSchematic>(JsonString, SchemaFormatter);
                }

                ID = origin.ID;

                FileName = origin.FileName;
                DisplayName = origin.DisplayName;

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

        public static implicit operator Actor(ActorSchematic schema)
        {
            Actor actor = new Actor();

            actor.ID = schema.ID;
            actor.FileName = schema.FileName;
            actor.DisplayName = schema.DisplayName;
            actor.Level = schema.Level;
            actor.MaxHealth = schema.MaxHealth;
            actor.Attributes = schema.Attributes;
            actor.ComponentRatios = schema.ComponentRatios;
            actor.MoveSet = schema.MoveSet;

            return actor;
        }

        public virtual void LevelUp()
        {
            Level += 1;
            MaxHealth = Level * (Attributes[0].Value * 1.5);
            Health = MaxHealth;
        }

        public void Mitigate(Move move, double result, Actor itself)
        {
            if (itself == this)
            {
                int TotalComponents = move.Components.Count;

                foreach (string component in move.Components)
                {
                    double fraction = result / TotalComponents;
                    MitigationValue += fraction * ComponentRatios[component];
                }

                Health -= MitigationValue;
            }
        }

        public void Attack(int move, Actor target)
        {
            MoveSet[move].Trigger(target, this);
        }
    }
}
