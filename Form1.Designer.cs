namespace GeneratoreCodM
{
    partial class Form1
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
            this.fileCsvLabel = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fileCsvLabel
            // 
            this.fileCsvLabel.AutoSize = true;
            this.fileCsvLabel.Location = new System.Drawing.Point(12, 16);
            this.fileCsvLabel.Name = "fileCsvLabel";
            this.fileCsvLabel.Size = new System.Drawing.Size(52, 15);
            this.fileCsvLabel.TabIndex = 1;
            this.fileCsvLabel.Text = "File CSV:";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(245, 12);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 2;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(78, 12);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.Size = new System.Drawing.Size(142, 23);
            this.fileNameBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 55);
            this.Controls.Add(this.fileNameBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.fileCsvLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Generatore CodM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label fileCsvLabel;
        private Button generateButton;
        private TextBox fileNameBox;
    }
}