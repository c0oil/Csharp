using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using cosii5.MVVMUtility;

namespace lab2
{
    public class MainViewModel : ViewModelBase
    {
        private COMPort port;
        private readonly StationAddress stationAddress = new StationAddress();

        public RelayCommand StartCommand { get; set; }
        public RelayCommand SendCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand AddGroupCommand { get; set; }

        #region Bindable properties

        public string[] Ports { get; set; }
        public int[] MaxSpeeds { get; set; }
        public byte NewGroup { get; set; }
        public string SendingMessage { get; set; }

        private string multicastGroups;
        public string MulticastGroups
        {
            get { return multicastGroups; }
            set
            {
                multicastGroups = value;
                OnPropertyChanged("MulticastGroups");
            }
        }
        
        private string recivedMessage;
        public string RecivedMessage
        {
            get { return recivedMessage; }
            set
            {
                recivedMessage = value;
                OnPropertyChanged("RecivedMessage");
            }
        }

        private bool hasStarted;
        public bool HasStarted 
        {
            get { return hasStarted; }
            set
            {
                hasStarted = value;
                OnPropertyChanged("HasStarted");
            }
        }

        private string selectedPort;
        public string SelectedPort
        {
            get { return selectedPort; }
            set
            {
                selectedPort = value;
                OnPropertyChanged("SelectedPort");
            }
        }

        private int selectedSpeed = 19200;
        public int SelectedSpeed
        {
            get { return selectedSpeed; }
            set
            {
                selectedSpeed = value;
                OnPropertyChanged("SelectedSpeed");
            }
        }

        private byte sourceAddress;
        public byte SourceAddress
        {
            get { return sourceAddress; }
            set
            {
                sourceAddress = value;
                OnPropertyChanged("SourceAddress");
            }
        }

        private byte destinationAddress;
        public byte DestinationAddress
        {
            get { return destinationAddress; }
            set
            {
                destinationAddress = value;
                OnPropertyChanged("DestinationAddress");
            }
        }
        #endregion

        public MainViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            MaxSpeeds = new[] { 110, 150, 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 };

            Ports = new string[16];
            for (int i = 0; i < 16; i++)
            {
                Ports[i] = ("COM" + (i + 1).ToString());
            }
            SelectedPort = Ports[0];

            MulticastGroups = stationAddress.GetGroups();

            StartCommand = new RelayCommand(OnStart) { IsEnabled = true };
            SendCommand = new RelayCommand(OnSend) { IsEnabled = true };
            ClearCommand = new RelayCommand(OnClear) { IsEnabled = true };
            AddGroupCommand = new RelayCommand(OnAddGroup) { IsEnabled = true };
        }

        private void OnAddGroup()
        {
            stationAddress.AddMulticast(NewGroup);
            MulticastGroups = stationAddress.GetGroups();
        }

        private void OnClear()
        {
            RecivedMessage = string.Empty;
        }

        private void OnSend()
        {
            var data = new List<byte>();
            var bytes = Encoding.ASCII.GetBytes(SendingMessage);
            for (int i = 0; i < bytes.Length; i += Packet.DataSize)
            {
                var packet = new Packet
                    {
                        Flag = BitStaffing.BBeginFlag,
                        SourceAddress = SourceAddress,
                        DestinationAddress = DestinationAddress,
                        Data = bytes.Skip(i).Take(Packet.DataSize).ToArray(),
                    };
                packet.CheckSum = BitStaffing.CRC(packet.Data).ToArray();

                // Set adress

                data.AddRange(packet.ToStaffedArray());
            }

            port.SendData(data.ToArray());
        }

        private void OnStart()
        {
            port = new COMPort(SelectedPort, SelectedSpeed, DataReceived);
            HasStarted = true;
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs eventArgs)
        {
            var data = new List<byte>();
            var reciveBytes = port.RecentReciveBytes;

            for (int i = 0; i < reciveBytes.Length;)
            {
                var messageWithoutFlag = reciveBytes.Skip(i + 1).TakeWhile(x => x != BitStaffing.BBeginFlag).ToArray();
                var unstaffedMessage = BitStaffing.Unstaffing(messageWithoutFlag).Take(Packet.Size - 1).ToArray();
                i += messageWithoutFlag.Length + 1;
                var packet = new Packet
                {
                    Flag = BitStaffing.BBeginFlag,
                    SourceAddress = unstaffedMessage[0],
                    DestinationAddress = unstaffedMessage[1],
                    Data = unstaffedMessage.Skip(2).Take(unstaffedMessage.Length - 4).ToArray(),
                    CheckSum = unstaffedMessage.Skip(unstaffedMessage.Length - 2).Take(2).ToArray(),
                };

                // Check adress
                if (!stationAddress.CheckPacketsAddress(SourceAddress, packet.DestinationAddress))
                {
                    return;
                }

                if (!BitStaffing.CheckCRC(packet.Data.Concat(packet.CheckSum).ToArray()))
                {
                    return;
                }

                data.AddRange(packet.Data.Take(Packet.DataSize));
            }

            RecivedMessage = Encoding.UTF8.GetString(data.ToArray());
        }
    }
}
