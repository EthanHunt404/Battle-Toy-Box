using System.Text.Json;
using static BattleEngine.common.Global;

namespace BattleEngine.main
{
    public record Move
    {
        public static int TotalMoves { get; private set; }

        public int ID { get; private set; }
        public string InternalName { get; private set; }

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
        public Components Component { get; set; }

        static Move()
        {
            TotalMoves = 0;
        }

        public Move()
        {
            ID = TotalMoves;
            TotalMoves += 1;

            InternalName = $"Move {ID}";
            DisplayName = $"Placeholder";
            Description = "Not Implemented";

            Power = 10;
            Category = Categories.MELEE;
            Component = Components.NEUTRAL;
        }
        public Move(string internalname, string displayname, string description, int power, Categories category, Components component)
        {
            ID = TotalMoves;
            TotalMoves += 1;

            InternalName = internalname;
            DisplayName = displayname;
            Description = description;

            Power = power;
            Category = category;
            Component = component;
        }
        public Move(string internalfile)
        {
            string JsonString = File.ReadAllText($@"{User.MovePath}\{internalfile}.json");
            MoveSchema origin = JsonSerializer.Deserialize<MoveSchema>(JsonString, JsonFormatter);

            ID = origin.ID;
            InternalName = origin.InternalName;

            DisplayName = origin.DisplayName;
            Description = origin.Description;

            Power = origin.Power;
            Category = origin.Category;
            Component = origin.Component;
        }

        public static implicit operator Move(MoveSchema schema)
        {
            Move move = new Move();

            move.ID = schema.ID;
            move.InternalName = schema.InternalName;

            move.DisplayName = schema.DisplayName;
            move.Description = schema.Description;

            move.Power = schema.Power;
            move.Category = schema.Category;
            move.Component = schema.Component;

            return move;
        }

        public virtual void Trigger(Actor target, Actor user)
        {
            double result = Power * user.Attributes["ATK"];
            IsHurt.Invoke(this, result, target);
        }
    }

    //Json Schema
    public struct MoveSchema
    {
        public string Version;

        public int ID;
        public string InternalName;

        public string DisplayName;
        public string Description;

        public int Power;
        public Categories Category;
        public Components Component;

        public MoveSchema()
        {
            Version = "0.0.1";
            ID = -1;

            InternalName = "Move Schematic";
            DisplayName = "PlaceHolder";
            Description = "Empty";

            Power = -1;
            Category = Categories.EFFECT;
            Component = Components.NONE;
        }

        public static explicit operator MoveSchema(Move move)
        {
            MoveSchema schema = new MoveSchema();

            schema.ID = move.ID;
            schema.InternalName = move.InternalName;

            schema.DisplayName = move.DisplayName;
            schema.Description = move.Description;

            schema.Power = move.Power;
            schema.Category = move.Category;
            schema.Component = move.Component;

            return schema;
        }
    }
}
