using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPU_OTOMASYON
{
    public partial class ElValfiForm2 : Form
    {
     

        private static ElValfiForm2 el2 = null;
        private static readonly object padlock = new object();
        public ElValfiForm2()
        {
            InitializeComponent();
           
        }


        public static ElValfiForm2 Instance()
        {
            if (el2 == null)
                el2 = new ElValfiForm2();

            return el2;
        }
        private void ElValfiForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ElValfiForm2.el2.Dispose();
            ElValfiForm2.el2 = null;
        }

    

        public void ControlOpenForm()
        {
            //lock (padlock)
            {
                if (ElValfiForm2.el2 != null)
                {
                    Instance().ShowDialog();
                }
            }
        }

        public void ControlCloseForm()
        {

            //lock (padlock)
            {
                if (ElValfiForm2.el2 != null)
                {
                    Instance().Hide();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ElValfiForm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            el2.Dispose();
            el2 = null;
        }
    }
}
