using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace DriverInstall_DotNet
{
    public partial class Form1 : Form
    {
        IntPtr scmHandle = IntPtr.Zero;
        IntPtr serviceHandle = IntPtr.Zero;

        // 声明 API 函数
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ChangeWindowMessageFilter(uint msg, uint dwFlag);

        // 定义消息和标志
        private const uint WM_DROPFILES = 0x0233;
        private const uint WM_COPYDATA = 0x004A;
        private const uint MSGFLT_ADD = 0x1;

        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += MainForm_DragEnter; // 绑定拖拽进入事件
            this.DragDrop += MainForm_DragDrop;    // 绑定拖拽释放事件

            // 启用窗口消息过滤（必须在窗体加载前调用）
            ChangeWindowMessageFilter(WM_DROPFILES, MSGFLT_ADD);
            ChangeWindowMessageFilter(WM_COPYDATA, MSGFLT_ADD);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                DriverFileName.Text = files[0];
            }
        }


        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // 检查拖入的数据是否包含文件
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // 显示复制图标
            }
            else
            {
                e.Effect = DragDropEffects.None; // 禁止拖拽
            }
        }


        private void put_Meesage(string mess)
        {
            string a = LogDisplay.Text;
            a = a + "\r\n";
            a += mess;
            LogDisplay.Text = a;

        }


        private void put_errMeesage(string mess)
        {
            mess += ":"+Win32ApiDemo.GetLastError().ToString();
            put_Meesage(mess);

        }

        private void Install_Click(object sender, EventArgs e)
        {
            string driverPath = DriverFileName.Text;
            if(driverPath == "")
            {
                put_Meesage("[Install_Click] plz input a driver file path!");
                return;
            }
            
            Win32Service service = new Win32Service(driverPath);
           ;
            if (!service.InstallService())
            {
                string mess = service.errorMess;
                put_errMeesage(mess);
                return;
            }
            put_Meesage("create service sucess");
        }


        private void Remove_Click(object sender, EventArgs e)
        {
            string driverPath = DriverFileName.Text;
            if (driverPath == "")
            {
                put_Meesage("plz input a driver file path!");
                return;
            }

            Win32Service service = new Win32Service(driverPath);
            ;
            if (!service.DeleteService())
            {
                string mess = service.errorMess;
                put_errMeesage(mess);
                return;
            }
            put_Meesage("delete service sucess");
        }



        private void Stop_Click(object sender, EventArgs e)
        {
            string driverPath = DriverFileName.Text;
            if (driverPath == "")
            {
                put_Meesage("plz input a driver file path!");
                return;
            }

            Win32Service service = new Win32Service(driverPath);
            ;
            if (!service.StopService())
            {
                string mess = service.errorMess;
                put_errMeesage(mess);
                return;
            }
            put_Meesage("service has been stop");
        }


        private void Start_Click(object sender, EventArgs e)
        {
            string driverPath = DriverFileName.Text;
            if (driverPath == "")
            {
                put_Meesage("plz input a driver file path!");
                return;
            }

            Win32Service service = new Win32Service(driverPath);
            ;
            if (!service.StartService())
            {
                string mess = service.errorMess;
                put_errMeesage(mess);
                return;
            }

            put_Meesage("start service sucess");
        }


        private void OpenDriverFIle_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                DriverFileName.Text = filePath;

                //Read the contents of the file into a stream
                /*                var fileStream = openFileDialog.OpenFile();

                                using (StreamReader reader = new StreamReader(fileStream))
                                {
                                    fileContent = reader.ReadToEnd();
                                }*/
            }


            /*            Win32ApiDemo.MessageBox(
                            IntPtr.Zero,
                            "Hello from Win32 API!",
                            "提示",
                            0x00000040 // MB_ICONINFORMATION
                        );*/



        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            LogDisplay.Text = string.Empty;
        }
    }
}
