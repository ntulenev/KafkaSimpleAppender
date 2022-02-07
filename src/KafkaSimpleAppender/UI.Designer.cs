﻿namespace KafkaSimpleAppender
{
    partial class UI
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
            this.lbKey = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.RichTextBox();
            this.tbMessage = new System.Windows.Forms.RichTextBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.lbTopic = new System.Windows.Forms.Label();
            this.tbTopic = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbKey
            // 
            this.lbKey.AutoSize = true;
            this.lbKey.Location = new System.Drawing.Point(12, 56);
            this.lbKey.Name = "lbKey";
            this.lbKey.Size = new System.Drawing.Size(26, 15);
            this.lbKey.TabIndex = 4;
            this.lbKey.Text = "Key";
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(70, 56);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(488, 71);
            this.tbKey.TabIndex = 5;
            this.tbKey.Text = "";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(70, 149);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(488, 225);
            this.tbMessage.TabIndex = 7;
            this.tbMessage.Text = "";
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Location = new System.Drawing.Point(12, 149);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(53, 15);
            this.lbMessage.TabIndex = 6;
            this.lbMessage.Text = "Message";
            // 
            // lbTopic
            // 
            this.lbTopic.AutoSize = true;
            this.lbTopic.Location = new System.Drawing.Point(12, 15);
            this.lbTopic.Name = "lbTopic";
            this.lbTopic.Size = new System.Drawing.Size(35, 15);
            this.lbTopic.TabIndex = 8;
            this.lbTopic.Text = "Topic";
            // 
            // tbTopic
            // 
            this.tbTopic.Location = new System.Drawing.Point(70, 12);
            this.tbTopic.Name = "tbTopic";
            this.tbTopic.Size = new System.Drawing.Size(488, 23);
            this.tbTopic.TabIndex = 9;
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(468, 380);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(90, 42);
            this.bSend.TabIndex = 10;
            this.bSend.Text = "Send data";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 434);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.tbTopic);
            this.Controls.Add(this.lbTopic);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.lbKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UI";
            this.Text = "Kafka Simple Appender";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label lbKey;
        private RichTextBox tbKey;
        private RichTextBox tbMessage;
        private Label lbMessage;
        private Label lbTopic;
        private TextBox tbTopic;
        private Button bSend;
    }
}