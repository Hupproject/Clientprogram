using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Threading;
using RCEventArgsLib;
using System.Windows.Documents;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class Class1 : MetroFramework.Forms.MetroForm

    {
        string ID;
        bool check = false;
        string Studentnumber;
        string sum;
        Properties.Settings settings = Properties.Settings.Default;
        string SIP1 = "/connect ";
        string ProcessList;
        public string SIP2 = "172.18.4.99"; //본인
        string SIP3 = " ";
        public string SIP4 = "172.18.4.99"; //서버
        string SIP5 = " 10203";
        string myIP = "45:";
        string sip; //원격제어, 화면제어할때 사용할 IP를 담음.
        RemoteClientForm rcf = null;
        public List<string> users = new List<string>();
        public static string protime;
        private System.Windows.Forms.Timer timer1;
        public static string[] proname;
        private Process[] processes;
        // IPSet ipset1 = new IPSet();
        NetworkStream stream = default(NetworkStream);
        TcpClient client = new TcpClient();
        public Class1()
        {
            InitializeComponent();
        }
        public Class1(string ID, string studentnumber)
        {
            this.ID = ID;
            this.Studentnumber = studentnumber;
            sum = Studentnumber +"_"+ ID;
            InitializeComponent();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            settings.Reload();
            SIP2 = settings.IPC1;
            SIP4 = settings.IPC2;
            

        }
        
        void Connect(string s)
        {
            char split = ' ';
            string[] parsedIP = s.Split(split); // 1 : client, 2 : server, 3 : server Port

            try
            {
                IPEndPoint clientAddr = new IPEndPoint(IPAddress.Parse(parsedIP[1]), 0);
                IPEndPoint serverAddr = new IPEndPoint(IPAddress.Parse(parsedIP[2]), Int32.Parse(parsedIP[3]));
                client = new TcpClient(clientAddr);
                client.Connect(serverAddr);
                stream = client.GetStream();
                Display("Chat Server Connected...");
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to Connect...");  
                Application.Exit();

            }
            if (!client.Connected)
            {
                MessageBox.Show("usage : /connect 클라이언트IP 서버IP 서버PORT");
            }
            else
            {
                
                byte[] buffer = Encoding.Default.GetBytes(sum);
                stream.Write(buffer, 0, buffer.Length);
                Thread clientThread = new Thread(Receiver);
                clientThread.IsBackground = true;
                clientThread.Start();
            }

        }
        void Display(string msg)
        {
            CheckForIllegalCrossThreadCalls = false;
            //클라이언트 _> 채팅 기능
            //richTextBox1.AppendText(msg + "\r\n");
            richTextBox1.Select(richTextBox1.Text.Length, 0);
            richTextBox1.ScrollToCaret();
        }
        void Receiver() //서버한테 메세지 받는것
        {
            try
            {
                while (true)
                {
                    int bufferSize = client.ReceiveBufferSize;
                    byte[] buffer = new byte[bufferSize];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string msg = Encoding.Default.GetString(buffer, 0, bytes);
                    if (!isControlMsg(msg)) Display(msg);
                    if (msg.StartsWith("**REMOTEEXIT**"))
                    {
                        SetupClient.Close();
                        timer_send_img.Stop();
                        richTextBox1.AppendText("서버 종료\n");
                        this.Refresh();
                        check = false;
                    }
                    else if (msg.StartsWith("[공지사항]"))
                    {
                        Console.WriteLine(msg); 
                        richTextBox1.AppendText(msg+"\n");
                    }
                }
            }
            catch (IOException e)
            {
                //
            }
        }
        bool isControlMsg(string msg)
        {
            bool isCtrl = true;
            if (msg.StartsWith("**userlist** "))
            {
                listBox1.Items.Clear();
                char split = ' ';
                string[] userlist = msg.Split(split);
                foreach (string s in userlist)
                {
                    if (s.StartsWith("**")) continue;
                    listBox1.Items.Add(s);
                }
            }
            else isCtrl = false;
            return isCtrl;
        }
        void Sender(string s)
        {   
            byte[] buffer = Encoding.Default.GetBytes(s);
            try 
            {
                stream.Write(buffer, 0, buffer.Length);
                Thread.Sleep(10);
                stream.Flush();
            }
            catch (IOException e)
            {
                ServerConnectEND();
                return;
            }
            
        }
        void Controller(string s)
        {
            if (s.StartsWith("/connect "))
            {
                Connect(s);
            }
            else if (!client.Connected)
            {
                MessageBox.Show("서버에 먼저 연결해주세요.");              
            }
            else if (s.Equals("/exit"))
            {
                Disconnect();
            }
            else
            {
                Sender(s);
            }
        }
        void Disconnect() //서버가 켜져있을때 클라이언트가 나가는 함수 (나 나갔다고 알려줌 서버한테)
        {
            byte[] buffer = Encoding.Default.GetBytes("/exit");
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();
            client.Close();
            Application.ExitThread();
            this.Close();
        }
        void ServerConnectEND()
        {
            stream.Close();
            client.Close();
            Application.ExitThread();
            this.Close();
            MessageBox.Show("서버가 종료 되었습니다.");
            Application.Exit();
        }
        private void GetAllProcessName()
        {
                    processes = Process.GetProcesses();
                    
                    int index = -1;
                    foreach (Process process in processes)
                    {
                        index++;
                        if (process.MainWindowHandle == IntPtr.Zero)
                        {
                            continue;
                        }

                        ProcessList += (process.ProcessName + " \n" );
                    }
                    Sender(myIP + ProcessList);
                    ProcessList = "";              
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) button1_Click(sender, e);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //클라이언트 -> 서버 채팅기능 안쓰면 지우기
            //string input = metroTextBox1.Text;
            //Controller(input);
            //metroTextBox1.Text = string.Empty;
            //metroTextBox1.Focus();
        }

        private void Class1_Load(object sender, EventArgs e)
        {
            SO();
            timer1.Start();
            rcf = new RemoteClientForm();
            Remote.Singleton.RecvedRCInfo += Singleton_RecvedRCInfo;
        }
        public void SO()
        {
            Controller(SIP1 + SIP2 + SIP3 + SIP4 + SIP5);
        }

        delegate void Remote_Dele(object sender, RecvRCInfoEventArgs e);
        private void Singleton_RecvedRCInfo(object sender, RecvRCInfoEventArgs e)
        {
            if (this.InvokeRequired)
            {
                object[] objs = new object[2] { sender, e };
                this.Invoke(new Remote_Dele(Singleton_RecvedRCInfo), objs);
            }
            else
            {
                //tbox_controller_ip.Text = e.IPAddressStr;
                sip = e.IPAddressStr;
                check = true;
                if (check == true) {
                    Remote.Singleton.RecvEventStart();
                    timer_send_img.Start();
                }
            }
        }

        private void timer_send_img_Tick(object sender, EventArgs e)
        {

            Rectangle rect = Remote.Singleton.Rect;
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            Size size2 = new Size(rect.Width, rect.Height);
            graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), size2);
            graphics.Dispose();

            try
            {
                ImageClient ic = new ImageClient();
                ic.Connect(sip, NetworkInfo.ImgPort);
                ic.SendImageAsync(bitmap, null);
            }
            catch
            {
                timer_send_img.Enabled = false;
                MessageBox.Show("서버에 문제가 있는 것 같아요");
                this.Close();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            String TESTID = Class2.DBStudentnumber;
            float time = Class2.stopwatch.ElapsedMilliseconds / 1000.0f;
            Class2.stopwatch.Stop();

            MySqlConnection connection = new MySqlConnection("Server = localhost;Database=dbtest2;Uid=root;Pwd=0000;");
            connection.Open();

            string insertQuery = "UPDATE account_info " +
                                 "SET Time = '" + time + "'" +
                                 "WHERE id = '" + TESTID + "'";
            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show(Class2.DBID + "님 이용 시간은 " + time + "초 입니다.");
                connection.Close();
                Disconnect();
                Application.Exit();
            }
            else
            {
                MessageBox.Show("시간 측정 불가, 이상이 없는지 확인바랍니다.");
                connection.Close();
                Disconnect();
                Application.Exit();
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetAllProcessName();
        }


        private void Class1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
            Application.Exit();
        }
    }
}
