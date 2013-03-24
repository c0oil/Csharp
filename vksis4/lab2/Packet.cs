using System.Collections.Generic;
using System.Linq;

namespace lab2
{
    class Packet
    {
        public const int Size = 26;
        public const int DataSize = 21;

        public byte Flag { get; set; }
        public byte DestinationAddress { get; set; }
        public byte SourceAddress { get; set; }
        public byte[] Data { get; set; }
        public byte[] CheckSum { get; set; }

        public byte[] ToStaffedArray()
        {
            var data = new List<byte> { SourceAddress, DestinationAddress};
            data.AddRange(Data);
            data.AddRange(CheckSum);
            data = BitStaffing.Staffing(data).ToList();
            data.Insert(0, Flag);
            return data.ToArray();
        }
    }
}
