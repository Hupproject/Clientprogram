using MetroFramework.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class DisplayShare : Form
    {
        bool check;
        Size csize;
        SendEventClient EventSC
        {
            get
            {
                return ControllerDis.Singleton.SendEventClient;
            }
        }

        public DisplayShare()
        {
            InitializeComponent();
        }
        private void RemoteClientForm_Load(object sender, EventArgs e)
        {
            ControllerDis.Singleton.RecvedImage += Singleton_RecvedImage;
            //metroTile1.Location = new Point(1780, 15);
        }

        private void Singleton_RecvedImage(object sender, RecvImageEventArgs e)
        {
            if (check == false)
            {
                ControllerDis.Singleton.StartEventClient();
                check = true;
                csize = e.Image.Size;
            }
            pbox_remote.Image = e.Image;
        }



        private Point ConvertPoint(int x, int y)
        {
            int nx = csize.Width * x / pbox_remote.Width;
            int ny = csize.Height * y / pbox_remote.Height;
            return new Point(nx, ny);
        }


        private void RemoteClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
