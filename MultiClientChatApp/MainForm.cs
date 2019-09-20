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
        Boolean started;

        protected delegate void UpdateDisplayDelegate(string message);

        List<TcpClient> clientList = new List<TcpClient>();

        public MainForm()
        {
            InitializeComponent();
            SendMessageButton.Enabled = false;
            DisconnectButton.Enabled = false;
        }

        //Send messages to the user
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

        //Updates Users screen so the message is visible
        private void UpdateDisplay(string message)
        {
            Chatscreen.Items.Add(message);
            Chatscreen.SelectedIndex = Chatscreen.Items.Count - 1;
        }

        //Creates server
        private async Task CreateServerAsync(string port, string buffer, string ip)
        {
            int portNumber = ParseStringToInt(port);
            int bufferSize = ParseStringToInt(buffer);
            string username = "Server";
            NameInputBox.Text = username;
            started = true;
            if (!checkInputForErrors(portNumber, bufferSize, ip)) { return; }
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ip), portNumber);
            tcpListener.Start();
            MessageInput.Enabled = true;
            SendMessageButton.Enabled = true;
            ConnectButton.Enabled = false;
            PortInputBox.Enabled = false;
            NameInputBox.Enabled = false;
            BufferInputBox.Enabled = false;
            IpInputBox.Enabled = false;
            SendMessageButton.Enabled = false;
            CreateServerButton.Enabled = false;
            DisconnectButton.Enabled = true;
            AddMessage($"Server is waiting for clients on port: {portNumber}");
            while (started)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                SendMessageButton.Enabled = true;
                clientList.Add(tcpClient);
                await Task.Run(() => ReceiveServerData(tcpClient, bufferSize));
            }
            ConnectButton.Enabled = true;
            CreateServerButton.Enabled = true;
            DisconnectButton.Enabled = false;
            NameInputBox.Enabled = true;
            IpInputBox.Enabled = true;
            PortInputBox.Enabled = true;
            BufferInputBox.Enabled = true;

            tcpListener.Stop();

        }

        //Create Server button event handler
        private async void CreateServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                await CreateServerAsync(PortInputBox.Text, BufferInputBox.Text, IpInputBox.Text);
            }
            catch
            {
                MessageBox.Show("A server has already been opened.", "Server error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Broadcasts message to al clients in the clientlist
        private async Task BroadcastMessageToClients(TcpClient client, string type, string username, string msg)
        {


            foreach (TcpClient user in clientList)
            {
                if (user.Client.RemoteEndPoint != client.Client.RemoteEndPoint)
                {
                    string message = EncodeMessage(type, username, msg);
                    int bufferSize = ParseStringToInt(BufferInputBox.Text);
                    do
                    {
                        if (bufferSize > message.Length)
                        {
                            bufferSize = message.Length;
                        }

                        string substring = message.Substring(0, bufferSize);
                        message = message.Remove(0, bufferSize);
                        byte[] buffer = Encoding.ASCII.GetBytes(substring);
                        await user.GetStream().WriteAsync(buffer, 0, bufferSize);
                    } while (message.Length > 0);
                }
            }
        }

        //Everything related to receiving data on the server
        private async void ReceiveServerData(TcpClient client, int bufferSize)
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
                        do
                        {
                            int readBytes = await network.ReadAsync(buffer, 0, bufferSize);
                            message = Encoding.ASCII.GetString(buffer, 0, readBytes);
                            fullMessage.Append(message);
                        }
                        while (fullMessage.ToString().IndexOf("@", 1) < 0);
                    }
                    catch (IOException)
                    {
                        fullMessage.Clear();
                        fullMessage.Append("@INFO||username||DISCONNECT@");
                        break;
                    }
                }
                while (network.DataAvailable);

                string decodedType = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\@)(.*?)(?=\|{2})"));
                string decodedUsername = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\|{2})(.*?)(?=\|{2})"));
                string decodedMessage = DecodeMessage(ProtocolRegexCheck(ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"\|(?:.(?!\|))+$")), new Regex(@"(?<=\|{2})(.*?)(?=\@)")));

                if (decodedType == "INFO" && decodedMessage == "DISCONNECTING")
                {
                    await DisconnectAsync(network, "INFO", decodedUsername, "DISCONNECT");
                    break;
                }
                else if (decodedType == "INFO" && decodedMessage == "DISCONNECT")
                {
                    break;
                }
                if (decodedType == "MESSAGE")
                {
                    await BroadcastMessageToClients(client, decodedType, decodedUsername, decodedMessage);
                    AddMessage($"{decodedUsername}: {decodedMessage}");
                }
            }
            network.Close();
            client.Close();
            clientList.RemoveAll(user => !user.Connected);
            AddMessage($"Connection with a client has been closed!");
        }

        //Checks a message with a certain regex condition
        private string ProtocolRegexCheck(string message, Regex regex)
        {
            return regex.Match(message).ToString();
        }

        //Decodes given string message
        private string DecodeMessage(string str)
        {
            str = Regex.Replace(str, "[&#124]", "|");
            str = Regex.Replace(str, "[&#64]", "@");

            return str;
        }

        //Connect to server button event handler
        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                await CreateConnectionAsync(PortInputBox.Text, BufferInputBox.Text, IpInputBox.Text, NameInputBox.Text);
            }
            catch
            {
                MessageBox.Show("An error has occurred.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Creates a connection with the server
        private async Task CreateConnectionAsync(string port, string buffer, string ip, string name)
        {
            int portNumber = ParseStringToInt(port);
            int bufferSize = ParseStringToInt(buffer);
            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("No username has been given", "No username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkInputForErrors(portNumber, bufferSize, ip)) { return; }
            AddMessage("Connecting");
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ip, portNumber);

                SendMessageButton.Enabled = true;
                CreateServerButton.Enabled = false;
                NameInputBox.Enabled = false;
                ConnectButton.Enabled = false;
                DisconnectButton.Enabled = true;
                IpInputBox.Enabled = false;
                PortInputBox.Enabled = false;
                BufferInputBox.Enabled = false;

                await Task.Run(() => ReceiveClientData(bufferSize));

                ConnectButton.Enabled = true;
                CreateServerButton.Enabled = true;
                SendMessageButton.Enabled = false;
                DisconnectButton.Enabled = false;
                NameInputBox.Enabled = true;
                IpInputBox.Enabled = true;
                PortInputBox.Enabled = true;
                BufferInputBox.Enabled = true;
                MessageInput.Text = "";
            }
            catch (SocketException)
            {
                MessageBox.Show("Could not create a connection with the server", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Send message button event handler
        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(MessageInput.Text) || networkStream == null) { return; }
            try
            {
                await SendMessageAsync("MESSAGE", NameInputBox.Text, MessageInput.Text);
            }
            catch
            {
                MessageBox.Show("Message could not be send", "Message error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sends message 
        private async Task SendMessageAsync(string type, string username, string msg)
        {
            string message = EncodeMessage(type, username, msg);
            int bufferSize = ParseStringToInt(BufferInputBox.Text);
            do
            {
                if (bufferSize > message.Length)
                {
                    bufferSize = message.Length;
                }

                string substring = message.Substring(0, bufferSize);
                message = message.Remove(0, bufferSize);
                byte[] buffer = Encoding.ASCII.GetBytes(substring);
                await networkStream.WriteAsync(buffer, 0, bufferSize);
            }
            while (message.Length > 0);

            AddMessage($"{username}: {msg}");
            MessageInput.Clear();
            MessageInput.Focus();
        }

        //Encodes given message that is split in: type, name and message
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

        //Parses a string to a int (for input fields)
        private int ParseStringToInt(string stringParam)
        {
            int numberValue;
            int.TryParse(stringParam, out numberValue);

            return numberValue;
        }

        //Checks if an ip is valid
        public bool CheckIpAddressValidation(string ipAdress)
        {
            string[] splitValueArray = ipAdress.Split('.');
            if (splitValueArray.Length != 4 || String.IsNullOrWhiteSpace(ipAdress) || splitValueArray[3] == "")
            {
                return false;
            }

            byte endByte;
            return splitValueArray.All(r => byte.TryParse(r, out endByte));
        }

        //Checks all input boxes for unvalid input
        private bool checkInputForErrors(int portNumber, int bufferSize, string ip)
        {
            if (!CheckIpAddressValidation(ip))
            {
                MessageBox.Show("Ip address is invalid, try again", "Invalid IP address", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!(portNumber >= 1024 && portNumber <= 65535))
            {
                MessageBox.Show("Port number is not valid, try again", "Invalid Port number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (bufferSize <= 0)
            {
                MessageBox.Show("Buffersize is not valid, try again", "Invalid buffer size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        //Diconnect button event handler
        private async void DisconnectButton_click(object sender, EventArgs e)
        {
            try
            {
                if (NameInputBox.Text == "Server")
                {
                    AddMessage("Server is closing");

                    foreach (TcpClient user in clientList.ToList())
                    {
                        await DisconnectAsync(user.GetStream(), "INFO", "Server", "DISCONNECTING");
                    }

                    started = false;

                    TcpClient tcpClient = new TcpClient();
                    await tcpClient.ConnectAsync("127.0.0.1", ParseStringToInt(PortInputBox.Text));
                    await DisconnectAsync(tcpClient.GetStream(), "INFO", "DUMMY", "DISCONNECTING");
                    tcpClient.Close();
                }
                else
                {
                    await DisconnectClientAsync(NameInputBox.Text);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("No clients found.", "Client warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show("An error has occurred.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Disconnects server
        private async Task DisconnectAsync(NetworkStream stream, string type, string name, string msg)
        {

            string message = EncodeMessage(type, name, msg);
            int bufferSize = ParseStringToInt(BufferInputBox.Text);
            do
            {
                if (bufferSize > message.Length)
                {
                    bufferSize = message.Length;
                }

                string substring = message.Substring(0, bufferSize);
                message = message.Remove(0, bufferSize);
                byte[] buffer = Encoding.ASCII.GetBytes(substring);
                await stream.WriteAsync(buffer, 0, bufferSize);
            } while (message.Length > 0);

        }

        //Disconnects user
        private async Task DisconnectClientAsync(string username)
        {
            await SendDisconnectAsync("INFO", username, "DISCONNECTING");
            ConnectButton.Enabled = true;
            DisconnectButton.Enabled = false;
            SendMessageButton.Enabled = false;

            NameInputBox.Enabled = true;
            IpInputBox.Enabled = true;
            PortInputBox.Enabled = true;
            BufferInputBox.Enabled = true;
        }

        //Everything related to receiving data in the client
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
                    do
                    {
                        int readBytes = await networkStream.ReadAsync(buffer, 0, bufferSize);
                        message = Encoding.ASCII.GetString(buffer, 0, readBytes);
                        fullMessage.Append(message);
                    } while (fullMessage.ToString().IndexOf("@", 1) < 0);

                }
                while (networkStream.DataAvailable);

                string decodedType = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\@)(.*?)(?=\|{2})"));
                string decodedUsername = ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"(?<=\|{2})(.*?)(?=\|{2})"));
                string decodedMessage = DecodeMessage(ProtocolRegexCheck(ProtocolRegexCheck(fullMessage.ToString(), new Regex(@"\|(?:.(?!\|))+$")), new Regex(@"(?<=\|{2})(.*?)(?=\@)")));

                if (decodedType == "INFO" && decodedMessage == "DISCONNECTING")
                {
                    await DisconnectAsync(networkStream, "INFO", decodedUsername, "DISCONNECT");
                    break;
                }
                if (decodedType == "INFO" && decodedMessage == "DISCONNECT")
                {
                    AddMessage($"{decodedUsername}: Disconnected!");
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

        //Sends disconnect message to client
        private async Task SendDisconnectAsync(string type, string username, string msg)
        {
            string message = EncodeMessage(type, username, msg);
            int bufferSize = ParseStringToInt(BufferInputBox.Text);
            do
            {
                if (bufferSize > message.Length)
                {
                    bufferSize = message.Length;
                }

                string substring = message.Substring(0, bufferSize);
                message = message.Remove(0, bufferSize);
                byte[] buffer = Encoding.ASCII.GetBytes(substring);
                await networkStream.WriteAsync(buffer, 0, bufferSize);
            }
            while (message.Length > 0);
        }
    }
}