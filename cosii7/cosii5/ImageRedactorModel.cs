using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using Microsoft.Win32;
using cosii5.MVVMUtility;

namespace cosii5
{
    public class ImageRedactorModel : ViewModelBase
    {
        private const string SourcePath = @"D:\www_goodfon_ru\12828-1366x768.jpg";
        public DigitalSignalProcessor Dsp { get; set; }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand FullScreenCommand { get; private set; }

        #region Bindble properties
        private BitmapSource sourceImage;
        public BitmapSource SourceImage 
        {
            get { return sourceImage; }
            set
            {
                sourceImage = value;
                OnPropertyChanged("SourceImage");
            }
        }

        private BitmapSource grayscaleImage;
        public BitmapSource GrayscaleImage
        {
            get { return grayscaleImage; }
            set
            {
                grayscaleImage = value;
                OnPropertyChanged("GrayscaleImage");
            }
        }

        private BitmapSource binarImage;
        public BitmapSource BinarImage
        {
            get { return binarImage; }
            set
            {
                binarImage = value;
                OnPropertyChanged("BinarImage");
            }
        }

        private BitmapSource targetImage;
        public BitmapSource TargetImage
        {
            get { return targetImage; }
            set
            {
                targetImage = value;
                OnPropertyChanged("TargetImage");
            }
        }

        public string BinarScore { get; set; }
        public string Clusters { get; set; }
        #endregion

        public ImageRedactorModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            SourceImage = new BitmapImage(new Uri(SourcePath));
            StartCommand = new RelayCommand(OnStart) { IsEnabled = true };
            Dsp = new DigitalSignalProcessor();
            BinarScore = "150";
            Clusters = "4";
        }

        private void OnStart()
        {
            Dsp.Width = SourceImage.PixelWidth;
            Dsp.Height = SourceImage.PixelHeight;
        }

        public void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { InitialDirectory = @"D:\www_goodfon_ru" };
	        if (ofd.ShowDialog() == true)
	        {
                SourceImage = new BitmapImage(new Uri(ofd.FileName));
	        }
        }

        public void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public static void SaveImageToFile(string filePath, BitmapSource image)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }
    }
}
