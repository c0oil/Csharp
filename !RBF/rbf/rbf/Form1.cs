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
using System.Reflection;

namespace RBFTry
{
    public partial class Form1 : Form
    {
        public const int CellSize = 6;
        public const int CellsCount = 90 / CellSize;
        private const int classAmmount = 3;
        private const int samplesPerClass = 4;

        private Bitmap testImage;
        private List<Bitmap> sampleImages = new List<Bitmap>();
        private string[] bitmapFilePaths;

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

        private void NoiseAndLoad(int pictureIndex)
        {
            int noise = int.Parse(txtNoise.Text);
            Bitmap noised = BitmapParser.Noise(new Bitmap(bitmapFilePaths[pictureIndex]), noise).ToBitmap();
            Bitmap scaledNoised = BitmapParser.Scale(noised, CellSize);
            this.adjustTestImage();
            picNoised.Image = scaledNoised;
        }

        private void LoadSamplePictures()
        {
            bitmapFilePaths = new[] 
            { 
                "a1.bmp", "a2.bmp", "a3.bmp", "a4.bmp", 
                "x1.bmp", "x2.bmp", "x3.bmp", "x4.bmp", 
                "z1.bmp", "z2.bmp", "z3.bmp", "z4.bmp" 
            };
            pictureBox1.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[0]), CellSize);
            pictureBox2.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[1]), CellSize);
            pictureBox3.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[2]), CellSize);
            pictureBox4.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[3]), CellSize);

            pictureBox8.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[4]), CellSize);
            pictureBox7.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[5]), CellSize);
            pictureBox6.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[6]), CellSize);
            pictureBox5.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[7]), CellSize);

            pictureBox12.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[8]), CellSize);
            pictureBox11.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[9]), CellSize);
            pictureBox10.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[10]), CellSize);
            pictureBox9.Image = BitmapParser.Scale(new Bitmap(bitmapFilePaths[11]), CellSize);

            for (int i = 0; i < classAmmount * samplesPerClass; i++)
            {
                sampleImages.Add(new Bitmap(bitmapFilePaths[i]));
            }
        }

        private void RecognizeAndLoadPicture()
        {
            var allSelections = new List<Selection>();
            RBF rbf = new RBF(
                pixelsCount: testImage.Height * testImage.Width,
                classAmount: classAmmount
            );

            for (int i = 0; i < classAmmount; i++)
            {
                var selection = new Selection();
                for (int j = i * samplesPerClass; j < i * samplesPerClass + samplesPerClass; j++)
                {
                    selection.AddSample(sampleImages[j]);
                }
                selection.ClassIndex = i;
                allSelections.Add(selection);
            }

            rbf.Teach(allSelections);
            lblIterations.Text = rbf.Iterations.ToString();

            Vector result = rbf.Recognize(testImage);

            var textBoxes = new List<TextBox> { textBox1, textBox2, textBox3 };
            for (int i = 0; i < classAmmount; i++)
            {
                double percentage = (result[i] * 100);
                textBoxes[i].Text = percentage.ToString("0.0");
            }

            barSimilarity1.Value = (int)(result[0] * 100);
            barSimilarity2.Value = (int)(result[1] * 100);
            barSimilarity3.Value = (int)(result[2] * 100);
        }

        private string GetSelectedBitmapFilePath()
        {
            return bitmapFilePaths[int.Parse(cmbImage.Text) - 1];
        }

        private void adjustTestImage()
        {
            int noiseTreshold = (int)txtNoise.Value;
            Random rand = new Random(DateTime.Now.Millisecond);

            testImage = new Bitmap(GetSelectedBitmapFilePath());

            // invert some random pixels / add noise to image
            for (int i = 0; i < testImage.Height; i++)
            {
                for (int j = 0; j < testImage.Width; j++)
                {
                    if (rand.Next(100) < noiseTreshold)
                    {
                        Color oldColor = testImage.GetPixel(i, j);
                        Color newColor = Color.FromArgb(255 - oldColor.R, 255 - oldColor.G, 255 - oldColor.B);
                        testImage.SetPixel(i, j, newColor);
                    }
                }
            }
        }

        private void btnNoise_Click(object sender, EventArgs e)
        {
            NoiseAndLoad(int.Parse(cmbImage.Text) - 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecognizeAndLoadPicture();
        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoiseAndLoad(int.Parse(cmbImage.Text) - 1);
        }
    }
}
