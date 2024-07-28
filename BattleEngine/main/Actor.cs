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
        public int ID { get; private set; }
        public string FileName { get; private set; }
        public string DisplayName { get; set; }

        private double _maxhp;
        public double MaxHealth
        {
            get { return _maxhp; }
            private set
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
            private set
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
            private set
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
            private set
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

        public List<StatAttribute> Attributes { get; private set; }
        public Dictionary<string, double> ComponentRatios { get; private set; }
        public List<Move> MoveSet { get; set; }

        public Actor()
        {
            ID = IdHandler.GetID(this);

            FileName = $"Actor {ID}";
            DisplayName = "Placeholder";

            Level = 5;

            MaxHealth = 500 * Level;
            Health = MaxHealth;

            MitigationValue = 0;
            IsHurt += Mitigate;

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

            MoveSet = [new Move()];
        }
        public Actor(string filename, string displayname, int lvl, params Move[] moves)
        {
            ID = IdHandler.GetID(this);

            FileName = filename.ToLower();
            DisplayName = displayname;

            Level = lvl;

            MaxHealth = 500 * Level;
            Health = MaxHealth;

            MitigationValue = 0;
            IsHurt += Mitigate;

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

            MoveSet = new List<Move>(moves);
        }
        public Actor(string filename, string displayname, int lvl, double[] ratios, params Move[] moves)
        {
            ID = IdHandler.GetID(this);

            FileName = filename;
            DisplayName = displayname;

            Level = lvl;

            MaxHealth = 500 * Level;
            Health = MaxHealth;

            MitigationValue = 0;
            IsHurt += Mitigate;

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

            MoveSet = new List<Move>(moves);
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

        public virtual void Mitigate(Move move, double result, Actor itself)
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

        public virtual void Attack(int move, Actor target)
        {
            MoveSet[move].Trigger(target, this);
        }
    }
}
