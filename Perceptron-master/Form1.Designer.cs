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
            this.picSample1 = new System.Windows.Forms.PictureBox();
            this.picSample2 = new System.Windows.Forms.PictureBox();
            this.picSample3 = new System.Windows.Forms.PictureBox();
            this.btnSelect1 = new System.Windows.Forms.Button();
            this.btnSelect2 = new System.Windows.Forms.Button();
            this.btnSelect3 = new System.Windows.Forms.Button();
            this.picNoised = new System.Windows.Forms.PictureBox();
            this.btnNoise = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio3 = new System.Windows.Forms.RadioButton();
            this.radio2 = new System.Windows.Forms.RadioButton();
            this.radio1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoise = new System.Windows.Forms.NumericUpDown();
            this.btnRecognizeNoised = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelRecognized = new System.Windows.Forms.Label();
            this.labelFirst = new System.Windows.Forms.Label();
            this.labelSecond = new System.Windows.Forms.Label();
            this.labelThird = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.labelIterations = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSample1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNoised)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoise)).BeginInit();
            this.SuspendLayout();
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
            // picNoised
            // 
            this.picNoised.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNoised.Location = new System.Drawing.Point(30, 31);
            this.picNoised.Name = "picNoised";
            this.picNoised.Size = new System.Drawing.Size(180, 180);
            this.picNoised.TabIndex = 10;
            this.picNoised.TabStop = false;
            // 
            // btnNoise
            // 
            this.btnNoise.Location = new System.Drawing.Point(241, 120);
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
            this.groupBox1.Location = new System.Drawing.Point(238, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 50);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sample:";
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
            this.groupBox2.Location = new System.Drawing.Point(9, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(648, 267);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Samples:";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "2";
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
            // txtNoise
            // 
            this.txtNoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtNoise.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtNoise.Location = new System.Drawing.Point(241, 153);
            this.txtNoise.Name = "txtNoise";
            this.txtNoise.Size = new System.Drawing.Size(114, 26);
            this.txtNoise.TabIndex = 16;
            this.txtNoise.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNoise.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // btnRecognizeNoised
            // 
            this.btnRecognizeNoised.Location = new System.Drawing.Point(241, 87);
            this.btnRecognizeNoised.Name = "btnRecognizeNoised";
            this.btnRecognizeNoised.Size = new System.Drawing.Size(109, 27);
            this.btnRecognizeNoised.TabIndex = 18;
            this.btnRecognizeNoised.Text = "Recognize Noised";
            this.btnRecognizeNoised.UseVisualStyleBackColor = true;
            this.btnRecognizeNoised.Click += new System.EventHandler(this.btnRecognizeNoised_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(388, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "Recognized as:";
            // 
            // labelRecognized
            // 
            this.labelRecognized.AutoSize = true;
            this.labelRecognized.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRecognized.Location = new System.Drawing.Point(547, 111);
            this.labelRecognized.Name = "labelRecognized";
            this.labelRecognized.Size = new System.Drawing.Size(14, 20);
            this.labelRecognized.TabIndex = 23;
            this.labelRecognized.Text = "-";
            // 
            // labelFirst
            // 
            this.labelFirst.AutoSize = true;
            this.labelFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFirst.Location = new System.Drawing.Point(93, 233);
            this.labelFirst.Name = "labelFirst";
            this.labelFirst.Size = new System.Drawing.Size(16, 24);
            this.labelFirst.TabIndex = 24;
            this.labelFirst.Text = "-";
            // 
            // labelSecond
            // 
            this.labelSecond.AutoSize = true;
            this.labelSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSecond.Location = new System.Drawing.Point(303, 233);
            this.labelSecond.Name = "labelSecond";
            this.labelSecond.Size = new System.Drawing.Size(16, 24);
            this.labelSecond.TabIndex = 25;
            this.labelSecond.Text = "-";
            // 
            // labelThird
            // 
            this.labelThird.AutoSize = true;
            this.labelThird.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelThird.Location = new System.Drawing.Point(514, 233);
            this.labelThird.Name = "labelThird";
            this.labelThird.Size = new System.Drawing.Size(16, 24);
            this.labelThird.TabIndex = 26;
            this.labelThird.Text = "-";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(392, 36);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(180, 13);
            this.progressBar1.TabIndex = 27;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(392, 55);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(180, 13);
            this.progressBar2.TabIndex = 28;
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(392, 74);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(180, 13);
            this.progressBar3.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(388, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 20);
            this.label5.TabIndex = 30;
            this.label5.Text = "Teaching iterations:";
            // 
            // labelIterations
            // 
            this.labelIterations.AutoSize = true;
            this.labelIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIterations.Location = new System.Drawing.Point(548, 139);
            this.labelIterations.Name = "labelIterations";
            this.labelIterations.Size = new System.Drawing.Size(14, 20);
            this.labelIterations.TabIndex = 31;
            this.labelIterations.Text = "-";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 542);
            this.Controls.Add(this.labelIterations);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelThird);
            this.Controls.Add(this.labelSecond);
            this.Controls.Add(this.labelFirst);
            this.Controls.Add(this.labelRecognized);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRecognizeNoised);
            this.Controls.Add(this.btnNoise);
            this.Controls.Add(this.txtNoise);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picNoised);
            this.Name = "Form1";
            this.Text = "Hopfield";
            ((System.ComponentModel.ISupportInitialize)(this.picSample1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNoised)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoise)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picSample1;
        private System.Windows.Forms.PictureBox picSample2;
        private System.Windows.Forms.PictureBox picSample3;
        private System.Windows.Forms.Button btnSelect1;
        private System.Windows.Forms.Button btnSelect2;
        private System.Windows.Forms.Button btnSelect3;
        private System.Windows.Forms.PictureBox picNoised;
        private System.Windows.Forms.Button btnNoise;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio3;
        private System.Windows.Forms.RadioButton radio2;
        private System.Windows.Forms.RadioButton radio1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown txtNoise;
        private System.Windows.Forms.Button btnRecognizeNoised;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelRecognized;
        private System.Windows.Forms.Label labelFirst;
        private System.Windows.Forms.Label labelSecond;
        private System.Windows.Forms.Label labelThird;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelIterations;
    }
}

