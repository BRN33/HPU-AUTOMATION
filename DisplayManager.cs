using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace HPU_OTOMASYON
{
    internal class DisplayManager
    {
        public static void SetDoubleBuffered(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        public static void TextBoxInvoke(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
                textBox.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = text;
                });
            else
            {
                textBox.Text = text;
            }
        }

        public static void LabelInvoke(Label label, string text)
        {
            if (label.InvokeRequired)
                label.Invoke((MethodInvoker)delegate
                {
                    label.Text = text;
                });
            else
            {
                label.Text = text;
            }
        }

        public static void LabelVisibleInvoke(Label label, bool visible)
        {
            if (label.InvokeRequired)
                label.Invoke((MethodInvoker)delegate
                {
                    label.Visible = visible;
                });
            else
            {
                label.Visible = visible;
            }
        }

        public static void TextVisibleInvoke(TextBox textBoxVisible, bool visible)
        {
            if (textBoxVisible.InvokeRequired)
                textBoxVisible.Invoke((MethodInvoker)delegate
                {
                    textBoxVisible.Visible = visible;
                });
            else
            {
                textBoxVisible.Visible = visible;
            }
        }

        public static void ButonVisibleInvoke(Button buton, bool visible)
        {
            if (buton.InvokeRequired)
                buton.Invoke((MethodInvoker)delegate
                {
                    buton.Visible = visible;
                });
            else
            {
                buton.Visible = visible;
            }
        }
    }
}
