using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVRDemo01.FormDemo
{
    public partial class ReplayDemo : Form
    {
        NVRContext m_NVRContext = null;
        public ReplayDemo()
        {
            InitializeComponent();
            m_NVRContext = new NVRContext();
            txtIP.Text = "192.168.2.231";
            txtPort.Text = "8000";
            txtUser.Text = "admin";
            txtPwd.Text = "cdmr123456*";
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            m_NVRContext.Replay(picPlayWin.Handle, dtpStart.Value, dtpEnd.Value);

          
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            m_NVRContext.LoginNVR(txtIP.Text, txtPort.Text, txtUser.Text,txtPwd.Text);
        }
    }
}
