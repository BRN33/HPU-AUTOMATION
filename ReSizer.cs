using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPU_OTOMASYON
{
    class ReSizer     // Bu Clas form elemnlarını ekrana göre boyutlandırma icin yazıldı
    {
        private static List<Control> controls = new List<Control>();
        private static List<Size> sizes = new List<Size>();
        private static List<Point> points = new List<Point>();
        private static List<Font> fonts = new List<Font>();

        public static int m_width { get; set; }
        public static int m_height { get; set; }

        public static void FormToBeResize(Form form)
        {
            m_width = form.ClientSize.Width;
            m_height = form.ClientSize.Height;

            foreach (Control ctrl in form.Controls)
            {
                controls.Add(ctrl);
                sizes.Add(ctrl.Size);
                points.Add(ctrl.Location);
                fonts.Add(ctrl.Font);
            }

            for (int i = 0; i < controls.Count; i++)
            {
                int dikeyOran = form.ClientSize.Width * sizes[i].Width / m_width;
                int yatayOran = form.ClientSize.Height * sizes[i].Height / m_height;

                int k = (form.ClientSize.Width * points[i].X) / m_width;
                int k1 = (form.ClientSize.Height * points[i].Y) / m_height;

                controls[i].Location = new Point(k, k1);

                controls[i].Size = new Size(dikeyOran, yatayOran);


            }
        }


        public static void GroupBoxToBeResize(GroupBox groupBox)
        {
            m_width = groupBox.ClientSize.Width;
            m_height = groupBox.ClientSize.Height;

            foreach (Control ctrl in groupBox.Controls)
            {
                controls.Add(ctrl);
                sizes.Add(ctrl.Size);
                points.Add(ctrl.Location);
            }

            for (int i = 0; i < controls.Count; i++)
            {
                int dikeyOran = groupBox.ClientSize.Width * sizes[i].Width / m_width;
                int yatayOran = groupBox.ClientSize.Height * sizes[i].Height / m_height;

                int k = (groupBox.ClientSize.Width * points[i].X) / m_width;
                int k1 = (groupBox.ClientSize.Height * points[i].Y) / m_height;

                controls[i].Location = new Point(k, k1);

                controls[i].Size = new Size(dikeyOran, yatayOran);
            }
        }

        public static void ResizeAndLocateControl(int clientSizeWidth, int clientSizeHeight)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                int dikeyOran = clientSizeWidth * sizes[i].Width / m_width;
                int yatayOran = clientSizeHeight * sizes[i].Height / m_height;

                int k = (clientSizeWidth * points[i].X) / m_width;
                int k1 = (clientSizeHeight * points[i].Y) / m_height;

                controls[i].Location = new Point(k, k1);

                controls[i].Size = new Size(dikeyOran, yatayOran);
                controls[i].Font = new Font("Arial", 22);
            }
        }

        internal static void FormToBeResize(GroupBox border_emg)
        {
            throw new NotImplementedException();
        }
    }
}
