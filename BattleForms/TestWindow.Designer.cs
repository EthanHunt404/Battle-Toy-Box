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
            ActorBar1 = new ProgressBar();
            ActorBar2 = new ProgressBar();
            EnemyBar1 = new ProgressBar();
            EnemyBar2 = new ProgressBar();
            SuspendLayout();
            // 
            // ActorBar1
            // 
            ActorBar1.Location = new Point(12, 12);
            ActorBar1.Name = "ActorBar1";
            ActorBar1.Size = new Size(339, 23);
            ActorBar1.TabIndex = 0;
            ActorBar1.Value = 100;
            // 
            // ActorBar2
            // 
            ActorBar2.Location = new Point(12, 41);
            ActorBar2.Name = "ActorBar2";
            ActorBar2.Size = new Size(339, 23);
            ActorBar2.TabIndex = 1;
            ActorBar2.Value = 100;
            // 
            // EnemyBar1
            // 
            EnemyBar1.Location = new Point(449, 12);
            EnemyBar1.Name = "EnemyBar1";
            EnemyBar1.Size = new Size(339, 23);
            EnemyBar1.TabIndex = 4;
            EnemyBar1.Value = 100;
            // 
            // EnemyBar2
            // 
            EnemyBar2.Location = new Point(449, 41);
            EnemyBar2.Name = "EnemyBar2";
            EnemyBar2.Size = new Size(339, 23);
            EnemyBar2.TabIndex = 5;
            EnemyBar2.Value = 100;
            // 
            // TestWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(EnemyBar2);
            Controls.Add(EnemyBar1);
            Controls.Add(ActorBar2);
            Controls.Add(ActorBar1);
            Name = "TestWindow";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar ActorBar1;
        private ProgressBar ActorBar2;
        private ProgressBar EnemyBar1;
        private ProgressBar EnemyBar2;
    }
}
