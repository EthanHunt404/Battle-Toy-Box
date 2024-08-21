using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace BattleEngine.main
{
    public class BattleHandler
    {
        List<Actor> PlayerParty { get; set; }
        List<Enemy> EnemyParty { get; set; }

        public delegate void TurnHandler(Enemy target, Actor[] party, Enemy[] enemies);
        public static event TurnHandler OnTurn;

        public BattleHandler(Actor[] party, Enemy[] enemies)
        {
            PlayerParty = new List<Actor>(party);
            EnemyParty = new List<Enemy>(enemies);
        }

        public void StartBattle()
        {  

        }
    }
}
