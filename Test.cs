using HPU_OTOMASYON.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml.Packaging.Ionic.Zlib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TwinCAT.Ads;
using Image = iTextSharp.text.Image;
using MessageBox = System.Windows.Forms.MessageBox;


namespace HPU_OTOMASYON
{
    public partial class Test : Form
    {
        LoginForm frm1 = new LoginForm();

        //private static Anasayfa m_Singleton;   // Form ile ilgili bir nesneyi tek seferde cagırmak icin Singleton olusturuyoruz
        //public static Anasayfa frm1Singleton()
        //{
        //    if (m_Singleton == null)
        //        m_Singleton = new Anasayfa();

        //    return m_Singleton;

        //}

        TcAdsClient tcAds;
        //string amsNo = "10.2.149.20.1.1";
        string amsNo = "5.45.65.150.1.1";// HPU Cihazındaki Twincat adresi bu


        public bool isImageExist = false;
        public bool isImageExist1 = false;

        public System.Timers.Timer main_Timer;      // Timer Thread olarak olusturduk

        public System.Timers.Timer Plc_Read_Timer;      // Timer Thread olarak olusturduk
        //public System.Timers.Timer timerChart1;
        public System.Timers.Timer Chart_Timer;      // Timer Thread olarak olusturduk
        private volatile bool _requestStop = false;   // Grafik  Timerı Start Stop kontrolü icin

        private static Object _kilit = new Object();


        List<Parameter> parameterList = new List<Parameter>();

        private volatile bool _isRunning = false;

        ConcurrentDictionary<string, string> dictRead = new ConcurrentDictionary<string, string>();// PLC den veri okuma için


        Dictionary<string, string> dictWrite = new Dictionary<string, string>(); //Plc ye yazma için
        Dictionary<int, string> dictState = new Dictionary<int, string>();//Plc den state okuma için

        Dictionary<double, double> dictBPx_1 = new Dictionary<double, double>();
        Dictionary<double, double> dictBPx_2 = new Dictionary<double, double>();


        string errorFilePath = "E:\\Log\\log.txt";
        private static Object locker = new Object();       ///< Error dosyasının thread-safe olması için kullanılan kilit


        int lastState = 100;
        int counter = 0;
        ReportGeneration report;

        DateTime sprSaveStarted = DateTime.MinValue;


        public static bool twConnect = false;
        bool warningHandValveActive = false;         // el valfi kontrol uyarısı
        bool warningFlexibleHoseActive = false;     //  esnek hortum uyarısı

        string reportType;

        bool isStarted = false;  // başla butonuna basıldı mı
        bool isTestCompleted = false; // Test tamamlandı mı
        bool is24VTestCompleted = false;  // State  109 da test tekrarı icin kullanılıyor   Test tamamlandı mı

        bool isEmergencyStop = false;

        bool elValfi = false;

        public bool step_117 = false;

        public iTextSharp.text.Image jpgChart;  //111 state cizilen grafik icin
        public iTextSharp.text.Image jpgChart2;//115 state cizilen grafik icin

        public Bitmap chartImage;
        //public Bitmap chartImage1;

        private System.Threading.Timer STTimer;


        public class AdsInitializeException : AdsException { }
        public AmsAddress ClientAddress { get; }

        public static Test m_test;
        public MainForm m_mf;

        public Test()
        {
            InitializeComponent();




            //MainForm.m_mf.m_user = user;
            main_Timer = new System.Timers.Timer();
            main_Timer.Interval = 100;
            main_Timer.Elapsed += Main_Timer_Elapsed;


            Plc_Read_Timer = new System.Timers.Timer();
            Plc_Read_Timer.Interval = 500;
            Plc_Read_Timer.Elapsed += Plc_Read_Timer_Elapsed;


            //STTimer = new System.Threading.Timer(Comolokko);// ,null,2, (ManageAllTrain);
            //timerChart1 = new System.Timers.Timer();
            //timerChart1.Interval = 1000;
            //timerChart1.Elapsed += timerChart1_Tick;

            DisplayManager.SetDoubleBuffered(this);
            DisplayManager.SetDoubleBuffered(chart1);
            DisplayManager.SetDoubleBuffered(border_emg);

            DisplayManager.SetDoubleBuffered(txt_BSxbar);

            DisplayManager.SetDoubleBuffered(txt_BPxbar);

            DisplayManager.SetDoubleBuffered(txt_Tarih);
            DisplayManager.SetDoubleBuffered(textBox1);
            DisplayManager.SetDoubleBuffered(textBox2);
            DisplayManager.SetDoubleBuffered(textBox3);
            DisplayManager.SetDoubleBuffered(textBox4);
            DisplayManager.SetDoubleBuffered(textBox5);
            DisplayManager.SetDoubleBuffered(textBox6);
            DisplayManager.SetDoubleBuffered(textBox7);
            DisplayManager.SetDoubleBuffered(textBox8);
            DisplayManager.SetDoubleBuffered(textBox9);
            DisplayManager.SetDoubleBuffered(txtNotBilgi1);




            ReSizer.FormToBeResize(this); //Form resize metodu
                                          //                              //ReSizer.FormToBeResize(this.border_emg);
                                          //  
                                          //ReSizer.FormToBeResize(this.grpBoxBilgi);



            dictRead.TryAdd("IsConnected", "False");


            //ReadParameterList();


            // Excelden veri okuma fonksiyonu okuma amk
            //ReadStateTexts();      // Excelden acıklama okuma fonksiyonu

          

        }

        public static Test Singleton(MainForm mf)
        {
            if (m_test == null)
                m_test = new Test(mf);

            return m_test;
        }

        private Test(MainForm mf)
           : this()
        {
            m_mf = mf;
        }

    
        private void Test_Load(object sender, EventArgs e)
        {
            maho = false;

            DisplayManager.LabelInvoke(lbl_TestYapan, MainForm.m_mf.m_user.Name + "  " + MainForm.m_mf.m_user.Surname + " --- " + "Ürün Seri No :" + " " + MainForm.m_mf.m_user.SeriNo);
            //lbl_TestYapan.Text = MainForm.m_mf.m_user.Name + "  " + MainForm.m_mf.m_user.Surname + " --- " + "Ürün Seri No :" + " " + MainForm.m_mf.m_user.SeriNo;
            
            btn_TestBaslat.Enabled = false;

            DisplayManager.LabelVisibleInvoke(lblDirenc1, false);
            DisplayManager.LabelVisibleInvoke(lblDirenc2, false);
            DisplayManager.TextVisibleInvoke(txt_Direnc1, false);
            DisplayManager.TextVisibleInvoke(txt_Direnc2, false);
            DisplayManager.ButonVisibleInvoke(btn_DirencOK, false);


            //lblDirenc1.Visible = false;// Direnc degerlerini girme groubBox
            //lblDirenc2.Visible = false;
            //txt_Direnc1.Visible = false;
            //txt_Direnc2.Visible = false;
            //btn_DirencOK.Visible = false;

           

            //Task.Factory.StartNew(() =>
            //{
            // ReadParameterList();
            //});

            Thread thread = new Thread(new ParameterizedThreadStart(ConnectAsync));
            //thread.Priority = ThreadPriority.Highest;
            thread.IsBackground = true;//??
            thread.Start();

            //STTimer.Change(1000, System.Threading.Timeout.Infinite);
            Thread threadTimer = new Thread(new ParameterizedThreadStart(Comolokko));
            //threadTimer.Priority = ThreadPriority.Highest;
            threadTimer.IsBackground = true;//??
            threadTimer.Start();



        }

