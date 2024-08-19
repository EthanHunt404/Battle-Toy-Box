using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace BattleEngine.main
{
    public class BattleTest
    {
        List<Actor> PlayerParty { get; set; }
        List<Enemy> EnemyParty { get; set; }

        public delegate void TurnHandler(Actor[] allies, Actor[] enemies);
        public event TurnHandler ?IsEnemyTurn;

        public BattleTest(Actor[] allies, Enemy[] enemies)
        {
            PlayerParty = new List<Actor>(allies);
            EnemyParty = new List<Enemy>(enemies);
        }

        public void StartBattle()
        {
            Grid grid = new Grid();
            grid.AddColumn();
            grid.AddColumn();

            foreach (Actor actor in PlayerParty)
            {

            }
        }
    }
}
