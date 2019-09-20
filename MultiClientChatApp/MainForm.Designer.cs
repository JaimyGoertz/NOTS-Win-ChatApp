namespace MultiClientChatApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary> 

        private void InitializeComponent()
        {
            this.Chatscreen = new System.Windows.Forms.ListBox();
            this.ConnectToServerBox = new System.Windows.Forms.GroupBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.BufferLabel = new System.Windows.Forms.Label();
            this.BufferInputBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortInputBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameInputBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.IpInputBox = new System.Windows.Forms.TextBox();
            this.IpLabel = new System.Windows.Forms.Label();
            this.CreateServerButton = new System.Windows.Forms.Button();
            this.MessageInput = new System.Windows.Forms.TextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.ConnectToServerBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Chatscreen
            // 
            this.Chatscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Chatscreen.FormattingEnabled = true;
            this.Chatscreen.ItemHeight = 16;
            this.Chatscreen.Location = new System.Drawing.Point(18, 12);
            this.Chatscreen.Name = "Chatscreen";
            this.Chatscreen.Size = new System.Drawing.Size(540, 372);
            this.Chatscreen.TabIndex = 0;
            // 
            // ConnectToServerBox
            // 
            this.ConnectToServerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectToServerBox.Controls.Add(this.DisconnectButton);
            this.ConnectToServerBox.Controls.Add(this.BufferLabel);
            this.ConnectToServerBox.Controls.Add(this.BufferInputBox);
            this.ConnectToServerBox.Controls.Add(this.PortLabel);
            this.ConnectToServerBox.Controls.Add(this.PortInputBox);
            this.ConnectToServerBox.Controls.Add(this.NameLabel);
            this.ConnectToServerBox.Controls.Add(this.NameInputBox);
            this.ConnectToServerBox.Controls.Add(this.ConnectButton);
            this.ConnectToServerBox.Controls.Add(this.IpInputBox);
            this.ConnectToServerBox.Controls.Add(this.IpLabel);
            this.ConnectToServerBox.Location = new System.Drawing.Point(570, 63);
            this.ConnectToServerBox.MinimumSize = new System.Drawing.Size(110, 0);
            this.ConnectToServerBox.Name = "ConnectToServerBox";
            this.ConnectToServerBox.Size = new System.Drawing.Size(206, 321);
            this.ConnectToServerBox.TabIndex = 4;
            this.ConnectToServerBox.TabStop = false;
            this.ConnectToServerBox.Text = "Connect to Server";
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(6, 276);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(190, 39);
            this.DisconnectButton.TabIndex = 9;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_click);
            // 
            // BufferLabel
            // 
            this.BufferLabel.AutoSize = true;
            this.BufferLabel.Location = new System.Drawing.Point(2, 163);
            this.BufferLabel.Name = "BufferLabel";
            this.BufferLabel.Size = new System.Drawing.Size(81, 17);
            this.BufferLabel.TabIndex = 8;
            this.BufferLabel.Text = "Buffer Size:";
            // 
            // BufferInputBox
            // 
            this.BufferInputBox.Location = new System.Drawing.Point(5, 183);
            this.BufferInputBox.Name = "BufferInputBox";
            this.BufferInputBox.Size = new System.Drawing.Size(185, 22);
            this.BufferInputBox.TabIndex = 7;
            this.BufferInputBox.Text = "1024";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(3, 118);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(38, 17);
            this.PortLabel.TabIndex = 6;
            this.PortLabel.Text = "Port:";
            // 
            // PortInputBox
            // 
            this.PortInputBox.Location = new System.Drawing.Point(5, 138);
            this.PortInputBox.Name = "PortInputBox";
            this.PortInputBox.Size = new System.Drawing.Size(188, 22);
            this.PortInputBox.TabIndex = 5;
            this.PortInputBox.Text = "9000";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(3, 28);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(49, 17);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Name:";
            // 
            // NameInputBox
            // 
            this.NameInputBox.Location = new System.Drawing.Point(3, 48);
            this.NameInputBox.Name = "NameInputBox";
            this.NameInputBox.Size = new System.Drawing.Size(188, 22);
            this.NameInputBox.TabIndex = 3;
            this.NameInputBox.Text = "User1";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(6, 229);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(191, 40);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // IpInputBox
            // 
            this.IpInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IpInputBox.Location = new System.Drawing.Point(3, 93);
            this.IpInputBox.Name = "IpInputBox";
            this.IpInputBox.Size = new System.Drawing.Size(191, 22);
            this.IpInputBox.TabIndex = 1;
            this.IpInputBox.Text = "127.0.0.1";
            // 
            // IpLabel
            // 
            this.IpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IpLabel.AutoSize = true;
            this.IpLabel.Location = new System.Drawing.Point(3, 73);
            this.IpLabel.Name = "IpLabel";
            this.IpLabel.Size = new System.Drawing.Size(80, 17);
            this.IpLabel.TabIndex = 0;
            this.IpLabel.Text = "IP Address:";
            // 
            // CreateServerButton
            // 
            this.CreateServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateServerButton.Location = new System.Drawing.Point(570, 12);
            this.CreateServerButton.Name = "CreateServerButton";
            this.CreateServerButton.Size = new System.Drawing.Size(188, 43);
            this.CreateServerButton.TabIndex = 9;
            this.CreateServerButton.Text = "Create Server";
            this.CreateServerButton.UseVisualStyleBackColor = true;
            this.CreateServerButton.Click += new System.EventHandler(this.CreateServerButton_Click);
            // 
            // MessageInput
            // 
            this.MessageInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageInput.Location = new System.Drawing.Point(18, 397);
            this.MessageInput.Name = "MessageInput";
            this.MessageInput.Size = new System.Drawing.Size(459, 22);
            this.MessageInput.TabIndex = 5;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendMessageButton.CausesValidation = false;
            this.SendMessageButton.Location = new System.Drawing.Point(483, 397);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(75, 23);
            this.SendMessageButton.TabIndex = 6;
            this.SendMessageButton.Text = "Send";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 442);
            this.Controls.Add(this.CreateServerButton);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.MessageInput);
            this.Controls.Add(this.ConnectToServerBox);
            this.Controls.Add(this.Chatscreen);
            this.MinimumSize = new System.Drawing.Size(570, 420);
            this.Name = "MainForm";
            this.Text = "NOTS chat app";
            this.ConnectToServerBox.ResumeLayout(false);
            this.ConnectToServerBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox Chatscreen;
        public System.Windows.Forms.GroupBox ConnectToServerBox;
        public System.Windows.Forms.Label IpLabel;
        public System.Windows.Forms.Button ConnectButton;
        public System.Windows.Forms.TextBox IpInputBox;
        public System.Windows.Forms.TextBox MessageInput;
        public System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.Label BufferLabel;
        private System.Windows.Forms.TextBox BufferInputBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox PortInputBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NameInputBox;
        private System.Windows.Forms.Button CreateServerButton;
        private System.Windows.Forms.Button DisconnectButton;
    }
}

