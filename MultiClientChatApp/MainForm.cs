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

        //protected delegate void UpdateDisplayDelegate(string message); //Alleen op de uit thread dingen aanpassen (weg)

        public MainForm()
        {
            InitializeComponent();
        }

        private async void AddMessage(string message)
        {
               await Task.Run(() => UpdateDisplay(message));
        }

        private void UpdateDisplay(string message)
        {
            Chatscreen.Items.Add(message);
        }

        private async void ListenButton_Click(object sender, EventArgs e)
        {
            try
            {
                await CreateServerAsync();
            }
            catch
            {
                MessageBox.Show("An error has occured");
            }
        }

        private async Task CreateServerAsync()
        {
            int portNumber = PortCheck(PortInputBox.Text);
            TcpListener tcpListener = new TcpListener(IPAddress.Any, portNumber);
            tcpListener.Start();
            AddMessage($"[Server] waiting for clients...");
            tcpClient = await Task.Run(() => tcpListener.AcceptTcpClientAsync());
            await Task.Run(() => ReceiveData());
        }

        private void ReceiveData()
        {
            int bufferSize = 1024;
            string message = "";
            byte[] buffer = new byte[bufferSize];//Dynamisch instelbaar vanaf de UI

            networkStream = tcpClient.GetStream();
            AddMessage("Connected!");

             while (true)
            {
                int readBytes = networkStream.Read(buffer, 0, bufferSize);//Goed kijken naar de waardes
                message = Encoding.ASCII.GetString(buffer, 0, readBytes);
                //Exception handling and using
                if (message == "bye")
                    break;

                AddMessage(message);
            }
            buffer = Encoding.ASCII.GetBytes("bye");
            networkStream.Write(buffer, 0, buffer.Length);

            // cleanup: misschien niet nodig. Checken of ze closed zijn
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
            int portNumber = PortCheck(PortInputBox.Text);

            if (portNumber != 0)
            {
                tcpClient = await Task.Run(() => new TcpClient(IpInputBox.Text, portNumber));
                await Task.Run(() => ReceiveData());
            }
            else
            {
                MessageBox.Show("Port has an invalid value");
            }
        }

        private int PortCheck(string port)
        {
            int portNumber;
            int.TryParse(port, out portNumber);

            return portNumber;
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            string message = MessageInput.Text;

            byte[] buffer = Encoding.ASCII.GetBytes(message);
            networkStream.Write(buffer, 0, buffer.Length);

            AddMessage(message);
            MessageInput.Clear();
            MessageInput.Focus();
        }
        //Zorg voor afsluiting tcpListener

        private Boolean matchesRegex(String input, String regex)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, regex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void IpLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
