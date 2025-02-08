using BattleEngine.common;
using Microsoft.VisualBasic.Logging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static BattleEngine.common.Global;
using Windows.UI.Popups;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace BattleForms
{
    public partial class TestWindow : Form
    {
        public TurnHandler BH { get; set; }
        public int CurrentMove { get; set; }

        public TestWindow()
        {
            InitializeComponent();
            IdHandler.ResetIDs();

            Actor[] TestActors = [new Actor("actor", true), new Actor("supporting", true)];
            Enemy[] TestEnemies = [new Enemy("enemy", true), new Enemy("annoyance", true)];

            BH = new TurnHandler(TestActors, TestEnemies);
            BattleLogger.TextBus += LogCatcher;
            TurnHandler.OnEndBattle += BattleStatePopUp;

            Clock.Start();
        }

        private void UpdateForm(object sender, EventArgs e)
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
            TargetSelector.Items.Reverse();

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
            try
            {
                string selected = TargetSelector.SelectedItem.ToString();
                Actor target = BH.MemberList.Find(actor => actor.DisplayName == selected);
                
                BH.CurrentMember.Attack(CurrentMove, target);

                BH.StepTurn();
                SkillBox.Items.Clear();
                Clock.Start();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please select a Valid Target", "Target Selection Exeption", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something unexpected happened, please report as soon as possible", "404", MessageBoxButtons.OK);
            }
        }

        private void BattleStatePopUp(bool victory)
        {
            if (victory)
            {
                MessageBox.Show("You Win!!!", "Congratulations", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("You Lost :(", "Condolences", MessageBoxButtons.OK);
            }
        }

        private void LogCatcher(string[] text)
        {
            DisplayLog.Items.Clear();
            DisplayLog.Items.AddRange(text.ToArray());
        }
    }
}
