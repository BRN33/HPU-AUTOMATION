namespace HPU_OTOMASYON
{
    partial class MotorTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotorTest));
            this.btn_Cikis = new System.Windows.Forms.Button();
            this.lbl_Test_Yapan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cikis
            // 
            this.btn_Cikis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Cikis.Location = new System.Drawing.Point(31, 345);
            this.btn_Cikis.Name = "btn_Cikis";
            this.btn_Cikis.Size = new System.Drawing.Size(197, 55);
            this.btn_Cikis.TabIndex = 0;
            this.btn_Cikis.Text = "ÇIKIŞ";
            this.btn_Cikis.UseVisualStyleBackColor = true;
            this.btn_Cikis.Click += new System.EventHandler(this.btn_Cikis_Click);
            // 
            // lbl_Test_Yapan
            // 
            this.lbl_Test_Yapan.AutoSize = true;
            this.lbl_Test_Yapan.Location = new System.Drawing.Point(31, 95);
            this.lbl_Test_Yapan.Name = "lbl_Test_Yapan";
            this.lbl_Test_Yapan.Size = new System.Drawing.Size(44, 16);
            this.lbl_Test_Yapan.TabIndex = 1;
            this.lbl_Test_Yapan.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(564, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "GELİŞTİRİLME AŞAMASINDA LÜTFEN SABIRLI OLUNUZ :)";
            // 
            // MotorTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2375, 750);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Test_Yapan);
            this.Controls.Add(this.btn_Cikis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MotorTest";
            this.Text = "MotorTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MotorTest_Load);
            this.Resize += new System.EventHandler(this.MotorTest_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cikis;
        private System.Windows.Forms.Label lbl_Test_Yapan;
        private System.Windows.Forms.Label label1;
    }
}