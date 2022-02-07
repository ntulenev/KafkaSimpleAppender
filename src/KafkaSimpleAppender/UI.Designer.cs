namespace KafkaSimpleAppender
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
            this.lbKeyType = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbKey = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.lbTopic = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbKeyType
            // 
            this.lbKeyType.AutoSize = true;
            this.lbKeyType.Location = new System.Drawing.Point(12, 9);
            this.lbKeyType.Name = "lbKeyType";
            this.lbKeyType.Size = new System.Drawing.Size(52, 15);
            this.lbKeyType.TabIndex = 0;
            this.lbKeyType.Text = "Key type";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboBox1.Location = new System.Drawing.Point(70, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 3;
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
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(70, 56);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(488, 71);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(70, 149);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(488, 225);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
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
            this.lbTopic.Location = new System.Drawing.Point(209, 9);
            this.lbTopic.Name = "lbTopic";
            this.lbTopic.Size = new System.Drawing.Size(35, 15);
            this.lbTopic.TabIndex = 8;
            this.lbTopic.Text = "Topic";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(267, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(291, 23);
            this.textBox1.TabIndex = 9;
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(468, 380);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(90, 42);
            this.bSend.TabIndex = 10;
            this.bSend.Text = "Send data";
            this.bSend.UseVisualStyleBackColor = true;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 434);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbTopic);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lbKey);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbKeyType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UI";
            this.Text = "Kafka Simple Appender";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbKeyType;
        private ComboBox comboBox1;
        private Label lbKey;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private Label lbMessage;
        private Label lbTopic;
        private TextBox textBox1;
        private Button bSend;
    }
}