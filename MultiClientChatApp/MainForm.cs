using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiClientChatApp
{
    public partial class MainForm : Form
    {

        TcpClient tcpClient;
        NetworkStream networkStream;

        protected delegate void UpdateDisplayDelegate(string message);

        List<TcpClient> clientList = new List<TcpClient>();

        public MainForm()
        {
            InitializeComponent();
            SendMessageButton.Enabled = false;
            DisconnectButton.Enabled = false;
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
                TcpListener tcpListener = new TcpListener(IPAddress.Parse(IpInputBox.Text), portNumber);
                tcpListener.Start();
                MessageInput.Enabled = false;
                SendMessageButton.Enabled = false;
                ConnectButton.Enabled = false;
                PortInputBox.Enabled = false;
                NameInputBox.Enabled = false;
                BufferInputBox.Enabled = false;
                IpInputBox.Enabled = false;
                DisconnectButton.Enabled = false;
                AddMessage($"Server is waiting for clients on port: {portNumber}");
                while (true)
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    SendMessageButton.Enabled = true;
                    clientList.Add(tcpClient);
                    await Task.Run(() => ReceiveServerData(tcpClient, bufferSize));
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

        private async Task BroadcastMessage(TcpClient client, string type, string username, string message)
        {
            string completeMessage = EncodeMessage(type, username, message);

            foreach (TcpClient user in clientList)
            {
                if (user.Client.RemoteEndPoint != client.Client.RemoteEndPoint)
                {
                    await SendMessageOnNetworkAsync(user.GetStream(), completeMessage);
                }
            }
        }

        private async Task SendMessageOnNetworkAsync(NetworkStream stream, string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        private async Task ReceiveServerData(TcpClient client, int bufferSize)
        {
            string message = "";
            byte[] buffer = new byte[bufferSize];
            NetworkStream network = client.GetStream();

            AddMessage("Connected!");

            while (network.CanRead)
            {

                StringBuilder fullMessage = new StringBuilder();

                do
                {
                    try
                    {
                        int readBytes = await network.ReadAsync(buffer, 0, bufferSize);
                        message = Encoding.ASCII.GetString(buffer, 0, readBytes);
                        fullMessage.Append(message);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "No connection is possible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        fullMessage.Clear();
                        fullMessage.Append("@INFO||unknown||DISCONNECT");
                        break;
                    }
                }
                while (network.DataAvailable);

                string decodedType = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\@)(.*?)(?=\|{2})"));
                string decodedUsername = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\|{2})(.*?)(?=\|{2})"));
                string decodedMessage = DecodeMessage(ProtocolRegexCheck(ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"\|(?:.(?!\|))+$")), new Regex(@"(?<=\|{2})(.*?)(?=\@)")));

                if (decodedType == "INFO" && decodedMessage == "disconnectFromServer")
                {
                    await SendDisconnectAsync(network, "INFO", decodedUsername, "disconnectFromClient");
                    break;
                }
                else if (decodedType == "INFO" && decodedMessage == "disconnectFromClient")
                {
                    break;
                }
                else if (decodedType == "MESSAGE")
                {
                    await BroadcastMessage(client, decodedType, decodedUsername, decodedMessage);
                    AddMessage($"{decodedUsername}: {decodedMessage}");
                }
            }
            network.Close();
            client.Close();

            AddMessage($"Connection with a client has closed!");
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
                SendMessageButton.Enabled = true;
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(IpInputBox.Text, portNumber);
                await Task.Run(() => ReceiveClientData(bufferSize));
                CreateServerButton.Enabled = false;
                NameInputBox.Enabled = false;
                
                DisconnectButton.Enabled = true;
            }
            catch (SocketException)
            {
                MessageBox.Show("Could not create a connection with the server", "No connection is possible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (NameInputBox.Text == "" || networkStream == null)
            {
                return;
            }
            try
            {
                await SendMessageAsync("MESSAGE", NameInputBox.Text, MessageInput.Text);
            }
            catch
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            else if (!(portNumber >= 1024 && portNumber <= 65535))
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


        private async void DisconnectButton_click(object sender, EventArgs e)
        {
            try
            {
                await DisconnectAsync();
            }
            catch (IOException)
            {
                MessageBox.Show("Error: Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("Warning: No clients found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task DisconnectAsync()
        {
            await SendMessageAsync("INFO", "server", "disconnectFromServer");
        }

        private async Task ReceiveClientData(int bufferSize)
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

                if (decodedType == "INFO" && decodedMessage == "disconnectToServer")
                {
                    await SendDisconnectAsync(networkStream, "INFO", decodedUsername, "disconnectToClient");
                    break;
                }
                else if (decodedType == "INFO" && decodedMessage == "disconnectToClient")
                {
                    break;
                }
                else if (decodedType == "MESSAGE")
                {
                    AddMessage($"{decodedUsername}: {decodedMessage}");
                }
            }
            networkStream.Close();
            tcpClient.Close();
            AddMessage($"Connection has been closed!");
        }

        private async Task SendDisconnectAsync(NetworkStream stream, string type, string username, string message)
        {
            string finalMessage = EncodeMessage(type, username, message);

            await SendMessageOnNetworkAsync(stream, finalMessage);
        }
    }
}