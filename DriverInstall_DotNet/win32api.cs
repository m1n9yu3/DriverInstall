using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DriverInstall_DotNet
{
    internal class win32api
    {

    }
}



public class Win32ApiDemo
{
    // 示例：声明 MessageBox 函数（来自 user32.dll）
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(
        IntPtr hWnd,
        string text,
        string caption,
        uint type
    );

    /*
        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        public static extern uint CreateServiceW(
            IntPtr hSCManager,
            string lpServiceName,
            string lpDisplayName,
            uint dwDesiredAccess,
            uint dwServiceType,
            uint dwStartType,
            uint dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            IntPtr lpdwTagId,
            string lpDependencies,
            string lpServiceStartName,
            string lpPassword
        );

        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        public static extern uint DeleteService(
            IntPtr hSCManager
        );


        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        public static extern uint ControlService(
            IntPtr hSCManager,
            uint dwControl,
            IntPtr lpServiceStatus
        );
    */

    [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetLastError();


}



public class Win32ServiceApi
{
    // 服务控制管理器访问权限
    public const uint SC_MANAGER_ALL_ACCESS = 0xF003F;

    // 服务访问权限
    public const uint SERVICE_ALL_ACCESS = 0xF01FF;

    // 服务类型
    public const uint SERVICE_WIN32_OWN_PROCESS = 0x00000010;
    public const uint SERVICE_KERNEL_DRIVER = 0x00000001;

    // 服务启动类型
    public const uint SERVICE_DEMAND_START = 0x00000003;

    // 服务错误控制类型
    public const uint SERVICE_ERROR_NORMAL = 0x00000001;

    // 服务控制码
    public const uint SERVICE_CONTROL_STOP = 0x00000001;

    // 服务状态
    [StructLayout(LayoutKind.Sequential)]
    public struct SERVICE_STATUS
    {
        public uint dwServiceType;
        public uint dwCurrentState;
        public uint dwControlsAccepted;
        public uint dwWin32ExitCode;
        public uint dwServiceSpecificExitCode;
        public uint dwCheckPoint;
        public uint dwWaitHint;
    }

    // 打开服务控制管理器
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr OpenSCManager(
        string lpMachineName,
        string lpDatabaseName,
        uint dwDesiredAccess
    );

    // 创建服务 (Unicode 版本)
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr CreateServiceW(
        IntPtr hSCManager,
        string lpServiceName,
        string lpDisplayName,
        uint dwDesiredAccess,
        uint dwServiceType,
        uint dwStartType,
        uint dwErrorControl,
        string lpBinaryPathName,
        string lpLoadOrderGroup,
        IntPtr lpdwTagId,
        string lpDependencies,
        string lpServiceStartName,
        string lpPassword
    );

    // 启动服务 (ANSI 版本)
    [DllImport("advapi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool StartServiceA(
        IntPtr hService,
        uint dwNumServiceArgs,
        string[] lpServiceArgVectors
    );

    // 控制服务
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr OpenServiceW(
        IntPtr hSCManager,
        string lpServiceName,
        uint dwDesiredAccess
    );


    // 控制服务
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool ControlService(
        IntPtr hService,
        uint dwControl,
        ref SERVICE_STATUS lpServiceStatus
    );

    // 删除服务
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool DeleteService(IntPtr hService);

    // 关闭服务句柄
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CloseServiceHandle(IntPtr hSCObject);
}