﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class RemoteClientForm : Form
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
        public RemoteClientForm()
        {
            InitializeComponent();
        }

        private void RemoteClientForm_Load(object sender, EventArgs e)
        {
            ControllerDis.Singleton.RecvedImage += Singleton_RecvedImage;
        }

        private void Singleton_RecvedImage(object sender, RecvImageEventArgs e)
        {
            if(check == false)
            {
                ControllerDis.Singleton.StartEventClient();
                check = true;
                csize = e.Image.Size;
            }
            pbox_remote.Image = e.Image;
        }

        private void RemoteClientForm_KeyUp(object sender, KeyEventArgs e)
        {
            if(check == true)
            {
                EventSC.SendKeyUp(e.KeyValue);
            }
        }

        private void RemoteClientForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (check == true)
            {
                EventSC.SendKeyDown(e.KeyValue);
            }
        }

        private void pbox_remote_MouseDown(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseDown(e.Button);
            }
        }

        private void pbox_remote_MouseMove(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Point pt = ConvertPoint(e.X, e.Y);
                EventSC.SendMouseMove(pt.X,pt.Y);
            }
        }

        private Point ConvertPoint(int x, int y)
        {
            int nx = csize.Width * x / pbox_remote.Width;
            int ny = csize.Height * y / pbox_remote.Height;
            return new Point(nx, ny);
        }

        private void pbox_remote_MouseUp(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseUp(e.Button);
            }

        }

        
    }
}
