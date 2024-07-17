using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using static BattleEngine.common.Global;
using static BattleEngine.main.Move;

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
            private set{
                if (value <= 1){
                    _maxhp = 1;
                }else if (value > (double)Values.HEALTHCAP){
                    _maxhp = (double)Values.HEALTHCAP;
                }else{
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
                else if (value > (int)Values.LEVELCAP)
                {
                    _level = (int)Values.LEVELCAP;
                }
                else
                {
                    _level = value;
                }
            }
        }

        public List<StatAttribute> Attributes { get; private set; }
        public List<Move> MoveSet { get; set; }

        public Actor()
        {
            ID = IdHandler.GetID(this);

            FileName = $"Actor {ID}";
            DisplayName = "Placeholder";

            Level = 5;

            MaxHealth = 100 * Level;
            Health = MaxHealth;

            MitigationValue = 0;
            IsHurt += Mitigate;

            Attributes = new List<StatAttribute>(DefaultAttributes);

            MoveSet = [new Move()];
        }
        public Actor(string filename, string displayname, int lvl, params Move[] moves)
        {
            ID = IdHandler.GetID(this);

            FileName = filename.ToLower();
            DisplayName = displayname;

            Level = lvl;

            MaxHealth = 100 * Level;
            Health = MaxHealth;

            MitigationValue = 0;
            IsHurt += Mitigate;

            Attributes = new List<StatAttribute>(DefaultAttributes);
            Attributes.ForEach(item => item.Value = 5 * Level);

            MoveSet = new List<Move>(moves);
        }
        public Actor(string filename, string displayname, int lvl, List<StatAttribute> attributes, params Move[] moves)
        {
            ID = IdHandler.GetID(this);

            FileName = filename;
            DisplayName = displayname;

            Level = lvl;

            MaxHealth = 100 * Level;
            Health = MaxHealth;

            MitigationValue = 0;
            IsHurt += Mitigate;

            Attributes = new List<StatAttribute>(attributes);

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
            actor.MoveSet = schema.MoveSet;

            return actor;
        }

        public virtual void LevelUp()
        {
            Level += 1;
            MaxHealth = Level * (Attributes[0].Value * 1.5);
            Health = MaxHealth;
        }

        public virtual void Mitigate(Move move, double result, Actor target)
        {
            if (target == this)
            {
                if (move.Components.Contains(DefaultComponents[1]))
                {
                    MitigationValue = result - (Attributes[2].Value + Attributes[0].Value);
                }
                else
                {
                    MitigationValue = result;
                }
                Health -= MitigationValue;
            }
        }

        public virtual void Attack(int move, Actor target)
        {
            MoveSet[move].Trigger(target, this);
        }
    }

    //Json Schema
    public record struct ActorSchematic
    {
        public string Version;

        public int ID;

        public string FileName;
        public string DisplayName;

        public double MaxHealth;

        public int Level;

        public List<StatAttribute> Attributes;
        public List<Move> MoveSet;

        public ActorSchematic()
        {
            Version = Global.Version;
            ID = -1;

            FileName = $"reference";
            DisplayName = "Actor Schematic";

            MaxHealth = -1;

            Level = -1;

            Attributes = new List<StatAttribute>();
            MoveSet = new List<Move>();
        }

        public static implicit operator ActorSchematic(Actor actor)
        {
            ActorSchematic schema = new ActorSchematic();

            schema.ID = actor.ID;
            schema.FileName = actor.FileName;
            schema.DisplayName = actor.DisplayName;
            schema.Level = actor.Level;
            schema.MaxHealth = actor.MaxHealth;
            schema.Attributes = actor.Attributes;

            foreach (Move move in actor.MoveSet)
            {
                schema.MoveSet.Add(move);
            }

            return schema;
        }
    }
}
