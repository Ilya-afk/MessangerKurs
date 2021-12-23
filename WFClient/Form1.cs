using MessangerKurs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFClient
{
    public partial class Form1 : Form
    {
        private static int MessageID = 0;
        private static string UserName;
        private static MessangerClient API = new MessangerClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UserName = textBox2.Text;
            string Message = textBox1.Text;
            if ((UserName.Length > 1) && (UserName.Length > 1))
            {
                MessangerKurs.Message msg = new MessangerKurs.Message(UserName, Message, DateTime.Now);
                API.SendMessageRestSharp(msg);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var getMessage = new Func<Task>(async () =>
            {
                MessangerKurs.Message msg = await API.GetMessageHTTPAsync(MessageID);
                while (msg != null)
                {
                    listBox1.Items.Add(msg);
                    MessageID++;
                    msg = await API.GetMessageHTTPAsync(MessageID);
                }
            });
            getMessage.Invoke();
        }

    }
}
