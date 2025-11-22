namespace Cloud
{
    partial class Form1
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
            this.btn_CreateCosmosClient = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.General = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_EnvType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_PrimaryKey = new System.Windows.Forms.TextBox();
            this.textBox_URI = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_DevId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_DevMail = new System.Windows.Forms.TextBox();
            this.textBox_DevName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DBandContainer = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_CountTables = new System.Windows.Forms.Button();
            this.btn_CountDBs = new System.Windows.Forms.Button();
            this.textBox_TablesCounter = new System.Windows.Forms.TextBox();
            this.textBox_DBsCounter = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox_TablesNames = new System.Windows.Forms.ComboBox();
            this.comboBox_DBsNames = new System.Windows.Forms.ComboBox();
            this.btn_GetTablesNames = new System.Windows.Forms.Button();
            this.btn_GetDBsNames = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_CreateDataInCloud = new System.Windows.Forms.Button();
            this.textBox_DatabaseInput = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_ContainerInput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Exercises = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comboBox_SearchedDBs = new System.Windows.Forms.ComboBox();
            this.btn_SearchDB = new System.Windows.Forms.Button();
            this.textBox_SearchDB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_databaseCounter = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_DoesDBExist = new System.Windows.Forms.Button();
            this.textBox_CheckDB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_DoesDBExist = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBox_DBsWithTablesCount = new System.Windows.Forms.ComboBox();
            this.btn_DBsWithTablesCount = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.General.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DBandContainer.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Exercises.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_CreateCosmosClient
            // 
            this.btn_CreateCosmosClient.BackColor = System.Drawing.Color.OldLace;
            this.btn_CreateCosmosClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateCosmosClient.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btn_CreateCosmosClient.Location = new System.Drawing.Point(488, 211);
            this.btn_CreateCosmosClient.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_CreateCosmosClient.Name = "btn_CreateCosmosClient";
            this.btn_CreateCosmosClient.Size = new System.Drawing.Size(234, 88);
            this.btn_CreateCosmosClient.TabIndex = 0;
            this.btn_CreateCosmosClient.Text = "Create Database";
            this.btn_CreateCosmosClient.UseVisualStyleBackColor = false;
            this.btn_CreateCosmosClient.Click += new System.EventHandler(this.btn_CreateCosmosClient_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.General);
            this.tabControl1.Controls.Add(this.DBandContainer);
            this.tabControl1.Controls.Add(this.Exercises);
            this.tabControl1.Location = new System.Drawing.Point(9, 14);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(763, 562);
            this.tabControl1.TabIndex = 1;
            // 
            // General
            // 
            this.General.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.General.Controls.Add(this.groupBox2);
            this.General.Controls.Add(this.groupBox1);
            this.General.Controls.Add(this.btn_CreateCosmosClient);
            this.General.Location = new System.Drawing.Point(4, 22);
            this.General.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.General.Name = "General";
            this.General.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.General.Size = new System.Drawing.Size(755, 536);
            this.General.TabIndex = 0;
            this.General.Text = "General Details and Cosmos Client Creation";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_EnvType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_PrimaryKey);
            this.groupBox2.Controls.Add(this.textBox_URI);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(20, 143);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(385, 114);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Env Details";
            // 
            // textBox_EnvType
            // 
            this.textBox_EnvType.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_EnvType.Location = new System.Drawing.Point(107, 24);
            this.textBox_EnvType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_EnvType.Name = "textBox_EnvType";
            this.textBox_EnvType.ReadOnly = true;
            this.textBox_EnvType.Size = new System.Drawing.Size(178, 21);
            this.textBox_EnvType.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Primary Key";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Env Type";
            // 
            // textBox_PrimaryKey
            // 
            this.textBox_PrimaryKey.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_PrimaryKey.Location = new System.Drawing.Point(107, 68);
            this.textBox_PrimaryKey.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_PrimaryKey.Name = "textBox_PrimaryKey";
            this.textBox_PrimaryKey.ReadOnly = true;
            this.textBox_PrimaryKey.Size = new System.Drawing.Size(178, 21);
            this.textBox_PrimaryKey.TabIndex = 5;
            // 
            // textBox_URI
            // 
            this.textBox_URI.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_URI.Location = new System.Drawing.Point(107, 47);
            this.textBox_URI.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_URI.Name = "textBox_URI";
            this.textBox_URI.ReadOnly = true;
            this.textBox_URI.Size = new System.Drawing.Size(178, 21);
            this.textBox_URI.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(66, 47);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "URI";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_DevId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_DevMail);
            this.groupBox1.Controls.Add(this.textBox_DevName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(385, 114);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Developer Details";
            // 
            // textBox_DevId
            // 
            this.textBox_DevId.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_DevId.Location = new System.Drawing.Point(107, 28);
            this.textBox_DevId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_DevId.Name = "textBox_DevId";
            this.textBox_DevId.ReadOnly = true;
            this.textBox_DevId.Size = new System.Drawing.Size(178, 21);
            this.textBox_DevId.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID";
            // 
            // textBox_DevMail
            // 
            this.textBox_DevMail.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_DevMail.Location = new System.Drawing.Point(107, 72);
            this.textBox_DevMail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_DevMail.Name = "textBox_DevMail";
            this.textBox_DevMail.ReadOnly = true;
            this.textBox_DevMail.Size = new System.Drawing.Size(178, 21);
            this.textBox_DevMail.TabIndex = 5;
            // 
            // textBox_DevName
            // 
            this.textBox_DevName.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_DevName.Location = new System.Drawing.Point(107, 50);
            this.textBox_DevName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_DevName.Name = "textBox_DevName";
            this.textBox_DevName.ReadOnly = true;
            this.textBox_DevName.Size = new System.Drawing.Size(178, 21);
            this.textBox_DevName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name";
            // 
            // DBandContainer
            // 
            this.DBandContainer.BackColor = System.Drawing.Color.Silver;
            this.DBandContainer.Controls.Add(this.groupBox5);
            this.DBandContainer.Controls.Add(this.groupBox4);
            this.DBandContainer.Controls.Add(this.groupBox3);
            this.DBandContainer.Location = new System.Drawing.Point(4, 22);
            this.DBandContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DBandContainer.Name = "DBandContainer";
            this.DBandContainer.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DBandContainer.Size = new System.Drawing.Size(755, 536);
            this.DBandContainer.TabIndex = 1;
            this.DBandContainer.Text = "Databases and Containers";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Tan;
            this.groupBox5.Controls.Add(this.btn_CountTables);
            this.groupBox5.Controls.Add(this.btn_CountDBs);
            this.groupBox5.Controls.Add(this.textBox_TablesCounter);
            this.groupBox5.Controls.Add(this.textBox_DBsCounter);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(20, 291);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Size = new System.Drawing.Size(693, 141);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Counting Entities In Cloud";
            // 
            // btn_CountTables
            // 
            this.btn_CountTables.Location = new System.Drawing.Point(20, 80);
            this.btn_CountTables.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_CountTables.Name = "btn_CountTables";
            this.btn_CountTables.Size = new System.Drawing.Size(212, 24);
            this.btn_CountTables.TabIndex = 7;
            this.btn_CountTables.Text = "Count Tables in Cloud";
            this.btn_CountTables.UseVisualStyleBackColor = true;
            this.btn_CountTables.Click += new System.EventHandler(this.btn_CountTables_Click);
            // 
            // btn_CountDBs
            // 
            this.btn_CountDBs.Location = new System.Drawing.Point(20, 40);
            this.btn_CountDBs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_CountDBs.Name = "btn_CountDBs";
            this.btn_CountDBs.Size = new System.Drawing.Size(212, 24);
            this.btn_CountDBs.TabIndex = 6;
            this.btn_CountDBs.Text = "Count Databases in Cloud";
            this.btn_CountDBs.UseVisualStyleBackColor = true;
            this.btn_CountDBs.Click += new System.EventHandler(this.btn_CountDBs_Click);
            // 
            // textBox_TablesCounter
            // 
            this.textBox_TablesCounter.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_TablesCounter.Location = new System.Drawing.Point(250, 80);
            this.textBox_TablesCounter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_TablesCounter.Name = "textBox_TablesCounter";
            this.textBox_TablesCounter.ReadOnly = true;
            this.textBox_TablesCounter.Size = new System.Drawing.Size(58, 24);
            this.textBox_TablesCounter.TabIndex = 1;
            // 
            // textBox_DBsCounter
            // 
            this.textBox_DBsCounter.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_DBsCounter.Location = new System.Drawing.Point(250, 40);
            this.textBox_DBsCounter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_DBsCounter.Name = "textBox_DBsCounter";
            this.textBox_DBsCounter.ReadOnly = true;
            this.textBox_DBsCounter.Size = new System.Drawing.Size(58, 25);
            this.textBox_DBsCounter.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Tan;
            this.groupBox4.Controls.Add(this.comboBox_TablesNames);
            this.groupBox4.Controls.Add(this.comboBox_DBsNames);
            this.groupBox4.Controls.Add(this.btn_GetTablesNames);
            this.groupBox4.Controls.Add(this.btn_GetDBsNames);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(20, 146);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(693, 141);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Databases and Tables in Cloud";
            // 
            // comboBox_TablesNames
            // 
            this.comboBox_TablesNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TablesNames.FormattingEnabled = true;
            this.comboBox_TablesNames.Location = new System.Drawing.Point(240, 89);
            this.comboBox_TablesNames.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox_TablesNames.Name = "comboBox_TablesNames";
            this.comboBox_TablesNames.Size = new System.Drawing.Size(340, 28);
            this.comboBox_TablesNames.TabIndex = 12;
            // 
            // comboBox_DBsNames
            // 
            this.comboBox_DBsNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DBsNames.FormattingEnabled = true;
            this.comboBox_DBsNames.Location = new System.Drawing.Point(240, 37);
            this.comboBox_DBsNames.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox_DBsNames.Name = "comboBox_DBsNames";
            this.comboBox_DBsNames.Size = new System.Drawing.Size(340, 28);
            this.comboBox_DBsNames.TabIndex = 11;
            // 
            // btn_GetTablesNames
            // 
            this.btn_GetTablesNames.Location = new System.Drawing.Point(11, 89);
            this.btn_GetTablesNames.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_GetTablesNames.Name = "btn_GetTablesNames";
            this.btn_GetTablesNames.Size = new System.Drawing.Size(212, 25);
            this.btn_GetTablesNames.TabIndex = 6;
            this.btn_GetTablesNames.Text = "Get Tables Names from Cloud";
            this.btn_GetTablesNames.UseVisualStyleBackColor = true;
            this.btn_GetTablesNames.Click += new System.EventHandler(this.btn_GetTablesNames_Click);
            // 
            // btn_GetDBsNames
            // 
            this.btn_GetDBsNames.Location = new System.Drawing.Point(11, 37);
            this.btn_GetDBsNames.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_GetDBsNames.Name = "btn_GetDBsNames";
            this.btn_GetDBsNames.Size = new System.Drawing.Size(212, 25);
            this.btn_GetDBsNames.TabIndex = 5;
            this.btn_GetDBsNames.Text = "Get DBs Names from Cloud";
            this.btn_GetDBsNames.UseVisualStyleBackColor = true;
            this.btn_GetDBsNames.Click += new System.EventHandler(this.btn_GetDBsNames_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Tan;
            this.groupBox3.Controls.Add(this.btn_CreateDataInCloud);
            this.groupBox3.Controls.Add(this.textBox_DatabaseInput);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox_ContainerInput);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(20, 16);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(693, 127);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Create Entities in Cloud";
            // 
            // btn_CreateDataInCloud
            // 
            this.btn_CreateDataInCloud.Location = new System.Drawing.Point(280, 80);
            this.btn_CreateDataInCloud.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_CreateDataInCloud.Name = "btn_CreateDataInCloud";
            this.btn_CreateDataInCloud.Size = new System.Drawing.Size(212, 31);
            this.btn_CreateDataInCloud.TabIndex = 5;
            this.btn_CreateDataInCloud.Text = "Create Data in Cloud";
            this.btn_CreateDataInCloud.UseVisualStyleBackColor = true;
            this.btn_CreateDataInCloud.Click += new System.EventHandler(this.btn_CreateDataInCloud_Click);
            // 
            // textBox_DatabaseInput
            // 
            this.textBox_DatabaseInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_DatabaseInput.Location = new System.Drawing.Point(154, 37);
            this.textBox_DatabaseInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_DatabaseInput.Name = "textBox_DatabaseInput";
            this.textBox_DatabaseInput.Size = new System.Drawing.Size(178, 21);
            this.textBox_DatabaseInput.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(77, 37);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Database";
            // 
            // textBox_ContainerInput
            // 
            this.textBox_ContainerInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ContainerInput.Location = new System.Drawing.Point(458, 37);
            this.textBox_ContainerInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_ContainerInput.Name = "textBox_ContainerInput";
            this.textBox_ContainerInput.Size = new System.Drawing.Size(178, 21);
            this.textBox_ContainerInput.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(381, 37);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 20);
            this.label9.TabIndex = 4;
            this.label9.Text = "Container";
            // 
            // Exercises
            // 
            this.Exercises.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Exercises.Controls.Add(this.groupBox8);
            this.Exercises.Controls.Add(this.groupBox7);
            this.Exercises.Controls.Add(this.groupBox6);
            this.Exercises.Location = new System.Drawing.Point(4, 22);
            this.Exercises.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Exercises.Name = "Exercises";
            this.Exercises.Size = new System.Drawing.Size(755, 536);
            this.Exercises.TabIndex = 2;
            this.Exercises.Text = "Exercises";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Tan;
            this.groupBox6.Controls.Add(this.comboBox_SearchedDBs);
            this.groupBox6.Controls.Add(this.btn_SearchDB);
            this.groupBox6.Controls.Add(this.textBox_SearchDB);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(19, 20);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox6.Size = new System.Drawing.Size(693, 127);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Search Databases Using Substring";
            // 
            // comboBox_SearchedDBs
            // 
            this.comboBox_SearchedDBs.FormattingEnabled = true;
            this.comboBox_SearchedDBs.Location = new System.Drawing.Point(352, 22);
            this.comboBox_SearchedDBs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox_SearchedDBs.Name = "comboBox_SearchedDBs";
            this.comboBox_SearchedDBs.Size = new System.Drawing.Size(92, 28);
            this.comboBox_SearchedDBs.TabIndex = 6;
            // 
            // btn_SearchDB
            // 
            this.btn_SearchDB.Location = new System.Drawing.Point(110, 74);
            this.btn_SearchDB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_SearchDB.Name = "btn_SearchDB";
            this.btn_SearchDB.Size = new System.Drawing.Size(212, 31);
            this.btn_SearchDB.TabIndex = 5;
            this.btn_SearchDB.Text = "Search";
            this.btn_SearchDB.UseVisualStyleBackColor = true;
            this.btn_SearchDB.Click += new System.EventHandler(this.btn_SearchDB_Click);
            // 
            // textBox_SearchDB
            // 
            this.textBox_SearchDB.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SearchDB.Location = new System.Drawing.Point(69, 27);
            this.textBox_SearchDB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_SearchDB.Name = "textBox_SearchDB";
            this.textBox_SearchDB.Size = new System.Drawing.Size(110, 21);
            this.textBox_SearchDB.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 28);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(268, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "Databases";
            // 
            // btn_databaseCounter
            // 
            this.btn_databaseCounter.Location = new System.Drawing.Point(0, 0);
            this.btn_databaseCounter.Name = "btn_databaseCounter";
            this.btn_databaseCounter.Size = new System.Drawing.Size(75, 23);
            this.btn_databaseCounter.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.Tan;
            this.groupBox7.Controls.Add(this.textBox_DoesDBExist);
            this.groupBox7.Controls.Add(this.btn_DoesDBExist);
            this.groupBox7.Controls.Add(this.textBox_CheckDB);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(19, 151);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(693, 127);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Checks if Database Exists";
            // 
            // btn_DoesDBExist
            // 
            this.btn_DoesDBExist.Location = new System.Drawing.Point(206, 80);
            this.btn_DoesDBExist.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DoesDBExist.Name = "btn_DoesDBExist";
            this.btn_DoesDBExist.Size = new System.Drawing.Size(212, 31);
            this.btn_DoesDBExist.TabIndex = 5;
            this.btn_DoesDBExist.Text = "Search";
            this.btn_DoesDBExist.UseVisualStyleBackColor = true;
            this.btn_DoesDBExist.Click += new System.EventHandler(this.btn_DoesDBExist_Click);
            // 
            // textBox_CheckDB
            // 
            this.textBox_CheckDB.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_CheckDB.Location = new System.Drawing.Point(143, 29);
            this.textBox_CheckDB.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_CheckDB.Name = "textBox_CheckDB";
            this.textBox_CheckDB.Size = new System.Drawing.Size(163, 21);
            this.textBox_CheckDB.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 28);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "Database Name";
            // 
            // textBox_DoesDBExist
            // 
            this.textBox_DoesDBExist.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_DoesDBExist.Location = new System.Drawing.Point(321, 29);
            this.textBox_DoesDBExist.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_DoesDBExist.Name = "textBox_DoesDBExist";
            this.textBox_DoesDBExist.ReadOnly = true;
            this.textBox_DoesDBExist.Size = new System.Drawing.Size(320, 21);
            this.textBox_DoesDBExist.TabIndex = 6;
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.Tan;
            this.groupBox8.Controls.Add(this.comboBox_DBsWithTablesCount);
            this.groupBox8.Controls.Add(this.btn_DBsWithTablesCount);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(19, 282);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(693, 127);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Databases with Count Of Tables";
            // 
            // comboBox_DBsWithTablesCount
            // 
            this.comboBox_DBsWithTablesCount.FormattingEnabled = true;
            this.comboBox_DBsWithTablesCount.Location = new System.Drawing.Point(321, 43);
            this.comboBox_DBsWithTablesCount.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_DBsWithTablesCount.Name = "comboBox_DBsWithTablesCount";
            this.comboBox_DBsWithTablesCount.Size = new System.Drawing.Size(320, 28);
            this.comboBox_DBsWithTablesCount.TabIndex = 6;
            // 
            // btn_DBsWithTablesCount
            // 
            this.btn_DBsWithTablesCount.Location = new System.Drawing.Point(18, 40);
            this.btn_DBsWithTablesCount.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DBsWithTablesCount.Name = "btn_DBsWithTablesCount";
            this.btn_DBsWithTablesCount.Size = new System.Drawing.Size(288, 31);
            this.btn_DBsWithTablesCount.TabIndex = 5;
            this.btn_DBsWithTablesCount.Text = "Get Databases With Their Tables Count";
            this.btn_DBsWithTablesCount.UseVisualStyleBackColor = true;
            this.btn_DBsWithTablesCount.Click += new System.EventHandler(this.btn_DBsWithTablesCount_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 600);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "it ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.General.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DBandContainer.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Exercises.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_CreateCosmosClient;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage General;
        private System.Windows.Forms.TabPage DBandContainer;
        private System.Windows.Forms.TextBox textBox_DevId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_DevName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_DevMail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_EnvType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_PrimaryKey;
        private System.Windows.Forms.TextBox textBox_URI;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_DatabaseInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_ContainerInput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_CreateDataInCloud;
        private System.Windows.Forms.TabPage Exercises;
        private System.Windows.Forms.Button btn_databaseCounter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_GetTablesNames;
        private System.Windows.Forms.Button btn_GetDBsNames;
        private System.Windows.Forms.ComboBox comboBox_DBsNames;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox_TablesCounter;
        private System.Windows.Forms.TextBox textBox_DBsCounter;
        private System.Windows.Forms.ComboBox comboBox_TablesNames;
        private System.Windows.Forms.Button btn_CountTables;
        private System.Windows.Forms.Button btn_CountDBs;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comboBox_SearchedDBs;
        private System.Windows.Forms.Button btn_SearchDB;
        private System.Windows.Forms.TextBox textBox_SearchDB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btn_DoesDBExist;
        private System.Windows.Forms.TextBox textBox_CheckDB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_DoesDBExist;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox comboBox_DBsWithTablesCount;
        private System.Windows.Forms.Button btn_DBsWithTablesCount;
    }
}

