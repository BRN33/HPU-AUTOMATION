using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace HPU_OTOMASYON
{
    public partial class MotorTest : Form
    {

        public class AdsInitializeException : AdsException { }
        public AmsAddress ClientAddress { get; }




        LoginForm frm1 = new LoginForm();

        TcAdsClient tcAds;      
        string amsNo = "5.45.65.150.1.1";// HPU Cihazındaki Twincat adresi bu

        public static MotorTest motor_test;
        public MainForm m_mf;

        public static MotorTest Singleton(MainForm mf)
        {
            if (motor_test == null)
                motor_test = new MotorTest(mf);

            return motor_test;
        }

        private MotorTest(MainForm mf)
           : this()
        {
            m_mf = mf;
        }
        public MotorTest()
        {
            InitializeComponent();

            ReSizer.FormToBeResize(this); //Form resize metodu
        }



        private void btn_Cikis_Click(object sender, EventArgs e)
        {

            this.Close();

            MainForm.m_mf.Show();
        }

        private void MotorTest_Resize(object sender, EventArgs e)
        {
            ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void MotorTest_Load(object sender, EventArgs e)
        {
            DisplayManager.LabelInvoke(lbl_Test_Yapan, MainForm.m_mf.m_user.Name + "  " + MainForm.m_mf.m_user.Surname + " --- " + "Ürün Seri No :" + " " + MainForm.m_mf.m_user.SeriNo);
        }
    }
}
