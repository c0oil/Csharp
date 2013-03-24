namespace Hopfield
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClear = new System.Windows.Forms.Button();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.picSample1 = new System.Windows.Forms.PictureBox();
            this.picSample2 = new System.Windows.Forms.PictureBox();
            this.picSample3 = new System.Windows.Forms.PictureBox();
            this.btnSelect1 = new System.Windows.Forms.Button();
            this.btnSelect2 = new System.Windows.Forms.Button();
            this.btnSelect3 = new System.Windows.Forms.Button();
            this.btnRecognizeDrawn = new System.Windows.Forms.Button();
            this.picRecognized = new System.Windows.Forms.PictureBox();
            this.picNoised = new System.Windows.Forms.PictureBox();
            this.btnNoise = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio1 = new System.Windows.Forms.RadioButton();
            this.radio2 = new System.Windows.Forms.RadioButton();
            this.radio3 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNoise = new System.Windows.Forms.NumericUpDown();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRecognizeNoised = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSample1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecognized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNoised)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoise)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(48, 109);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 27);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lineShape4
            // 
            this.lineShape4.Enabled = false;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 350;
            this.lineShape4.X2 = 170;
            this.lineShape4.Y1 = 216;
            this.lineShape4.Y2 = 216;
            // 
            // lineShape3
            // 
            this.lineShape3.Enabled = false;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 351;
            this.lineShape3.X2 = 351;
            this.lineShape3.Y1 = 36;
            this.lineShape3.Y2 = 216;
            // 
            // lineShape2
            // 
            this.lineShape2.Enabled = false;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 170;
            this.lineShape2.X2 = 170;
            this.lineShape2.Y1 = 34;
            this.lineShape2.Y2 = 214;
            // 
            // lineShape1
            // 
            this.lineShape1.Enabled = false;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 170;
            this.lineShape1.X2 = 350;
            this.lineShape1.Y1 = 33;
            this.lineShape1.Y2 = 33;
            // 
            // picSample1
            // 
            this.picSample1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSample1.Location = new System.Drawing.Point(17, 38);
            this.picSample1.Name = "picSample1";
            this.picSample1.Size = new System.Drawing.Size(180, 180);
            this.picSample1.TabIndex = 2;
            this.picSample1.TabStop = false;
            // 
            // picSample2
            // 
            this.picSample2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSample2.Location = new System.Drawing.Point(224, 38);
            this.picSample2.Name = "picSample2";
            this.picSample2.Size = new System.Drawing.Size(180, 180);
            this.picSample2.TabIndex = 3;
            this.picSample2.TabStop = false;
            // 
            // picSample3
            // 
            this.picSample3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSample3.Location = new System.Drawing.Point(429, 38);
            this.picSample3.Name = "picSample3";
            this.picSample3.Size = new System.Drawing.Size(180, 180);
            this.picSample3.TabIndex = 4;
            this.picSample3.TabStop = false;
            // 
            // btnSelect1
            // 
            this.btnSelect1.Location = new System.Drawing.Point(52, 228);
            this.btnSelect1.Name = "btnSelect1";
            this.btnSelect1.Size = new System.Drawing.Size(109, 27);
            this.btnSelect1.TabIndex = 5;
            this.btnSelect1.Text = "Select";
            this.btnSelect1.UseVisualStyleBackColor = true;
            this.btnSelect1.Click += new System.EventHandler(this.btnSelect1_Click);
            // 
            // btnSelect2
            // 
            this.btnSelect2.Location = new System.Drawing.Point(260, 228);
            this.btnSelect2.Name = "btnSelect2";
            this.btnSelect2.Size = new System.Drawing.Size(109, 27);
            this.btnSelect2.TabIndex = 6;
            this.btnSelect2.Text = "Select";
            this.btnSelect2.UseVisualStyleBackColor = true;
            this.btnSelect2.Click += new System.EventHandler(this.btnSelect2_Click);
            // 
            // btnSelect3
            // 
            this.btnSelect3.Location = new System.Drawing.Point(467, 228);
            this.btnSelect3.Name = "btnSelect3";
            this.btnSelect3.Size = new System.Drawing.Size(109, 27);
            this.btnSelect3.TabIndex = 7;
            this.btnSelect3.Text = "Select";
            this.btnSelect3.UseVisualStyleBackColor = true;
            this.btnSelect3.Click += new System.EventHandler(this.btnSelect3_Click);
            // 
            // btnRecognizeDrawn
            // 
            this.btnRecognizeDrawn.Location = new System.Drawing.Point(48, 76);
            this.btnRecognizeDrawn.Name = "btnRecognizeDrawn";
            this.btnRecognizeDrawn.Size = new System.Drawing.Size(109, 27);
            this.btnRecognizeDrawn.TabIndex = 8;
            this.btnRecognizeDrawn.Text = "Recognize Drawn";
            this.btnRecognizeDrawn.UseVisualStyleBackColor = true;
            this.btnRecognizeDrawn.Click += new System.EventHandler(this.btnRecognizeDrawn_Click);
            // 
            // picRecognized
            // 
            this.picRecognized.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picRecognized.Location = new System.Drawing.Point(17, 30);
            this.picRecognized.Name = "picRecognized";
            this.picRecognized.Size = new System.Drawing.Size(180, 180);
            this.picRecognized.TabIndex = 9;
            this.picRecognized.TabStop = false;
            // 
            // picNoised
            // 
            this.picNoised.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNoised.Location = new System.Drawing.Point(633, 33);
            this.picNoised.Name = "picNoised";
            this.picNoised.Size = new System.Drawing.Size(180, 180);
            this.picNoised.TabIndex = 10;
            this.picNoised.TabStop = false;
            // 
            // btnNoise
            // 
            this.btnNoise.Location = new System.Drawing.Point(844, 122);
            this.btnNoise.Name = "btnNoise";
            this.btnNoise.Size = new System.Drawing.Size(109, 27);
            this.btnNoise.TabIndex = 13;
            this.btnNoise.Text = "Noise";
            this.btnNoise.UseVisualStyleBackColor = true;
            this.btnNoise.Click += new System.EventHandler(this.btnNoise_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio3);
            this.groupBox1.Controls.Add(this.radio2);
            this.groupBox1.Controls.Add(this.radio1);
            this.groupBox1.Location = new System.Drawing.Point(841, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 50);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sample:";
            // 
            // radio1
            // 
            this.radio1.AutoSize = true;
            this.radio1.Checked = true;
            this.radio1.Location = new System.Drawing.Point(7, 20);
            this.radio1.Name = "radio1";
            this.radio1.Size = new System.Drawing.Size(31, 17);
            this.radio1.TabIndex = 0;
            this.radio1.TabStop = true;
            this.radio1.Text = "1";
            this.radio1.UseVisualStyleBackColor = true;
            this.radio1.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // radio2
            // 
            this.radio2.AutoSize = true;
            this.radio2.Location = new System.Drawing.Point(44, 20);
            this.radio2.Name = "radio2";
            this.radio2.Size = new System.Drawing.Size(31, 17);
            this.radio2.TabIndex = 1;
            this.radio2.Text = "2";
            this.radio2.UseVisualStyleBackColor = true;
            this.radio2.CheckedChanged += new System.EventHandler(this.radio2_CheckedChanged);
            // 
            // radio3
            // 
            this.radio3.AutoSize = true;
            this.radio3.Location = new System.Drawing.Point(81, 20);
            this.radio3.Name = "radio3";
            this.radio3.Size = new System.Drawing.Size(31, 17);
            this.radio3.TabIndex = 2;
            this.radio3.Text = "3";
            this.radio3.UseVisualStyleBackColor = true;
            this.radio3.CheckedChanged += new System.EventHandler(this.radio3_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.picSample3);
            this.groupBox2.Controls.Add(this.picSample1);
            this.groupBox2.Controls.Add(this.picSample2);
            this.groupBox2.Controls.Add(this.btnSelect1);
            this.groupBox2.Controls.Add(this.btnSelect2);
            this.groupBox2.Controls.Add(this.btnSelect3);
            this.groupBox2.Location = new System.Drawing.Point(180, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(648, 267);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Samples:";
            // 
            // txtNoise
            // 
            this.txtNoise.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtNoise.Location = new System.Drawing.Point(844, 155);
            this.txtNoise.Name = "txtNoise";
            this.txtNoise.Size = new System.Drawing.Size(114, 20);
            this.txtNoise.TabIndex = 16;
            this.txtNoise.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNoise.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape4,
            this.lineShape3,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1052, 542);
            this.shapeContainer1.TabIndex = 1;
            this.shapeContainer1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.picRecognized);
            this.groupBox3.Location = new System.Drawing.Point(386, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(216, 231);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Recognized:";
            // 
            // btnRecognizeNoised
            // 
            this.btnRecognizeNoised.Location = new System.Drawing.Point(844, 89);
            this.btnRecognizeNoised.Name = "btnRecognizeNoised";
            this.btnRecognizeNoised.Size = new System.Drawing.Size(109, 27);
            this.btnRecognizeNoised.TabIndex = 18;
            this.btnRecognizeNoised.Text = "Recognize Noised";
            this.btnRecognizeNoised.UseVisualStyleBackColor = true;
            this.btnRecognizeNoised.Click += new System.EventHandler(this.btnRecognizeNoised_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(519, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 542);
            this.Controls.Add(this.btnRecognizeNoised);
            this.Controls.Add(this.btnNoise);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtNoise);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picNoised);
            this.Controls.Add(this.btnRecognizeDrawn);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "Form1";
            this.Text = "Hopfield";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picSample1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecognized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNoised)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoise)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private System.Windows.Forms.PictureBox picSample1;
        private System.Windows.Forms.PictureBox picSample2;
        private System.Windows.Forms.PictureBox picSample3;
        private System.Windows.Forms.Button btnSelect1;
        private System.Windows.Forms.Button btnSelect2;
        private System.Windows.Forms.Button btnSelect3;
        private System.Windows.Forms.Button btnRecognizeDrawn;
        private System.Windows.Forms.PictureBox picRecognized;
        private System.Windows.Forms.PictureBox picNoised;
        private System.Windows.Forms.Button btnNoise;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio3;
        private System.Windows.Forms.RadioButton radio2;
        private System.Windows.Forms.RadioButton radio1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown txtNoise;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRecognizeNoised;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

