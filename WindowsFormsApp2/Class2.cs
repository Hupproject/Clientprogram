using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Class2 : MetroFramework.Forms.MetroForm
    {
        public static Stopwatch stopwatch = new Stopwatch();

        float time = 0;
        public static string DBID;
        public static string DBStudentnumber;

        string ID;
        string Studentnumber;
        public Class2()
        {
            InitializeComponent();
            this.metroTextBox1.KeyDown += this.textBox2_KeyDown;
            this.metroTextBox2.KeyDown += this.textBox1_KeyDown;
        }

        private void Class2_Load(object sender, EventArgs e)
        {
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) button1_Click(sender, e);
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) metroTextBox2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server = localhost;Database=dbtest2;Uid=root;Pwd=0000;");
                connection.Open();

                int login_status = 0;

                ID = metroTextBox1.Text;
                Studentnumber = metroTextBox2.Text;
                DBID = metroTextBox1.Text;
                DBStudentnumber = metroTextBox2.Text;

                string loginid = metroTextBox1.Text;
                string loginpwd = metroTextBox2.Text;

                string selectQuery = "SELECT * FROM account_info WHERE name = \'" + loginid + "\' ";

                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);

                MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                stopwatch.Start();

                if (metroTextBox1.Text.Equals(string.Empty) || metroTextBox2.Text.Equals(string.Empty))
                {
                    MessageBox.Show("값이 없습니다.", "Error");
                }
                else
                {
                    while (userAccount.Read())
                    {
                        if (loginid == (string)userAccount["name"] && loginpwd == (string)userAccount["id"])
                        {
                            login_status = 1;
                        }
                    }
                    connection.Close();

                    if (login_status == 0)
                    {
                        connection.Open();
                        string insertQuery = "INSERT INTO account_info (name, id, Time) VALUES ('" + ID + "', '" + Studentnumber + "', '" + time + "');";
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);

                        if (command.ExecuteNonQuery() == 1)
                        {
                            DialogResult dr = MessageBox.Show("이름 : " + ID + "\n학번 : " + Studentnumber, "회원 정보", MessageBoxButtons.OKCancel);
                            if (dr == DialogResult.OK)
                            {
                                Class1 clientform = new Class1(ID, Studentnumber);
                                this.Hide();
                                clientform.StartPosition = FormStartPosition.Manual;
                                clientform.Location = new Point(50, 200);
                                clientform.ShowDialog();
                                connection.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("비정상 입력 정보, 재확인 요망");
                        }
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("이름 : " + ID + "\n학번 : " + Studentnumber, "회원 정보", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            Class1 clientform = new Class1(ID, Studentnumber);
                            this.Hide();
                            clientform.StartPosition = FormStartPosition.Manual;
                            clientform.Location = new Point(50, 200);
                            clientform.ShowDialog();
                            connection.Open();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        { 
        }

        private void IPmenu_Click(object sender, EventArgs e)
        {
            IPSet ipSetForm = new IPSet();
            ipSetForm.Show();
        }

        
    }
}
