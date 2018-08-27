using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NVRDemo01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CHCNetSDK.NET_DVR_Init();
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            FormDemo.ReplayDemo replayDemo = new FormDemo.ReplayDemo();
            replayDemo.ShowDialog(this);
        }
    }
}
