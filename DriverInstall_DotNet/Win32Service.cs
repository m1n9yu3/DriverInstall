using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DriverInstall_DotNet
{
    internal class Win32Service
    {
        private IntPtr scmHandle = IntPtr.Zero;
        /*private IntPtr curServiceHandle = IntPtr.Zero;*/
        public string errorMess = "";
        private string driverPath = "";

        public Win32Service() { }
        public Win32Service(string szDriverPath) {
            driverPath = szDriverPath;
        }
        private bool GetScm()
        {
            // has been get the scm handle 
            if(scmHandle != IntPtr.Zero)
            {
                MessageBox.Show("111");
                return true;
            }

            scmHandle = Win32ServiceApi.OpenSCManager(
                null,                   // 本地计算机
                null,                   // 默认数据库 (SERVICES_ACTIVE_DATABASE)
                Win32ServiceApi.SC_MANAGER_ALL_ACCESS
            );

            if (scmHandle == IntPtr.Zero)
            {
                return false;
            }
            return true;
        }

        public bool InstallService() { if (driverPath == "") { errorMess = "not set driverPath"; return false; } return InstallService(driverPath); }

        public bool InstallService(string cur_driverPath)
        {
            if (!GetScm()) { errorMess="not open scm"; return false; }
            char split = '\\';
            string[] cur_list = cur_driverPath.Split(split);
            string serviceName = cur_driverPath.Split(split)[cur_list.Length - 1];
            split = '.';
            cur_list = cur_driverPath.Split(split);
            string displayName = cur_list[0];
            string binPath = cur_driverPath; // 服务程序路径

            /*            errorMess = serviceName;
                        return false;*/
            IntPtr curServiceHandle = Win32ServiceApi.CreateServiceW(
                scmHandle,
                serviceName,
                displayName,
                Win32ServiceApi.SERVICE_ALL_ACCESS,
                Win32ServiceApi.SERVICE_KERNEL_DRIVER,
                Win32ServiceApi.SERVICE_DEMAND_START,
                Win32ServiceApi.SERVICE_ERROR_NORMAL,
                binPath,
                null,                   // 加载组 (无)
                IntPtr.Zero,            // 标签 ID (无)
                null,                   // 依赖服务 (无)
                null,                   // 账户 (LocalSystem)
                null                    // 密码 (无)
            );

            if (curServiceHandle == IntPtr.Zero) { errorMess = "create service faild"; return false; }

            // close 
            if (curServiceHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(curServiceHandle);
                curServiceHandle = IntPtr.Zero;
            }
            /*            if (scmHandle != IntPtr.Zero)
                            Win32ServiceApi.CloseServiceHandle(scmHandle);*/
            if (scmHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(scmHandle);
                scmHandle = IntPtr.Zero;
            }
            return true;
        }

        public bool StartService() { if (driverPath == "") { errorMess = "not set driverPath"; return false; } return StartService(driverPath); }

        public bool StartService(string cur_driverPath)
        {
            if (!GetScm()) { errorMess = "not open scm"; return false; }
            char split = '\\';
            string[] cur_list = cur_driverPath.Split(split);
            string serviceName = cur_driverPath.Split(split)[cur_list.Length - 1];
            split = '.';
            cur_list = cur_driverPath.Split(split);
            string displayName = cur_list[0];
            string binPath = cur_driverPath; // 服务程序路径

            // OpenSerivce
            IntPtr curServiceHandle = Win32ServiceApi.OpenServiceW(scmHandle, serviceName, Win32ServiceApi.SERVICE_ALL_ACCESS);
            if (curServiceHandle == IntPtr.Zero)
            { errorMess = "create service faild"; return false; }

            if (curServiceHandle == IntPtr.Zero) { errorMess = "create service faild, maybe has been exits";  }

            bool started = Win32ServiceApi.StartServiceA(
                curServiceHandle,
                0,                      // 无参数
                null                    // 参数数组 (无)
            );


            // close 
            if (curServiceHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(curServiceHandle);
                curServiceHandle = IntPtr.Zero;
            }
            /*            if (scmHandle != IntPtr.Zero)
                            Win32ServiceApi.CloseServiceHandle(scmHandle);*/
            if (scmHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(scmHandle);
                scmHandle = IntPtr.Zero;
            }


            if (!started)
            {
                errorMess = "start service faild"; return false;
            }

            return true;
        }

        public bool StopService() { if (driverPath == "") { errorMess = "not set driverPath"; return false; }  return StopService(driverPath); }

        public bool StopService(string cur_driverPath)
        {
            if (!GetScm()) { errorMess = "not open scm"; return false; }
            char split = '\\';
            string[] cur_list = cur_driverPath.Split(split);
            string serviceName = cur_driverPath.Split(split)[cur_list.Length - 1];
            split = '.';
            cur_list = cur_driverPath.Split(split);
            string displayName = cur_list[0];
            string binPath = cur_driverPath; // 服务程序路径

            // OpenSerivce
            IntPtr curServiceHandle = Win32ServiceApi.OpenServiceW(scmHandle, serviceName, Win32ServiceApi.SERVICE_ALL_ACCESS);
            if(curServiceHandle == IntPtr.Zero)
            { errorMess = "create service faild"; return false; }

            // Stop Service
            Win32ServiceApi.SERVICE_STATUS status = new Win32ServiceApi.SERVICE_STATUS();
            bool controlled = Win32ServiceApi.ControlService(
                curServiceHandle,
                Win32ServiceApi.SERVICE_CONTROL_STOP,
                ref status
            );


            // close 
            if (curServiceHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(curServiceHandle);
                curServiceHandle = IntPtr.Zero;
            }
            /*            if (scmHandle != IntPtr.Zero)
                            Win32ServiceApi.CloseServiceHandle(scmHandle);*/
            if (scmHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(scmHandle);
                scmHandle = IntPtr.Zero;
            }

            if (!controlled) { errorMess = "not stop service"; return false; }
            return true;
        }

        public bool DeleteService() { if (driverPath == "") { errorMess = "not set driverPath"; return false; }  return DeleteService(driverPath); }

        public bool DeleteService(string cur_driverPath)
        {
            /*StopService(cur_driverPath);*/
            if (!GetScm()) { errorMess = "not open scm"; return false; }
            char split = '\\';
            string[] cur_list = cur_driverPath.Split(split);
            string serviceName = cur_driverPath.Split(split)[cur_list.Length - 1];
            split = '.';
            cur_list = cur_driverPath.Split(split);
            string displayName = cur_list[0];
            string binPath = cur_driverPath; // 服务程序路径

            // OpenSerivce
            IntPtr curServiceHandle = Win32ServiceApi.OpenServiceW(scmHandle, serviceName, Win32ServiceApi.SERVICE_ALL_ACCESS);
            if (curServiceHandle == IntPtr.Zero){ errorMess = "create service faild"; return false; }

            bool deleted = Win32ServiceApi.DeleteService(curServiceHandle);
            if (!deleted) { errorMess = "delete service faild"; return false; }

            // close 
            if (curServiceHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(curServiceHandle);
                curServiceHandle = IntPtr.Zero;
            }
            /*            if (scmHandle != IntPtr.Zero)
                            Win32ServiceApi.CloseServiceHandle(scmHandle);*/
            if (scmHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(scmHandle);
                scmHandle = IntPtr.Zero;
            }




            return true;
        }

        ~Win32Service()
        {

            // 因为只有在程序退出时释放！，所以不走析构函数
/*            // close 
            if (curServiceHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(curServiceHandle);
                curServiceHandle = IntPtr.Zero;
            }
            *//*            if (scmHandle != IntPtr.Zero)
                            Win32ServiceApi.CloseServiceHandle(scmHandle);*//*
            if (scmHandle != IntPtr.Zero)
            {
                Win32ServiceApi.CloseServiceHandle(scmHandle);
                scmHandle = IntPtr.Zero;
            }*/

        }
    }
}
