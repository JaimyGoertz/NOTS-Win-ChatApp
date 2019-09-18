using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
            SendMessageButton.Enabled = false;
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

            try
            {
                TcpListener tcpListener = new TcpListener(IPAddress.Any, portNumber);
                tcpListener.Start();
                MessageInput.Enabled = false;
                SendMessageButton.Enabled = false;
                ConnectButton.Enabled = false;
                PortInputBox.Enabled = false;
                NameInputBox.Enabled = false;
                BufferInputBox.Enabled = false;
                IpInputBox.Enabled = false;
                AddMessage($"Server is waiting for clients on port: {portNumber}");
                while (true)
                {
                    tcpClient = await Task.Run(() => tcpListener.AcceptTcpClientAsync());
                    SendMessageButton.Enabled = true;
                    await Task.Run(() => ReceiveData(bufferSize));
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Cannot start a server", "Server error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            using (networkStream = tcpClient.GetStream())
            {
                AddMessage("Connected!");

                while (networkStream.CanRead)
                {

                    StringBuilder fullMessage = new StringBuilder();

                    do
                    {
                        try
                        {
                            int readBytes = await networkStream.ReadAsync(buffer, 0, bufferSize);
                            message = Encoding.ASCII.GetString(buffer, 0, readBytes);
                            fullMessage.Append(message);
                        }
                        catch (IOException)
                        {
                            message = "bye";
                            break;
                        }
                    }
                    while (networkStream.DataAvailable);

                    string decodedType = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\@)(.*?)(?=\|{2})"));
                    string decodedUsername = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\|{2})(.*?)(?=\|{2})"));
                    string decodedMessage = DecodeMessage(ProtocolRegexCheck(ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"\|(?:.(?!\|))+$")), new Regex(@"(?<=\|{2})(.*?)(?=\@)")));

                    if (decodedType == "INFO" && decodedMessage == "disconnect")
                    {
                        break;
                    }

                    else if (decodedType == "MESSAGE")
                    {
                        AddMessage($"{decodedUsername}: {decodedMessage}");
                        // Send message to other clients.
                    }
                }
            }
            tcpClient.Close();
            AddMessage("Connection closed");
        }

        private string ProtocolRegexCheck(string message, Regex regex)
        {
            return regex.Match(message).ToString();
        }

        private string DecodeMessage(string str)
        {
            str = Regex.Replace(str, "[&#124]", "|");
            str = Regex.Replace(str, "[&#64]", "@");

            return str;
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
            if (String.IsNullOrWhiteSpace(NameInputBox.Text))
            {
                MessageBox.Show("No username has been given", "No username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkInputForErrors(portNumber, bufferSize)) { return; }
            AddMessage("Connecting");
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(IpInputBox.Text, portNumber);
                await Task.Run(() => ReceiveData(bufferSize));
                CreateServerButton.Enabled = false;
                NameInputBox.Enabled = false;
                SendMessageButton.Enabled = true;
            }
            catch (SocketException exception)
            {
                MessageBox.Show(exception.Message, "No connection is possible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }

        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (NameInputBox.Text == "" || networkStream == null)
            {
                return;
            }
            await SendMessageAsync("MESSAGE", NameInputBox.Text, MessageInput.Text);
        }

        private async Task SendMessageAsync(string type, string name, string message)
        {
            string completedMessage = EncodeMessage(type, name, message);

            byte[] buffer = Encoding.ASCII.GetBytes(completedMessage);
            await networkStream.WriteAsync(buffer, 0, buffer.Length);

            AddMessage($"{name}: {message}");
            MessageInput.Clear();
            MessageInput.Focus();
        }

        private string EncodeMessage(string type, string name, string message)
        {
            type = Regex.Replace(type, "[|]", "&#124");
            type = Regex.Replace(type, "[@]", "&#64");
            name = Regex.Replace(name, "[|]", "&#124");
            name = Regex.Replace(name, "[@]", "&#64");
            message = Regex.Replace(message, "[|]", "&#124");
            message = Regex.Replace(message, "[@]", "&#64");
            string newMessage = $"@{type}||{name}||{message}@";

            return newMessage;
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
            if (splitValues.Length != 4 || String.IsNullOrWhiteSpace(ipAdress) || splitValues[3] == "")
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
