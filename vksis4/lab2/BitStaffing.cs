using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab2
{
    public static class BitStaffing
    {
        private const int NumBitInChar = 8;
        private const int SizeCRC = 16;
        private static readonly List<int> Polinom16 = new List<int>();

        private static readonly List<int> BeginFlag = new List<int> { 0, 1, 1, 1, 1, 1, 1 };
        private static readonly List<int> MBeginFlag = new List<int> { 0, 1, 1, 1, 1, 1, 1, 1 };

        public static byte BBeginFlag = 0x7E;

        static BitStaffing()
        {
            for (int i = 0; i < SizeCRC + 1; i++)
                Polinom16.Add(0);
            Polinom16[16] = 1;
            Polinom16[15] = 1;
            Polinom16[2] = 1;
            Polinom16[0] = 1;
            Polinom16.Reverse();
        }

/*        private static IEnumerable<int> GetBitSequence(Char sign)
        {
            var bitList = new List<int>(NumBitInChar);
            int code = sign;
            for (int i = 0; i < NumBitInChar; i++)
            {
                bitList.Add(code % 2);
                code /= 2;
            }
            bitList.Reverse(0, NumBitInChar);

            return bitList;
        }

        private static string GetCharSequence(IEnumerable<int> bitList)
        {
            string str = "";
            int code = 0, i = NumBitInChar - 1;
            foreach (var bit in bitList)
            {                
                code += bit * (int)Math.Pow(2, i);    
                if (i == 0)
                {
                    str += (Char)code;
                    code = 0;
                    i = NumBitInChar;
                }
                i--;
            }
            return str;
        }*/

        private static int FindFlag(IEnumerator<int> i, IEnumerable<int> flag)
        {
            int numUnits = 0;
            foreach (var bit in flag)
            {
                if (!i.MoveNext())
                    return numUnits;
                if (bit == i.Current)                                    
                    numUnits++;                
                else
                    break;                
            }
            return numUnits;
        }

        public static IEnumerable<byte> Staffing(IEnumerable<byte> message)
        {
            var bitList = new List<int>(NumBitInChar * message.Count());
            foreach (var sign in message)
            {
                bitList.AddRange(HexToBool(new List<byte> { sign }));
            }

            for (int i = 0, numUnits; i < bitList.Count - BeginFlag.Count; i += numUnits)
            {
                numUnits = FindFlag(bitList.GetRange(i, BeginFlag.Count).GetEnumerator(), BeginFlag);
                if (numUnits == BeginFlag.Count)
                {
                    bitList.Insert(i + 6, 1);
                    i++;
                }
                if (numUnits == 0)
                {
                    numUnits = 1;
                }
            }

            return BoolToHex(bitList);
        }

        public static IEnumerable<byte> Unstaffing(IEnumerable<byte> byteList)
        {
            var bitList = HexToBool(byteList).ToList();
            for (int i = 0, numUnits; i < bitList.Count - MBeginFlag.Count; i += numUnits)
            {
                numUnits = FindFlag(bitList.GetRange(i, MBeginFlag.Count).GetEnumerator(), MBeginFlag);
                if (numUnits == MBeginFlag.Count)
                {
                    bitList.RemoveAt(i + 7);
                    i--;
                }
                if (numUnits == 0)
                {
                    numUnits = 1;
                }
            }
            //hack
            while (bitList.Count % 8 != 0)
                bitList.RemoveAt(bitList.Count - 1);
            return BoolToHex(bitList);
        }

        private static IEnumerable<byte> BoolToHex(List<int> bitList)
        {
            int remainder = (bitList.Count % 8) == 0 ? 0 : (8 - bitList.Count % 8);
            for (int j = 0; j < remainder; j++)
            {
                bitList.Add(0);
            }

            var hexData = new List<byte>();
            byte oneByte = 0;
            int i = 7;
            foreach (var bit in bitList)
            {
                oneByte += (byte)(bit * Math.Pow(2, i--));
                if (i < 0)
                {
                    hexData.Add(oneByte);
                    i = 7;
                    oneByte = 0;
                }
            }
            return hexData;
        }

        private static IEnumerable<int> HexToBool(IEnumerable<byte> byteList)
        {
            var bitList = new List<int>();
            foreach (var b in byteList)
            {
                var oneByte = b;
                for (int j = 7; j >= 0; j--)
                {
                    int bit = oneByte / (int)Math.Pow(2, j);
                    oneByte -= (byte)(bit * Math.Pow(2, j));
                    bitList.Add(bit); 
                }
            }
            return bitList;
        }

        /*public static string BoolToString(List<int> bitList)
        {
            int remainder = (bitList.Count % 8) == 0 ? 0 : (8 - bitList.Count % 8);
            for (int j = 0; j < remainder; j++)
            {
                bitList.Add(0);
            }

            string str = "";
            int i = 8;
            foreach (var bit in bitList)
            {
                str += bit;
                i--;
                if (i == 0)
                {
                    str += ' ';
                    i = 8;
                }
            }
            return str;
        }*/

        /*public static string BoolToHexString(List<int> bitList)
        {
            string str = "";
            int i = 8, num = 0;
            foreach (var bit in bitList)
            {
                i--;
                num += bit * (int)Math.Pow(2, i);
                if (i == 0)
                {
                    str += num.ToString("X2") + ' ';
                    num = 0;
                    i = 8;
                }
            }
            return str;
        }*/

        public static IEnumerable<byte> CRC(IEnumerable<byte> byteList)
        {
            var data = HexToBool(byteList).ToList();
            List<int> crc = data.Take(SizeCRC + 1).ToList();
            for (int i = 0; i < SizeCRC; i++)
                data.Add(0);

            for(int i = SizeCRC + 1; i < data.Count; i++)
            {
                if (crc.First() == 1)
                {
                    crc = SumCRC(crc).ToList();                    
                }
                crc.RemoveAt(0);
                crc.Add(data[i]);
            }

            if (crc.First() == 1)
            {
                crc = SumCRC(crc).ToList(); 
            }
            crc.RemoveAt(0);
            

            for (int i = 0; i < SizeCRC; i++)
                data.RemoveAt(data.Count - 1);
            return BoolToHex(crc);
        }

        public static bool CheckCRC(IEnumerable<byte> recieveData)
        {
            return CRC(recieveData).All(x => x == 0);
        }

        private static IEnumerable<int> SumCRC(IEnumerable<int> first)
        {
            return first.Zip(Polinom16, (x, y) => (x + y) % 2);
        }
    }
}
