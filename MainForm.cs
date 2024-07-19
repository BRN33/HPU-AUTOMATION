using HPU_OTOMASYON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HPU_OTOMASYON
{
    public partial class MainForm : Form
    {
        public  User m_user;
        public static MainForm m_mf;


        internal PLCValue m_plcValue;

        public MainForm()
        {
            InitializeComponent();
            m_mf = this;

            m_user = new User();

            m_plcValue= new PLCValue();


            this.Visible = false;


            using (var form = new LoginForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Visible = true;
                }
            }

            DisplayManager.SetDoubleBuffered(this);
            DisplayManager.SetDoubleBuffered(btn_BTA);
            DisplayManager.SetDoubleBuffered(btn_DTA);
            DisplayManager.SetDoubleBuffered(btn_ITA);

            DisplayManager.SetDoubleBuffered(btn_MotorBoji);
            DisplayManager.SetDoubleBuffered(btn_TasıyıcıBoji);
            DisplayManager.SetDoubleBuffered(btn_YardımcıBoji);
            DisplayManager.SetDoubleBuffered(button1);
            DisplayManager.SetDoubleBuffered(button4);
            DisplayManager.SetDoubleBuffered(button5);
            DisplayManager.SetDoubleBuffered(button6);
            DisplayManager.SetDoubleBuffered(button7);
            DisplayManager.SetDoubleBuffered(button8);
            DisplayManager.SetDoubleBuffered(button9);

            //ReSizer.FormToBeResize(this);
        }


        //public MainForm(User user)
        //{
        //    InitializeComponent();
           

        //    ReSizer.FormToBeResize(this);
        //}


        //private static MainForm m_SingletonAna;   // Anasayfa ile ilgili bir nesneyi tek seferde cagırmak icin Singleton olusturuyoruz
        //public static MainForm frmAna_Singleton()
        //{
        //    if (m_SingletonAna == null)
        //        m_SingletonAna = nesne;

        //    return m_SingletonAna;

        //}

        private void Anasayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Form1 frm1 = Form1.frm1Singleton();

            //frm1.Show();
        }

        private void btn_BTA_Click(object sender, EventArgs e)
        {

            groupBox_BTA.Show();
            groupBox_DTA.Hide();
            groupBox_ITA.Hide();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
           
            groupBox_BTA.Hide();
            groupBox_DTA.Hide();
            groupBox_ITA.Hide();
        }

        private void btn_ITA_Click(object sender, EventArgs e)
        {
            groupBox_ITA.Show();
            groupBox_BTA.Hide();
            groupBox_DTA.Hide();
        }

        private void btn_DTA_Click(object sender, EventArgs e)
        {
            groupBox_DTA.Show();
            groupBox_ITA.Hide();
            groupBox_BTA.Hide();
        }

        private void btn_MotorBoji_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Geliştirme Aşamasında","Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);


            MotorTest testModal = MotorTest.Singleton(this);
            testModal.Owner = this;
            testModal.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;


            using (var form = new LoginForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Visible = true;
                }
            }

        }

        private void btn_TasıyıcıBoji_Click(object sender, EventArgs e)
        {
            Test testModal = Test.Singleton(this);
            testModal.Owner = this;
            testModal.ShowDialog();



        }

        private void btn_YardımcıBoji_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Geliştirme Aşamasında", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Geliştirme Aşamasında", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Geliştirme Aşamasında", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Geliştirme Aşamasında", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Geliştirme Aşamasında", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Geliştirme Aşamasında", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Geliştirme Aşamasında", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Anasayfa_Resize(object sender, EventArgs e)
        {
            ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
        }
    }
}
