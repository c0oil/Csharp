using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab2
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }        

/*
        private void start_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Com = new COMPort(viewModel.SelectedPort, viewModel.SelectedSpeed);

            text1.Background = Brushes.White;

            start.IsEnabled = false;
            stop.IsEnabled = true;
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Com.Close();
            text1.Clear();
            
            text1.Background = text3.Background;
            
            start.IsEnabled = true;
            stop.IsEnabled = false;
        }*/

        /*private void text1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                System.Text.Encoding encoding = System.Text.Encoding.ASCII;
                viewModel.Com.SendData(encoding.GetBytes(text1.Text));
                text1.Clear();
            }
            
        }*/

        /*private int prevNumPackets = 0;
        private void text1_TextChanged(object sender, TextChangedEventArgs e)
        {  
            List<int> line = BitStaffing.Staffing(text1.Text);            
            int sizePacket = 21 * 8;            

            if ((line.Count / sizePacket) != prevNumPackets)
            {
                prevNumPackets = line.Count / sizePacket;
                if ((line.Count / sizePacket) == 0)
                {
                    return;
                }
                List<int> dst = new List<int> { 0, 0, 0, 0, 0, 0, 0, 1 },
                        src = new List<int> { 0, 0, 0, 0, 0, 0, 1, 0 },
                        crc,
                        beginFlag = new List<int>() { 0, 1, 1, 1, 1, 1, 1, 0 };                
                
                beginFlag.AddRange(dst);
                beginFlag.AddRange(src);
                beginFlag.AddRange(line.GetRange(0, line.Count - line.Count % sizePacket));  
                beginFlag.AddRange(crc = BitStaffing.CRC(beginFlag));

                if (haveError.IsChecked == true)
                {
                    Random rand = new Random();
                    int changedElement = rand.Next(dst.Count + src.Count - 1, beginFlag.Count - crc.Count - 1);
                    indexError.Text = changedElement.ToString();
                    beginFlag[changedElement] = (beginFlag[changedElement] + 1) % 2;
                    line[changedElement - dst.Count - src.Count - 1] = (line[changedElement - dst.Count - src.Count - 1] + 1) % 2;

                }

                if (!BitStaffing.CheckCRC(beginFlag, crc))
                {
                    text3.Background = Brushes.Red;
                }
                else
                {
                    text3.Background = Brushes.Green;
                }

                text2.Text = BitStaffing.BoolToString(beginFlag);

                text3.Text = BitStaffing.Unstaffing(line.GetRange(0, line.Count - line.Count % sizePacket));
            }
            else
            {
                text3.Background = Brushes.LightGray;
                indexError.Text = "none";
            }

        }*/

        private void addGroupButton_Click(object sender, RoutedEventArgs e)
        {
                
        }

        private void addressTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                
        }
    }
}
