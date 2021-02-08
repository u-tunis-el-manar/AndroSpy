using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SV
{
    public partial class livescreen : MetroFramework.Forms.MetroForm
    {
        Socket sock;
        public string ID = "";
        public Form1.infoAl infoAl;
        public livescreen(Socket sck, string id)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 4;
            sock = sck; ID = id;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                try
                {
                    byte[] senddata = Form1.MyDataPacker("SCREENLIVEOPEN", System.Text.Encoding.UTF8.GetBytes(comboBox1.SelectedItem.ToString()));
                    sock.BeginSend(senddata, 0, senddata.Length, SocketFlags.None, null, null);
                }
                catch (Exception) { }
                button1.Enabled = false;
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                try
                {
                    if(infoAl != null) { infoAl.CloseSocks(); }
                    byte[] senddata = Form1.MyDataPacker("SCREENLIVECLOSE", System.Text.Encoding.UTF8.GetBytes("ECHO"));
                    sock.BeginSend(senddata, 0, senddata.Length, SocketFlags.None, null, null);
                }
                catch (Exception) { }
                button2.Enabled = false;
                button1.Enabled = true;
            }
        }

        private void livescreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            button2.PerformClick();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                try
                {
                    byte[] senddata = Form1.MyDataPacker("SCREENQUALITY", Encoding.UTF8.GetBytes(comboBox1.SelectedItem.ToString()));
                    sock.BeginSend(senddata, 0, senddata.Length, SocketFlags.None, null, null);
                }
                catch (Exception) { }
            }
        }
    }
}
