using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiClientChatApp
{
    public partial class MainForm : Form
    {

        TcpClient tcpClient;
        NetworkStream networkStream;

        protected delegate void UpdateDisplayDelegate(string message);

        public MainForm()
        {
            InitializeComponent();
        }

        private void AddMessage(string message) 
        {
            if (Chatscreen.InvokeRequired)
            {
                Chatscreen.Invoke(new UpdateDisplayDelegate(UpdateDisplay), new object[] { message });
            }
            else
            {
                UpdateDisplay(message);
            }
        }

        private void UpdateDisplay(string message)
        {
            Chatscreen.Items.Add(message);
        }

        private async Task CreateServerAsync()
        {
            int portNumber = ParseStringToInt(PortInputBox.Text);
            int bufferSize = ParseStringToInt(BufferInputBox.Text);
            if (!checkInputForErrors(portNumber, bufferSize)) { return; }

            TcpListener tcpListener = new TcpListener(IPAddress.Any, portNumber);
                tcpListener.Start();
                MessageInput.Enabled = false;
                SendMessageButton.Enabled = false;
                CreateServerButton.Enabled = false;
                ConnectButton.Enabled = false;
                AddMessage($"Server is waiting for clients...");
                tcpClient = await Task.Run(() => tcpListener.AcceptTcpClientAsync());
                await Task.Run(() => ReceiveData(bufferSize));
            
        }

        private async void CreateServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                await CreateServerAsync();
            }
            catch
            {
                MessageBox.Show("A server has already been opened.");
            }
        }

        private async void ReceiveData(int bufferSize)
        {
            string message = "";
            byte[] buffer = new byte[bufferSize];

            networkStream = tcpClient.GetStream();
            AddMessage("Connected!");

             while (networkStream.CanRead)
            {

                StringBuilder fullMessage = new StringBuilder();

                do
                {
                    try
                    {
                        int readBytes = await networkStream.ReadAsync(buffer, 0, bufferSize);//Goed kijken naar de waardes
                        message = Encoding.ASCII.GetString(buffer, 0, readBytes);
                        fullMessage.Append(message);
                    }
                    catch (InvalidOperationException exception)
                    {
                        MessageBox.Show(exception.Message, "No connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        message = "bye";
                        break;
                    }
                }
                while (networkStream.DataAvailable);
                
                if (message == "bye")
                {
                    break;
                }
                AddMessage(message);
            }
            buffer = Encoding.ASCII.GetBytes("bye");
            networkStream.Write(buffer, 0, buffer.Length);

            
            networkStream.Close();
            tcpClient.Close();

            AddMessage("Connection closed");
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                await CreateConnectionAsync();
            }
            catch
            {
                MessageBox.Show("An error has occured");
            }
        }

        private async Task CreateConnectionAsync()
        {
            int portNumber = ParseStringToInt(PortInputBox.Text);
            int bufferSize = ParseStringToInt(BufferInputBox.Text);


            if (!checkInputForErrors(portNumber, bufferSize)) { return;}
            try
            {
                AddMessage("Connecting");
                tcpClient = await Task.Run(() => new TcpClient(IpInputBox.Text, portNumber));
                await Task.Run(() => ReceiveData(bufferSize));
                CreateServerButton.Enabled = false;
                NameInputBox.Enabled = false;
            }
            catch (SocketException exception)
            {
                MessageBox.Show(exception.Message, "No connection is possible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            string message = $"{NameInputBox.Text}:{MessageInput.Text}";

            byte[] buffer = Encoding.ASCII.GetBytes(message);
            networkStream.Write(buffer, 0, buffer.Length);

            AddMessage(message);
            MessageInput.Clear();
            MessageInput.Focus();
        }
        private int ParseStringToInt(string stringParam)
        {
            int number;
            int.TryParse(stringParam, out number);

            return number;
        }

        public bool CheckIpAddressValidation(string ipAdress)
        {

            string[] splitValues = ipAdress.Split('.');
            if (splitValues.Length != 4 || String.IsNullOrWhiteSpace(ipAdress)|| splitValues[3]=="")
            {
                return false;
            }

            byte readyByte;

            return splitValues.All(r => byte.TryParse(r, out readyByte));
        }

        private bool checkInputForErrors(int portNumber, int bufferSize)
        {
            if (!CheckIpAddressValidation(IpInputBox.Text))
            {
                MessageBox.Show("Ip address is invalid, try again", "Invalid IP address", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!(portNumber >= 1024 && portNumber <= 65535))
            {
                MessageBox.Show("Port number is not valid, try again", "Invalid Port number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (bufferSize < 1)
            {
                MessageBox.Show("Buffersize is not valid, try again", "Invalid buffer size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void IpLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
