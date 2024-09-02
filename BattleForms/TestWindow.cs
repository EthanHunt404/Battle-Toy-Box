using BattleEngine.common;
using Microsoft.VisualBasic.Logging;
using System.Collections.Generic;
using Windows.UI.Popups;

namespace BattleForms
{
    public partial class TestWindow : Form
    {
        public TurnHandler BH { get; set; }
        public int CurrentMove { get; set; }

        public TestWindow()
        {
            InitializeComponent();
            FolderStructurer.CreateStructure();
            IdHandler.ResetIDs();

            Actor[] TestActors = [new Actor(), new Actor()];
            Enemy[] TestEnemies = [new Enemy(EnemyAITypes.WILD), new Enemy(EnemyAITypes.WILD)];
            BH = new TurnHandler(TestActors, TestEnemies);

            Clock.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            int labelindex = 0;
            int barindex = 0;

            Control[] IntermediaryCollection = new Control[Controls.Count];
            Controls.CopyTo(IntermediaryCollection, 0);
            List<Control> AllControls = new List<Control>(IntermediaryCollection.Reverse());

            foreach (Control control in AllControls)
            {
                Type type = control.GetType();
                if (type == typeof(Label))
                {
                    Label label = (Label)control;
                    if (labelindex < BH.PlayerParty.Count)
                    {
                        label.Text = BH.PlayerParty[labelindex].DisplayName;
                        labelindex++;
                    }
                    else
                    {
                        label.Text = BH.EnemyParty[labelindex - BH.PlayerParty.Count].DisplayName;
                        labelindex++;
                    }
                }
                else if (type == typeof(ProgressBar))
                {
                    ProgressBar bar = (ProgressBar)control;
                    if (barindex < BH.PlayerParty.Count)
                    {
                        bar.Maximum = (int)BH.PlayerParty[barindex].MaxHealth;
                        bar.Value = (int)BH.PlayerParty[barindex].Health;
                        barindex++;
                    }
                    else
                    {
                        bar.Maximum = (int)BH.EnemyParty[barindex - BH.PlayerParty.Count].MaxHealth;
                        bar.Value = (int)BH.EnemyParty[barindex - BH.PlayerParty.Count].Health;
                        barindex++;
                    }
                }
            }

            foreach (Actor actor in BH.MemberList)
            {
                TargetSelector.Items.Add(actor.DisplayName);
            }

            int index = 0;
            foreach (Move move in BH.CurrentMember.MoveSet)
            {
                SkillBox.Items.Add(move.DisplayName);
                SkillBox.Items[index].SubItems.Add(move.Description);
                SkillBox.Items[index].SubItems.Add(move.Power.ToString());
                index++;
            }

            Clock.Stop();
        }

        private void SelectSkill(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            CurrentMove = e.ItemIndex;
        }

        private void UseSkill(object sender, EventArgs e)
        {
            int targetindex = TargetSelector.SelectedIndex;
            Actor target = BH.MemberList[targetindex];

            BH.CurrentMember.Attack(CurrentMove, target);

            BH.StepTurn();
            SkillBox.Items.Clear();
            Clock.Start();
        }
    }
}
