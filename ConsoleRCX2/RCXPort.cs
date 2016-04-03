using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ConsoleRCX2
{
    class RCXPort
    {
        private SerialPort comport;
        private String serialOutput;
        public RCXPort()
        {
            serialOutput = "";
            comport=new SerialPort("COM3", 2400, Parity.Odd, 8, StopBits.One);
            comport.DataReceived += Comport_DataReceived;
            comport.DtrEnable = true;
            comport.RtsEnable = true;
            comport.Open();
        }

        public void send(string userInput)
        {
            byte[] userData = setBytes(userInput);
            comport.Write(userData, 0, userData.Length);
        }

        private void Comport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = comport.BytesToRead;
            // Create a byte array buffer to hold the incoming data
            byte[] buffer = new byte[bytes];
            // Read the data from the port and store it in our buffer
            comport.Read(buffer, 0, bytes);
            // Show the user the incoming data in hex format
            //RCXResponseText = ByteArrayToHexString(buffer);
            serialOutput = ByteArrayToHexString(buffer);
            //serialOutput += ",";
        }

        private byte[] setBytes(string byteString)
        {
            byte sum = 0;
            List<byte> byteList = new List<byte>();
            string[] byteStringArray = byteString.Split(' ');
            byteList.Add((byte)(0x55));
            byteList.Add((byte)(0xff));
            byteList.Add((byte)(0x00));
            foreach (var currByte in byteStringArray)
            {
                byte xByte = (byte)Convert.ToByte(currByte, 16);
                byteList.Add(xByte);
                byte notxByte = (byte)(~xByte);
                byteList.Add(notxByte);
                sum += xByte;
            }
            byteList.Add((byte)(sum & 0xff));
            byteList.Add((byte)(~sum & 0xff));

            return byteList.ToArray();
        }

        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }


        private static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return sb.ToString().ToUpper();
        }


    }
}
