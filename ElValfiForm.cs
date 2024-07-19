using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads.TypeSystem;

namespace HPU_OTOMASYON
{
    public partial class ElValfiForm : Form
    {
        private static ElValfiForm el = null;
        private static readonly object padlock = new object();
        public ElValfiForm()
        {
            InitializeComponent();
        }

        public static ElValfiForm Instance()
        {
            if (el == null)
                el = new ElValfiForm();

            return el;
        }
        private void ElValfiForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ElValfiForm.el.Dispose();
            ElValfiForm.el = null;
        }

        public void ControlCloseForm()
        {

            //lock (padlock)
            {
                if (ElValfiForm.el != null)
                {
                    Instance().Hide();
                }
            }
        }

        public void ControlOpenForm()
        {
            //lock (padlock)
            {
                if (ElValfiForm.el != null)
                {
                    Instance().ShowDialog();
                }
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
