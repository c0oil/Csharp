using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DIP6
{
    public partial class Form1 : Form
    {
        public const int Size = 12;

        private ConcurrentNetwork network;
        private bool[][] Picture;
        private PictureBox picture;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            Picture = new bool[Size][];
            for (var i = 0; i < Size; i++)
                Picture[i] = new bool[Size];
        }

        public void ReadFromFile(string path)
        {
            var sr = new StreamReader(path);
            for (var i = 0; i < Size; i++)
            {
                var s = sr.ReadLine();
                for (int j = 0; j < Size; j++)
                    Picture[i][j] = s[j] == '1' ? true : false;
            }
        }

        public void DrawPicture(PictureBox pictureBox)
        {
            var brCl = new SolidBrush(Color.White);
            var brFl = new SolidBrush(Color.Black);

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    var br = Picture[i][j] ? brFl : brCl;
                    pictureBox.CreateGraphics().FillRectangle(br, i * 10, j * 10, 10, 10);
                }
            }
        }

        public void Noise(int pc)
        {
            var size2 = Size * Size;
            var n = (int)(1.44 * pc);
            n = n >= size2 ? size2 : n;
            var parr = new int[size2];
            var parr2 = new int[size2];
            var r = new Random();
            for (var i = 0; i < size2; i++)
            {
                parr2[i] = r.Next(200);
                parr[i] = i;
            }
            for (var i = 1; i < size2; i++)
            {
                if (parr2[i] >= parr2[i - 1]) continue;
                var tmp = parr2[i];
                parr2[i] = parr2[i - 1];
                parr2[i - 1] = tmp;
                tmp = parr[i];
                parr[i] = parr[i - 1];
                parr[i - 1] = tmp;
                i = 0;
            }
            for (var i = 0; i < n; i++)
                Picture[parr[i] / Size][parr[i] % Size] = !Picture[parr[i] / Size][parr[i] % Size];

        }

        public double[] ToDouble()
        {
            var tarr = new double[Size * Size];
            for (var i = 0; i < Size * Size; i++)
                tarr[i] = Picture[i / Size][i % Size] ? 1.0 : 0.0;
            return tarr;
        }

        private void bntNoize_Click(object sender, EventArgs e)
        {
            var noize = 0;
            try
            {
                noize = int.Parse(tbxNoize.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid argument");
            }
            Noise(noize);
            picture = pictureForReproduce;
            DrawPicture(picture);
        }

        private void bntTeach_Click(object sender, EventArgs e)
        {
            var imgs = new double[3][];
            var file = new OpenFileDialog();

            for (var i = 0; i < 3; i++)
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    ReadFromFile(file.FileName);
                    imgs[i] = ToDouble();
                }
                switch (i)
                {
                    case 0:
                        picture = pictureClass1;
                        break;
                    case 1:
                        picture = pictureClass2;
                        break;
                    case 2:
                        picture = pictureClass3;
                        break;
                }
                DrawPicture(picture);
            }

            network = new ConcurrentNetwork(
                clusterSize: Size * Size,
                clusterCount: 2
            );
            network.Teach(imgs);
            String classStr = "Class ";
            lblClass1.Text = classStr + (network.Output[0] + 1);
            lblClass2.Text = classStr + (network.Output[1] + 1);
           // lblClass3.Text = classStr + (network.Res[2] + 1);
        }

        private void btnReproduce_Click(object sender, EventArgs e)
        {
            lblReproducedClass.Text = "Class " + (network.Reproduct(ToDouble()) + 1);
        }

        private void bntLoad_Click(object sender, EventArgs e)
        {
            var file = new OpenFileDialog { Multiselect = false };

            if (file.ShowDialog() != DialogResult.OK) return;
            ReadFromFile(file.FileName);
            picture = pictureForReproduce;
            DrawPicture(picture);
        }
    }
}
