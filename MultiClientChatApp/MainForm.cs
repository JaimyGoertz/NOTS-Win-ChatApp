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
        Thread thread;

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

        private void ListenButton_Click(object sender, EventArgs e)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 9000);
            tcpListener.Start();

            //listChats.Items.Add("Listening for client.");     // conform opdracht maar zonder hergebruik
            AddMessage("Listening for client.");

            tcpClient = tcpListener.AcceptTcpClient();
            thread = new Thread(new ThreadStart(ReceiveData));
            thread.Start();
        }

        private void ReceiveData()
        {
            int bufferSize = 2;
            string message = "";
            byte[] buffer = new byte[bufferSize];

            networkStream = tcpClient.GetStream();
            AddMessage("Connected!");

             while (true)
            {
                int readBytes = networkStream.Read(buffer, 0, bufferSize);
                message = Encoding.ASCII.GetString(buffer, 0, readBytes);

                if (message == "bye")
                    break;

                AddMessage(message);
            }
            buffer = Encoding.ASCII.GetBytes("bye");
            networkStream.Write(buffer, 0, buffer.Length);

            // cleanup:
            networkStream.Close();
            tcpClient.Close();

            AddMessage("Connection closed");
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            AddMessage("Connecting...");
            tcpClient = new TcpClient(IpInputBox.Text, 9000);
            thread = new Thread(new ThreadStart(ReceiveData));
            thread.Start();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
