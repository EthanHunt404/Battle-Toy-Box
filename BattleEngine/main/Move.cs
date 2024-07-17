using System.Text.Json;
using System.Xml.Linq;
using static BattleEngine.common.Global;

namespace BattleEngine.main
{
    public record Move
    {
        public int ID { get; private set; }
        public string FileName { get; private set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }

        private int _power;
        public int Power
        {
            get { return _power; }
            set
            {
                if (value > (int)Values.POWERCAP)
                {
                    _power = (int)Values.POWERCAP;
                }
                else if (value < 0)
                {
                    _power = 0;
                }
                else
                {
                    _power = value;
                }
            }
        }

        public delegate void DamageHandler(Move move, double result, Actor target);
        public static event DamageHandler IsHurt;

        public Categories Category { get; set; }
        public List<string> Components { get; set; }

        public Move()
        {
            ID = IdHandler.GetID(this);

            FileName = $"Move {ID}";
            DisplayName = $"Placeholder";
            Description = "Not Implemented";

            Power = 10;
            Category = Categories.MELEE;

            Components = [DefaultComponents[1]];
        }
        public Move(string filename, string displayname, string description, int power, Categories category, params string[] components)
        {
            ID = IdHandler.GetID(this);

            FileName = filename.ToLower();
            DisplayName = $@"{displayname}";
            Description = $@"{description}";

            Power = power;
            Category = category;

            Components = new List<string>(components);
            Components.ForEach(item => { item.ToUpper(); });
        }
        public Move(string name, bool isfile)
        {
            if (isfile == true)
            {
                string JsonString;
                MoveSchematic origin;

                if (name.Contains(".json"))
                {
                    JsonString = File.ReadAllText(name);
                    origin = JsonSerializer.Deserialize<MoveSchematic>(JsonString, SchemaFormatter);
                }
                else
                {
                    JsonString = File.ReadAllText($@"{User.MovePath}\{name}.json");
                    origin = JsonSerializer.Deserialize<MoveSchematic>(JsonString, SchemaFormatter);
                }

                ID = origin.ID;
                FileName = origin.FileName;

                DisplayName = $@"{origin.DisplayName}";
                Description = $@"{origin.Description}";

                Power = origin.Power;
                Category = origin.Category;
                
                Components = new List<string>(origin.Components);
                Components.ForEach(item => { item.ToUpper(); });
            }
            else
            {
                throw new ArgumentNullException("isfile", "param was never confirmed");
            }
        }

        public static implicit operator Move(MoveSchematic schema)
        {
            Move move = new Move();

            move.ID = schema.ID;
            move.FileName = schema.FileName;

            move.DisplayName = schema.DisplayName;
            move.Description = schema.Description;

            move.Power = schema.Power;
            move.Category = schema.Category;
            move.Components = schema.Components;

            return move;
        }

        public virtual void Trigger(Actor target, Actor user)
        {
            double result = Power * user.Attributes[1].Value;
            IsHurt.Invoke(this, result, target);
        }
    }

    //Json Schema
    public record struct MoveSchematic
    {
        public string Version;

        public int ID;
        public string FileName;

        public string DisplayName;
        public string Description;

        public int Power;
        public Categories Category;
        public List<string> Components;

        public MoveSchematic()
        {
            Version = Global.Version;
            ID = -1;

            FileName = "reference";
            DisplayName = "Move Schematic";
            Description = "Empty";

            Power = -1;
            Category = Categories.EFFECT;
            Components = new List<string>();
        }
        public static implicit operator MoveSchematic(Move move)
        {
            MoveSchematic schema = new MoveSchematic();

            schema.ID = move.ID;
            schema.FileName = move.FileName;

            schema.DisplayName = move.DisplayName;
            schema.Description = move.Description;

            schema.Power = move.Power;
            schema.Category = move.Category;
            schema.Components = move.Components;

            return schema;
        }
    }
}