        private void Test_FormClosed(object sender, FormClosedEventArgs e)
        {


            //tcAds.Dispose();
            ResetInputForm();

        }
        private void Plc_Read_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }

        }

        private void Main_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }





        private void btn_Anasayfa_Click(object sender, EventArgs e)
        {


            this.Close();

            MainForm.m_mf.Show();


        }




        #region  BASLAT  Butonu 
        private void btn_TestBaslat_Click(object sender, EventArgs e)
        {
            isStarted = true;
            btn_TestBaslat.Enabled = false;
            btn_TestBaslat.ForeColor = System.Drawing.Color.Red;

            string paramName = dictWrite["StartButonBasildi"];
            WriteStringToPlc(tcAds, paramName, "TRUE");



            chart1.Show();  // Grafik alanını göster



        }
        #endregion


        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm.m_mf.Show();

            //tcAds.Disconnect();
            _isRunning = false;
        }



        #region   Connect Fonksiyonu
        public void ConnectAsync(object o)
        {
            try
            {

                //ReadParameterList();
                //ReadStateTexts();
                //dictRead.Add("IsConnected", "False");
                this.Cursor = Cursors.WaitCursor;

                ReadParameterList();
                ReadStateTexts();

                this.Cursor = Cursors.Default;


                tcAds = new TcAdsClient();

                //await Task.Run(() => tcAds.Connect(amsNo, 851)); // Bağlantı işlemi

                tcAds.Connect(amsNo, 851);  //PLC  ye baglantı kurma

                if (tcAds.IsConnected)
                {
                    //MessageBox.Show("PLC BAGLANTSI YAPILDI");
                    //StateInfo info = tcAds.ReadState();
                    dictRead["IsConnected"] = "True";
                    lbl_PlcDurumu.Text = "PLC - BAGLI";
                    lbl_PlcDurumu.ForeColor = System.Drawing.Color.Green;
                    pictureBox1.Image = Resources.gren;

                    twConnect = true;


                    string paramName = dictWrite["BTATasiyiciBogi"];
                    WriteStringToPlc(tcAds, paramName, "TRUE");


                    Plc_Read_Timer.Start();

                    main_Timer.Start();


                }
                else
                {
                    //ErrorLog(ex.ToString());
                    dictRead["IsConnected"] = "False";
                    lbl_PlcDurumu.Text = "BAGLANTI YOK";
                    lbl_PlcDurumu.ForeColor = System.Drawing.Color.Red;
                    pictureBox1.Image = Resources.red;
                }




            }
            catch (Exception ex)
            {
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message, ex.StackTrace, ex.TargetSite.ToString(), "Baglantı problemi var!!!");
                ErrorLog(ex.ToString());
            }
        }

        #endregion


        private void BlinkImage()
        {
            pictureBox1.Visible = false; // Hide the image
            Thread.Sleep(100); // Wait for a short duration (adjust as needed)
            pictureBox1.Visible = true; // Show the image again
        }

        string holo;
        #region  Test icin arka planda  calısan  Ana Fonksiyon
        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {


            //try
            //{


            //PLC_Read(sender);


            if (dictRead["IsConnected"] != "True")
            {
                try
                {

                    tcAds.Connect(amsNo, 851);
                    //StateInfo info = tcAds.ReadState();
                    dictRead["IsConnected"] = "True";


                }
                catch
                {
                    dictRead["IsConnected"] = "False";

                }

            }


            //try
            //{

            for (int i = 0; i < parameterList.Count; i++)
            {

                if (parameterList[i].variableType == "BOOL")
                {
                    dictRead[parameterList[i].name] = ReadBoolFromPlc(tcAds, parameterList[i].variableName);
                }

                else if (parameterList[i].variableType == "INT")
                {
                    dictRead[parameterList[i].name] = ReadINTFromPlc(tcAds, parameterList[i].variableName);

                }

                else if (parameterList[i].iotype == "Server READ")
                {
                    dictRead[parameterList[i].name] = ReadStringFromPlc(tcAds, parameterList[i].variableName);


                }


                if (isStarted)
                {
                    MainForm.m_mf.m_plcValue.St12wr = ReadBoolFromPlc(tcAds, "st12_wr");
                    if (MainForm.m_mf.m_plcValue.St12wr == "False") /* || dictRead["DI Reset Buton"] == "True")*/
                    {
                        isStarted = false;

                        string paramName = dictWrite["StartButonBasildi"];

                        WriteStringToPlc(tcAds, paramName, "FALSE");
                        DisplayManager.TextBoxInvoke(txtNotBilgi1, "Test Baslıyor...");



                    }
                }



                #region    STATE 101

                if (MainForm.m_mf.m_plcValue.TestStep == "101")
                {
                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "HPU ile test cihazı arasındaki konnektör bağlantılarını yapınız.\r\nHPU tipini (Motorlu, Taşıyıcı vs.) ekrandan seçiniz." +
                    //       " \r\nHidrolik yağ seviyesinin uygun olup olmadığı kontrol ediniz. HPU, minimum NAS6 seviyesinde temizlenmiş Pentopol J32 yağı ile üst gözetleme camının yarısına kadar doldurulur. " +
                    //       "\r\nTeste başlamadan önce yalıtım direnci testinin yapılmış olması gerekmektedir.\r\nBaşla butonuna tıklanarak sonraki adıma geçilecektir.");

                    MainForm.m_mf.m_plcValue.St12wr = ReadBoolFromPlc(tcAds, "st12_wr");

                    if (isEmergencyStop == false && MainForm.m_mf.m_plcValue.St12wr == "False")
                    {

                        isEmergencyStop = true;

                        //MessageBox.Show("YAhhhh","Uyarı", MessageBoxIcon.Hand,MessageBoxButton.OK);



                        DialogResult secenek = DialogResult.None;
                        secenek = ShowError("Yağ seviyesini kontrol ediniz?");

                        if (secenek == DialogResult.OK)
                        {
                            string paramName = dictWrite["YagSeviyesi"];

                            WriteStringToPlc(tcAds, paramName, "TRUE");



                        }

                        Thread.Sleep(500);


                        DialogResult secenek2 = new DialogResult();
                        secenek2 = ShowError("Yalıtım direnci testi yapınız!");


                        if (secenek2 == System.Windows.Forms.DialogResult.OK)
                        {

                            FlexibleMessageBox.Show("Lütfen Direnc Degerlerini Giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DisplayManager.LabelVisibleInvoke(lblDirenc1, true);
                            DisplayManager.LabelVisibleInvoke(lblDirenc2, true);
                            DisplayManager.TextVisibleInvoke(txt_Direnc1, true);
                            DisplayManager.TextVisibleInvoke(txt_Direnc2, true);
                            DisplayManager.ButonVisibleInvoke(btn_DirencOK, true);



                            //lblDirenc1.Visible = true;// Direnc degerlerini girme 
                            //lblDirenc2.Visible = true;
                            //txt_Direnc1.Visible = true;
                            //txt_Direnc2.Visible = true;
                            //btn_DirencOK.Visible = true;
                            ////string paramName = dictWrite["YalitimDirenciTesti"];

                            ////WriteStringToPlc(tcAds, paramName, "TRUE");

                        }


                    }
                }
                #endregion


                #region   STATE 102
                if (isEmergencyStop == true && dictRead["StartButonAktif"] == "True")  //DI Emergency Stop yerine StartButonAktif   geldi
                {
                    if (MainForm.m_mf.m_plcValue.TestStep == "102")
                    {


                        //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Ünitenin P ve S çıkışlarındaki tapaları çıkararak yerlerine çabuk bağlantı kaplinlerini takın,test cihazına ait olan kaliper ve akümülatör ile" +
                        //"HPU arasındaki hidrolik hat bağlantısını gerçekleştirin." +
                        //"Kaliper ve akümülatör hatlarının havası alınması gerektiği durumlarda" +
                        //"kaliper ve akümülatör çıkışlarından hava alınması için uygun bir tanka şeffaf bir hortum yardımıyla" +
                        //"ara bağlantı oluşturun ve bağlantının vanalarını açın." +
                        //"Daha sonra 3ve4 aşamalarını takip edin.Eğer hava alma işlemi yapılmayacaksa 3ve4 aşamalarını atlayabilirsiniz.Eğer bu seçenek seçilirse 6.aşamaya geçilir");




                        DialogResult result = new DialogResult();
                        result = ShowError("Ünitenin P ve S çıkışlarındaki tapaları çıkararak yerlerine çabuk bağlantı kaplinlerini takın, " +
                            "\ntest cihazına ait olan kaliper ve akümülatör ile HPU arasındaki hidrolik hat bağlantısını gerçekleştirin");

                        if (result == DialogResult.OK)
                        {
                            string parametre3 = dictWrite["HidrolikBaglantilar"];
                            WriteStringToPlc(tcAds, parametre3, "TRUE");
                            //Thread.Sleep(2000);

                        }
                        Thread.Sleep(2000);

                        DialogResult result2 = new DialogResult();
                        result2 = ShowErrorYesNo("Kaliper ve akümülatör hatlarının havası alınması gerektiği durumlarda kaliper ve akümülatör " +
                            "\nçıkışlarından hava alınması için uygun bir tanka şeffaf bir hortum yardımıyla ara bağlantı \noluşturun ve bağlantının vanalarını açın.");

                        string parametre = dictWrite["HavaAlmaIslemi"];


                        string parametre2 = dictWrite["HavaAlmaIslemiAtlama"];

                        if (result2 == DialogResult.Yes)
                        {
                            //string parametre = dictWrite["HavaAlmaIslemi"];
                            WriteStringToPlc(tcAds, parametre, "TRUE");
                            WriteStringToPlc(tcAds, parametre2, "FALSE");

                        }
                        else if (result2 == DialogResult.No)
                        {

                            //string parametre = dictWrite["HavaAlmaIslemiAtlama"];
                            WriteStringToPlc(tcAds, parametre2, "TRUE");
                            WriteStringToPlc(tcAds, parametre, "FALSE");

                        }




                        isEmergencyStop = false;
                        isStarted = false;




                    }
                }



                #endregion


                #region  STATE 103
                if (MainForm.m_mf.m_plcValue.TestStep == "103")
                {
                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Bu adımda motor çalışmaya başlar ve akümülatöre bağlı şeffaf hortumdan hava kabarcığı ile beraber hidrolik yağ akmaya başlar." +
                    //      " Hidrolik yağ içinde hava kabarcığı bittikten sonra  akümülatöre bağlı şeffaf hortum vanası kapatılır." +
                    //      " Vana kapandıktan sonra BS basıncı 40 bara çıkıncaya kadar motor çalışır ve sonrasında durur." +
                    //      "\nBu işlem esnasında motor en fazla 30sn çalışır ve sonrasında yukarıdaki işlemler gerçekleşmese de durur. " +
                    //      "\nHPU'nun yağının tamamlanması gerektiğine dair uyar gelir ise yağı tamamlayarak onay veriniz.  Onaydan sonra yukarıdaki işlem baştan tekrarlanır");

                    MainForm.m_mf.m_plcValue.St13wr = ReadBoolFromPlc(tcAds, "st13_wr");
                    //isEmergencyStop = false;
                    if (MainForm.m_mf.m_plcValue.St13wr == "True")
                    {


                        DialogResult secenek2 = new DialogResult();
                        secenek2 = ShowError("HPU'nun yağını uygun seviyeye kadar tamamlayınız!");


                        if (secenek2 == System.Windows.Forms.DialogResult.OK)
                        {
                            string paramName2 = dictWrite["YagTamamlandi"];

                            WriteStringToPlc(tcAds, paramName2, "TRUE");

                            Thread.Sleep(1000);

                            WriteStringToPlc(tcAds, paramName2, "FALSE");


                        }

                    }


                }

                #endregion


                #region   STATE 104
                if (MainForm.m_mf.m_plcValue.TestStep == "104")
                {
                    DisplayManager.TextBoxInvoke(txtNotBilgi1, "Bu adımda motor çalışmaya başlar ve kaliper şeffaf hortumdan hava kabarcığı ile beraber hidrolik yağ akmaya başlar.Hidrolik yağ içinde hava kabarcığı bittikten sonra kaliper şeffaf hortum vanası kapatılır. " +
                        "Vana kapandıktan sonra BS ve BP basıncı 40 bara çıkıncaya kadar motor çalışır ve sonrasında durur\t" +
                        "Bu islem esnasında motor en fazla 30sn çalışır ve sonrasında yukarıdaki işlemler gerçekleşmese de durur\t" +
                        "HPU'nun yağının tamamlanması gerektiğine dair uyar gelir ise yağı tamamlayarak onay veriniz. Onaydan sonra yukarıdaki işlem baştan tekrarlanır.");




                    DialogResult secenek2 = new DialogResult();
                    secenek2 = ShowError("HPU'nun yağını uygun seviyeye kadar tamamlayınız!!!");


                    if (secenek2 == System.Windows.Forms.DialogResult.OK)
                    {
                        string paramName = dictWrite["YagTamamlandi"];

                        WriteStringToPlc(tcAds, paramName, "TRUE");

                        Thread.Sleep(1000);

                        WriteStringToPlc(tcAds, paramName, "FALSE");


                    }


                    //tc0SaveStarted = DateTime.MinValue;

                    //BackgroundWorker bw = new BackgroundWorker();
                    //bw.DoWork += Bw_DoWork;

                    //bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                    //bw.RunWorkerAsync("Besleme gerilim seviyesi 16.8V için test tekrar yapılacaktır.");
                    //if (bw.IsBusy == true && bw.WorkerSupportsCancellation == true)
                    //{
                    //    bw.CancelAsync();
                    //}

                }
                #endregion


                #region  STATE 105

                if (MainForm.m_mf.m_plcValue.TestStep == "105")
                {

                    DisplayManager.TextBoxInvoke(txtNotBilgi1, "Akümülatör ve kaliper hattı havası alındıktan sonra hattaki hidrolik yağın tanka geri dönmesi için manuel olarak" +
                        " H (el valfini) aktif ederek (basılı tutarak) BS basıncının 0 bara düşmesini sağlayınız. " +
                        "\r\nBS basıncı 0 bara düştükten sonra HPU yağ seviyesi tekrar kontrol edilir ve eksilen hidrolik yağ tamamlanır." +
                        "\r\nBasıncın 0 bara düştükten ve yağ seviyesini tamamladıktan sonra onay vererek bir sonraki adıma geçiniz.");

                    MainForm.m_mf.m_plcValue.St14wr = ReadBoolFromPlc(tcAds, "st14_wr");

                    if (MainForm.m_mf.m_plcValue.St14wr == "True")/*ShowError("El valfi ile basıncı sıfırlayınız."))*/
                    {
                        MainForm.m_mf.m_plcValue.ElValfiBraek120 = ReadStringFromPlc(tcAds, "st5_wr");

                        if (!string.IsNullOrEmpty(MainForm.m_mf.m_plcValue.ElValfiBraek120))
                        {
                            string sülo = MainForm.m_mf.m_plcValue.ElValfiBraek120.Substring(0, 1);

                            if ((Convert.ToInt32(sülo) <= 5))
                            {
                                DialogResult res = new DialogResult();
                                res = ShowError("El valfi ile uyarı penceresi kapanana kadar basıncı sıfırlayınız");

                            }
                            else
                            {
                                DialogResult secenek = new DialogResult();
                                secenek = ShowErrorYesNo("Basınç değeri 4mA değerinin üzerindedir. Devam edilsin mi?");
                            }



                        }

                    }
                    MainForm.m_mf.m_plcValue.St13wr = ReadBoolFromPlc(tcAds, "st13_wr");

                    if (MainForm.m_mf.m_plcValue.St13wr == "True")
                    {

                        DialogResult secenek = new DialogResult();
                        secenek = ShowError("HPU'nun yağ seviyesini tekrar kontrol ediniz ve eksilen hidrolik yağı tamamlayınız.");

                        if (secenek == System.Windows.Forms.DialogResult.OK)
                        {
                            string paramName = dictWrite["YagTamamlandi"];

                            WriteStringToPlc(tcAds, paramName, "TRUE");

                            Thread.Sleep(1000);

                            WriteStringToPlc(tcAds, paramName, "FALSE");


                        }

                    }


                }
                #endregion


                // STATE  106  otoamatik devam edecektir.
                if (MainForm.m_mf.m_plcValue.TestStep == "106")
                {

                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Bu adımda motor çalışmaya başlar. Aynı anda bir kronomometre aktif edilir. Akümülatör basıncı 150 bara ulaşana kadar motor çalışır ve motor durdurulur." +
                    //    "\r\nMotorun aktif olma süresi kayıt edilir. Maksimum çalışma süresi 40sn olmalıdır. \nMotorun bu adımda çektiği pik akım değeri kaydedilir. Maksimum akım değeri 30A olmalıdır");

                }


                #region STATE 107

                MainForm.m_mf.m_plcValue.St15wr = ReadBoolFromPlc(tcAds, "st15_wr");
                if (MainForm.m_mf.m_plcValue.TestStep == "107" && MainForm.m_mf.m_plcValue.St15wr == "True")
                {
                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "BS basıncı 120 bara düştüğü anda motor tekrar çalışmaya başlar, basıncı 150 bara çıkarır ve durur." +
                    //    "\r\nBP basıncı 120 bardan 150 bara çıkış süresi kaydedilir. Maksimum süre 20sn olmalıdır." +
                    //    "\r\nMotor 150 barda durduktan sonra ilk 60sn ve ikinci 60 sn basınç düşüşü kaydedilir. " +
                    //    "\r\nİlk 60sn basınç düşmesi maksimum 15 bar, ikinci 60 sn basınç düşmesi maksimum 5 bar olmalıdır.");


                    MainForm.m_mf.m_plcValue.Bsbar = ReadStringFromPlc(tcAds, "st3_wr");
                    if (Convert.ToDouble(MainForm.m_mf.m_plcValue.Bsbar) > 120) //Bs_bar 120 nin altına düsünce uyarının kapanması
                    {

                        ElValfiForm.Instance().ControlOpenForm();

                    }

                }
                #endregion


                #region   STATE 108
                if (MainForm.m_mf.m_plcValue.TestStep == "108")
                {
                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Bu adımda motor aktif edilir. BS basıncı yükselmeye devam eder, 165-190 bar arasındaki bir basınçta W Basınç Emniyet Valfi aktif olur ve basınç sabitlenir." +
                    //            " Basıncın 5sn sabit kaldığı nokta kaydedilir. Basınç 165-190 bar arasında sabitlenmiş olmalıdır." +
                    //            "\r\nEğer basınç 190 bar üzerine çıkmaya devam ederse motor otomatik olarak durdurulur.");

                    MainForm.m_mf.m_plcValue.St15wr = ReadBoolFromPlc(tcAds, "st15_wr");
                    if (MainForm.m_mf.m_plcValue.St15wr == "True")
                    {
                        //DisplayManager.TextBoxInvoke(Test.Singleton(MainForm.m_mf).textBox10, " Hamzaaa :");
                        MainForm.m_mf.m_plcValue.Bsbar = ReadStringFromPlc(tcAds, "st3_wr");
                        //DisplayManager.TextBoxInvoke(Test.Singleton(MainForm.m_mf).textBox10, " MainForm.m_mf.m_plcValue.Bsbar = ReadStringFromPlc(tcAds, \"st3_wr\");");
                        if (Convert.ToDouble(MainForm.m_mf.m_plcValue.Bsbar) > 120) //Bs_bar 120 nin altına düsünce uyarının kapanması
                        {
                            //ElValfiForm.ShowForm();

                            ElValfiForm.Instance().ControlOpenForm();


                        }

                    }


                }
                #endregion



                #region   STATE  109

                if (MainForm.m_mf.m_plcValue.TestStep == "109")
                {

                    DisplayManager.LabelVisibleInvoke(lbl_Test_Tekrar, true);

                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur. Daha sonra AS valfine 0,8A akım verilir." +
                    //            " BP basıncı 100±2 bara yükselir. Daha sonra AS valfi kapatılır ve D valfi Aktif değil (0V) konumuna getirilir. BP basıncı 34-36 bar arasında olmalıdır." +
                    //            " Eğer basınç bu aralıkta ise IT valfi ayarı ok, değilse ayar yapılmalıdır. Ayar yapıldıktan sonra test tekrarlanır.");

                    if (MainForm.m_mf.m_plcValue.St16wr == "True" && is24VTestCompleted == false) //Bs_bar 120 nin altına düsünce uyarının kapanması
                    {

                        //FlexibleMessageBox.Show("Lütfen Direnc Degerlerini Giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult secenek2 = new DialogResult();
                        secenek2 = ShowError("IT Valfi ayarı bozuk. Ayar yapılması gerekmektedir.!");
                        if (secenek2 == DialogResult.OK)
                        {
                            string paramName = dictWrite["ITValfAyariYapildi"];

                            WriteStringToPlc(tcAds, paramName, "TRUE");


                            Thread.Sleep(1000);

                            WriteStringToPlc(tcAds, paramName, "FALSE");

                            is24VTestCompleted = true;  // test in ilk adımını gecmek icin true yaptık

                            //AutoClosingMessageBox.Show("Test  Tekrarlanıyor  !!!", "BİLGİ", 4000);



                        }



                    }
                    //MainForm.m_mf.m_plcValue.St16wr = ReadBoolFromPlc(tcAds, "st16_wr");

                    if (MainForm.m_mf.m_plcValue.St16wr == "True" && is24VTestCompleted == true)  // testin 2. adımını yapıyoruz
                    {
                        DialogResult secenek2 = new DialogResult();
                        secenek2 = ShowErrorYesNo("IT Valfi ayarı bozuk. Ayar yapılması gerekmektedir.!");
                        if (secenek2 == DialogResult.OK)
                        {
                            string paramName = dictWrite["ITValfAyariYapildi"];

                            WriteStringToPlc(tcAds, paramName, "TRUE");

                            Thread.Sleep(1000);

                            WriteStringToPlc(tcAds, paramName, "FALSE");


                        }
                        else
                        {
                            string paramName = dictWrite["TestPass"];

                            WriteStringToPlc(tcAds, paramName, "TRUE");


                            Thread.Sleep(1000);

                            WriteStringToPlc(tcAds, paramName, "FALSE");


                        }

                    }

                }
                #endregion


                // State 110  otomatik devam edecektir.
                #region   STATE 110  
                if (MainForm.m_mf.m_plcValue.TestStep == "110")  // State 111 oldugunda grafik cizilir.
                {

                    DisplayManager.LabelVisibleInvoke(lbl_Test_Tekrar, false);

                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "AS valfine 0,8A akım verilir. BP basıncı 100±2 bara yükselmelidir. Daha sonra D valfi aktif değil (0V) konumuna getirilir. " +
                    //            "\r\nBP basıncı 34-36 bara düşer. 60 saniye boyunca meydana gelecek basınç kaybı maksimum 2 bar olacaktır. " +
                    //            "\r\n\nNot: Bu işlemler esnasında BS basıncı 120 barın altına düşerse motor tekrar devreye girerek basıncı 150 bara yükseltir ve motor otomatik olarak durdurulur");

                }

                #endregion

                #region   STATE 111    Grafik baslatılıyor
                if (MainForm.m_mf.m_plcValue.TestStep == "111")  // State 111 oldugunda grafik cizilir.
                {
                    _isRunning = true;
                    //this.Invoke(new MethodInvoker(delegate ()    // timer ayrı bir component / thread olarak calıstıgı icin Invoke Method olarak calıstırdık
                    //{
                    //    timerChart1.Start();


                    //}));

                    //timerChart1.Enabled = true;
                    //BP_bar degerine ait basınc-zaman grafigi olusturulmaya baslanıyor.

                    //if (_requestStop)
                    //{
                    //    Chart_Timer.Start();
                    //}


                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur. Daha sonra AS valfine 0,8A akım verilir." +
                    //            " BP basıncı 100±2 bara yükselir. Daha sonra D valfi Aktif değil (0V) konumuna getirilir. " +
                    //            "BP basıncının 95 bardan 41 bara düşüş süresi ölçülür ve kaydedilir. Bu süre maksimum 500 milisaniye olmalıdır." +
                    //            " \r\nD valfi aktif edilir ve BP basıncının 100±2 bara yükseldiği görülür. BP basıncının D valfi aktif edildikten sonra basıncı 41 bardan 95 bara " +
                    //            "maksimum 800 milisaniyede yükselmelidir.");


                }



                #endregion

                // State  112  otomatik olarak devam edecektir.
                #region   STATE 112    Grafik baslatılıyor
                if (MainForm.m_mf.m_plcValue.TestStep == "112")
                {

                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur." +
                    //           "\r\nDaha sonra AS valfine 0,8A akım verilir. BP basıncı 100±2 bara yükselir." +
                    //           "\r\nDaha sonra D ve E valfi Aktif değil (0V) konumuna getirilir. BP basıncının 0 (<1) bara düştüğü görülür.");


                    SaveChartImagejpgChart(sender);


                }

                #endregion


                #region   STATE 113    Otomatik olarak devam edecektir.
                if (MainForm.m_mf.m_plcValue.TestStep == "113")
                {

                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur. " +
                    //            "Daha sonra AS valfine 0,8A akım verilir." +
                    //            " BP basıncı 100±2 bara yükselir. " +
                    //            "Bu işlem esnasında BP basıncı 10 bardan 90 bara maksimum 900 milisaniyede yükselmelidir." +
                    //            "Daha sonra sırasıyla AS valfinin akım değeri 0A'e, AT valfinin akım değeri 0,8A'e ayarlanır." +
                    //            " Bu işlem esnasında BP basıncı 90 bardan 10 bara maksimum 1 saniyede düşmelidir.");
                }

                #endregion

                // State 114  otomatik olarak devam edecektir.
                #region   STATE 114    Otomatik olarak devam edecektir.
                if (MainForm.m_mf.m_plcValue.TestStep == "114")  // State 111 oldugunda grafik cizilir.
                {


                    if (chart1.InvokeRequired)
                    {
                        foreach (var series in chart1.Series)
                        {
                            series.Points.Clear();// ilk grafigi temizliyoruz

                        }
                    }
                    else
                    {
                        foreach (var series in chart1.Series)
                        {
                            series.Points.Clear();// ilk grafigi temizliyoruz

                        }
                    }




                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur. " +
                    //            "\r\nBu işlem esnasında BP basıncındaki değişim 60 saniye boyunca kaydedilir." +
                    //            "\r\nBP'deki 60 saniyedeki basınç artışı 10 bardan küçük olmalıdır");
                }

                #endregion

                #region   STATE 115    Grafik baslatılıyor
                if (MainForm.m_mf.m_plcValue.TestStep == "115")  // State 111 oldugunda grafik cizilir.
                {

                    _isRunning = true;

                    //this.Invoke(new MethodInvoker(delegate ()    // timer ayrı bir component / thread olarak calıstıgı icin Invoke Method olarak calıstırdık
                    //{

                    //    timerChart1.Start(); //BP_bar degerine ait 2. basınc-zaman grafigi olusturulmaya baslanıyor.

                    //}));


                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur. " +
                    //            "Daha sonra AS valfine 0A'den 0,8A'e kadar sabit bir artış hızıyla akım verilir. BP basıncı 0 bardan 100±2 bara yükselir." +
                    //            " Bu işlem esnasındaki basınç artışı kaydedilerek çizdirilir." +
                    //            "Daha sonra AS valfinin akım değeri 0,8A'dan 0A'ya sabit bir azalış hızıyla düşürülür. " +
                    //            "BP basıncı 100±2 bardan 0 bara düşer. Bu işlem esnasındaki basınç düşüşü kaydedilerek çizdirilir.");

                }

                #endregion


                // State 116  otomatik olarak devam edecektir.
                #region   STATE 116    Otomatik olarak devam edecektir.
                if (MainForm.m_mf.m_plcValue.TestStep == "116")  // State 111 oldugunda grafik cizilir.
                {

                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur. AS valfine 0,8A akım verilir." +
                    //            " BP basıncı 100±2 bara yükselir." +
                    //            "\r\nDaha sonra AS valfi 0 A değerine getirilir ve BP basıncındaki düşüş 60 saniye boyunca izlenir. " +
                    //            "\r\nBP'deki 60 saniyedeki basınç düşüşü 10 bardan küçük olmalıdır");





                    SaveChartImagejpgChart2(sender);


                    //SaveChartImage();

                }

                #endregion


                #region  STATE  117  Son test adımı


                if (MainForm.m_mf.m_plcValue.TestStep == "117")/*ShowError("El valfi ile basıncı sıfırlayınız."))*/
                {


                    //DisplayManager.TextBoxInvoke(txtNotBilgi1, "Motor çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur." +
                    //             " El valfi yardımıyla akümülatör basıncı yavaş bir şekilde 0 bara düşürülür. Bu aşamada tank içinde" +
                    //             " oluşan basınç maksimum 0,5 bar olmalıdır.\nMotor tekrar çalıştırılmaya başlanır ve BS basıncı 150 bara ulaştığında durdurulur." +
                    //             " Bu aşamada tank içinde oluşan basınç maksimum -0,1 bar olmalıdır.\r\nTest tamamlanır ve rapor kaydedilir. " +
                    //             "Rapor başarı ile kaydedildikten sonra HPU bağlantılarını sökebilirsiniz.");


                    MainForm.m_mf.m_plcValue.St14wr = ReadBoolFromPlc(tcAds, "st14_wr");
                    if (MainForm.m_mf.m_plcValue.St14wr == "True")
                    {
                      

                        ElValfiForm2.Instance().ControlOpenForm();


                      

                        ////ShowError("El valfi ile uyarı penceresi kapanana kadar basıncı sıfırlayınız");
                        //if (elValfi)
                        //{
                        //    //MainForm.m_mf.m_plcValue.ElValfiBraek120 = ReadStringFromPlc(tcAds, "st5_wr");

                        //    //if (!string.IsNullOrEmpty(MainForm.m_mf.m_plcValue.ElValfiBraek120))
                        //    //{
                        //    //    string sülo = MainForm.m_mf.m_plcValue.ElValfiBraek120.Substring(0, 2);

                        //    //    if ((Convert.ToInt32(sülo) <= 60))
                        //    elValfi = true;
                        //}
                        //else
                        //{   elValfi = false;

                        //    //}

                        //    //elValfi = false;
                        //}
                        //elValfi = false;
                    }
                    MainForm.m_mf.m_plcValue.St38wr = ReadBoolFromPlc(tcAds, "st38_wr");
                    if (MainForm.m_mf.m_plcValue.St38wr == "True")  // TestTamamlandı  True gelirse
                    {

                        Pdf_Save();


                        if (!step_117)
                        {
                            step_117 = true;
                            ShowError("Belge başarılı bir şekilde kaydedildi.\nHPU baglantılarını sökebilirsiniz");
                        }

                    }

                }
                #endregion

                #region    STATE 900 testten çıkıs

                if (MainForm.m_mf.m_plcValue.TestStep == "900")/*ShowError("El valfi ile basıncı sıfırlayınız."))*/
                {
                    if (maho)
                    {
                        goto PC;
                    }


                    string paramName1 = dictWrite["StartButonBasildi"];
                    WriteStringToPlc(tcAds, paramName1, "FALSE");

                    string paramName = dictWrite["BTATasiyiciBogi"];
                    WriteStringToPlc(tcAds, paramName, "FALSE");


                    DialogResult res = ShowError("Test Basarılı bir şekilde tamamlandı.\nRaporu İnceleyebilirsiniz.");

                    if (res == DialogResult.OK)
                    {
                        maho = true;

                        Plc_Read_Timer.Stop();

                        main_Timer.Stop();


                        this.Hide();

                        //MainForm.m_mf.Visible = true;
                        MainForm.m_mf.BringToFront();



                    }
                PC: Console.WriteLine();
                }
                #endregion


            }



        }

        #endregion

        bool maho_117 = false;
        bool maho = false;

        private void OpenHomePage()
        {
            MainForm.m_mf.Show();
        }

        private void CloseForm()
        {
            this.DestroyHandle();
            this.Close();

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //Anasayfa anasayfa = new Anasayfa(MainForm.m_mf.m_user);

            //anasayfa.Show();

            //CloseForm();

        }


        #region    Ekrana uyarı verme  fonksiyonları
        DialogResult ShowError(string message)  //I call this function in another thread
        {
            DialogResult result = DialogResult.None;

            FlexibleMessageBox.FONT = new System.Drawing.Font("Calimbra", 18);

            result = FlexibleMessageBox.Show(message, "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);




            return result;
        }


        DialogResult ShowErrorYesNo(string message)  //I call this function in another thread
        {
            DialogResult result = System.Windows.Forms.DialogResult.None;

            FlexibleMessageBox.FONT = new System.Drawing.Font("Calimbra", 18);

            result = FlexibleMessageBox.Show(message, "BİLGİ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);



            return result;
        }

        #endregion



        #region  Log Tutma Fonksiyonu
        public void ErrorLog(string exception)
        {
            try
            {
                //Wait for resource to be free
                lock (locker)
                {

                    using (FileStream file = new FileStream(errorFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                    using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                    {
                        writer.WriteLine(DateTime.Now.ToString() + " " + exception);
                    }
                    FileInfo fileInfo = new FileInfo(errorFilePath); //Dosya boyutu belli bir yerden sonra silme
                    long fileSize = fileInfo.Length;
                    if (fileSize > 5000)
                    {
                        fileInfo.Delete(); //Dosya boyutu 5 kb gecince silme
                    }

                }

            }
            catch
            {
                //File not available, conflict with other class instances or application
            }
        }

        #endregion




        #region  Reset Fonksiyonu
        public void ResetInputForm()
        {
            try
            {


                //timer_bw.Dispose();
                frm1.txt_Adi.Text = string.Empty;
                frm1.txt_Soyadi.Text = string.Empty;
                frm1.txt_Sicil.Text = string.Empty;
                frm1.txt_SeriNo.Text = string.Empty;


                txtNotBilgi1.Text = string.Empty;
                //txt_Tarih.Text = DateTime.Now.ToShortDateString();

            }

            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
            }

        }

        #endregion
        //private readonly object osman = new object();

        #region PLC den Bool veri okuma
        private string ReadBoolFromPlc(TcAdsClient tcAds, string name)
        {
            try
            {
                //lock(osman)
                {

                    // creates a stream with a length of 4 byte 
                    AdsStream ds = new AdsStream(10);

                    int varHandle = tcAds.CreateVariableHandle("PRG_ADS." + name);

                    BinaryReader br = new BinaryReader(ds, System.Text.Encoding.ASCII);

                    int length = tcAds.Read(varHandle, ds);

                    bool value = br.ReadBoolean();



                    return value.ToString();
                }



            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
                return string.Empty;
            }

        }

        #endregion

        /// <summary>
        /// Verilen isimdeki değişkene value değerini yazar
        /// </summary>
        /// <param name="tcAds"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// 


        #region  PLC den String veri okuma
        private string ReadStringFromPlc(TcAdsClient tcAds, string name)
        {
            try
            {
                //int k = 0;

                //if (name == "st25_wr")
                //    k = 0;


                // creates a stream with a length of 4 byte 
                AdsStream ds = new AdsStream(10);


                int varHandle = tcAds.CreateVariableHandle("PRG_ADS." + name);

                BinaryReader br = new BinaryReader(ds, System.Text.Encoding.ASCII);

                int length = tcAds.Read(varHandle, ds);

                string value = new string(br.ReadChars(length));


                int index = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsDigit(value[i]) && value[i] != '.')
                    {
                        index = i;
                        break;
                    }

                }

                if (index != 0)
                    value = value.Substring(0, index);

                double fValue = Convert.ToDouble(value);



                //value = fValue.ToString("N2");









                return value;
            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
                return string.Empty;
            }

        }

        #endregion

        #region  PLC den INT  veri okuma
        private string ReadINTFromPlc(TcAdsClient tcAds, string name)
        {
            try
            {
                // creates a stream with a length of 4 byte 
                AdsStream ds = new AdsStream(10);

                int varHandle = tcAds.CreateVariableHandle("PRG_ADS." + name);

                BinaryReader br = new BinaryReader(ds, System.Text.Encoding.ASCII);

                int length = tcAds.Read(varHandle, ds);

                short value = br.ReadInt16();

                string str = value.ToString();


                return str;
            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
                return string.Empty;
            }

        }

        #endregion

        #region  PLC ye veri yazma
        private void WriteStringToPlc(TcAdsClient tcAds, string name, string value)
        {
            try
            {

                // yazma işlemi

                AdsStream adsStream = new AdsStream(value.Length + 1);
                BinaryWriter writer = new BinaryWriter(adsStream, System.Text.Encoding.ASCII);
                writer.Write(value.ToCharArray());
                //add terminating zero
                writer.Write('\0');
                if (tcAds.Session == null)
                {
                    int varHandle = tcAds.CreateVariableHandle("PRG_ADS." + name);

                    tcAds.Write(varHandle, adsStream);
                }
                //else
                //{
                //    MessageBox.Show("Target Port bulunamadı!");
                //}

            }
            catch (Exception ex)
            {

                ErrorLog(ex.ToString());

            }

        }

        #endregion


        #region Excelden State(Acıklamaları) okuma
        public void ReadStateTexts()
        {

            FileInfo parameterFile = new FileInfo("iolist.xlsx");


            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            //using (ExcelPackage package = new ExcelPackage(parameterFile))

            if (!System.IO.File.Exists(parameterFile.FullName))
            {
                Console.WriteLine("Excel dosyası bulunamadı!");
                Environment.Exit(0);
            }

            // Acıklamalar sayfası okunur
            //ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            OfficeOpenXml.ExcelPackage Excel = new OfficeOpenXml.ExcelPackage(parameterFile);
            OfficeOpenXml.ExcelWorksheet worksheet = Excel.Workbook.Worksheets[2];


            int rows = worksheet.Dimension.End.Row;

            for (int i = 2; i <= rows; i++)
            {

                int stateNumber = Convert.ToInt32(worksheet.Cells[i, 1].Value);

                if (worksheet.Cells[i, 2].Value != null)
                {
                    string stateText = worksheet.Cells[i, 2].Value.ToString();

                    dictState.Add(stateNumber, stateText);
                }



            }


        }
        #endregion


        #region  Excel den ADS_LIST verisi okuma
        public void ReadParameterList()
        {

            try
            {
                FileInfo parameterFile = new FileInfo("iolist.xlsx");

                //MessageBox.Show(parameterFile.FullName);

                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;




                if (!System.IO.File.Exists(parameterFile.FullName))
                {
                    Console.WriteLine("Excel dosyası bulunamadı!");
                    Environment.Exit(0);
                }

                // ADS sayfası okunur

                OfficeOpenXml.ExcelPackage Excel = new OfficeOpenXml.ExcelPackage(parameterFile);
                OfficeOpenXml.ExcelWorksheet worksheet = Excel.Workbook.Worksheets[1];
                //Excel.Application xlApp = new Excel.Application();
                //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open("iolist.xlsx");
                //Excel._Worksheet worksheet = xlWorkbook.Sheets[2];
                //Excel.Range xlRange = worksheet.UsedRange;

                //ExcelWorksheet worksheet = package.Workbook.Worksheets[2];



                int rows = worksheet.Dimension.End.Row;
                //int rows = xlRange.Rows.Count;

                for (int i = 2; i <= rows; i++)
                {
                    try
                    {
                        //if (worksheet.Cells[i, 2].Value == null) 
                        //{ 
                        //   return;
                        //}

                        dynamic varName = worksheet.Cells[i, 2].Value;
                        dynamic parName = worksheet.Cells[i, 3].Value;
                        dynamic type = worksheet.Cells[i, 4].Value;
                        dynamic iotype = worksheet.Cells[i, 7].Value;

                        Parameter newParameter = new Parameter(varName, parName, type, iotype);

                        parameterList.Add(newParameter);

                        if (iotype == "Server READ")
                        {
                            dictRead.TryAdd(parName, "0");
                        }

                        if (iotype == "server WRITE")
                        {
                            dictWrite.Add(parName, varName);
                        }

                    }



                    catch (Exception ex)
                    {
                        ErrorLog(ex.ToString());

                    }

                }


                dictRead.TryAdd("iState", string.Empty);
                dictRead.TryAdd("Type", string.Empty);





            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());

            }
        }
        #endregion





        #region  Chart icin yapılmıs denem fonksiyonu . Duruma göre  kullanılabilir
        public void AddChartToDocument(Document doc, List<System.Windows.Forms.DataVisualization.Charting.Series> series, string xTitle, string yTitle, int xMax, int yMax, int xInterval, int yInterval)
        {


            try
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.Series s in series)

                    //s.SetCustomProperty("LineTension", "0.0");
                    s.Points.AddXY(dictRead["lrBS_bar"]);

                //prepare chart control....
                System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chart.Width = 850;
                chart.Height = 350;


                for (int i = 0; i < series.Count; i++)
                {
                    chart.Series.Add(series[i]);

                    chart.Series[i].ChartType = SeriesChartType.Line;
                    chart.Series[i].IsVisibleInLegend = true;

                }


                //create legend
                chart.Legends.Add("test");
                chart.Legends[0].BorderWidth = 3;
                chart.Legends[0].BorderColor = System.Drawing.Color.Black;



                //create chartareas...
                System.Windows.Forms.DataVisualization.Charting.ChartArea ca = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                ca.Name = "ChartArea1";

                ca.BackColor = System.Drawing.Color.White;

                ca.BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
                ca.BorderWidth = 0;
                ca.BorderDashStyle = ChartDashStyle.Solid;

                ca.AxisX = new System.Windows.Forms.DataVisualization.Charting.Axis();
                ca.AxisX.Title = xTitle;
                ca.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                ca.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                ca.AxisX.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);

                ca.AxisY = new System.Windows.Forms.DataVisualization.Charting.Axis();
                ca.AxisY.Title = yTitle;
                ca.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                ca.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                ca.AxisY.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);


                chart.ChartAreas.Add(ca);

                chart.ChartAreas[0].AxisX.Minimum = 0;
                chart.ChartAreas[0].AxisX.Maximum = xMax;

                chart.ChartAreas[0].AxisY.Minimum = 0;
                chart.ChartAreas[0].AxisY.Maximum = yMax;

                chart.ChartAreas[0].AxisX.Interval = xInterval;
                chart.ChartAreas[0].AxisY.Interval = yInterval;

                chart.IsSoftShadows = true;

                chart.Show();

                //Bitmap chartImage;


                using (MemoryStream ms = new MemoryStream())
                {
                    chart.SaveImage(ms, ChartImageFormat.Png);
                    chartImage = new Bitmap(ms);
                }


                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(chartImage, BaseColor.WHITE, false);

                jpg.Alignment = 1;

                doc.Add(jpg);   //pdf dosyasına grafigi eklemek icin


            }
            catch (Exception ex)
            {
                ShowError(ex.ToString());
                doc.Close();
            }


        }

        #endregion

        private void txtNotBilgi_TextChanged(object sender, EventArgs e)
        {
            //txtNotBilgi.Text = "";
            //char[] str = txtNotBilgi.Text.ToCharArray();
            //foreach (char items in str)
            //{
            //    var cx = (from t in dictState where t.Key == Convert.ToInt32(items.ToString()) select t);
            //    foreach (var item in cx)
            //    {
            //        txtNotBilgi.Text += item.Value;

            //    }
            //}
        }


        #region  PLC den verileri anlık cekme  icin kullanılan timer.
        private void timer_bw_Tick(object sender, EventArgs e)

        {
            MainForm.m_mf.m_plcValue.TestStep = ReadINTFromPlc(tcAds, "st11_wr");
            MainForm.m_mf.m_plcValue.Voltage = ReadStringFromPlc(tcAds, "st1_wr");
            MainForm.m_mf.m_plcValue.Current = ReadStringFromPlc(tcAds, "st2_wr");
            MainForm.m_mf.m_plcValue.Bsbar = ReadStringFromPlc(tcAds, "st3_wr");
            MainForm.m_mf.m_plcValue.BsXbar = ReadStringFromPlc(tcAds, "st39_wr");
            MainForm.m_mf.m_plcValue.Bpbar = ReadStringFromPlc(tcAds, "st4_wr");
            MainForm.m_mf.m_plcValue.BpXbar = ReadStringFromPlc(tcAds, "st40_wr");
            MainForm.m_mf.m_plcValue.AsValf = ReadStringFromPlc(tcAds, "st7_wr");
            MainForm.m_mf.m_plcValue.AtValf = ReadStringFromPlc(tcAds, "st8_wr");

            MainForm.m_mf.m_plcValue.EmergencyBrakeValf = ReadBoolFromPlc(tcAds, "st9_wr");
            MainForm.m_mf.m_plcValue.ParkingBrakeValf = ReadBoolFromPlc(tcAds, "st10_wr");


            MainForm.m_mf.m_plcValue.St16wr = ReadBoolFromPlc(tcAds, "st16_wr");
            //txt_Tarih.Text = DateTime.Now.ToString("dd/MM/yy") + " -- " + DateTime.Now.ToString("HH:mm:ss");
            //lbl_VolSensor.Text = "UGUR BARAN";

            DisplayManager.LabelInvoke(txt_Tarih, DateTime.Now.ToString("dd/MM/yy") + " -- " + DateTime.Now.ToString("HH:mm:ss"));

            DisplayManager.TextBoxInvoke(textBox1, ReadStringFromPlc(tcAds, "st1_wr"));
            DisplayManager.TextBoxInvoke(textBox2, ReadStringFromPlc(tcAds, "st2_wr"));
            DisplayManager.TextBoxInvoke(textBox3, ReadStringFromPlc(tcAds, "st3_wr"));
            DisplayManager.TextBoxInvoke(textBox4, ReadStringFromPlc(tcAds, "st4_wr"));
            DisplayManager.TextBoxInvoke(textBox5, ReadStringFromPlc(tcAds, "st7_wr"));
            DisplayManager.TextBoxInvoke(textBox6, ReadStringFromPlc(tcAds, "st8_wr"));
            DisplayManager.TextBoxInvoke(textBox7, ReadBoolFromPlc(tcAds, "st9_wr"));

            DisplayManager.TextBoxInvoke(textBox8, ReadBoolFromPlc(tcAds, "st10_wr"));

            DisplayManager.TextBoxInvoke(textBox9, ReadINTFromPlc(tcAds, "st11_wr"));

            DisplayManager.LabelInvoke(lbl_TestAdımı, ReadINTFromPlc(tcAds, "st11_wr"));
            //lbl_TestAdımı.Text = ReadINTFromPlc(tcAds, "st11_wr"); // Test Adımı

            DisplayManager.TextBoxInvoke(txt_BSxbar, ReadStringFromPlc(tcAds, "st39_wr"));

            DisplayManager.TextBoxInvoke(txt_BPxbar, ReadStringFromPlc(tcAds, "st40_wr"));

           

        }
        #endregion



        #region   Test Form Closing
        private void Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isRunning = false;

            m_test = null;

            twConnect = false;
            isEmergencyStop = false;

            isStarted = false;

            RequestStop();


        }

        #endregion
        private volatile bool _shouldStop;
        public void RequestStop()
        {
            _shouldStop = true;
            _isRunning = false;

        }
        #region Chart Create Timer

        public void Comolokko(object o)
        {

            try
            {

                while (!_shouldStop)
                {



                    while (_isRunning)
                    {

                        var BPx_bar = ReadStringFromPlc(tcAds, "st4_wr");


                        //var BP_bar = dictRead["BP_bar"];


                        var zaman = DateTime.Now.ToString("HH:mm:ss");




                        this.Invoke(new MethodInvoker(delegate ()    // timer ayrı bir component / thread olarak calıstıgı icin Invoke Method olarak calıstırdık
                        {

                            //lock (chartLock)
                            //{
                            //    Monitor.Wait(chartLock);

                            chart1.Series["Basınc-Zaman"].Points.AddXY(zaman, BPx_bar);
                            chart1.Series["Basınc-Zaman"].Color = System.Drawing.Color.Red;
                            //chart1.Show();

                            //chart1.Series.Add(s1);

                            //chart1.Series.Add(s1);
                            //chart1.Dock = DockStyle.Fill;



                            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;


                            chart1.ChartAreas[0].AxisX.Title = "Zaman (sn)";
                            chart1.ChartAreas[0].AxisY.Title = "Basınç (Bar)";

                            chart1.ChartAreas[0].AxisX.Minimum = 0;
                            chart1.ChartAreas[0].AxisX.Maximum = 100;

                            chart1.ChartAreas[0].AxisY.Minimum = 0;
                            chart1.ChartAreas[0].AxisY.Maximum = 120;

                            chart1.ChartAreas[0].AxisX.Interval = 10;
                            chart1.ChartAreas[0].AxisY.Interval = 10;



                            chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 14, System.Drawing.FontStyle.Bold);
                            chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 14, System.Drawing.FontStyle.Bold);

                            //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;


                            chart1.ChartAreas[0].BorderWidth = 0;//Bu satır chart çerçevesinin kalınlığını 0 olarak ayarlar (çerçeveyi gizler).
                            chart1.ChartAreas[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);


                            // Grafik kontrolü için AutoSizeMode özelliğini ayarlayın

                            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                            chart1.ChartAreas[0].CursorX.AutoScroll = true;
                            chart1.ChartAreas[0].CursorY.AutoScroll = true;
                            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                            chart1.ChartAreas[0].CursorX.Interval = 0.01;
                            chart1.ChartAreas[0].CursorY.Interval = 0.01;
                            chart1.ChartAreas[0].CursorX.LineWidth = 2;
                            chart1.ChartAreas[0].CursorY.LineWidth = 2;



                        }));

                        Thread.Sleep(1000);
                    }


                    Thread.Sleep(1000);
                }


            }


            catch (Exception ex)
            {
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message, ex.StackTrace, ex.TargetSite.ToString(), "timechart patladı");

                StringBuilder sbr = new StringBuilder("Grafik hatası");
                sbr.Append(ex.StackTrace.ToString());
                sbr.Append(ex.TargetSite.ToString());
                sbr.Append("Grafik hatası");

                MessageBox.Show(sbr.ToString());
            }


        }
        //private void timerChart1_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {


        //        var BPx_bar = ReadStringFromPlc(tcAds, "st4_wr");


        //        //var BP_bar = dictRead["BP_bar"];


        //        var zaman = DateTime.Now.ToString("HH:mm:ss");




        //        this.Invoke(new MethodInvoker(delegate ()    // timer ayrı bir component / thread olarak calıstıgı icin Invoke Method olarak calıstırdık
        //        {

        //            //lock (chartLock)
        //            //{
        //            //    Monitor.Wait(chartLock);

        //            chart1.Series["Basınc-Zaman"].Points.AddXY(zaman, BPx_bar);
        //            chart1.Series["Basınc-Zaman"].Color = System.Drawing.Color.Red;
        //            //chart1.Show();

        //            //chart1.Series.Add(s1);

        //            //chart1.Series.Add(s1);
        //            //chart1.Dock = DockStyle.Fill;



        //            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;


        //            chart1.ChartAreas[0].AxisX.Title = "Zaman (sn)";
        //            chart1.ChartAreas[0].AxisY.Title = "Basınç (Bar)";

        //            chart1.ChartAreas[0].AxisX.Minimum = 0;
        //            chart1.ChartAreas[0].AxisX.Maximum = 100;

        //            chart1.ChartAreas[0].AxisY.Minimum = 0;
        //            chart1.ChartAreas[0].AxisY.Maximum = 120;

        //            chart1.ChartAreas[0].AxisX.Interval = 10;
        //            chart1.ChartAreas[0].AxisY.Interval = 10;



        //            chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 14, System.Drawing.FontStyle.Bold);
        //            chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 14, System.Drawing.FontStyle.Bold);

        //            //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
        //            chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;


        //            chart1.ChartAreas[0].BorderWidth = 0;//Bu satır chart çerçevesinin kalınlığını 0 olarak ayarlar (çerçeveyi gizler).
        //            chart1.ChartAreas[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);


        //            // Grafik kontrolü için AutoSizeMode özelliğini ayarlayın

        //            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
        //            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
        //            chart1.ChartAreas[0].CursorX.AutoScroll = true;
        //            chart1.ChartAreas[0].CursorY.AutoScroll = true;
        //            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        //            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
        //            chart1.ChartAreas[0].CursorX.Interval = 0.01;
        //            chart1.ChartAreas[0].CursorY.Interval = 0.01;
        //            chart1.ChartAreas[0].CursorX.LineWidth = 2;
        //            chart1.ChartAreas[0].CursorY.LineWidth = 2;

        //            //chart1.IsSoftShadows = true;

        //            //    Monitor.Pulse(chartLock);
        //            //}
        //        }));



        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(DateTime.Now.ToString(), ex.Message, ex.StackTrace, ex.TargetSite.ToString(), "timechart patladı");

        //        StringBuilder sbr = new StringBuilder("Grafik hatası");
        //        sbr.Append(ex.StackTrace.ToString());
        //        sbr.Append(ex.TargetSite.ToString());
        //        sbr.Append("Grafik hatası");

        //        MessageBox.Show(sbr.ToString());
        //    }

        //}


        #endregion




        #region   Direnc degerlerini giriniz
        private void btn_DirencOK_Click(object sender, EventArgs e)
        {
            string paramName = dictWrite["YalitimDirenciTesti"];

            WriteStringToPlc(tcAds, paramName, "TRUE");

            if (btn_TestBaslat.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                btn_TestBaslat.Invoke((MethodInvoker)delegate
                {
                    btn_TestBaslat.Enabled = true;
                    btn_TestBaslat.ForeColor = System.Drawing.Color.Green;
                });
            }
            else
            {
                btn_TestBaslat.Enabled = true;
                btn_TestBaslat.ForeColor = System.Drawing.Color.Green;
            }


            DisplayManager.LabelVisibleInvoke(lblDirenc1, false);
            DisplayManager.LabelVisibleInvoke(lblDirenc2, false);
            DisplayManager.TextVisibleInvoke(txt_Direnc1, false);
            DisplayManager.TextVisibleInvoke(txt_Direnc2, false);
            DisplayManager.ButonVisibleInvoke(btn_DirencOK, false);

            //lblDirenc1.Visible = false;
            //lblDirenc2.Visible = false;
            //txt_Direnc1.Visible = false;
            //txt_Direnc2.Visible = false;
            //btn_DirencOK.Visible = false;


            FlexibleMessageBox.Show("Testi Baslatabilirsiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion


        private readonly object fileLock = new object();
       
        #region    Pdf Kaydetme Fonksiyonu
        public void Pdf_Save()
        {
         

            lock (fileLock)
            {
                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string second = DateTime.Now.ToString("HH-mm");
                string filePath = $"E:/Reports/TEST_{date}-{second}.pdf";

                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        iTextSharp.text.Document doc = new iTextSharp.text.Document();
                        PdfWriter.GetInstance(doc, fs);
                        doc.AddCreationDate();

                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Resources.bga, System.Drawing.Imaging.ImageFormat.Jpeg);
                        img.ScalePercent(0.5f * 60);
                        img.Left = 0;
                        string FONT = "FreeSans.ttf";


                        iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "ISO-8859-1", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                        iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);

                        BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        //bf.CorrectArabicAdvance();
                        int pageNumber = 1;

                        int totalPageNumber = 2;

                        //iTextSharp.text.Document doc = new iTextSharp.text.Document();
                        //string date = DateTime.Now.ToString("dd-MM-yyyy");
                        //string second = DateTime.Now.ToString("HH-mm");
                        //PdfWriter.GetInstance(doc, new FileStream("E:/Reports/TEST_" + date + "-" + second + ".pdf", FileMode.Create));
                        ////PdfWriter.GetInstance(doc, new FileStream("Bilgiler4.pdf", FileMode.Create));
                        //doc.AddCreationDate();

                        //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Resources.bga, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //img.ScalePercent(0.5f * 60);
                        //img.Left = 0;
                        //iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "ISO-8859-1", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                        //iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);


                        //BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, "Cp1254", BaseFont.EMBEDDED);

                        //iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                        if (!doc.IsOpen())

                        {

                            doc.Open();

                        }

                        // en üste resim ekliyoruz.
                        img.Alignment = 1;
                        doc.Add(img);

                        var tarih = DateTime.Now.ToString("dd/MM/yy");
                        var saat = DateTime.Now.ToLongTimeString();


                        #region Üstteki tablo



                        PdfPTable table = new PdfPTable(2);//üstteki tablo bilgileri
                        table.DefaultCell.Border = 0;
                        table.WidthPercentage = 100;


                        PdfPCell cell = new PdfPCell(new Phrase("", normalFont));
                        cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                        cell.Colspan = 4;
                        cell.Rowspan = 1;
                        cell.BorderWidth = 3;
                        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                        //table.TotalWidth = 216f;

                        //float[] widths = new float[] { 1f, 6f, 8f, 10f };
                        //table.SetWidths(widths);
                        cell = new PdfPCell((new Phrase("Test Yapan(Ad-Soyad)", normalFont)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase(MainForm.m_mf.m_user.Name + " " + MainForm.m_mf.m_user.Surname, normalFont)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase("Test Yapan(Sicil No)", normalFont)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase(MainForm.m_mf.m_user.PersonNo)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase(MainForm.m_mf.m_user.SeriNo)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase("Tarih", normalFont)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase(tarih + "  Saat : " + saat, normalFont)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);



                        cell = new PdfPCell((new Phrase("İmza  ", normalFont)));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell((new Phrase(" ")));
                        cell.BorderWidth = 1;
                        table.AddCell(cell);

                        doc.Add(table);


                        #endregion

                        //PdfPTable table1 = new PdfPTable(2);
                        //table1.DefaultCell.Border = 0;
                        //table1.WidthPercentage = 80;





                        PdfPCell cell12 = new PdfPCell();
                        cell12.VerticalAlignment = Element.ALIGN_CENTER;

                        //table1.AddCell(cell11);

                        //table1.AddCell(cell12);


                        #region Ortadaki tablo



                        PdfPTable table2 = new PdfPTable(4);
                        table2.DefaultCell.Border = 0;
                        table2.SetWidths(new int[] { 2, 4, 4, 4 });//tablo satır boyutları
                        table2.WidthPercentage = 100;

                        //One row added

                        //PdfPCell cell = new PdfPCell();
                        //cell.FixedHeight = 30.0f;
                        //cell.AddElement(new Paragraph("Yeni Pargaraff"));

                        PdfPCell cell21 = new PdfPCell();
                        cell21.HorizontalAlignment = 1;
                        cell21.BorderWidth = 1;
                        cell21.Colspan = 0;
                        cell21.AddElement(new Paragraph("Talimattaki \n Bölüm", normalFont));

                        PdfPCell cell22 = new PdfPCell();
                        cell22.HorizontalAlignment = 1;
                        cell22.BorderWidth = 1;
                        cell22.Colspan = 0;
                        cell22.AddElement(new Paragraph("Test Adımı", normalFont));

                        PdfPCell cell23 = new PdfPCell();
                        cell23.HorizontalAlignment = 1;
                        cell23.BorderWidth = 1;
                        cell23.Colspan = 0;
                        cell23.AddElement(new Paragraph("  Hedef", normalFont));

                        PdfPCell cell24 = new PdfPCell();
                        cell24.HorizontalAlignment = 1;
                        cell24.BorderWidth = 1;
                        cell24.Colspan = 0;
                        cell24.AddElement(new Paragraph("  Bulunan Deger", normalFont));

                        //table2.AddCell(cell);
                        table2.AddCell(cell21);
                        table2.AddCell(cell22);
                        table2.AddCell(cell23);
                        table2.AddCell(cell24);


                        //New Row Added

                        cell = new PdfPCell();
                        cell.AddElement(new Paragraph("\n6.1", normalFont));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement(new Paragraph("İlk üretim\nBakım-Onarım Sonrası", normalFont));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement(new Paragraph("  R >= 650 kOhm\n  R >= 25 kOhm", normalFont));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);




                        cell = new PdfPCell();
                        cell.AddElement(new Paragraph("  R = " + txt_Direnc1.Text + " Ohm \n  R = " + txt_Direnc2.Text + " Ohm", normalFont));
                        cell.HorizontalAlignment = 1;
                        //cell.BorderWidth = 1;
                        table2.AddCell(cell);




                        cell = new PdfPCell((new Phrase("\n\n\n\n\n\n6.2", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement(new Paragraph("P = 0 → 150 Bar\n\nP = 120 → 150 Bar\n60s sonra\n120s sonra", normalFont));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement(new Paragraph("  t <= 40s\n  Imax = 30A\n  t <= 20s\n  \u2206 P <= 15 Bar\n  \u2206 P <= 20 Bar", normalFont));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);
                        //.Substring(0, ReadStringFromPlc(tcAds, "st20_wr").IndexOf('.') + 1)
                        cell = new PdfPCell();
                        cell.AddElement(new Paragraph("  t = " + ReadStringFromPlc(tcAds, "st17_wr") + "s" + "\n  Imax = " + ReadStringFromPlc(tcAds, "st18_wr") + "A" + "\n  t = " + ReadStringFromPlc(tcAds, "st19_wr") + "s" + "\n  \u2206 P = " + ReadStringFromPlc(tcAds, "st20_wr") + " Bar" + "\n  \u2206 P = " + ReadStringFromPlc(tcAds, "st21_wr") + " Bar", normalFont)); ;
                        cell.HorizontalAlignment = 2;

                        table2.AddCell(cell);


                        cell = new PdfPCell();

                        cell.AddElement(new Paragraph("6.3", normalFont));

                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Paragraph("Emniyet Valfinin Kontrolü                        " + "\nP = 150 → 190 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        //cell.Border = iTextSharp.text.Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Paragraph("\n  165 <= P <= 190 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("\n  P = " + ReadStringFromPlc(tcAds, "st22_wr") + " Bar", normalFont)));
                        cell.HorizontalAlignment = 2;

                        table2.AddCell(cell);

                        cell = new PdfPCell((new Phrase("6.4", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("\nTank Valfi", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  Ptank < 0,5 Bar" + "\n  Ptank > -0,1 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);



                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  " + ReadStringFromPlc(tcAds, "st23_wr") + "\n  " + ReadStringFromPlc(tcAds, "st24_wr"), normalFont)));
                        cell.HorizontalAlignment = 2;

                        table2.AddCell(cell);

                        cell = new PdfPCell((new Phrase("6.5", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("IT Valfinin Kontrolü", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  PDBV = 36+-2 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  P = " + ReadStringFromPlc(tcAds, "st25_wr") + " Bar", normalFont)));
                        cell.HorizontalAlignment = 2;
                        table2.AddCell(cell);


                        cell = new PdfPCell((new Phrase("\n\n\n\n\n6.6", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("\nAF Zamanı", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  P = 130+-2 Bar" + "\n  t <= 500ms" + "\n  t <= 800ms" + "\n  P = 130+-2 Bar ", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  P = " + ReadStringFromPlc(tcAds, "st28_wr") + " Bar" + "\n  t = " + ReadStringFromPlc(tcAds, "st26_wr") + "ms" + "\n  t = " + ReadStringFromPlc(tcAds, "st27_wr") + "ms" + "\n  P = " + ReadStringFromPlc(tcAds, "st29_wr") + " Bar", normalFont)));
                        cell.HorizontalAlignment = 2;
                        table2.AddCell(cell);

                        cell = new PdfPCell((new Phrase("6.7", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("Park Fren Valfi", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  Pmp <= 1 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  " + ReadBoolFromPlc(tcAds, "st30_wr") + " , " + ReadBoolFromPlc(tcAds, "st31_wr"), normalFont)));
                        cell.HorizontalAlignment = 2;
                        table2.AddCell(cell);

                        cell = new PdfPCell((new Phrase("\n\n\n\n\n6.8", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Paragraph("\nMaksimum Histeristik\nPmp = 10 → 90 Bar\nPmp = 90 → 10 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Paragraph("  P = 130+-2 Bar" + "\n  Hys <= 2 Bar" + "\n  t <= 900ms" + "\n  t <= 1000ms", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("  P = " + ReadStringFromPlc(tcAds, "st35_wr") + " Bar" + "\n  Hys = " + ReadStringFromPlc(tcAds, "st32_wr") + " Bar" + "\n  t = " + ReadStringFromPlc(tcAds, "st33_wr") + "ms" + "\n  t = " + ReadStringFromPlc(tcAds, "st34_wr") + "ms", normalFont)));
                        cell.HorizontalAlignment = 2;
                        table2.AddCell(cell);

                        cell = new PdfPCell((new Phrase("6.9", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("AS Sızıntı Kontrolü 60s", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase(" \u2206" + "P <= 10 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase(" \u2206" + "P = " + ReadStringFromPlc(tcAds, "st36_wr") + " Bar", normalFont)));
                        cell.HorizontalAlignment = 2;
                        table2.AddCell(cell);

                        cell = new PdfPCell((new Phrase("6.10", normalFont)));
                        cell.HorizontalAlignment = 0;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase("AT Sızıntı Kontrolü 60s", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase(" \u2206" + "P <= 10 Bar", normalFont)));
                        cell.HorizontalAlignment = 1;
                        table2.AddCell(cell);

                        cell = new PdfPCell();
                        cell.AddElement((new Phrase(" \u2206" + "P = " + ReadStringFromPlc(tcAds, "st37_wr") + " Bar", normalFont)));
                        cell.HorizontalAlignment = 2;

                        table2.AddCell(cell);

                        #endregion

                        PdfPTable table4 = new PdfPTable(4);//en alttaki tablo bilgileri
                        table4.DefaultCell.Border = 0;
                        table4.WidthPercentage = 100;
                        cell = new PdfPCell(new Phrase());
                        cell.BorderWidth = 1;
                        table4.AddCell(cell);


                        #region En alttaki tablo

                        PdfPTable table3 = new PdfPTable(4);//en alttaki tablo bilgileri
                        table3.DefaultCell.Border = 0;
                        table3.WidthPercentage = 100;

                        cell = new PdfPCell((new Phrase("Ünite Seri No", normalFont)));
                        cell.BorderWidth = 1;
                        table3.AddCell(cell);

                        cell = new PdfPCell((new Phrase("")));
                        cell.BorderWidth = 1;

                        table3.AddCell(cell);

                        cell = new PdfPCell((new Phrase("Ünite Test Koşullarını \n      Sağlıyor", normalFont)));
                        cell.BorderWidth = 1;

                        table3.AddCell(cell);

                        cell = new PdfPCell((new Phrase("")));
                        cell.BorderWidth = 1;

                        table3.AddCell(cell);

                        #endregion



                        doc.Add(table2);
                        doc.Add(table3);



                        string page = pageNumber.ToString() + "/" + totalPageNumber.ToString();
                        Paragraph footer = new Paragraph(page, normalFont);
                        footer.Alignment = 2;
                        doc.Add(footer);
                        //doc.Add(footer);





                        doc.NewPage();
                        doc.Add(new Paragraph("     BP Basınc - Zaman Grafiği - 111 Nolu Adım", normalFont));
                        jpgChart.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);
                        jpgChart.Alignment = Element.ALIGN_CENTER;
                        doc.Add(jpgChart);

                        doc.Add(new Paragraph(" "));

                        //doc.NewPage();
                        doc.Add(new Paragraph("     BP Basınc - Zaman Grafiği - 115 Nolu Adım", normalFont));
                        jpgChart2.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);
                        jpgChart2.Alignment = Element.ALIGN_CENTER;
                        doc.Add(jpgChart2);

                        pageNumber++;
                        string page1 = pageNumber.ToString() + "/" + totalPageNumber.ToString();
                        Paragraph footer1 = new Paragraph(page1, normalFont);
                        footer1.Alignment = 2;
                        doc.Add(footer1);
                        //doc.Add(footer);



                        doc.Close();
                    }


                }
                catch (Exception ex)
                {
                    Logging.WriteLog(DateTime.Now.ToString(), ex.Message, ex.StackTrace, ex.TargetSite.ToString(), "pdf patladı");


                    StringBuilder sbr = new StringBuilder("Pdf cıktısı hatası baslatılıyorr ");
                    sbr.Append(ex.StackTrace.ToString());
                    sbr.Append(ex.TargetSite.ToString());
                    sbr.Append("Pdf cıktısı hatası bitiyor");

                    MessageBox.Show(sbr.ToString());

                    ErrorLog("Pdf cıktısı hatası baslıtorr");
                    ErrorLog(ex.StackTrace.ToString());

                    ErrorLog(ex.TargetSite.ToString());
                    ErrorLog("Pdf cıktısı hatası bitiyor");
                    ShowError("Rapor kaydedilirken hata oluştu!");
                }
            }
        }

        #endregion



        private void Test_Resize(object sender, EventArgs e)
        {
            ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
            //ReSizer.ResizeAndLocateControl(this.border_emg.Width, this.border_emg.Height);
            //ReSizer.ResizeAndLocateControl(this.groupBox1.Width, this.groupBox1.Height);
        }

        private void border_emg_Resize(object sender, EventArgs e)
        {
            //border_emg.Size = new Size(this.ClientSize.Width/2 - 300, this.ClientSize.Height/2 - 10);
            //ReSizer.ResizeAndLocateControl(this.border_emg.Width,this.border_emg.Height);

            //ReSizer.GroupBoxToBeResize(border_emg);
        }


        #region    Grafik Cizme fonksiyonu
        public void Chart_Ciz()
        {
            try
            {
                var BP_bar = ReadStringFromPlc(tcAds, "st4_wr");


                //var BP_bar = dictRead["BP_bar"];



                var zaman = DateTime.Now.ToString("ss");


                this.Invoke(new MethodInvoker(delegate ()    // timer ayrı bir component / thread olarak calıstıgı icin Invoke Method olarak calıstırdık
                {



                    chart1.Series["Basınc-Zaman"].Points.AddXY(zaman, BP_bar);
                    chart1.Series["Basınc-Zaman"].Color = System.Drawing.Color.Red;
                    //chart1.Show();

                    //chart1.Series.Add(s1);
                    chart1.Dock = DockStyle.None;

                    chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                    chart1.ChartAreas[0].CursorX.AutoScroll = true;
                    chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;  // 4  satır sonradan eklendi
                    chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;


                    chart1.ChartAreas[0].AxisX.Title = "Zaman (sn)";
                    chart1.ChartAreas[0].AxisY.Title = "Basınç (Bar)";

                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.Maximum = 50;

                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Maximum = 120;

                    chart1.ChartAreas[0].AxisX.Interval = 10;
                    chart1.ChartAreas[0].AxisY.Interval = 10;



                    chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 14, System.Drawing.FontStyle.Bold);
                    chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 14, System.Drawing.FontStyle.Bold);

                    //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                    chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;

                    chart1.ChartAreas[0].BorderWidth = 0;
                    chart1.ChartAreas[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);




                    //chart1.IsSoftShadows = true;

                    Bitmap chartImage;


                    using (MemoryStream ms = new MemoryStream())
                    {


                        chart1.SaveImage(ms, ChartImageFormat.Png);
                        chartImage = new Bitmap(ms);
                    }


                    jpgChart = iTextSharp.text.Image.GetInstance(chartImage, BaseColor.WHITE, false);


                    jpgChart.Alignment = 1;
                }));
            }
            catch (Exception ex)
            {

                StringBuilder sbr = new StringBuilder("Grafik hatası");
                sbr.Append(ex.StackTrace.ToString());
                sbr.Append(ex.TargetSite.ToString());
                sbr.Append("Grafik hatası");

                MessageBox.Show(sbr.ToString());
            }
        }

        #endregion

        #region       PLC den veri okuma fonksiyonu
        private void PLC_Read(object state)
        {


            MainForm.m_mf.m_plcValue.TestStep = ReadINTFromPlc(tcAds, "st11_wr");
            MainForm.m_mf.m_plcValue.Voltage = ReadStringFromPlc(tcAds, "st1_wr");
            MainForm.m_mf.m_plcValue.Current = ReadStringFromPlc(tcAds, "st2_wr");
            MainForm.m_mf.m_plcValue.Bsbar = ReadStringFromPlc(tcAds, "st3_wr");
            MainForm.m_mf.m_plcValue.BsXbar = ReadStringFromPlc(tcAds, "st39_wr");
            MainForm.m_mf.m_plcValue.Bpbar = ReadStringFromPlc(tcAds, "st4_wr");
            MainForm.m_mf.m_plcValue.BpXbar = ReadStringFromPlc(tcAds, "st40_wr");
            MainForm.m_mf.m_plcValue.AsValf = ReadStringFromPlc(tcAds, "st7_wr");
            MainForm.m_mf.m_plcValue.AtValf = ReadStringFromPlc(tcAds, "st8_wr");

            MainForm.m_mf.m_plcValue.EmergencyBrakeValf = ReadBoolFromPlc(tcAds, "st9_wr");
            MainForm.m_mf.m_plcValue.ParkingBrakeValf = ReadBoolFromPlc(tcAds, "st10_wr");

            MainForm.m_mf.m_plcValue.St16wr = ReadBoolFromPlc(tcAds, "st16_wr");

            DisplayManager.LabelInvoke(txt_Tarih, DateTime.Now.ToString("dd/MM/yy") + " -- " + DateTime.Now.ToString("HH:mm:ss"));

            DisplayManager.TextBoxInvoke(textBox1, MainForm.m_mf.m_plcValue.Voltage);
            DisplayManager.TextBoxInvoke(textBox2, MainForm.m_mf.m_plcValue.Current);
            DisplayManager.TextBoxInvoke(textBox3, MainForm.m_mf.m_plcValue.Bsbar);
            DisplayManager.TextBoxInvoke(textBox4, MainForm.m_mf.m_plcValue.Bpbar);
            DisplayManager.TextBoxInvoke(textBox5, MainForm.m_mf.m_plcValue.AsValf);
            DisplayManager.TextBoxInvoke(textBox6, MainForm.m_mf.m_plcValue.AtValf);
            DisplayManager.TextBoxInvoke(textBox7, MainForm.m_mf.m_plcValue.EmergencyBrakeValf);

            DisplayManager.TextBoxInvoke(textBox8, MainForm.m_mf.m_plcValue.ParkingBrakeValf);

            DisplayManager.TextBoxInvoke(textBox9, MainForm.m_mf.m_plcValue.TestStep);

            DisplayManager.LabelInvoke(lbl_TestAdımı, MainForm.m_mf.m_plcValue.TestStep);
            //lbl_TestAdımı.Text = ReadINTFromPlc(tcAds, "st11_wr"); // Test Adımı

            DisplayManager.TextBoxInvoke(txt_BSxbar, MainForm.m_mf.m_plcValue.BsXbar);

            DisplayManager.TextBoxInvoke(txt_BPxbar, MainForm.m_mf.m_plcValue.BpXbar);
           

        }
        #endregion



        private void btn_Test_Bitir_Click(object sender, EventArgs e)
        {
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.CancelAsync();


            MainForm.m_mf.Show();

            CloseForm();
        }

        private void Test_Shown(object sender, EventArgs e)
        {
            //main_Timer = new System.Threading.Timer(PLC_Read, null, 1000, 500);   //  PLC den sürekli veri ouyan Thread Timerı baslattık
        }




        // chart1 nesnesine erişimi senkronize etmek için bir kilit nesnesi oluşturun
        private readonly object chartLock = new object();

        private void SaveChartImagejpgChart(object o)
        {

            _isRunning = false;

            // Zaman alan işlemleri burada gerçekleştirin
            Bitmap chartImage;

            lock (chartLock)
            {
                //Monitor.Wait(chartLock);


                using (MemoryStream ms = new MemoryStream())
                {
                    chart1.Invoke(new System.Action(() =>
                    {
                        chart1.SaveImage(ms, ChartImageFormat.Png);
                    }));
                    chartImage = new Bitmap(ms);
                }

                //Monitor.Pulse(chartLock);

            }

            // iTextSharp görüntü örneğini oluştur
            jpgChart = iTextSharp.text.Image.GetInstance(chartImage, BaseColor.WHITE, false);

          
        }



        private void SaveChartImagejpgChart2(object o)
        {

            _isRunning = false;

            // Zaman alan işlemleri burada gerçekleştirin
            Bitmap chartImage1;

            lock (chartLock)
            {
                //Monitor.Wait(chartLock);


                using (MemoryStream ms = new MemoryStream())
                {
                    chart1.Invoke(new System.Action(() =>
                    {
                        chart1.SaveImage(ms, ChartImageFormat.Png);
                    }));
                    chartImage1 = new Bitmap(ms);
                }

                //Monitor.Pulse(chartLock);

            }

            // iTextSharp görüntü örneğini oluştur
            jpgChart2 = iTextSharp.text.Image.GetInstance(chartImage1, BaseColor.WHITE, false);



        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            //ReSizer.ResizeAndLocateControl(groupBox1.ClientSize.Width, groupBox1.ClientSize.Height);
            //ReSizer.GroupBoxToBeResize(groupBox1);

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            PLC_Read(sender);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(textBox3.Text))
            //{
            //    //if ((Convert.ToDouble(textBox3.Text) < 120))
            //    {
            //        //textBox3.Clear();
            //        double sülo = Convert.ToDouble(textBox3.Text);
            //        DisplayManager.TextBoxInvoke(Test.Singleton(MainForm.m_mf).textBox10, textBox3.Text+"\n");


            //        DisplayManager.TextBoxInvoke(Test.Singleton(MainForm.m_mf).textBox10, "süloooo"+sülo);
            //        //ElValfiForm.Instance.Close();

            //    }
            //}
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text))
            {

                string osmin;

                dictState.TryGetValue(int.Parse(textBox9.Text), out osmin);
                txtNotBilgi1.Clear();
                DisplayManager.TextBoxInvoke(txtNotBilgi1, osmin);

            }

        }
    }


}
