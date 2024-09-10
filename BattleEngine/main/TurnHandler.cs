using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleEngine.common.Global;

namespace BattleEngine.main
{
    public class TurnHandler
    {
        public List<Actor> PlayerParty { get; set; }
        public List<Enemy> EnemyParty { get; set; }

        public List<Actor> MemberList { get; set; }

        public Actor CurrentMember { get; private set; }
        public int CurrentTurn { get; private set; }

        public delegate void TurnDelegate(Enemy target, Actor[] party, Enemy[] enemies);
        public static event TurnDelegate OnTurn;

        public TurnHandler(Actor[] party, Enemy[] enemies)
        {
            PlayerParty = new List<Actor>(party);
            EnemyParty = new List<Enemy>(enemies);

            MemberList = new List<Actor>(PlayerParty);
            MemberList.AddRange(EnemyParty);

            CurrentTurn = 0;
            OrderMembers();
            TurnLoop();
        }

        private void OrderMembers()
        {
            MemberList.OrderBy(actor => actor.Attributes.Last());
        }

        private void TurnLoop()
        {
            Actor member = MemberList[CurrentTurn];

            if (PlayerParty.Contains(member))
            {
                CurrentMember = member;
            }
            else if (EnemyParty.Contains(member))
            {
                Enemy intermediary = EnemyParty.Find(enemy => member.FileName == enemy.FileName);
                OnTurn.Invoke(intermediary, PlayerParty.ToArray(), EnemyParty.ToArray());
            }
        }

        public void StepTurn()
        {
            if (CurrentTurn >= MemberList.Count - 1)
            {
                CurrentTurn = 0;
                TurnLoop();
            }
            else
            {
                CurrentTurn++;
                TurnLoop();
            }
        }
        public void StepTurn(int i)
        {
            if (CurrentTurn >= MemberList.Count - 1)
            {
                CurrentTurn = 0;
                TurnLoop();
            }
            else
            {
                CurrentTurn += i;
                TurnLoop();
            }
        }
    }
}
