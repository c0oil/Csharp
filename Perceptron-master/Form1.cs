using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra;

namespace Hopfield
{
    public partial class Form1 : Form
    {
        private DrawField drawField = new DrawField();
        private string[] bitmapFilePaths = new string[3];

        public Form1()
        {
            InitializeComponent();
            InitializePictures();
        }

        private void InitializePictures()
        {
            LoadSamplePictures();
            NoiseAndLoad(pictureIndex: 0);
        }

        private void LoadSamplePictures()
        {
            bitmapFilePaths = new[] { "1.bmp", "2.bmp", "3.bmp" };
            picSample1.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[0]), times: DrawField.CellSize);
            picSample2.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[1]), times: DrawField.CellSize);
            picSample3.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[2]), times: DrawField.CellSize);
        }

        private bool CursorIsInsideDrawField(int x, int y)
        {
            return true;
        }

        private void NoiseAndLoad(int pictureIndex)
        {
            int noise = int.Parse(txtNoise.Text);
            Bitmap noised = BitmapParser.Noise(new Bitmap(bitmapFilePaths[pictureIndex]), noise).ToBitmap();
            Bitmap scaledNoised = BitmapParser.Scale(noised, times: DrawField.CellSize);
            picNoised.Image = scaledNoised;
        }

        private void btnSelect1_Click(object sender, EventArgs e)
        {
            LoadImage(picSample1);
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            LoadImage(picSample2);
        }

        private void btnSelect3_Click(object sender, EventArgs e)
        {
            LoadImage(picSample3);
        }

        private void LoadImage(PictureBox pictureBox)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    int index = int.Parse(pictureBox.Name.Substring(pictureBox.Name.Length - 1)) - 1;
                    bitmapFilePaths[index] = filePath;
                    Debug.WriteLine("loaded image for index {0}", index);
                    pictureBox.Image = BitmapParser.Scale(new Bitmap(filePath), times: DrawField.CellSize);
                }
            }
        }

        private void radio1_CheckedChanged(object sender, EventArgs e)
        {
            NoiseAndLoad(GetCheckedRadioButtonIndex());
        }

        private int GetCheckedRadioButtonIndex()
        {
            var checkedRadioButton = groupBox1.Controls.OfType<RadioButton>().First(r => r.Checked);
            return int.Parse(checkedRadioButton.Text) - 1;
        }

        private void radio2_CheckedChanged(object sender, EventArgs e)
        {
            NoiseAndLoad(GetCheckedRadioButtonIndex());
        }

        private void radio3_CheckedChanged(object sender, EventArgs e)
        {
            NoiseAndLoad(GetCheckedRadioButtonIndex());
        }

        private void btnNoise_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Noise");
            NoiseAndLoad(GetCheckedRadioButtonIndex());
        }

        private void btnRecognizeDrawn_Click(object sender, EventArgs e)
        {
            RecognizeAndLoadPicture(drawField.GetMatrix());
        }

        private void RecognizeAndLoadPicture(Matrix recognizable)
        {
            Debug.WriteLine(recognizable.ToPrettyString());
            Bitmap[] sampleImages = bitmapFilePaths
                .Select(path => new Bitmap(path))
                .ToArray();
            Vector inputVector = recognizable.ToVectorByRows();

            int iterations = 0;
            bool okay = false;
            Perceptron perceptron = null;
            while (!okay)
            {
                perceptron = new Perceptron(
                    pixelsCount: inputVector.Length, 
                    sampleAmmount: sampleImages.Length
                );

                perceptron.Teach(sampleImages, ref iterations);
                Debug.WriteLine("iterations: {0}", iterations);
                if (iterations > 10)
                {
                    okay = true;
                }
            }

            Vector result = perceptron.Recognize(inputVector);
            labelIterations.Text = iterations.ToString();

            Debug.WriteLine("Similarity:");
            Enumerable.Range(0, sampleImages.Length)
                .ToList()
                .ForEach(i => Debug.WriteLine("{0} -> {1}", i, result[i]));

            labelFirst.Text = (result[0] * 100).ToString("0.00") + "%";
            labelSecond.Text = (result[1] * 100).ToString("0.00") + "%";
            labelThird.Text = (result[2] * 100).ToString("0.00") + "%";

            progressBar1.Value = (int)(result[0] * 100);
            progressBar2.Value = (int)(result[1] * 100);
            progressBar3.Value = (int)(result[2] * 100);

            int resultIndex = result.ToList().IndexOf(result.Max()) + 1;
            labelRecognized.Text = resultIndex.ToString();
            Debug.WriteLine("resultIndex: {0}", resultIndex);
        }

        private string GetSelectedBitmapFilePath()
        { 
            return bitmapFilePaths[GetCheckedRadioButtonIndex()];
        }

        private void btnRecognizeNoised_Click(object sender, EventArgs e)
        {
            Matrix noised = BitmapParser.Noise(new Bitmap(GetSelectedBitmapFilePath()), ammount: int.Parse(txtNoise.Text));
            Debug.WriteLine("input:");
            Debug.WriteLine(noised.ToPrettyString());
            RecognizeAndLoadPicture(noised);
        }
    }
}
