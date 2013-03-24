using System.Windows.Media.Imaging;

namespace cosii5
{
    public partial class FullImage
    {
        public FullImage()
        {
            InitializeComponent();
        }

        public void Initialize(BitmapSource sourceImage)
        {
            image.Source = sourceImage;
            image.Height = sourceImage.PixelHeight;
            image.Width = sourceImage.PixelWidth;
            image.UpdateLayout();
        }
    }
}
