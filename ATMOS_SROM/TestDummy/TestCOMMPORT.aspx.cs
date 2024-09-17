using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.TestDummy
{
    public partial class TestCOMMPORT : System.Web.UI.Page
    {
        private SerialPort _serialPort;
        #region "METHHOD"
        // Untuk split string setiap n karakter
        public static List<string> SplitIntoParts(string s, int partLength)
        {
            var list = new List<string>();

            if (!string.IsNullOrEmpty(s) && partLength > 0)
            {
                for (var i = 0; i < s.Length; i += partLength)
                {
                    list.Add(s.Substring(i, Math.Min(partLength, s.Length - i)));
                }
            }

            return list;
        }
        // untuk menghitung nilai CRC dengn XoR
        private string XorText(List<string> listtocount)
        {
            string newText = "";
            string ResText = "";
            int charValue = 01;
            for (int i = 0; i < listtocount.Count; i++)
            {
                int key = Convert.ToInt32(listtocount[i]); //get the ASCII value of the character
                charValue = charValue ^= key; //xor the value

                newText = charValue.ToString();//newText += char.ConvertFromUtf32(charValue); //convert back to string
            }

            const int MaxLogMessageLength = 2;

            int n = Encoding.Unicode.GetByteCount(newText);

            if (n > MaxLogMessageLength)
            {
                newText = newText.Substring(0, MaxLogMessageLength / 2); // Most UTF16 chars are 2 bytes.

                while (Encoding.Unicode.GetByteCount(newText) > MaxLogMessageLength)
                    newText = newText.Substring(0, newText.Length - 1);
            }
            return newText;
        }
        //untuk ubah format string jadi 0X...
         private string to0XText(List<string> listtocount)
        {
            string newText = "";
       
            for (int i = 0; i < listtocount.Count; i++)
            {
                if (newText == "")
                {
                    newText = "0X" + listtocount[i];
                }
                else
                {
                    //if (i == listtocount.Count - 1)
                    //{
                    //    newText = newText + ", 0X" + newText.Substring(0, 1);
                    //}
                    //else
                    //{
                        newText = newText + ", 0X" + listtocount[i];
                    //}
                }
            }
             return newText;
         }
         //untuk ubah hexadecimal number jadi ASCII
         public static string ConvertHex(String hexString)
         {
             string ascii = "";
             try
             {
                 for (int i = 0; i < hexString.Length; i += 2)
                 {
                     String hs = string.Empty;

                     hs = hexString.Substring(i, 2);
                     uint decval = System.Convert.ToUInt32(hs, 16);
                     char character = System.Convert.ToChar(decval);
                     ascii += character;
                 }
             }
             catch (Exception ex) { Console.WriteLine(ex.Message); }
             return ascii;
         }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            _serialPort = new SerialPort();
            _serialPort.PortName = "COM4";// SerialPort.GetPortNames()[0];
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.RtsEnable = true;
            _serialPort.DtrEnable = true;
            _serialPort.Handshake = Handshake.None;
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
     
        }
      
       
        #region "GET PORT"
        public List<string> GetAllPorts()
        {
            List<String> allPorts = new List<String>();
            foreach (String portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                allPorts.Add(portName);
            }
            return allPorts;
        }
        #endregion

        protected void btnSendEDC_Click(object sender, EventArgs e)
        {
           //_serialPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
           //_serialPort.ReadTimeout = 500;
           //_serialPort.WriteTimeout = 500;
           // _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
            byte[] ba = Encoding.Default.GetBytes(txtamount.Text);
            var hexStringHEaderSale1 = "02";
            var hexStringHEaderSale2 = "01";
            var hexStringHEaderSale3 = "3130";
            var hexstringcloser = "03";
            var hexString = BitConverter.ToString(ba);
            hexString = hexString.Replace("-", "");

            byte[] ba2 = Encoding.Default.GetBytes(txtaddamount.Text);
            var hexString2 = BitConverter.ToString(ba2);
            hexString2 = hexString2.Replace("-", "");

            int hexlenght = hexString.Length / 2;
            int hexlenght2 = hexString2.Length / 2;
            for (int i = hexlenght; i < 12; i++)
            {
                hexString = "30" + hexString;
                hexlenght = hexString.Length / 2;
            }
            for (int i = hexlenght2; i < 12; i++)
            {
                hexString2 = "30" + hexString2;
                hexlenght2 = hexString2.Length / 2;
            }
            String tosplit = hexStringHEaderSale3 + hexString + hexString2 + hexstringcloser;
            List<String> listsplit = SplitIntoParts(tosplit, 2);
            string XorRes = XorText(listsplit);
            var HextoEDC = hexStringHEaderSale1 + hexStringHEaderSale2 + hexStringHEaderSale3 + hexString + hexString2 + hexstringcloser + XorRes ;
            List<String> listsplit2 = SplitIntoParts(HextoEDC, 2);
            //listsplit2.Add(XorRes);
            string to0XTextRRes = to0XText(listsplit2);
            //int HextoEDClenght = HextoEDC.Length / 2;
            //for (int i = HextoEDClenght; i < 60; i++)
            //{
            //    HextoEDC = "0X" + HextoEDC;
            //    HextoEDClenght = HextoEDC.Length / 2;
            //}

            //Echo Test
            //String tosplit = "02013B30032";
            ////List<String> listsplit = SplitIntoParts(tosplit, 2);
            ////string XorRes = XorText(listsplit);
            //var HextoEDC = tosplit ;

            lblPORT.Text = HextoEDC;//to0XTextRRes;//ConvertHex(HextoEDC);
            //Label1.Text = HextoEDC;
            byte[] array = System.Text.Encoding.UTF8.GetBytes(lblPORT.Text);
            try
            {
                if (!(_serialPort.IsOpen))
                    _serialPort.Open();
                var datasend = array;//new byte[] {Convert.ToByte(HextoEDC)};
                //_serialPort.Encoding = System.Text.Encoding.ASCII;
                _serialPort.Write(datasend, 0, datasend.Length);
                if (_serialPort.BytesToRead > 0) //if there is data in the buffer
                {
                    _serialPort.ReadByte(); //read a byte
                }
                string message = _serialPort.ReadExisting();
                _serialPort.Close();
                //Console.WriteLine(message);
                Label1.Text = message;//"OK";
            }
            catch (Exception ex)
            {
                Label1.Text = "Error opening/writing to serial port :: " + ex.Message + " Error!";
            }  

        }

       
        private void _serialPort_DataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            byte[] comBuffer = new byte[304];
            int lengh = _serialPort.Read(comBuffer, 0, 304);
            Array.Resize(ref comBuffer, lengh);

            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
        }
    }
}