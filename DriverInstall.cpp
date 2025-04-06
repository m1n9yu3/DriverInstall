// DriverInstall.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include<Windows.h>

#define DriverName "MDriver.sys"

#define READDRIVERNAME

int main()
{
	//// 通过文件指定打开驱动名称
	// 

#ifdef  READDRIVERNAME
	FILE* DriverNameFile = NULL;
	char* szBuff = NULL;

	//= fopen_s("1.txt", "r");
	fopen_s(&DriverNameFile, "1.txt", "r");

	fseek(DriverNameFile, 0, SEEK_END);
	int fLength = ftell(DriverNameFile);
	szBuff = (char*)malloc(fLength);

	fseek(DriverNameFile, 0, SEEK_SET);
	fread(szBuff, 1, fLength, DriverNameFile);
	fclose(DriverNameFile);

#endif //  READDRIVERNAME



	//char szDriverPath[255] = { 0 };
	char* szDriverPath = NULL;
	SERVICE_STATUS ServiceStatus = { NULL };
	
	// 初始化 szDriverPath,将其指向当前目录的驱动文件。
	szDriverPath = (char*)malloc(255);
	memset(szDriverPath, 0, 255);

	DWORD nResult = GetCurrentDirectoryA(200, szDriverPath);
	if (nResult == 0 || nResult != 200)
	{
		nResult += 30;
		realloc(szDriverPath, nResult+1);
		memset(szDriverPath, 0, nResult+1);
		DWORD nResult1 = GetCurrentDirectoryA(nResult, szDriverPath);

		if (nResult1 == 0)
		{
			printf("GetCurrentDirectoryA Error!\r\n");
			//free(szDriverPath);
			//return 0;
		}
	}
	
	strcat_s(szDriverPath, 255, "\\");
	

#ifdef READDRIVERNAME
	strcat_s(szDriverPath, 255, szBuff);
#else
	strcat_s(szDriverPath, 255, DriverName);
#endif // READDRIVERNAME

	// 打开 SC管理
	SC_HANDLE hScm = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);
	if (hScm == NULL)
	{
		printf("SCM open faild : GetLastError():%d\r\n", GetLastError());
		goto end;
	}

	// 创建一个服务
	SC_HANDLE hService = CreateServiceA(hScm, "MDriver", "MDriver111", SERVICE_ALL_ACCESS, SERVICE_KERNEL_DRIVER, SERVICE_DEMAND_START, \
		SERVICE_ERROR_NORMAL, DriverName, NULL, NULL, NULL, NULL, NULL);
	if (hService == NULL)
	{
		printf("the services create faild : GetLastError():%d\r\n", GetLastError());
		
		goto end1;
	}

	// 启动服务
	if (0 == StartServiceA(hService, 0, NULL))
	{
		printf("the service start faild: GetLastError():%d\r\n", GetLastError());
		goto end2;
	}

	// 相关操控代码
	printf("the driver has been started！！！\r\n");


	// 停止服务
	if (0 == ControlService(hService, SERVICE_CONTROL_STOP, &ServiceStatus))
	{
		printf("the driver not start！！！\r\n");
	}

	// 尾部
	// 删除对应服务
end2:
	DeleteService(hService);
	CloseServiceHandle(hService);
end1:
	CloseServiceHandle(hScm);
end:
	printf("DriverPath:%s\n", szDriverPath);
	free(szDriverPath);

#ifdef READDRIVERNAME
	free(szBuff);

#endif // READDRIVERNAME


	// 暂停屏幕
	getchar();
}


