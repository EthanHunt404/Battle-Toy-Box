using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Schema;
using static BattleEngine.common.Global;
using static BattleEngine.main.Move;
using static BattleEngine.main.Schematics;

namespace BattleEngine.main
{
    public class Actor
    {
        //Adressers
        public string ?FileName { get; protected set; }
        public int ?ID { get; protected set; }
        public string ?DisplayName { get; set; }

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

        public bool Alive { get; protected set; }

        public List<StatAttribute> Attributes { get; set; }
        public Dictionary<string, double> ComponentRatios { get; protected set; }
        public List<Move> MoveSet { get; set; }

        [JsonConstructor()]
        public Actor(string filename)
        {
            FileName = filename;
            ID = IdHandler.GetID(this);

            DisplayName = $"{FileName[0].ToString().ToUpper()}{FileName.Substring(1)} {ID + 1}";

            Level = 5;
            
            Alive = true;

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

            MaxHealth = 100 * Attributes[0].Value;
            Health = MaxHealth;

            MoveSet = new List<Move>(ListOfTestingMoves);

            OnHurt += Mitigate;
        }
        public Actor(string filename, string displayname, int lvl, double[] ratios, params Move[] moves)
        {
            FileName = filename.ToLower();
            ID = IdHandler.GetID(this);

            DisplayName = $"{displayname} {ID + 1}";

            Level = lvl;

            Alive = true;

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

            MaxHealth = 100 * Attributes[0].Value;
            Health = MaxHealth;

            MoveSet = new List<Move>(moves);

            OnHurt += Mitigate;
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

                FileName = origin.FileName;
                ID = IdHandler.GetID(this);

                DisplayName = $"{origin.DisplayName} {ID + 1}";

                Level = origin.Level;

                Alive = true;

                MaxHealth = origin.MaxHealth;
                Health = MaxHealth;

                OnHurt += Mitigate;

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
            Actor actor = new Actor(schema.FileName);

            actor.DisplayName = schema.DisplayName;
            actor.Level = schema.Level;
            actor.MaxHealth = schema.MaxHealth;
            actor.Attributes = schema.Attributes;
            actor.ComponentRatios = schema.ComponentRatios;
            actor.MoveSet = schema.MoveSet;

            return actor;
        }

        protected virtual void DeathCheck()
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

        public void Restore()
        {
            Health = MaxHealth;
            Alive = true;
        }

        public virtual void LevelUp()
        {
            throw new NotImplementedException();
            //Level += 1;
            //MaxHealth = Level * (Attributes[0].Value * 1.5);
            //Restore();
        }

        public void Mitigate(Move move, double result, params Actor[] targets)
        {
            if (!Alive) return;

            double mitigationvalue = 0;

            if (targets.Contains(this))
            {
                int TotalComponents = move.Components.Count;

                foreach (string component in move.Components)
                {
                    double fraction = result / TotalComponents;
                    mitigationvalue += fraction * ComponentRatios[component];
                }

                Health -= mitigationvalue;

                DeathCheck();

                BattleLogger.Log($"{DisplayName} received {mitigationvalue} damage!");
            }
        }

        public void Attack(int move, params Actor[] targets)
        {
            if (!Alive) return;

            BattleLogger.Log($"{DisplayName} used {MoveSet[move].DisplayName}!");
            MoveSet[move].Trigger(this, targets);
        }
        public void Attack(Move move, params Actor[] targets)
        {
            if (!Alive) return;

            BattleLogger.Log($"{DisplayName} used {move.DisplayName}!");
            move.Trigger(this, targets);
        }
    }
}
