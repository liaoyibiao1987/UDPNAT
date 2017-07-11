﻿using System;
using System.Windows.Forms;
using UDPCOMMON;

namespace UDPNAT
{
    public partial class frmServer : Form
    {
        private Server _server;

        public frmServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _server = new Server();
            _server.OnWriteLog += server_OnWriteLog;
            _server.OnUserChanged += OnUserChanged;
            try
            {
                _server.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //刷新用户列表 
        private void OnUserChanged(UserCollection users)
        {
            listBox2.DisplayMember = "FullName";
            listBox2.DataSource = null;
            listBox2.DataSource = users;
        }

        //显示跟踪消息 
        public void server_OnWriteLog(string msg)
        {
            listBox1.Items.Add(msg);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_server != null)
                _server.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //发送消息给所有在线用户 
            P2P_TalkMessage msg = new P2P_TalkMessage(textBox1.Text);
            foreach (object o in listBox2.Items)
            {
                User user = o as User;
                if (user != null) _server.SendMessage(msg, user.NetPoint);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void btnStartHolding_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 2)
            {
                User u1 = listBox2.Items[0] as User;
                User u2 = listBox2.Items[1] as User;


                P2P_HoldingsMessage msg1 = new P2P_HoldingsMessage(u2.GetIPPort());
                P2P_HoldingsMessage msg2 = new P2P_HoldingsMessage(u1.GetIPPort());

                if (u1 != null) _server.SendMessage(msg1, u1.NetPoint);
                if (u2 != null) _server.SendMessage(msg2, u2.NetPoint);
            }
            else
            {
                MessageBox.Show("必须为两个用户才能相互打通.");
            }

        }
    }
}
