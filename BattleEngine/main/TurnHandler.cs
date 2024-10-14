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
        public List<Actor> DeadList { get; set; }

        public Actor CurrentMember { get; private set; }
        public int CurrentTurn { get; private set; }

        public delegate void TurnDelegate(Enemy target, Actor[] party, Enemy[] enemies);
        public static event TurnDelegate OnTurn;

        public static event BooleanDelegate OnEndBattle;

        public TurnHandler(Actor[] party, Enemy[] enemies)
        {
            PlayerParty = new List<Actor>(party);
            EnemyParty = new List<Enemy>(enemies);

            MemberList = new List<Actor>();
            MemberList.AddRange(PlayerParty);
            MemberList.AddRange(EnemyParty);

            DeadList = new List<Actor>();

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

            if (DeadList.Contains(member))
            {
                StepTurn();
                return;
            }

            if (PlayerParty.Contains(member))
            {
                CurrentMember = member;
                CheckBattleState();
            }
            else if (EnemyParty.Contains(member))
            {
                Enemy intermediary = EnemyParty.Find(enemy => member.InternalName == enemy.InternalName);
                OnTurn.Invoke(intermediary, PlayerParty.ToArray(), EnemyParty.ToArray());
                CheckBattleState();
                StepTurn();
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

        private void CheckBattleState()
        {
            //Death Toll
            foreach (Actor member in MemberList)
            {
                if (member.Alive == false)
                {
                    DeadList.Add(member);
                }
                else
                {
                    continue;
                }
            }

            //Verifications
            foreach (Actor member in EnemyParty)
            {
                int count = 0;
                if (DeadList.Contains(member))
                {
                    count++;
                    if (count == MemberList.Count)
                    {
                        EndBattle(true);
                    }
                }
                else
                {
                    break;
                }
            }
            foreach (Actor member in PlayerParty)
            {
                int count = 0;
                if (DeadList.Contains(member))
                {
                    count++;
                    if (count == PlayerParty.Count)
                    {
                        EndBattle(false);
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void EndBattle(bool victory)
        {
            if (victory)
            {
                BattleLogger.Log("When the dust settles down, The party remains Victorious!");
                OnEndBattle.Invoke(true);
                RestoreMembers();
            }
            else
            {
                BattleLogger.Log("When the dust settles down, The Party has been layed down, Defeated...");
                OnEndBattle.Invoke(false);
                RestoreMembers();
            }
        }

        public void LevelUpMembers()
        {
            throw new NotImplementedException();
        }
        public void RestoreMembers()
        {
            foreach (Actor member in MemberList)
            {
                member.Restore();
            }
        }
    }
}
