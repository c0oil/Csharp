using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab2
{
    class StationAddress
    {
        private const byte Unicast = 127;
        private const byte Broadcast = 255;
        private readonly List<byte> multicast = new List<byte> { 254 };

        public void AddMulticast(byte addr)
        {
            multicast.Add(addr);
        }

        public bool CheckPacketsAddress(byte own, byte receiveDest)
        {
            if (receiveDest == Broadcast)
            {
                return true;
            }

            if (Unicast < receiveDest )
            {
                return multicast.Any(addr => addr == receiveDest);
            }

            if (own <= Unicast)
            {
                return own == receiveDest;
            }

            return false;
        }

        public string GetGroups()
        {
            var t = multicast.Select(x => x.ToString()).ToList();
            return String.Join(", ", t);
        }
    }
}
