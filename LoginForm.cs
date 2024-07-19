//using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HPU_OTOMASYON
{
    public partial class LoginForm : Form
    {
       
        //private static LoginForm m_Singleton;   // Form ile ilgili bir nesneyi tek seferde cagırmak icin Singleton olusturuyoruz
        //public static LoginForm frm1Singleton()
        //{
        //    if (m_Singleton == null)
        //        m_Singleton = new LoginForm();

        //    return m_Singleton;

        //}


        public LoginForm()
        {
            InitializeComponent();
           

            //SetDoubleBuffered(this);
            ReSizer.FormToBeResize(this);


        }


        private void Form1_Load(object sender, EventArgs e)
        {

            txt_Adi.Text = "";
            txt_Soyadi.Text = "";
            txt_Sicil.Text = "";
            txt_SeriNo.Text = "";
        }

        private void btn_Giris_Click(object sender, EventArgs e)
        {
            if (txt_Sicil.Text == string.Empty)
            {
                FlexibleMessageBox.Show("Lütfen bir Sicil Numarası giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (txt_Sicil.Text != string.Empty)
            {
               MainForm.m_mf.m_user.Name = txt_Adi.Text;
               MainForm.m_mf.m_user.Surname = txt_Soyadi.Text;
               MainForm.m_mf.m_user.PersonNo = txt_Sicil.Text;
               MainForm.m_mf.m_user.SeriNo = (txt_SeriNo.Text);

                //this.Hide();

                //MainForm anasayfa = new MainForm(MainForm.m_mf.m_user);

                this.DialogResult = DialogResult.OK;
                this.Close();
                //anasayfa.Show();

            }
        }

        private void btn_Kapat_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
            //DestroyHandle();



        }

        private void txt_Adi_TextChanged(object sender, EventArgs e)
        {
            //MainForm.m_mf.m_user.Name = txt_Adi.Text;
            if (txt_Adi.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                txt_Adi.Invoke((MethodInvoker)delegate
                {
                    MainForm.m_mf.m_user.Name = txt_Adi.Text;
                });
            }
            else
            {
                MainForm.m_mf.m_user.Name = txt_Adi.Text;
            }
        }

        private void txt_Soyadi_TextChanged(object sender, EventArgs e)
        {
            //MainForm.m_mf.m_user.Surname = txt_Soyadi.Text;
            if (txt_Soyadi.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                txt_Soyadi.Invoke((MethodInvoker)delegate
                {
                    MainForm.m_mf.m_user.Surname = txt_Soyadi.Text;
                });
            }
            else
            {
                MainForm.m_mf.m_user.Surname = txt_Soyadi.Text;
            }
        }

        private void txt_Sicil_TextChanged(object sender, EventArgs e)
        {
            MainForm.m_mf.m_user.PersonNo = txt_Sicil.Text;

            if (txt_Sicil.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                txt_Sicil.Invoke((MethodInvoker)delegate
                {
                    MainForm.m_mf.m_user.PersonNo = txt_Sicil.Text;
                });
            }
            else
            {
                MainForm.m_mf.m_user.PersonNo = txt_Sicil.Text;
            }
        }

        private void txt_SeriNo_TextChanged(object sender, EventArgs e)
        {
        }

        private void btn_Kapat_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
            //DestroyHandle();
        }



        //private void txt_SeriNo_TextChanged_1(object sender, EventArgs e)
        //{

        //    MainForm.m_mf.mMainForm.m_mf.m_user.SeriNo = Convert.ToInt32(txt_Sicil.Text);
        //}



        private void btn_Giris_Click_1(object sender, EventArgs e)
        {
            //this.Hide();

            //MainForm anasayfa = new MainForm(MainForm.m_mf.m_user);


            //anasayfa.Show();

        }

        //private void txt_Sicil_TextChanged_1(object sender, EventArgs e)
        //{

        //    MainForm.m_mf.m_user.PersonNo = Convert.ToInt32(txt_Sicil.Text);
        //}

        //private void txt_Soyadi_TextChanged_1(object sender, EventArgs e)
        //{

        //    MainForm.m_mf.m_user.Surname = txt_Soyadi.Text;
        //}



        //private void txt_Adi_TextChanged_1(object sender, EventArgs e)
        //{

        //    MainForm.m_mf.m_user.Name = txt_Adi.Text;
        //}



        private void Form1_Resize(object sender, EventArgs e)
        {
            ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void txt_Adi_Resize(object sender, EventArgs e)
        {
            //TextBox bytext = (TextBox)sender;
            //if (bytext==txt_Adi)
            //{
            //    txt_Adi.Size = new Size(this.ClientSize.Width - 100, this.ClientSize.Height - 3);
            //}
            //else if (bytext == txt_Sicil)
            //{
            //    txt_Sicil.Size = new Size(this.ClientSize.Width  - 100, this.ClientSize.Height - 3);
            //}
            //else if (bytext == txt_SeriNo)
            //{
            //    txt_SeriNo.Size = new Size(this.ClientSize.Width  - 100, this.ClientSize.Height - 3);
            //}
            //else if (bytext == txt_Soyadi)
            //{
            //    txt_Soyadi.Size = new Size(this.ClientSize.Width  - 100, this.ClientSize.Height - 3);
            //}

        }

        //public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        //{
        //    //Taxes: Remote Desktop Connection and painting
        //    //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
        //    if (System.Windows.Forms.SystemInformation.TerminalServerSession)
        //        return;

        //    System.Reflection.PropertyInfo aProp =
        //          typeof(System.Windows.Forms.Control).GetProperty(
        //                "DoubleBuffered",
        //                System.Reflection.BindingFlags.NonPublic |
        //                System.Reflection.BindingFlags.Instance);

        //    aProp.SetValue(c, true, null);
        //}

        private void panel1_Resize(object sender, EventArgs e)
        {
            //panel1.Size = new Size(this.ClientSize.Width - 400, this.ClientSize.Height - 3);
        }

        private void txt_Adi_SizeChanged(object sender, EventArgs e)
        {
            //TextBox tb = sender as TextBox;
            //if (tb.Height < 10) return;
            //if (tb == null) return;
            //if (tb.Text == "") return;
            //SizeF stringSize;

            //// create a graphics object for this form
            //using (Graphics gfx = this.CreateGraphics())
            //{
            //    // Get the size given the string and the font
            //    stringSize = gfx.MeasureString(tb.Text, tb.Font);
            //    //test how many rows
            //    int rows = (int)((double)tb.Height / (stringSize.Height));
            //    if (rows == 0)
            //        return;
            //    double areaAvailable = rows * stringSize.Height * tb.Width;
            //    double areaRequired = stringSize.Width * stringSize.Height * 1.1;

            //    if (areaAvailable / areaRequired > 1.3)
            //    {
            //        while (areaAvailable / areaRequired > 1.3)
            //        {
            //            tb.Font = new Font(tb.Font.FontFamily, tb.Font.Size * 1.1F);
            //            stringSize = gfx.MeasureString(tb.Text, tb.Font);
            //            areaRequired = stringSize.Width * stringSize.Height * 1.1;
            //        }
            //    }
            //    else
            //    {
            //        while (areaRequired * 1.3 > areaAvailable)
            //        {
            //            tb.Font = new Font(tb.Font.FontFamily, tb.Font.Size / 1.1F);
            //            stringSize = gfx.MeasureString(tb.Text, tb.Font);
            //            areaRequired = stringSize.Width * stringSize.Height * 1.1;
            //        }
            //    }
            //}
        }

        private void label4_Resize(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font.FontFamily, 52);
        }

        private void txt_SeriNo_TextChanged_1(object sender, EventArgs e)
        {
            if (txt_SeriNo.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                txt_SeriNo.Invoke((MethodInvoker)delegate
                {
                    MainForm.m_mf.m_user.SeriNo = txt_SeriNo.Text;
                });
            }
            else
            {
                MainForm.m_mf.m_user.SeriNo = txt_SeriNo.Text;
            }
        }

        private void txt_Sicil_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }

}
