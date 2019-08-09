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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label_Test_Count = new System.Windows.Forms.Label();
            this.richTextBox_ReportResult = new System.Windows.Forms.RichTextBox();
            this.button_ReportResult = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox_saveData = new System.Windows.Forms.CheckBox();
            this.textBox_input_deveui = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButton_Hex = new System.Windows.Forms.RadioButton();
            this.radioButton_Char = new System.Windows.Forms.RadioButton();
            this.checkBox_Confirm = new System.Windows.Forms.CheckBox();
            this.button_CancelSend = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_DownPort = new System.Windows.Forms.TextBox();
            this.textBox_Number = new System.Windows.Forms.TextBox();
            this.comboBox_Cmd = new System.Windows.Forms.ComboBox();
            this.textBox_Time = new System.Windows.Forms.TextBox();
            this.textBox_Payload = new System.Windows.Forms.TextBox();
            this.checkBox_Number = new System.Windows.Forms.CheckBox();
            this.button_SendToMsp = new System.Windows.Forms.Button();
            this.checkBox_Time = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_APPKEY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_appnonce = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_appnonce);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_APPKEY);
            this.groupBox1.Controls.Add(this.label5);
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
            this.groupBox1.Size = new System.Drawing.Size(578, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // textBoxDevEui
            // 
            this.textBoxDevEui.Location = new System.Drawing.Point(319, 59);
            this.textBoxDevEui.Name = "textBoxDevEui";
            this.textBoxDevEui.Size = new System.Drawing.Size(142, 21);
            this.textBoxDevEui.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(236, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "DEVEUI:";
            // 
            // buttonUnRegister
            // 
            this.buttonUnRegister.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonUnRegister.Location = new System.Drawing.Point(477, 54);
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
            this.buttonRegister.Location = new System.Drawing.Point(477, 16);
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
            this.Text_MSPPort.Location = new System.Drawing.Point(319, 22);
            this.Text_MSPPort.Name = "Text_MSPPort";
            this.Text_MSPPort.Size = new System.Drawing.Size(142, 21);
            this.Text_MSPPort.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 60);
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
            this.label2.Location = new System.Drawing.Point(239, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "端口号：";
            // 
            // textBoxJoinEui
            // 
            this.textBoxJoinEui.Location = new System.Drawing.Point(90, 60);
            this.textBoxJoinEui.Name = "textBoxJoinEui";
            this.textBoxJoinEui.Size = new System.Drawing.Size(142, 21);
            this.textBoxJoinEui.TabIndex = 0;
            // 
            // Text_MSPIP
            // 
            this.Text_MSPIP.Location = new System.Drawing.Point(90, 22);
            this.Text_MSPIP.Name = "Text_MSPIP";
            this.Text_MSPIP.Size = new System.Drawing.Size(142, 21);
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
            this.groupBox2.Size = new System.Drawing.Size(578, 329);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.Location = new System.Drawing.Point(6, 19);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.Size = new System.Drawing.Size(566, 292);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Location = new System.Drawing.Point(596, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(488, 460);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label_Test_Count);
            this.groupBox9.Controls.Add(this.richTextBox_ReportResult);
            this.groupBox9.Controls.Add(this.button_ReportResult);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Controls.Add(this.checkBox_saveData);
            this.groupBox9.Controls.Add(this.textBox_input_deveui);
            this.groupBox9.Location = new System.Drawing.Point(9, 206);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(457, 242);
            this.groupBox9.TabIndex = 20;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "上行操作";
            // 
            // label_Test_Count
            // 
            this.label_Test_Count.AutoSize = true;
            this.label_Test_Count.Location = new System.Drawing.Point(198, 28);
            this.label_Test_Count.Name = "label_Test_Count";
            this.label_Test_Count.Size = new System.Drawing.Size(0, 12);
            this.label_Test_Count.TabIndex = 21;
            // 
            // richTextBox_ReportResult
            // 
            this.richTextBox_ReportResult.Location = new System.Drawing.Point(10, 96);
            this.richTextBox_ReportResult.Name = "richTextBox_ReportResult";
            this.richTextBox_ReportResult.Size = new System.Drawing.Size(441, 140);
            this.richTextBox_ReportResult.TabIndex = 20;
            this.richTextBox_ReportResult.Text = "";
            // 
            // button_ReportResult
            // 
            this.button_ReportResult.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ReportResult.Location = new System.Drawing.Point(333, 52);
            this.button_ReportResult.Name = "button_ReportResult";
            this.button_ReportResult.Size = new System.Drawing.Size(108, 29);
            this.button_ReportResult.TabIndex = 19;
            this.button_ReportResult.Text = "分析结果";
            this.button_ReportResult.UseVisualStyleBackColor = true;
            this.button_ReportResult.Click += new System.EventHandler(this.button_ReportResult_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(6, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 21);
            this.label9.TabIndex = 18;
            this.label9.Text = "录入设备编号:";
            // 
            // checkBox_saveData
            // 
            this.checkBox_saveData.AutoSize = true;
            this.checkBox_saveData.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_saveData.Location = new System.Drawing.Point(8, 20);
            this.checkBox_saveData.Name = "checkBox_saveData";
            this.checkBox_saveData.Size = new System.Drawing.Size(125, 25);
            this.checkBox_saveData.TabIndex = 17;
            this.checkBox_saveData.Text = "保存终端数据";
            this.checkBox_saveData.UseVisualStyleBackColor = true;
            this.checkBox_saveData.CheckedChanged += new System.EventHandler(this.checkBox_saveData_CheckedChanged);
            // 
            // textBox_input_deveui
            // 
            this.textBox_input_deveui.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_input_deveui.Location = new System.Drawing.Point(122, 53);
            this.textBox_input_deveui.Name = "textBox_input_deveui";
            this.textBox_input_deveui.Size = new System.Drawing.Size(194, 29);
            this.textBox_input_deveui.TabIndex = 16;
            this.textBox_input_deveui.TextChanged += new System.EventHandler(this.textBox_input_deveui_TextChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.button_CancelSend);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.textBox_DownPort);
            this.groupBox7.Controls.Add(this.textBox_Number);
            this.groupBox7.Controls.Add(this.comboBox_Cmd);
            this.groupBox7.Controls.Add(this.textBox_Time);
            this.groupBox7.Controls.Add(this.textBox_Payload);
            this.groupBox7.Controls.Add(this.checkBox_Number);
            this.groupBox7.Controls.Add(this.button_SendToMsp);
            this.groupBox7.Controls.Add(this.checkBox_Time);
            this.groupBox7.Location = new System.Drawing.Point(9, 16);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(472, 184);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "下行操作";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButton_Hex);
            this.groupBox8.Controls.Add(this.radioButton_Char);
            this.groupBox8.Controls.Add(this.checkBox_Confirm);
            this.groupBox8.Location = new System.Drawing.Point(10, 15);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(220, 48);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            // 
            // radioButton_Hex
            // 
            this.radioButton_Hex.AutoSize = true;
            this.radioButton_Hex.Location = new System.Drawing.Point(72, 20);
            this.radioButton_Hex.Name = "radioButton_Hex";
            this.radioButton_Hex.Size = new System.Drawing.Size(71, 16);
            this.radioButton_Hex.TabIndex = 1;
            this.radioButton_Hex.Text = "十六进制";
            this.radioButton_Hex.UseVisualStyleBackColor = true;
            // 
            // radioButton_Char
            // 
            this.radioButton_Char.AutoSize = true;
            this.radioButton_Char.Checked = true;
            this.radioButton_Char.Location = new System.Drawing.Point(5, 20);
            this.radioButton_Char.Name = "radioButton_Char";
            this.radioButton_Char.Size = new System.Drawing.Size(59, 16);
            this.radioButton_Char.TabIndex = 0;
            this.radioButton_Char.TabStop = true;
            this.radioButton_Char.Text = "字符串";
            this.radioButton_Char.UseVisualStyleBackColor = true;
            // 
            // checkBox_Confirm
            // 
            this.checkBox_Confirm.AutoSize = true;
            this.checkBox_Confirm.Location = new System.Drawing.Point(154, 20);
            this.checkBox_Confirm.Name = "checkBox_Confirm";
            this.checkBox_Confirm.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Confirm.TabIndex = 2;
            this.checkBox_Confirm.Text = "确认帧";
            this.checkBox_Confirm.UseVisualStyleBackColor = true;
            // 
            // button_CancelSend
            // 
            this.button_CancelSend.Enabled = false;
            this.button_CancelSend.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_CancelSend.Location = new System.Drawing.Point(9, 146);
            this.button_CancelSend.Name = "button_CancelSend";
            this.button_CancelSend.Size = new System.Drawing.Size(85, 30);
            this.button_CancelSend.TabIndex = 18;
            this.button_CancelSend.Text = "取消发送";
            this.button_CancelSend.UseVisualStyleBackColor = true;
            this.button_CancelSend.Click += new System.EventHandler(this.buttonCancelSend_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(249, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "Payload:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(5, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "下行端口号:";
            // 
            // textBox_DownPort
            // 
            this.textBox_DownPort.Location = new System.Drawing.Point(107, 76);
            this.textBox_DownPort.Name = "textBox_DownPort";
            this.textBox_DownPort.Size = new System.Drawing.Size(118, 21);
            this.textBox_DownPort.TabIndex = 4;
            // 
            // textBox_Number
            // 
            this.textBox_Number.Location = new System.Drawing.Point(308, 113);
            this.textBox_Number.Name = "textBox_Number";
            this.textBox_Number.Size = new System.Drawing.Size(160, 21);
            this.textBox_Number.TabIndex = 14;
            // 
            // comboBox_Cmd
            // 
            this.comboBox_Cmd.FormattingEnabled = true;
            this.comboBox_Cmd.Location = new System.Drawing.Point(252, 77);
            this.comboBox_Cmd.Name = "comboBox_Cmd";
            this.comboBox_Cmd.Size = new System.Drawing.Size(216, 20);
            this.comboBox_Cmd.TabIndex = 7;
            this.comboBox_Cmd.SelectedIndexChanged += new System.EventHandler(this.comboBox_Cmd_SelectedIndexChanged);
            // 
            // textBox_Time
            // 
            this.textBox_Time.Location = new System.Drawing.Point(65, 112);
            this.textBox_Time.Name = "textBox_Time";
            this.textBox_Time.Size = new System.Drawing.Size(160, 21);
            this.textBox_Time.TabIndex = 13;
            // 
            // textBox_Payload
            // 
            this.textBox_Payload.Location = new System.Drawing.Point(252, 46);
            this.textBox_Payload.Name = "textBox_Payload";
            this.textBox_Payload.Size = new System.Drawing.Size(216, 21);
            this.textBox_Payload.TabIndex = 8;
            // 
            // checkBox_Number
            // 
            this.checkBox_Number.AutoSize = true;
            this.checkBox_Number.Location = new System.Drawing.Point(254, 115);
            this.checkBox_Number.Name = "checkBox_Number";
            this.checkBox_Number.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Number.TabIndex = 12;
            this.checkBox_Number.Text = "计次";
            this.checkBox_Number.UseVisualStyleBackColor = true;
            // 
            // button_SendToMsp
            // 
            this.button_SendToMsp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_SendToMsp.Location = new System.Drawing.Point(366, 146);
            this.button_SendToMsp.Name = "button_SendToMsp";
            this.button_SendToMsp.Size = new System.Drawing.Size(98, 30);
            this.button_SendToMsp.TabIndex = 9;
            this.button_SendToMsp.Text = "发送";
            this.button_SendToMsp.UseVisualStyleBackColor = true;
            this.button_SendToMsp.Click += new System.EventHandler(this.buttonSendToMSP_Click);
            // 
            // checkBox_Time
            // 
            this.checkBox_Time.AutoSize = true;
            this.checkBox_Time.Location = new System.Drawing.Point(10, 115);
            this.checkBox_Time.Name = "checkBox_Time";
            this.checkBox_Time.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Time.TabIndex = 11;
            this.checkBox_Time.Text = "定时";
            this.checkBox_Time.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(11, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "APPKEY:";
            this.label5.UseWaitCursor = true;
            // 
            // textBox_APPKEY
            // 
            this.textBox_APPKEY.Location = new System.Drawing.Point(90, 93);
            this.textBox_APPKEY.Name = "textBox_APPKEY";
            this.textBox_APPKEY.Size = new System.Drawing.Size(218, 21);
            this.textBox_APPKEY.TabIndex = 8;
            this.textBox_APPKEY.Text = "00112233445566778899aabbccddeeff";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(322, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 21);
            this.label6.TabIndex = 9;
            this.label6.Text = "appnonce:";
            // 
            // textBox_appnonce
            // 
            this.textBox_appnonce.Location = new System.Drawing.Point(418, 93);
            this.textBox_appnonce.Name = "textBox_appnonce";
            this.textBox_appnonce.Size = new System.Drawing.Size(142, 21);
            this.textBox_appnonce.TabIndex = 10;
            // 
            // CLaa_IoT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 487);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CLaa_IoT";
            this.Text = "CLaa-IoT";
            this.Load += new System.EventHandler(this.CLaa_IoT_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
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
        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radioButton_Hex;
        private System.Windows.Forms.RadioButton radioButton_Char;
        private System.Windows.Forms.CheckBox checkBox_Confirm;
        private System.Windows.Forms.Button button_CancelSend;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_DownPort;
        private System.Windows.Forms.TextBox textBox_Number;
        private System.Windows.Forms.ComboBox comboBox_Cmd;
        private System.Windows.Forms.TextBox textBox_Time;
        private System.Windows.Forms.TextBox textBox_Payload;
        private System.Windows.Forms.CheckBox checkBox_Number;
        private System.Windows.Forms.Button button_SendToMsp;
        private System.Windows.Forms.CheckBox checkBox_Time;
        private System.Windows.Forms.CheckBox checkBox_saveData;
        private System.Windows.Forms.TextBox textBox_input_deveui;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_ReportResult;
        private System.Windows.Forms.RichTextBox richTextBox_ReportResult;
        private System.Windows.Forms.Label label_Test_Count;
        private System.Windows.Forms.TextBox textBox_APPKEY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_appnonce;
    }
}

