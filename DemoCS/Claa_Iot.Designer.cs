namespace DemoCS
{
    partial class CLaa_IoT
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxDevEui = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonUnRegister = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.Text_MSPPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxJoinEui = new System.Windows.Forms.TextBox();
            this.Text_MSPIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonFind = new System.Windows.Forms.Button();
            this.buttonCancelSend = new System.Windows.Forms.Button();
            this.checkBox_File = new System.Windows.Forms.CheckBox();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.textBoxTimes = new System.Windows.Forms.TextBox();
            this.checkBoxNumber = new System.Windows.Forms.CheckBox();
            this.checkBoxTimes = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonHex = new System.Windows.Forms.RadioButton();
            this.radioButtonChar = new System.Windows.Forms.RadioButton();
            this.checkBoxConfirmed = new System.Windows.Forms.CheckBox();
            this.buttonSendToMSP = new System.Windows.Forms.Button();
            this.textBoxPayload = new System.Windows.Forms.TextBox();
            this.comboBox_Cmd = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSendToPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxDevEui);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonUnRegister);
            this.groupBox1.Controls.Add(this.buttonRegister);
            this.groupBox1.Controls.Add(this.Text_MSPPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxJoinEui);
            this.groupBox1.Controls.Add(this.Text_MSPIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // textBoxDevEui
            // 
            this.textBoxDevEui.Location = new System.Drawing.Point(372, 75);
            this.textBoxDevEui.Name = "textBoxDevEui";
            this.textBoxDevEui.Size = new System.Drawing.Size(197, 21);
            this.textBoxDevEui.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(289, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "DEVEUI:";
            // 
            // buttonUnRegister
            // 
            this.buttonUnRegister.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonUnRegister.Location = new System.Drawing.Point(584, 70);
            this.buttonUnRegister.Name = "buttonUnRegister";
            this.buttonUnRegister.Size = new System.Drawing.Size(89, 32);
            this.buttonUnRegister.TabIndex = 2;
            this.buttonUnRegister.Text = "反注册";
            this.buttonUnRegister.UseVisualStyleBackColor = true;
            this.buttonUnRegister.Click += new System.EventHandler(this.buttonUnRegister_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRegister.Location = new System.Drawing.Point(584, 16);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(89, 32);
            this.buttonRegister.TabIndex = 1;
            this.buttonRegister.Text = "注册";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // Text_MSPPort
            // 
            this.Text_MSPPort.Enabled = false;
            this.Text_MSPPort.Location = new System.Drawing.Point(372, 22);
            this.Text_MSPPort.Name = "Text_MSPPort";
            this.Text_MSPPort.Size = new System.Drawing.Size(197, 21);
            this.Text_MSPPort.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "JOINEUI:";
            this.label3.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(292, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "端口号：";
            // 
            // textBoxJoinEui
            // 
            this.textBoxJoinEui.Location = new System.Drawing.Point(90, 76);
            this.textBoxJoinEui.Name = "textBoxJoinEui";
            this.textBoxJoinEui.Size = new System.Drawing.Size(193, 21);
            this.textBoxJoinEui.TabIndex = 0;
            // 
            // Text_MSPIP
            // 
            this.Text_MSPIP.Location = new System.Drawing.Point(90, 22);
            this.Text_MSPIP.Name = "Text_MSPIP";
            this.Text_MSPIP.Size = new System.Drawing.Size(193, 21);
            this.Text_MSPIP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "MSP_IP:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox_Log);
            this.groupBox2.Location = new System.Drawing.Point(12, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(686, 282);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.Location = new System.Drawing.Point(6, 12);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.Size = new System.Drawing.Size(674, 264);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonFind);
            this.groupBox3.Controls.Add(this.buttonCancelSend);
            this.groupBox3.Controls.Add(this.checkBox_File);
            this.groupBox3.Controls.Add(this.textBoxFileName);
            this.groupBox3.Controls.Add(this.textBoxNumber);
            this.groupBox3.Controls.Add(this.textBoxTimes);
            this.groupBox3.Controls.Add(this.checkBoxNumber);
            this.groupBox3.Controls.Add(this.checkBoxTimes);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.buttonSendToMSP);
            this.groupBox3.Controls.Add(this.textBoxPayload);
            this.groupBox3.Controls.Add(this.comboBox_Cmd);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxSendToPort);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(704, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 413);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // buttonFind
            // 
            this.buttonFind.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonFind.Location = new System.Drawing.Point(164, 314);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(75, 29);
            this.buttonFind.TabIndex = 19;
            this.buttonFind.Text = "查找";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // buttonCancelSend
            // 
            this.buttonCancelSend.Enabled = false;
            this.buttonCancelSend.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCancelSend.Location = new System.Drawing.Point(132, 361);
            this.buttonCancelSend.Name = "buttonCancelSend";
            this.buttonCancelSend.Size = new System.Drawing.Size(85, 30);
            this.buttonCancelSend.TabIndex = 18;
            this.buttonCancelSend.Text = "取消发送";
            this.buttonCancelSend.UseVisualStyleBackColor = true;
            this.buttonCancelSend.Click += new System.EventHandler(this.buttonCancelSend_Click);
            // 
            // checkBox_File
            // 
            this.checkBox_File.AutoSize = true;
            this.checkBox_File.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_File.Location = new System.Drawing.Point(16, 285);
            this.checkBox_File.Name = "checkBox_File";
            this.checkBox_File.Size = new System.Drawing.Size(175, 25);
            this.checkBox_File.TabIndex = 17;
            this.checkBox_File.Text = "验收终端deveui文件";
            this.checkBox_File.UseVisualStyleBackColor = true;
            this.checkBox_File.CheckedChanged += new System.EventHandler(this.checkBox_File_CheckedChanged);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxFileName.Location = new System.Drawing.Point(16, 314);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(137, 29);
            this.textBoxFileName.TabIndex = 16;
            this.textBoxFileName.Text = "请输入deveui所在文件名称";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(70, 228);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(160, 21);
            this.textBoxNumber.TabIndex = 14;
            // 
            // textBoxTimes
            // 
            this.textBoxTimes.Location = new System.Drawing.Point(70, 195);
            this.textBoxTimes.Name = "textBoxTimes";
            this.textBoxTimes.Size = new System.Drawing.Size(160, 21);
            this.textBoxTimes.TabIndex = 13;
            // 
            // checkBoxNumber
            // 
            this.checkBoxNumber.AutoSize = true;
            this.checkBoxNumber.Location = new System.Drawing.Point(16, 230);
            this.checkBoxNumber.Name = "checkBoxNumber";
            this.checkBoxNumber.Size = new System.Drawing.Size(48, 16);
            this.checkBoxNumber.TabIndex = 12;
            this.checkBoxNumber.Text = "计次";
            this.checkBoxNumber.UseVisualStyleBackColor = true;
            // 
            // checkBoxTimes
            // 
            this.checkBoxTimes.AutoSize = true;
            this.checkBoxTimes.Location = new System.Drawing.Point(15, 200);
            this.checkBoxTimes.Name = "checkBoxTimes";
            this.checkBoxTimes.Size = new System.Drawing.Size(48, 16);
            this.checkBoxTimes.TabIndex = 11;
            this.checkBoxTimes.Text = "定时";
            this.checkBoxTimes.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonHex);
            this.groupBox4.Controls.Add(this.radioButtonChar);
            this.groupBox4.Controls.Add(this.checkBoxConfirmed);
            this.groupBox4.Location = new System.Drawing.Point(10, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(220, 48);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            // 
            // radioButtonHex
            // 
            this.radioButtonHex.AutoSize = true;
            this.radioButtonHex.Location = new System.Drawing.Point(72, 20);
            this.radioButtonHex.Name = "radioButtonHex";
            this.radioButtonHex.Size = new System.Drawing.Size(71, 16);
            this.radioButtonHex.TabIndex = 1;
            this.radioButtonHex.Text = "十六进制";
            this.radioButtonHex.UseVisualStyleBackColor = true;
            // 
            // radioButtonChar
            // 
            this.radioButtonChar.AutoSize = true;
            this.radioButtonChar.Checked = true;
            this.radioButtonChar.Location = new System.Drawing.Point(5, 20);
            this.radioButtonChar.Name = "radioButtonChar";
            this.radioButtonChar.Size = new System.Drawing.Size(59, 16);
            this.radioButtonChar.TabIndex = 0;
            this.radioButtonChar.TabStop = true;
            this.radioButtonChar.Text = "字符串";
            this.radioButtonChar.UseVisualStyleBackColor = true;
            // 
            // checkBoxConfirmed
            // 
            this.checkBoxConfirmed.AutoSize = true;
            this.checkBoxConfirmed.Location = new System.Drawing.Point(154, 20);
            this.checkBoxConfirmed.Name = "checkBoxConfirmed";
            this.checkBoxConfirmed.Size = new System.Drawing.Size(60, 16);
            this.checkBoxConfirmed.TabIndex = 2;
            this.checkBoxConfirmed.Text = "确认帧";
            this.checkBoxConfirmed.UseVisualStyleBackColor = true;
            // 
            // buttonSendToMSP
            // 
            this.buttonSendToMSP.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSendToMSP.Location = new System.Drawing.Point(16, 361);
            this.buttonSendToMSP.Name = "buttonSendToMSP";
            this.buttonSendToMSP.Size = new System.Drawing.Size(98, 30);
            this.buttonSendToMSP.TabIndex = 9;
            this.buttonSendToMSP.Text = "发送";
            this.buttonSendToMSP.UseVisualStyleBackColor = true;
            this.buttonSendToMSP.Click += new System.EventHandler(this.buttonSendToMSP_Click);
            // 
            // textBoxPayload
            // 
            this.textBoxPayload.Location = new System.Drawing.Point(14, 121);
            this.textBoxPayload.Name = "textBoxPayload";
            this.textBoxPayload.Size = new System.Drawing.Size(216, 21);
            this.textBoxPayload.TabIndex = 8;
            // 
            // comboBox_Cmd
            // 
            this.comboBox_Cmd.FormattingEnabled = true;
            this.comboBox_Cmd.Location = new System.Drawing.Point(14, 158);
            this.comboBox_Cmd.Name = "comboBox_Cmd";
            this.comboBox_Cmd.Size = new System.Drawing.Size(216, 20);
            this.comboBox_Cmd.TabIndex = 7;
            this.comboBox_Cmd.SelectedIndexChanged += new System.EventHandler(this.comboBox_Cmd_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(11, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 6;
            this.label6.Text = "Payload:";
            // 
            // textBoxSendToPort
            // 
            this.textBoxSendToPort.Location = new System.Drawing.Point(112, 65);
            this.textBoxSendToPort.Name = "textBoxSendToPort";
            this.textBoxSendToPort.Size = new System.Drawing.Size(118, 21);
            this.textBoxSendToPort.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(10, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "下行端口号:";
            // 
            // CLaa_IoT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 437);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CLaa_IoT";
            this.Text = "CLaa-IoT";
            this.Load += new System.EventHandler(this.CLaa_IoT_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Text_MSPPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Text_MSPIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxJoinEui;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonUnRegister;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.TextBox textBoxDevEui;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxConfirmed;
        private System.Windows.Forms.RadioButton radioButtonHex;
        private System.Windows.Forms.RadioButton radioButtonChar;
        private System.Windows.Forms.TextBox textBoxSendToPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_Cmd;
        private System.Windows.Forms.TextBox textBoxPayload;
        private System.Windows.Forms.Button buttonSendToMSP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxNumber;
        private System.Windows.Forms.CheckBox checkBoxTimes;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.TextBox textBoxTimes;
        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.CheckBox checkBox_File;
        private System.Windows.Forms.Button buttonCancelSend;
        private System.Windows.Forms.Button buttonFind;
    }
}

