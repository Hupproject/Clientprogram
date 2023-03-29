using System;
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
    public partial class IPSet : MetroFramework.Forms.MetroForm
    {
        string myip;
        string serverip;
        Properties.Settings settings = Properties.Settings.Default;

        public IPSet()
        {
            InitializeComponent();
        }

        private void IPSet_Load(object sender, EventArgs e)
        {
            settings.Reload();
            metroTextBox1.Text = settings.IPC1;
            metroTextBox2.Text = settings.IPC2;
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            settings.IPC1 = metroTextBox1.Text;
            settings.IPC2 = metroTextBox2.Text;
            settings.Save();
            this.Close();
        }
    }
}
