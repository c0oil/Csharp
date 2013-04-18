namespace DIP6
{
    partial class Form1
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
            this.bntNoize = new System.Windows.Forms.Button();
            this.tbxNoize = new System.Windows.Forms.TextBox();
            this.bntTeach = new System.Windows.Forms.Button();
            this.btnReproduce = new System.Windows.Forms.Button();
            this.lblClass2 = new System.Windows.Forms.Label();
            this.pictureClass1 = new System.Windows.Forms.PictureBox();
            this.pictureClass2 = new System.Windows.Forms.PictureBox();
            this.pictureClass3 = new System.Windows.Forms.PictureBox();
            this.pictureForReproduce = new System.Windows.Forms.PictureBox();
            this.bntLoad = new System.Windows.Forms.Button();
            this.lblClass1 = new System.Windows.Forms.Label();
            this.lblClass3 = new System.Windows.Forms.Label();
            this.lblReproducedClass = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClass1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClass2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClass3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureForReproduce)).BeginInit();
            this.SuspendLayout();
            // 
            // bntNoize
            // 
            this.bntNoize.Location = new System.Drawing.Point(19, 219);
            this.bntNoize.Name = "bntNoize";
            this.bntNoize.Size = new System.Drawing.Size(90, 23);
            this.bntNoize.TabIndex = 2;
            this.bntNoize.Text = "Noise";
            this.bntNoize.UseVisualStyleBackColor = true;
            this.bntNoize.Click += new System.EventHandler(this.bntNoize_Click);
            // 
            // tbxNoize
            // 
            this.tbxNoize.Location = new System.Drawing.Point(115, 220);
            this.tbxNoize.Name = "tbxNoize";
            this.tbxNoize.Size = new System.Drawing.Size(42, 20);
            this.tbxNoize.TabIndex = 7;
            this.tbxNoize.Text = "0";
            // 
            // bntTeach
            // 
            this.bntTeach.Location = new System.Drawing.Point(19, 161);
            this.bntTeach.Name = "bntTeach";
            this.bntTeach.Size = new System.Drawing.Size(90, 23);
            this.bntTeach.TabIndex = 9;
            this.bntTeach.Text = "Teach";
            this.bntTeach.UseVisualStyleBackColor = true;
            this.bntTeach.Click += new System.EventHandler(this.bntTeach_Click);
            // 
            // btnReproduce
            // 
            this.btnReproduce.Location = new System.Drawing.Point(19, 248);
            this.btnReproduce.Name = "btnReproduce";
            this.btnReproduce.Size = new System.Drawing.Size(90, 23);
            this.btnReproduce.TabIndex = 10;
            this.btnReproduce.Text = "Recognize";
            this.btnReproduce.UseVisualStyleBackColor = true;
            this.btnReproduce.Click += new System.EventHandler(this.btnReproduce_Click);
            // 
            // lblClass2
            // 
            this.lblClass2.AutoSize = true;
            this.lblClass2.Location = new System.Drawing.Point(310, 12);
            this.lblClass2.Name = "lblClass2";
            this.lblClass2.Size = new System.Drawing.Size(32, 13);
            this.lblClass2.TabIndex = 11;
            this.lblClass2.Text = "Class";
            // 
            // pictureClass1
            // 
            this.pictureClass1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureClass1.Location = new System.Drawing.Point(12, 12);
            this.pictureClass1.Name = "pictureClass1";
            this.pictureClass1.Size = new System.Drawing.Size(120, 120);
            this.pictureClass1.TabIndex = 12;
            this.pictureClass1.TabStop = false;
            // 
            // pictureClass2
            // 
            this.pictureClass2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureClass2.Location = new System.Drawing.Point(184, 12);
            this.pictureClass2.Name = "pictureClass2";
            this.pictureClass2.Size = new System.Drawing.Size(120, 120);
            this.pictureClass2.TabIndex = 13;
            this.pictureClass2.TabStop = false;
            // 
            // pictureClass3
            // 
            this.pictureClass3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureClass3.Location = new System.Drawing.Point(357, 12);
            this.pictureClass3.Name = "pictureClass3";
            this.pictureClass3.Size = new System.Drawing.Size(120, 120);
            this.pictureClass3.TabIndex = 14;
            this.pictureClass3.TabStop = false;
            // 
            // pictureForReproduce
            // 
            this.pictureForReproduce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureForReproduce.Location = new System.Drawing.Point(183, 159);
            this.pictureForReproduce.Name = "pictureForReproduce";
            this.pictureForReproduce.Size = new System.Drawing.Size(120, 120);
            this.pictureForReproduce.TabIndex = 15;
            this.pictureForReproduce.TabStop = false;
            // 
            // bntLoad
            // 
            this.bntLoad.Location = new System.Drawing.Point(19, 190);
            this.bntLoad.Name = "bntLoad";
            this.bntLoad.Size = new System.Drawing.Size(90, 23);
            this.bntLoad.TabIndex = 16;
            this.bntLoad.Text = "Load";
            this.bntLoad.UseVisualStyleBackColor = true;
            this.bntLoad.Click += new System.EventHandler(this.bntLoad_Click);
            // 
            // lblClass1
            // 
            this.lblClass1.AutoSize = true;
            this.lblClass1.Location = new System.Drawing.Point(138, 12);
            this.lblClass1.Name = "lblClass1";
            this.lblClass1.Size = new System.Drawing.Size(32, 13);
            this.lblClass1.TabIndex = 17;
            this.lblClass1.Text = "Class";
            // 
            // lblClass3
            // 
            this.lblClass3.AutoSize = true;
            this.lblClass3.Location = new System.Drawing.Point(483, 12);
            this.lblClass3.Name = "lblClass3";
            this.lblClass3.Size = new System.Drawing.Size(0, 13);
            this.lblClass3.TabIndex = 18;
            // 
            // lblReproducedClass
            // 
            this.lblReproducedClass.AutoSize = true;
            this.lblReproducedClass.Location = new System.Drawing.Point(309, 159);
            this.lblReproducedClass.Name = "lblReproducedClass";
            this.lblReproducedClass.Size = new System.Drawing.Size(32, 13);
            this.lblReproducedClass.TabIndex = 19;
            this.lblReproducedClass.Text = "Class";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 295);
            this.Controls.Add(this.lblReproducedClass);
            this.Controls.Add(this.lblClass3);
            this.Controls.Add(this.lblClass1);
            this.Controls.Add(this.bntLoad);
            this.Controls.Add(this.pictureForReproduce);
            this.Controls.Add(this.pictureClass3);
            this.Controls.Add(this.pictureClass2);
            this.Controls.Add(this.pictureClass1);
            this.Controls.Add(this.lblClass2);
            this.Controls.Add(this.btnReproduce);
            this.Controls.Add(this.bntTeach);
            this.Controls.Add(this.tbxNoize);
            this.Controls.Add(this.bntNoize);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureClass1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClass2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClass3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureForReproduce)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntNoize;
        private System.Windows.Forms.TextBox tbxNoize;
        private System.Windows.Forms.Button bntTeach;
        private System.Windows.Forms.Button btnReproduce;
        private System.Windows.Forms.Label lblClass2;
        private System.Windows.Forms.PictureBox pictureClass1;
        private System.Windows.Forms.PictureBox pictureClass2;
        private System.Windows.Forms.PictureBox pictureClass3;
        private System.Windows.Forms.PictureBox pictureForReproduce;
        private System.Windows.Forms.Button bntLoad;
        private System.Windows.Forms.Label lblClass1;
        private System.Windows.Forms.Label lblClass3;
        private System.Windows.Forms.Label lblReproducedClass;
    }
}

