﻿using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static BattleEngine.common.Global;
using static BattleEngine.main.Schematics;

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
                if (value > (int)Limits.POWERCAP)
                {
                    _power = (int)Limits.POWERCAP;
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

        public delegate void DamageHandler(Move move, double result, params Actor[] targets);
        public static event DamageHandler ?OnHurt;

        public Categories Category { get; set; }
        public List<string> Components { get; set; }
        public Dictionary<string, double> AttributeRatioes { get; set; }

        [JsonConstructor()]
        public Move(string filename)
        {
            FileName = filename;
            ID = IdHandler.GetID(this);

            DisplayName = $"{FileName[0].ToString().ToUpper()}{FileName.Substring(1)} {ID + 1}";
            Description = "Not Implemented";

            Power = 10;
            Category = Categories.MELEE;

            Components = [ListOfComponents[1]];

            AttributeRatioes = new Dictionary<string, double>();
            for (int i = 0; i < ListOfAttributes.Count; i++)
            {
                AttributeRatioes.Add(ListOfAttributes[i].Name.ToUpper(), 0);
            }
            AttributeRatioes[ListOfAttributes[1].Name.ToUpper()] = 1.0;
        }
        public Move(string filename, string displayname, string description, int power, Categories category, double[] ratioes, params string[] components)
        {
            FileName = filename.ToLower();
            ID = IdHandler.GetID(this);

            DisplayName = $"{displayname} {ID + 1}";
            Description = description;

            Power = power;
            Category = category;

            Components = new List<string>(components);
            foreach (string item in components)
            {
                item.ToUpper();
            }

            AttributeRatioes = new Dictionary<string, double>();
            for (int i = 0; i < ListOfAttributes.Count; i++)
            {
                AttributeRatioes.Add(ListOfAttributes[i].Name.ToUpper(), ratioes[i]);
            }
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

                FileName = origin.FileName;
                ID = IdHandler.GetID(this);

                DisplayName = $"{origin.DisplayName} {ID + 1}";
                Description = origin.Description;

                Power = origin.Power;
                Category = origin.Category;
                
                Components = new List<string>(origin.Components);
                foreach (string item in Components)
                {
                    item.ToUpper();
                }

                AttributeRatioes = new Dictionary<string, double>(origin.AttributeRatioes);
            }
            else
            {
                throw new ArgumentNullException("isfile", "param was never confirmed");
            }
        }

        public static implicit operator Move(MoveSchematic schema)
        {
            Move move = new Move(schema.FileName);

            move.DisplayName = schema.DisplayName;
            move.Description = schema.Description;
            move.Power = schema.Power;
            move.Category = schema.Category;
            move.Components = schema.Components;
            move.AttributeRatioes = schema.AttributeRatioes;

            return move;
        }

        public void Trigger(Actor user, params Actor[] targets)
        {
            double result = 0;
            double multiplier = 0;

            for (int i = 0; i < user.Attributes.Count; i++)
            {
                string currentstatname = user.Attributes[i].Name.ToUpper();
                if (AttributeRatioes.ContainsKey(currentstatname) && AttributeRatioes[currentstatname] != 0)
                {
                    multiplier += user.Attributes[i].Value * AttributeRatioes[currentstatname];
                }
            }

            result = Power * multiplier;
            OnHurt.Invoke(this, result, targets);
        }
    }
}
