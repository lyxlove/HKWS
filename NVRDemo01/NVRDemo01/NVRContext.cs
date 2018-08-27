using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVRDemo01
{
    public class NVRContext : CHCNetSDK
    {

        public Int32 m_lUserID = -1;
        public NET_DVR_DEVICEINFO_V30 DeviceInfo;
        public NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
        private Int32 m_lPlayHandle = -1;
        private uint iLastErr = 0;
        private uint dwAChanTotalNum = 0;
        private uint dwDChanTotalNum = 0;

        public void Init()
        {

            NET_DVR_Init();
        }

        public void LoginNVR(string strIP,string strPort,string strUserName,string strPwd)
        {
            Int16 iPort = Int16.Parse(strPort);
            m_lUserID = NET_DVR_Login_V30(strIP, iPort, strUserName, strPwd, ref DeviceInfo);
            if (m_lUserID < 0)
            {
                iLastErr = NET_DVR_GetLastError();
                MessageBox.Show($"登录失败,{iLastErr}");
                return;
            }
            else
            {
                MessageBox.Show("登录成功");
                dwAChanTotalNum = (uint)DeviceInfo.byChanNum;
                dwDChanTotalNum = (uint)DeviceInfo.byIPChanNum + 256 * (uint)DeviceInfo.byHighDChanNum;
                if (dwAChanTotalNum > 0)
                {

                }
                else
                {

                }

            }
        }


        public void IPInfoChannel()
        {
            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);
        }

        public void Replay(System.IntPtr hWnd, DateTime startTime, DateTime endTime)
        {
            try
            {
                NET_DVR_VOD_PARA nET_DVR_VOD_PARA = new NET_DVR_VOD_PARA();
                nET_DVR_VOD_PARA.dwSize = (uint)Marshal.SizeOf(nET_DVR_VOD_PARA);
                nET_DVR_VOD_PARA.struIDInfo.dwChannel = 33;
                nET_DVR_VOD_PARA.hWnd = hWnd;

                nET_DVR_VOD_PARA.struBeginTime.dwYear = (uint)startTime.Year;
                nET_DVR_VOD_PARA.struBeginTime.dwMonth = (uint)startTime.Month;
                nET_DVR_VOD_PARA.struBeginTime.dwDay = (uint)startTime.Day;
                nET_DVR_VOD_PARA.struBeginTime.dwHour = (uint)startTime.Hour;
                nET_DVR_VOD_PARA.struBeginTime.dwMinute = (uint)startTime.Minute;
                nET_DVR_VOD_PARA.struBeginTime.dwSecond = (uint)startTime.Second;

                nET_DVR_VOD_PARA.struEndTime.dwYear = (uint)endTime.Year;
                nET_DVR_VOD_PARA.struEndTime.dwMonth = (uint)endTime.Month;
                nET_DVR_VOD_PARA.struEndTime.dwDay = (uint)endTime.Day;
                nET_DVR_VOD_PARA.struEndTime.dwHour = (uint)endTime.Hour;
                nET_DVR_VOD_PARA.struEndTime.dwMinute = (uint)endTime.Minute;
                nET_DVR_VOD_PARA.struEndTime.dwSecond = (uint)endTime.Second;

                m_lPlayHandle = NET_DVR_PlayBackByTime_V40(m_lUserID, ref nET_DVR_VOD_PARA);

                uint iOutValue = 0;
                NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
