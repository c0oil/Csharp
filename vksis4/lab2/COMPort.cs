using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;

namespace lab2
{
    public class COMPort
    {
        private SerialPort port;
        public byte[] RecentReciveBytes { get; set; }

        public COMPort(String name, int speed, SerialDataReceivedEventHandler handler)
        {
            string[] portnames = SerialPort.GetPortNames();
            if (!portnames.Contains(name))
            {
                throw new Exception();
            }

            Initialize(name, speed, handler);
        }

        private void Initialize(String name, int speed, SerialDataReceivedEventHandler handler)
        {
            port = new SerialPort(name, speed, Parity.None, 8, StopBits.One);            
            port.DataReceived += DataReceived;
            port.DataReceived += handler;
            port.ErrorReceived += ErrorReceived;
            port.Open();  
        }

        public void Close()
        {
            port.Close();
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs eventArgs)
        {
            RecentReciveBytes = new byte[port.BytesToRead];
            port.Read(RecentReciveBytes, 0, RecentReciveBytes.Length);
        }

        private void ErrorReceived(object sender, SerialErrorReceivedEventArgs eventArgs)
        {

        }

        public void SendData(byte[] data)
        {
            port.RtsEnable = true;
            port.Write(data, 0, data.Length);
            Thread.Sleep(100);
            port.RtsEnable = false;
        }

        
    }
}
