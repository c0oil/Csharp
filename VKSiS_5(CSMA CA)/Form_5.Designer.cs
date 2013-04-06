namespace VKSiS_5
{
	partial class VKSiS_5
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
            this.debugBox = new System.Windows.Forms.RichTextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.writeButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.loopCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // debugBox
            // 
            this.debugBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.debugBox.BackColor = System.Drawing.Color.White;
            this.debugBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.debugBox.ForeColor = System.Drawing.Color.Black;
            this.debugBox.Location = new System.Drawing.Point(14, 99);
            this.debugBox.Name = "debugBox";
            this.debugBox.Size = new System.Drawing.Size(365, 309);
            this.debugBox.TabIndex = 10;
            this.debugBox.Text = "Debug:\n";
            // 
            // inputBox
            // 
            this.inputBox.BackColor = System.Drawing.Color.White;
            this.inputBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputBox.ForeColor = System.Drawing.Color.Black;
            this.inputBox.Location = new System.Drawing.Point(14, 15);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(272, 21);
            this.inputBox.TabIndex = 0;
            this.inputBox.Text = "input";
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.Color.White;
            this.outputBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outputBox.ForeColor = System.Drawing.Color.Black;
            this.outputBox.Location = new System.Drawing.Point(14, 46);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(272, 21);
            this.outputBox.TabIndex = 1;
            this.outputBox.Text = "output";
            // 
            // writeButton
            // 
            this.writeButton.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.writeButton.Location = new System.Drawing.Point(292, 11);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(87, 25);
            this.writeButton.TabIndex = 2;
            this.writeButton.Text = "Write";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // readButton
            // 
            this.readButton.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readButton.Location = new System.Drawing.Point(292, 44);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(87, 25);
            this.readButton.TabIndex = 2;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // loopCheckBox
            // 
            this.loopCheckBox.AutoSize = true;
            this.loopCheckBox.Location = new System.Drawing.Point(297, 75);
            this.loopCheckBox.Name = "loopCheckBox";
            this.loopCheckBox.Size = new System.Drawing.Size(82, 18);
            this.loopCheckBox.TabIndex = 3;
            this.loopCheckBox.Text = "Use Loop";
            this.loopCheckBox.UseVisualStyleBackColor = true;
            // 
            // VKSiS_5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(393, 421);
            this.Controls.Add(this.loopCheckBox);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.debugBox);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "VKSiS_5";
            this.Text = "VKSiS 5";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox debugBox;
		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.TextBox outputBox;
		private System.Windows.Forms.Button writeButton;
		private System.Windows.Forms.Button readButton;
		private System.Windows.Forms.CheckBox loopCheckBox;
	}
}

