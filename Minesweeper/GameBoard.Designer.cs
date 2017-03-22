namespace Minesweeper
{
    partial class GameBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.MineCountLabel = new System.Windows.Forms.Label();
            this.InstructionsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Ed\'s Minesweeper!";
            // 
            // MineCountLabel
            // 
            this.MineCountLabel.AutoSize = true;
            this.MineCountLabel.Location = new System.Drawing.Point(52, 50);
            this.MineCountLabel.Name = "MineCountLabel";
            this.MineCountLabel.Size = new System.Drawing.Size(56, 13);
            this.MineCountLabel.TabIndex = 1;
            this.MineCountLabel.Text = "minecount";
            // 
            // InstructionsLabel
            // 
            this.InstructionsLabel.AutoSize = true;
            this.InstructionsLabel.Location = new System.Drawing.Point(63, 72);
            this.InstructionsLabel.Name = "InstructionsLabel";
            this.InstructionsLabel.Size = new System.Drawing.Size(199, 65);
            this.InstructionsLabel.TabIndex = 2;
            this.InstructionsLabel.Text = "1) Left-click to uncover squares.\r\n2) Right-click to flag mined squares.\r\n3) Numb" +
    "ers in uncovered squares show \r\nthe number of adjacent mines.\r\n4) Safely uncover" +
    "/flag all squares to win.";
            // 
            // GameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 401);
            this.Controls.Add(this.InstructionsLabel);
            this.Controls.Add(this.MineCountLabel);
            this.Controls.Add(this.label1);
            this.Name = "GameBoard";
            this.Text = "Minesweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MineCountLabel;
        private System.Windows.Forms.Label InstructionsLabel;
    }
}

