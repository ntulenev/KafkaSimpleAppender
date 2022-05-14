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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.lbKey = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.RichTextBox();
            this.tbMessage = new System.Windows.Forms.RichTextBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.lbTopic = new System.Windows.Forms.Label();
            this.tbTopic = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.tbKeyType = new System.Windows.Forms.Label();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.cbJson = new System.Windows.Forms.CheckBox();
            this.rbSingle = new System.Windows.Forms.RadioButton();
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.bLoadFile = new System.Windows.Forms.Button();
            this.rbFileLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lbKey
            // 
            this.lbKey.AutoSize = true;
            this.lbKey.Location = new System.Drawing.Point(14, 106);
            this.lbKey.Name = "lbKey";
            this.lbKey.Size = new System.Drawing.Size(33, 20);
            this.lbKey.TabIndex = 4;
            this.lbKey.Text = "Key";
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(80, 106);
            this.tbKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(557, 93);
            this.tbKey.TabIndex = 5;
            this.tbKey.Text = "";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(80, 260);
            this.tbMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(557, 268);
            this.tbMessage.TabIndex = 7;
            this.tbMessage.Text = "";
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Location = new System.Drawing.Point(14, 226);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(61, 20);
            this.lbMessage.TabIndex = 6;
            this.lbMessage.Text = "Payload";
            // 
            // lbTopic
            // 
            this.lbTopic.AutoSize = true;
            this.lbTopic.Location = new System.Drawing.Point(14, 20);
            this.lbTopic.Name = "lbTopic";
            this.lbTopic.Size = new System.Drawing.Size(45, 20);
            this.lbTopic.TabIndex = 8;
            this.lbTopic.Text = "Topic";
            // 
            // tbTopic
            // 
            this.tbTopic.Location = new System.Drawing.Point(80, 16);
            this.tbTopic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbTopic.Name = "tbTopic";
            this.tbTopic.Size = new System.Drawing.Size(338, 27);
            this.tbTopic.TabIndex = 9;
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(534, 646);
            this.bSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(103, 56);
            this.bSend.TabIndex = 10;
            this.bSend.Text = "Send data";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // tbKeyType
            // 
            this.tbKeyType.AutoSize = true;
            this.tbKeyType.Location = new System.Drawing.Point(425, 20);
            this.tbKeyType.Name = "tbKeyType";
            this.tbKeyType.Size = new System.Drawing.Size(66, 20);
            this.tbKeyType.TabIndex = 11;
            this.tbKeyType.Text = "Key type";
            // 
            // cbTypes
            // 
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(491, 16);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(146, 28);
            this.cbTypes.TabIndex = 12;
            this.cbTypes.SelectedIndexChanged += new System.EventHandler(this.cbTypes_SelectedIndexChanged);
            // 
            // cbJson
            // 
            this.cbJson.AutoSize = true;
            this.cbJson.Location = new System.Drawing.Point(80, 224);
            this.cbJson.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbJson.Name = "cbJson";
            this.cbJson.Size = new System.Drawing.Size(142, 24);
            this.cbJson.TabIndex = 13;
            this.cbJson.Text = "Validate as JSON";
            this.cbJson.UseVisualStyleBackColor = true;
            // 
            // rbSingle
            // 
            this.rbSingle.AutoSize = true;
            this.rbSingle.Checked = true;
            this.rbSingle.Location = new System.Drawing.Point(80, 63);
            this.rbSingle.Name = "rbSingle";
            this.rbSingle.Size = new System.Drawing.Size(133, 24);
            this.rbSingle.TabIndex = 14;
            this.rbSingle.TabStop = true;
            this.rbSingle.Text = "Single message";
            this.rbSingle.UseVisualStyleBackColor = true;
            this.rbSingle.CheckedChanged += new System.EventHandler(this.rbSingle_CheckedChanged);
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.Location = new System.Drawing.Point(219, 63);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(124, 24);
            this.rbFile.TabIndex = 14;
            this.rbFile.Text = "Load from file";
            this.rbFile.UseVisualStyleBackColor = true;
            this.rbFile.CheckedChanged += new System.EventHandler(this.rbFile_CheckedChanged);
            // 
            // bLoadFile
            // 
            this.bLoadFile.Enabled = false;
            this.bLoadFile.Location = new System.Drawing.Point(80, 543);
            this.bLoadFile.Name = "bLoadFile";
            this.bLoadFile.Size = new System.Drawing.Size(93, 79);
            this.bLoadFile.TabIndex = 15;
            this.bLoadFile.Text = "Open File";
            this.bLoadFile.UseVisualStyleBackColor = true;
            // 
            // rbFileLog
            // 
            this.rbFileLog.BackColor = System.Drawing.SystemColors.Info;
            this.rbFileLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rbFileLog.Location = new System.Drawing.Point(179, 543);
            this.rbFileLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbFileLog.Name = "rbFileLog";
            this.rbFileLog.ReadOnly = true;
            this.rbFileLog.Size = new System.Drawing.Size(458, 79);
            this.rbFileLog.TabIndex = 16;
            this.rbFileLog.Text = "";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 715);
            this.Controls.Add(this.rbFileLog);
            this.Controls.Add(this.bLoadFile);
            this.Controls.Add(this.rbFile);
            this.Controls.Add(this.rbSingle);
            this.Controls.Add(this.cbJson);
            this.Controls.Add(this.cbTypes);
            this.Controls.Add(this.tbKeyType);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.tbTopic);
            this.Controls.Add(this.lbTopic);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.lbKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private Label tbKeyType;
        private ComboBox cbTypes;
        private CheckBox cbJson;
        private RadioButton rbSingle;
        private RadioButton rbFile;
        private Button bLoadFile;
        private RichTextBox rbFileLog;
    }
}