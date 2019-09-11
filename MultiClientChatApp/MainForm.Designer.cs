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
            this.ListenButton = new System.Windows.Forms.Button();
            this.ConnectToServerBox = new System.Windows.Forms.GroupBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.IpInputBox = new System.Windows.Forms.TextBox();
            this.IpLabel = new System.Windows.Forms.Label();
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
            this.Chatscreen.Location = new System.Drawing.Point(6, 10);
            this.Chatscreen.Name = "Chatscreen";
            this.Chatscreen.Size = new System.Drawing.Size(540, 372);
            this.Chatscreen.TabIndex = 0;
            // 
            // ListenButton
            // 
            this.ListenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListenButton.Location = new System.Drawing.Point(564, 12);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(200, 58);
            this.ListenButton.TabIndex = 3;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(ListenButton_Click);
            // 
            // ConnectToServerBox
            // 
            this.ConnectToServerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectToServerBox.Controls.Add(this.ConnectButton);
            this.ConnectToServerBox.Controls.Add(this.IpInputBox);
            this.ConnectToServerBox.Controls.Add(this.IpLabel);
            this.ConnectToServerBox.Location = new System.Drawing.Point(564, 103);
            this.ConnectToServerBox.MinimumSize = new System.Drawing.Size(110, 0);
            this.ConnectToServerBox.Name = "ConnectToServerBox";
            this.ConnectToServerBox.Size = new System.Drawing.Size(206, 130);
            this.ConnectToServerBox.TabIndex = 4;
            this.ConnectToServerBox.TabStop = false;
            this.ConnectToServerBox.Text = "Connect to Server";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(6, 86);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(191, 23);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // IpInputBox
            // 
            this.IpInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IpInputBox.Location = new System.Drawing.Point(6, 49);
            this.IpInputBox.Name = "IpInputBox";
            this.IpInputBox.Size = new System.Drawing.Size(191, 22);
            this.IpInputBox.TabIndex = 1;
            // 
            // IpLabel
            // 
            this.IpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IpLabel.AutoSize = true;
            this.IpLabel.Location = new System.Drawing.Point(6, 29);
            this.IpLabel.Name = "IpLabel";
            this.IpLabel.Size = new System.Drawing.Size(97, 17);
            this.IpLabel.TabIndex = 0;
            this.IpLabel.Text = "Chatserver IP:";
            // 
            // MessageInput
            // 
            this.MessageInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageInput.Location = new System.Drawing.Point(6, 398);
            this.MessageInput.Name = "MessageInput";
            this.MessageInput.Size = new System.Drawing.Size(459, 22);
            this.MessageInput.TabIndex = 5;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendMessageButton.CausesValidation = false;
            this.SendMessageButton.Location = new System.Drawing.Point(471, 397);
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
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.MessageInput);
            this.Controls.Add(this.ConnectToServerBox);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.Chatscreen);
            this.MinimumSize = new System.Drawing.Size(570, 280);
            this.Name = "MainForm";
            this.Text = "NOTS chat app";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ConnectToServerBox.ResumeLayout(false);
            this.ConnectToServerBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox Chatscreen;
        public System.Windows.Forms.Button ListenButton;
        public System.Windows.Forms.GroupBox ConnectToServerBox;
        public System.Windows.Forms.Label IpLabel;
        public System.Windows.Forms.Button ConnectButton;
        public System.Windows.Forms.TextBox IpInputBox;
        public System.Windows.Forms.TextBox MessageInput;
        public System.Windows.Forms.Button SendMessageButton;
    }
}

