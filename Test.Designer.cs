
namespace HPU_OTOMASYON
{
    partial class Test
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test));
            this.btn_Anasayfa = new System.Windows.Forms.Button();
            this.btn_TestBaslat = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_DirencOK = new System.Windows.Forms.Button();
            this.lblDirenc1 = new System.Windows.Forms.Label();
            this.lbl_TestYapan = new System.Windows.Forms.Label();
            this.lblDirenc2 = new System.Windows.Forms.Label();
            this.txt_Direnc2 = new System.Windows.Forms.TextBox();
            this.txt_Direnc1 = new System.Windows.Forms.TextBox();
            this.border_emg = new System.Windows.Forms.GroupBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.txt_BPxbar = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txt_BSxbar = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_Emergenc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNotBilgi1 = new System.Windows.Forms.TextBox();
            this.lbl_PlcDurumu = new System.Windows.Forms.Label();
            this.grpBoxBilgi = new System.Windows.Forms.GroupBox();
            this.lbl_Test_Tekrar = new System.Windows.Forms.Label();
            this.lbl_TestAdımı = new System.Windows.Forms.Label();
            this.timer_bw = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txt_Tarih = new System.Windows.Forms.Label();
            this.timer_NotBilgi = new System.Windows.Forms.Timer(this.components);
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ımageList2 = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.border_emg.SuspendLayout();
            this.grpBoxBilgi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Anasayfa
            // 
            this.btn_Anasayfa.AutoSize = true;
            this.btn_Anasayfa.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btn_Anasayfa.Location = new System.Drawing.Point(10, 679);
            this.btn_Anasayfa.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Anasayfa.Name = "btn_Anasayfa";
            this.btn_Anasayfa.Size = new System.Drawing.Size(135, 34);
            this.btn_Anasayfa.TabIndex = 0;
            this.btn_Anasayfa.Text = "Çıkış";
            this.btn_Anasayfa.UseVisualStyleBackColor = true;
            this.btn_Anasayfa.Visible = false;
            this.btn_Anasayfa.Click += new System.EventHandler(this.btn_Anasayfa_Click);
            // 
            // btn_TestBaslat
            // 
            this.btn_TestBaslat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_TestBaslat.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_TestBaslat.Location = new System.Drawing.Point(223, 55);
            this.btn_TestBaslat.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TestBaslat.Name = "btn_TestBaslat";
            this.btn_TestBaslat.Size = new System.Drawing.Size(242, 100);
            this.btn_TestBaslat.TabIndex = 6;
            this.btn_TestBaslat.Text = "TEST  BASLAT";
            this.btn_TestBaslat.UseVisualStyleBackColor = true;
            this.btn_TestBaslat.Click += new System.EventHandler(this.btn_TestBaslat_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Silver;
            this.groupBox1.Controls.Add(this.btn_DirencOK);
            this.groupBox1.Controls.Add(this.lblDirenc1);
            this.groupBox1.Controls.Add(this.btn_TestBaslat);
            this.groupBox1.Controls.Add(this.lbl_TestYapan);
            this.groupBox1.Controls.Add(this.lblDirenc2);
            this.groupBox1.Controls.Add(this.txt_Direnc2);
            this.groupBox1.Controls.Add(this.txt_Direnc1);
            this.groupBox1.Location = new System.Drawing.Point(10, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(470, 164);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Resize += new System.EventHandler(this.groupBox1_Resize);
            // 
            // btn_DirencOK
            // 
            this.btn_DirencOK.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_DirencOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_DirencOK.Location = new System.Drawing.Point(196, 105);
            this.btn_DirencOK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DirencOK.Name = "btn_DirencOK";
            this.btn_DirencOK.Size = new System.Drawing.Size(104, 37);
            this.btn_DirencOK.TabIndex = 12;
            this.btn_DirencOK.Text = "TAMAM";
            this.btn_DirencOK.UseVisualStyleBackColor = true;
            this.btn_DirencOK.Click += new System.EventHandler(this.btn_DirencOK_Click);
            // 
            // lblDirenc1
            // 
            this.lblDirenc1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDirenc1.AutoSize = true;
            this.lblDirenc1.BackColor = System.Drawing.Color.Silver;
            this.lblDirenc1.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDirenc1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDirenc1.Location = new System.Drawing.Point(15, 39);
            this.lblDirenc1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirenc1.Name = "lblDirenc1";
            this.lblDirenc1.Size = new System.Drawing.Size(170, 22);
            this.lblDirenc1.TabIndex = 3;
            this.lblDirenc1.Text = "Yalıtım Direnci 1 :";
            // 
            // lbl_TestYapan
            // 
            this.lbl_TestYapan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_TestYapan.AutoSize = true;
            this.lbl_TestYapan.BackColor = System.Drawing.Color.Silver;
            this.lbl_TestYapan.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_TestYapan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_TestYapan.Location = new System.Drawing.Point(40, 131);
            this.lbl_TestYapan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TestYapan.Name = "lbl_TestYapan";
            this.lbl_TestYapan.Size = new System.Drawing.Size(47, 26);
            this.lbl_TestYapan.TabIndex = 4;
            this.lbl_TestYapan.Text = "-----";
            // 
            // lblDirenc2
            // 
            this.lblDirenc2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDirenc2.AutoSize = true;
            this.lblDirenc2.BackColor = System.Drawing.Color.Silver;
            this.lblDirenc2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDirenc2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDirenc2.Location = new System.Drawing.Point(15, 80);
            this.lblDirenc2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirenc2.Name = "lblDirenc2";
            this.lblDirenc2.Size = new System.Drawing.Size(170, 22);
            this.lblDirenc2.TabIndex = 4;
            this.lblDirenc2.Text = "Yalıtım Direnci 2 :";
            // 
            // txt_Direnc2
            // 
            this.txt_Direnc2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Direnc2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_Direnc2.Location = new System.Drawing.Point(198, 76);
            this.txt_Direnc2.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Direnc2.MaxLength = 4;
            this.txt_Direnc2.Name = "txt_Direnc2";
            this.txt_Direnc2.Size = new System.Drawing.Size(163, 29);
            this.txt_Direnc2.TabIndex = 10;
            this.txt_Direnc2.Text = "11";
            // 
            // txt_Direnc1
            // 
            this.txt_Direnc1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Direnc1.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_Direnc1.Location = new System.Drawing.Point(198, 39);
            this.txt_Direnc1.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Direnc1.MaxLength = 4;
            this.txt_Direnc1.Name = "txt_Direnc1";
            this.txt_Direnc1.Size = new System.Drawing.Size(163, 29);
            this.txt_Direnc1.TabIndex = 10;
            this.txt_Direnc1.Text = "10";
            // 
            // border_emg
            // 
            this.border_emg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.border_emg.BackColor = System.Drawing.Color.Silver;
            this.border_emg.Controls.Add(this.textBox9);
            this.border_emg.Controls.Add(this.textBox8);
            this.border_emg.Controls.Add(this.textBox7);
            this.border_emg.Controls.Add(this.textBox6);
            this.border_emg.Controls.Add(this.textBox5);
            this.border_emg.Controls.Add(this.txt_BPxbar);
            this.border_emg.Controls.Add(this.textBox4);
            this.border_emg.Controls.Add(this.txt_BSxbar);
            this.border_emg.Controls.Add(this.textBox3);
            this.border_emg.Controls.Add(this.textBox2);
            this.border_emg.Controls.Add(this.label1);
            this.border_emg.Controls.Add(this.textBox1);
            this.border_emg.Controls.Add(this.label12);
            this.border_emg.Controls.Add(this.lbl_Emergenc);
            this.border_emg.Controls.Add(this.label3);
            this.border_emg.Controls.Add(this.label11);
            this.border_emg.Controls.Add(this.label9);
            this.border_emg.Controls.Add(this.label2);
            this.border_emg.Controls.Add(this.label10);
            this.border_emg.Controls.Add(this.label8);
            this.border_emg.Controls.Add(this.label7);
            this.border_emg.Controls.Add(this.label6);
            this.border_emg.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.border_emg.Location = new System.Drawing.Point(10, 229);
            this.border_emg.Margin = new System.Windows.Forms.Padding(2);
            this.border_emg.Name = "border_emg";
            this.border_emg.Padding = new System.Windows.Forms.Padding(2);
            this.border_emg.Size = new System.Drawing.Size(470, 484);
            this.border_emg.TabIndex = 8;
            this.border_emg.TabStop = false;
            this.border_emg.Resize += new System.EventHandler(this.border_emg_Resize);
            // 
            // textBox9
            // 
            this.textBox9.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox9.Location = new System.Drawing.Point(256, 34);
            this.textBox9.Margin = new System.Windows.Forms.Padding(2);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(204, 35);
            this.textBox9.TabIndex = 4;
            this.textBox9.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox8.Location = new System.Drawing.Point(256, 574);
            this.textBox8.Margin = new System.Windows.Forms.Padding(2);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(204, 35);
            this.textBox8.TabIndex = 4;
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox7.Location = new System.Drawing.Point(256, 529);
            this.textBox7.Margin = new System.Windows.Forms.Padding(2);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(204, 35);
            this.textBox7.TabIndex = 4;
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox6.Location = new System.Drawing.Point(256, 460);
            this.textBox6.Margin = new System.Windows.Forms.Padding(2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(204, 35);
            this.textBox6.TabIndex = 4;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox5.Location = new System.Drawing.Point(256, 414);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(204, 35);
            this.textBox5.TabIndex = 4;
            // 
            // txt_BPxbar
            // 
            this.txt_BPxbar.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_BPxbar.Location = new System.Drawing.Point(256, 353);
            this.txt_BPxbar.Margin = new System.Windows.Forms.Padding(2);
            this.txt_BPxbar.Name = "txt_BPxbar";
            this.txt_BPxbar.Size = new System.Drawing.Size(204, 35);
            this.txt_BPxbar.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox4.Location = new System.Drawing.Point(256, 309);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(204, 35);
            this.textBox4.TabIndex = 4;
            // 
            // txt_BSxbar
            // 
            this.txt_BSxbar.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_BSxbar.Location = new System.Drawing.Point(256, 243);
            this.txt_BSxbar.Margin = new System.Windows.Forms.Padding(2);
            this.txt_BSxbar.Name = "txt_BSxbar";
            this.txt_BSxbar.Size = new System.Drawing.Size(204, 35);
            this.txt_BSxbar.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox3.Location = new System.Drawing.Point(256, 199);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(204, 35);
            this.textBox3.TabIndex = 4;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox2.Location = new System.Drawing.Point(256, 136);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(204, 35);
            this.textBox2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(38, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Test Adımı :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.Location = new System.Drawing.Point(256, 93);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(204, 35);
            this.textBox1.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(38, 580);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(194, 29);
            this.label12.TabIndex = 3;
            this.label12.Text = "Park Fren Valfi :";
            // 
            // lbl_Emergenc
            // 
            this.lbl_Emergenc.AutoSize = true;
            this.lbl_Emergenc.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_Emergenc.Location = new System.Drawing.Point(38, 421);
            this.lbl_Emergenc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Emergenc.Name = "lbl_Emergenc";
            this.lbl_Emergenc.Size = new System.Drawing.Size(162, 29);
            this.lbl_Emergenc.TabIndex = 3;
            this.lbl_Emergenc.Text = "ASvalf (mA) :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(38, 358);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 29);
            this.label3.TabIndex = 3;
            this.label3.Text = "BPx bar :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(38, 537);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(187, 29);
            this.label11.TabIndex = 3;
            this.label11.Text = "Acil Fren Valfi :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(38, 314);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 29);
            this.label9.TabIndex = 3;
            this.label9.Text = "BP bar :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(38, 248);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "BSx bar :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(38, 466);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 29);
            this.label10.TabIndex = 2;
            this.label10.Text = "ATvalf (mA) :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(38, 204);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 29);
            this.label8.TabIndex = 2;
            this.label8.Text = "BS bar :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(38, 141);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = "Akım :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(38, 102);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 29);
            this.label6.TabIndex = 0;
            this.label6.Text = "Gerilim :";
            // 
            // txtNotBilgi1
            // 
            this.txtNotBilgi1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtNotBilgi1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotBilgi1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtNotBilgi1.ForeColor = System.Drawing.Color.Red;
            this.txtNotBilgi1.Location = new System.Drawing.Point(2, 15);
            this.txtNotBilgi1.Margin = new System.Windows.Forms.Padding(2);
            this.txtNotBilgi1.Multiline = true;
            this.txtNotBilgi1.Name = "txtNotBilgi1";
            this.txtNotBilgi1.Size = new System.Drawing.Size(735, 255);
            this.txtNotBilgi1.TabIndex = 0;
            this.txtNotBilgi1.Text = "...";
            this.txtNotBilgi1.TextChanged += new System.EventHandler(this.txtNotBilgi_TextChanged);
            // 
            // lbl_PlcDurumu
            // 
            this.lbl_PlcDurumu.AutoSize = true;
            this.lbl_PlcDurumu.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_PlcDurumu.Location = new System.Drawing.Point(44, 20);
            this.lbl_PlcDurumu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_PlcDurumu.Name = "lbl_PlcDurumu";
            this.lbl_PlcDurumu.Size = new System.Drawing.Size(101, 16);
            this.lbl_PlcDurumu.TabIndex = 9;
            this.lbl_PlcDurumu.Text = "PLC DURUMU";
            // 
            // grpBoxBilgi
            // 
            this.grpBoxBilgi.BackColor = System.Drawing.Color.LightGray;
            this.grpBoxBilgi.Controls.Add(this.lbl_Test_Tekrar);
            this.grpBoxBilgi.Controls.Add(this.lbl_TestAdımı);
            this.grpBoxBilgi.Controls.Add(this.txtNotBilgi1);
            this.grpBoxBilgi.Location = new System.Drawing.Point(490, 441);
            this.grpBoxBilgi.Margin = new System.Windows.Forms.Padding(2);
            this.grpBoxBilgi.Name = "grpBoxBilgi";
            this.grpBoxBilgi.Padding = new System.Windows.Forms.Padding(2);
            this.grpBoxBilgi.Size = new System.Drawing.Size(739, 272);
            this.grpBoxBilgi.TabIndex = 10;
            this.grpBoxBilgi.TabStop = false;
            this.grpBoxBilgi.Text = "                                      Test Adım No :                             " +
    "                                                                                " +
    "                                ";
            // 
            // lbl_Test_Tekrar
            // 
            this.lbl_Test_Tekrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Test_Tekrar.AutoSize = true;
            this.lbl_Test_Tekrar.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_Test_Tekrar.Location = new System.Drawing.Point(512, 8);
            this.lbl_Test_Tekrar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Test_Tekrar.Name = "lbl_Test_Tekrar";
            this.lbl_Test_Tekrar.Size = new System.Drawing.Size(245, 22);
            this.lbl_Test_Tekrar.TabIndex = 13;
            this.lbl_Test_Tekrar.Text = "TEST TEKRARLANIYOR...";
            this.lbl_Test_Tekrar.Visible = false;
            // 
            // lbl_TestAdımı
            // 
            this.lbl_TestAdımı.AutoSize = true;
            this.lbl_TestAdımı.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_TestAdımı.Location = new System.Drawing.Point(554, 3);
            this.lbl_TestAdımı.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TestAdımı.Name = "lbl_TestAdımı";
            this.lbl_TestAdımı.Size = new System.Drawing.Size(592, 29);
            this.lbl_TestAdımı.TabIndex = 1;
            this.lbl_TestAdımı.Text = "-----                                                                            " +
    " ";
            this.lbl_TestAdımı.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timer_bw
            // 
            this.timer_bw.Tick += new System.EventHandler(this.timer_bw_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            // 
            // txt_Tarih
            // 
            this.txt_Tarih.AutoSize = true;
            this.txt_Tarih.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.txt_Tarih.Location = new System.Drawing.Point(1040, 15);
            this.txt_Tarih.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_Tarih.Name = "txt_Tarih";
            this.txt_Tarih.Size = new System.Drawing.Size(23, 18);
            this.txt_Tarih.TabIndex = 1;
            this.txt_Tarih.Text = "---";
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(200, 200);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // chart1
            // 
            chartArea1.BackImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Hand;
            legend1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(492, 61);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series1.BorderColor = System.Drawing.Color.Aqua;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.SystemColors.Window;
            series1.Name = "Basınc-Zaman";
            dataPoint1.BackImage = "D:\\HPU_OTOMASYON\\HPU_OTOMASYON\\Resources\\logo.png";
            series1.Points.Add(dataPoint1);
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(736, 375);
            this.chart1.TabIndex = 12;
            this.chart1.Text = "chart1";
            this.chart1.Visible = false;
            // 
            // ımageList2
            // 
            this.ımageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.ımageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HPU_OTOMASYON.Properties.Resources.red;
            this.pictureBox1.Location = new System.Drawing.Point(3, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 734);
            this.ControlBox = false;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_Tarih);
            this.Controls.Add(this.grpBoxBilgi);
            this.Controls.Add(this.lbl_PlcDurumu);
            this.Controls.Add(this.border_emg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Anasayfa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(653, 86);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "                                                                                 " +
    "                                                                        HPU TEST" +
    " OTOMASTONU";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Test_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Test_FormClosed);
            this.Load += new System.EventHandler(this.Test_Load);
            this.Shown += new System.EventHandler(this.Test_Shown);
            this.Resize += new System.EventHandler(this.Test_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.border_emg.ResumeLayout(false);
            this.border_emg.PerformLayout();
            this.grpBoxBilgi.ResumeLayout(false);
            this.grpBoxBilgi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Anasayfa;
        private System.Windows.Forms.Button btn_TestBaslat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox border_emg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpBoxBilgi;
        private System.Windows.Forms.Label lbl_Emergenc;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label12;
       
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Timer timer_bw;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblDirenc1;
        private System.Windows.Forms.Label lblDirenc2;
        private System.Windows.Forms.TextBox txt_Direnc1;
        private System.Windows.Forms.Button btn_DirencOK;
        private System.Windows.Forms.Label txt_Tarih;
        private System.Windows.Forms.Timer timer_NotBilgi;
        private System.Windows.Forms.Label lbl_TestAdımı;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_BPxbar;
        private System.Windows.Forms.TextBox txt_BSxbar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Direnc2;
        private System.Windows.Forms.Label lbl_TestYapan;
        public System.Windows.Forms.ImageList ımageList1;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        public System.Windows.Forms.Label lbl_PlcDurumu;
        private System.Windows.Forms.ImageList ımageList2;
        public System.Windows.Forms.Label lbl_Test_Tekrar;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        public System.Windows.Forms.TextBox txtNotBilgi1;
    }
}