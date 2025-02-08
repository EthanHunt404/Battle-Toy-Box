namespace BattleForms
{
    partial class TestWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            HealthBar1 = new ProgressBar();
            HealthBar2 = new ProgressBar();
            HealthBar3 = new ProgressBar();
            HealthBar4 = new ProgressBar();
            DisplayLabel1 = new Label();
            DisplayLabel2 = new Label();
            DisplayLabel3 = new Label();
            DisplayLabel4 = new Label();
            SelectButton = new Button();
            DisplayLog = new ListBox();
            SkillBox = new ListView();
            displaynamecolumn = new ColumnHeader();
            descriptioncolumn = new ColumnHeader();
            powercolumn = new ColumnHeader();
            Clock = new System.Windows.Forms.Timer(components);
            TargetSelector = new DomainUpDown();
            SuspendLayout();
            // 
            // HealthBar1
            // 
            HealthBar1.Location = new Point(12, 22);
            HealthBar1.Name = "HealthBar1";
            HealthBar1.Size = new Size(379, 23);
            HealthBar1.TabIndex = 0;
            HealthBar1.Value = 100;
            // 
            // HealthBar2
            // 
            HealthBar2.Location = new Point(12, 67);
            HealthBar2.Name = "HealthBar2";
            HealthBar2.Size = new Size(379, 23);
            HealthBar2.TabIndex = 1;
            HealthBar2.Value = 100;
            // 
            // HealthBar3
            // 
            HealthBar3.Location = new Point(409, 22);
            HealthBar3.Name = "HealthBar3";
            HealthBar3.Size = new Size(379, 23);
            HealthBar3.TabIndex = 4;
            HealthBar3.Value = 100;
            // 
            // HealthBar4
            // 
            HealthBar4.Location = new Point(409, 67);
            HealthBar4.Name = "HealthBar4";
            HealthBar4.Size = new Size(379, 23);
            HealthBar4.TabIndex = 5;
            HealthBar4.Value = 100;
            // 
            // DisplayLabel1
            // 
            DisplayLabel1.AutoSize = true;
            DisplayLabel1.Location = new Point(12, 4);
            DisplayLabel1.Name = "DisplayLabel1";
            DisplayLabel1.Size = new Size(32, 15);
            DisplayLabel1.TabIndex = 6;
            DisplayLabel1.Text = "label";
            // 
            // DisplayLabel2
            // 
            DisplayLabel2.AutoSize = true;
            DisplayLabel2.Location = new Point(12, 48);
            DisplayLabel2.Name = "DisplayLabel2";
            DisplayLabel2.Size = new Size(32, 15);
            DisplayLabel2.TabIndex = 7;
            DisplayLabel2.Text = "label";
            // 
            // DisplayLabel3
            // 
            DisplayLabel3.AutoSize = true;
            DisplayLabel3.Location = new Point(407, 4);
            DisplayLabel3.Name = "DisplayLabel3";
            DisplayLabel3.Size = new Size(32, 15);
            DisplayLabel3.TabIndex = 8;
            DisplayLabel3.Text = "label";
            // 
            // DisplayLabel4
            // 
            DisplayLabel4.AutoSize = true;
            DisplayLabel4.Location = new Point(409, 48);
            DisplayLabel4.Name = "DisplayLabel4";
            DisplayLabel4.Size = new Size(32, 15);
            DisplayLabel4.TabIndex = 9;
            DisplayLabel4.Text = "label";
            // 
            // SelectButton
            // 
            SelectButton.Location = new Point(11, 375);
            SelectButton.Name = "SelectButton";
            SelectButton.Size = new Size(254, 40);
            SelectButton.TabIndex = 11;
            SelectButton.Text = "Use Skill";
            SelectButton.UseVisualStyleBackColor = true;
            SelectButton.Click += UseSkill;
            // 
            // DisplayLog
            // 
            DisplayLog.FormattingEnabled = true;
            DisplayLog.ItemHeight = 15;
            DisplayLog.Location = new Point(409, 96);
            DisplayLog.Name = "DisplayLog";
            DisplayLog.Size = new Size(379, 319);
            DisplayLog.TabIndex = 12;
            // 
            // SkillBox
            // 
            SkillBox.Columns.AddRange(new ColumnHeader[] { displaynamecolumn, descriptioncolumn, powercolumn });
            SkillBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SkillBox.Location = new Point(12, 96);
            SkillBox.Name = "SkillBox";
            SkillBox.Size = new Size(379, 273);
            SkillBox.TabIndex = 14;
            SkillBox.UseCompatibleStateImageBehavior = false;
            SkillBox.View = View.Details;
            SkillBox.ItemSelectionChanged += SelectSkill;
            // 
            // displaynamecolumn
            // 
            displaynamecolumn.Text = "Name";
            displaynamecolumn.TextAlign = HorizontalAlignment.Center;
            displaynamecolumn.Width = 100;
            // 
            // descriptioncolumn
            // 
            descriptioncolumn.Text = "Description";
            descriptioncolumn.TextAlign = HorizontalAlignment.Center;
            descriptioncolumn.Width = 215;
            // 
            // powercolumn
            // 
            powercolumn.Text = "Power";
            powercolumn.TextAlign = HorizontalAlignment.Center;
            // 
            // Clock
            // 
            Clock.Interval = 1000;
            Clock.Tick += UpdateForm;
            // 
            // TargetSelector
            // 
            TargetSelector.Location = new Point(271, 384);
            TargetSelector.Name = "TargetSelector";
            TargetSelector.Size = new Size(120, 23);
            TargetSelector.TabIndex = 15;
            TargetSelector.Text = "TargetSelector";
            // 
            // TestWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TargetSelector);
            Controls.Add(SkillBox);
            Controls.Add(DisplayLog);
            Controls.Add(SelectButton);
            Controls.Add(DisplayLabel4);
            Controls.Add(DisplayLabel3);
            Controls.Add(DisplayLabel2);
            Controls.Add(DisplayLabel1);
            Controls.Add(HealthBar4);
            Controls.Add(HealthBar3);
            Controls.Add(HealthBar2);
            Controls.Add(HealthBar1);
            Name = "TestWindow";
            Text = "label";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        internal ProgressBar HealthBar1;
        internal ProgressBar HealthBar2;
        internal ProgressBar HealthBar3;
        internal ProgressBar HealthBar4;
        internal Label DisplayLabel1;
        internal Label DisplayLabel2;
        internal Label DisplayLabel4;
        internal Button SelectButton;
        internal ListBox DisplayLog;
        internal ListView SkillBox;
        internal ColumnHeader descriptioncolumn;
        internal ColumnHeader powercolumn;
        private System.Windows.Forms.Timer Clock;
        private ColumnHeader displaynamecolumn;
        private DomainUpDown TargetSelector;
        internal Label DisplayLabel3;
    }
}
