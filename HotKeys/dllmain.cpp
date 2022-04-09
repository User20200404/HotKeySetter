// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include <iostream>
#include <Windows.h>
#include <shellapi.h>

//枚举执行状态常数
#define ENUM_BREAK  "";
#define ENUM_SUCCESS 0;
#define ENUM_DLL_LOAD_FAILED 1;
#define ENUM_FUNC_LOAD_FAILED 2;

//错误反馈请求常数
#define ERR_REPORT_SHOW_MSGBOX 1; //HotKeySetter应显示显式消息框，指示错误内容和所在dll。
#define ERR_REPORT_WRITE_LOG 2; //HotKeySetter应写出错误日志。
#define ERR_REPORT_DO_NONE 0; //不作任何操作。
#define ERR_REPORT_THORW_EXCEPTION 3; //HotKeySetter应抛出异常，指示了错误信息和所在dll。
#define ERR_REPORT_FATAL 4; //HotKeySetter出现致命错误，应立即退出。(避免使用本标志) 

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}



extern "C"  __declspec(dllexport) BOOL SetHotKey(HWND hwnd, int id, UINT fsModifiers, UINT vkcode)
{
	return RegisterHotKey(hwnd, id, fsModifiers, vkcode);
}
extern "C"  __declspec(dllexport) BOOL UnSetHotKey(HWND hwnd,int id)
{
	return UnregisterHotKey(hwnd, id);
}
extern"C" __declspec(dllexport) UINT GetfsModifiersFromString(char* string)
{
	if (_strcmpi(string, "MOD_ALT"))
		return MOD_ALT;
	if (_strcmpi(string, "MOD_CONTROL"))
		return MOD_CONTROL;
	if (_strcmpi(string, "MOD_NOREPEAT"))
		return MOD_NOREPEAT;
	if (_strcmpi(string, "MOD_SHIFT"))
		return MOD_SHIFT;
	if (_strcmpi(string, "MOD_WIN"))
		return MOD_WIN;
	return 0;
}
extern "C" __declspec(dllexport) BOOL SetHotKey_String(HWND hwnd, int id, char* fsModifiers_string, UINT vkcode)
{
	return RegisterHotKey(hwnd, id, GetfsModifiersFromString(fsModifiers_string), vkcode);
}

extern "C" __declspec(dllexport) int HotKeyEventMainDelegate(char* dllName,char* entryPoint,int index,char* param)
{
	typedef int (__cdecl *EventMain)(int index,char* param);
	HMODULE hDLL = LoadLibraryA(dllName);
	if (hDLL != NULL)
	{
		HANDLE hProc = GetProcAddress(hDLL, entryPoint);
		if (hProc != NULL)
		{
			return ((EventMain)hProc)(index ,param);
		}
		FreeLibrary(hDLL);
	}
	return 0;
}

extern "C" __declspec(dllexport) int HotKeyEventErrorDelegate(char* dllName, char* entryPoint, int index, int errCode, OUT char* errMsg)
{
	typedef int(__cdecl* GetErrorMsg)(int index, int errCode, OUT char* errMsg);
	HMODULE hDLL = LoadLibraryA(dllName);
	int ret = 0;
	if (hDLL != NULL)
	{
		HANDLE hProc = GetProcAddress(hDLL, entryPoint);
		if (hProc != NULL)
		{
			ret = ((GetErrorMsg)hProc)( index, errCode, errMsg);
		}
		FreeLibrary(hDLL);
	}
	return ret;
}

extern "C" __declspec(dllexport) void HotKeyGetDescriptionDelegate(char* dllName, char* entryPoint,int index, OUT char* title, OUT char* detail)
{
	typedef void(__cdecl* GetDes)(int index,OUT char* title, OUT char* detail);
	HMODULE hDLL = LoadLibraryA(dllName);
	if (hDLL != NULL)
	{
		HANDLE hProc = GetProcAddress(hDLL, entryPoint);
		if (hProc != NULL)
		{
			((GetDes)hProc)(index,title,detail);
		}
		FreeLibrary(hDLL);
	}
}

extern "C" __declspec(dllexport) int HotKeyEnumEventDelegate(char* dllName, char* entryPoint, int index, OUT char* desFuncEntry, OUT char* mainFuncEntry, OUT char* errorFuncEntry)
{
	typedef void(__cdecl* EnumHotKeyEvent)(int index, OUT char* desFuncEntry, OUT char* mainFuncEntry, OUT char* errorFuncEntry);
	HMODULE hDLL = LoadLibraryA(dllName);
	if (hDLL != NULL)
	{
		HANDLE hProc = GetProcAddress(hDLL, entryPoint);
		if (hProc != NULL)
		{
			((EnumHotKeyEvent)hProc)(index, desFuncEntry, mainFuncEntry, errorFuncEntry);
			FreeLibrary(hDLL);
			return ENUM_SUCCESS;
		}
		else 
		{
			FreeLibrary(hDLL);
			return ENUM_FUNC_LOAD_FAILED;
		}

	}
	else return ENUM_DLL_LOAD_FAILED;
}

//以下是事件功能函数

/// <summary>
/// 枚举本DLL为HotKeySetter支持的事件库。
/// </summary>
/// <param name="index">事件索引，应从0开始。</param>
/// <param name="desFuncEntry">描述函数入口点。</param>
/// <param name="mainFuncEntry">执行函数入口点。</param>
/// <param name="errorFuncEntry">错误提供函数入口点。</param>
extern "C" __declspec(dllexport) void EnumHotKeyEventSupport(int index, OUT char* desFuncEntry,OUT char* mainFuncEntry,OUT char* errorFuncEntry)
{
	switch (index)
	{
	case 0:
	{
		char des[] = "GetDescription";
		char main[] = "RunExternHandler";
		char error[] = "ErrorReporter";
		memcpy_s(desFuncEntry, 256, des, 15);
		memcpy_s(mainFuncEntry, 256, main, 17);
		memcpy_s(errorFuncEntry, 256, error, 14);
		break;
	}
	case 1:
	{
		char des[] = "GetDescription";
		char main[] = "DebugMessageBox";
		char error[] = "";
		memcpy_s(desFuncEntry, 256, des, 15);
		memcpy_s(mainFuncEntry, 256, main, 16);
		memcpy_s(errorFuncEntry, 256, error, 1);
		break;
	}
	case 2:
	{
		char des[] = "GetDescription";
		char main[] = "TerminateCurrentWindowProcess";
		char error[] = "";
		memcpy_s(desFuncEntry, 256, des, 15);
		memcpy_s(mainFuncEntry, 256, main, 30);
		memcpy_s(errorFuncEntry, 256, error, 1);
		break;
	}
	case 3:
	{
		char des[] = "GetDescription";
		char main[] = "PasteEventHandler";
		char error[] = "";
		memcpy_s(desFuncEntry, 256, des, 15);
		memcpy_s(mainFuncEntry, 256, main, 18);
		memcpy_s(errorFuncEntry, 256, error, 1);
		break;
	}
	default:
		break;
	}
}



extern "C" __declspec(dllexport) int TerminateCurrentWindowProcess(int index, char* param)
{
	HWND hWnd = GetForegroundWindow();
	if (hWnd != NULL)
	{
		DWORD tid, pid;
		tid = GetWindowThreadProcessId(hWnd,&pid);
		HANDLE hProc = OpenProcess(PROCESS_TERMINATE, false, pid);
		if (hProc != NULL)
		{
			TerminateProcess(hProc, 0);
		}
	}
	return GetLastError();
}


/// <summary>
/// 获取事件描述。
/// </summary>
/// <param name="index">事件索引。</param>
/// <param name="title">事件标题。</param>
/// <param name="details">事件详细描述。</param>
extern "C" __declspec(dllexport) void GetDescription(int index, OUT char* title, OUT char* detail)
{
	switch (index)
	{
	case 0:
	{
		char tit[] = "运行外部程序";
		char des[] = "调用cmd.exe执行一个外部程序。你可以在参数开头加入\"runas:\"来使用管理员权限运行该程序。";
		memcpy_s(detail, 1024, des, 86);
		memcpy_s(title, 1024, tit, 13);
		break;
	}
	case 1:
	{
		char tit[] = "弹出消息框";
		char des[] = "弹出一个消息框来显示信息。";
		memcpy_s(detail, 1024, des, 27);
		memcpy_s(title, 1024, tit, 11);
		break;
	}
	case 2:
	{
		char tit[] = "结束当前窗口进程";
		char des[] = "获取当前窗口的进程并调用TerminateProcess来结束进程。";
		memcpy_s(detail, 1024, des, 53);
		memcpy_s(title, 1024, tit, 17);
		break;
	}
	case 3:
	{
		char tit[] = "快捷输入内容";
		char des[] = "将参数中的内容复制到剪切板并进行粘贴操作";
		memcpy_s(detail, 1024, des, 41);
		memcpy_s(title, 1024, tit, 13);
	}
	default:
		break;
	}
}

extern "C" __declspec(dllexport) int DebugMessageBox(int index, char* input_param)
{
	MessageBoxA(NULL, input_param, "消息", MB_OK+MB_ICONINFORMATION);
	return 0;
}


/// <summary>
/// 运行外部程序的执行函数。
/// </summary>
/// <param name="index">事件索引。</param>
/// <param name="input_param">参数</param>
extern "C" __declspec(dllexport) int RunExternHandler(int index,char* input_param)
{
	char* runAsStatus = new char[7];
	char* operation = new char[6];
	char* fileName = new char[1024];
	char* executeCommand = new char[1029];
	strncpy_s(runAsStatus, 7, input_param, 6);

	if (_strcmpi(runAsStatus, "runas:") == 0)
	{
		strncpy_s(operation, 6, "runas", 5);
		strncpy_s(fileName, 1024, input_param + 6, 1024);
	}
	else
	{
		strncpy_s(operation, 6, "open", 4);
		strncpy_s(fileName, 1024, input_param, 1024);
	}
	strcpy_s(executeCommand, 1029, "/c \"");
	strcat_s(executeCommand, 1029, fileName);
	strcat_s(executeCommand, 1029, "\"");
	ShellExecuteA(NULL, operation, "cmd.exe", executeCommand, NULL, SW_HIDE);

	delete[] fileName, executeCommand, runAsStatus, operation;
	return GetLastError();
}

/// <summary>
/// 错误提供函数。
/// </summary>
/// <param name="index"></param>
/// <param name="errCode"></param>
/// <returns>返回一个值，指示了HotKeySetter本体应如何处理该错误。</returns>
extern"C" __declspec(dllexport) int ErrorReporter(int index, int errCode,OUT char* outputMsg)
{
	switch (index)
	{
	case 0: //RunExternHandler的错误报告
	{
		switch (errCode)
		{
		case 0:
		{
			//没有发生错误。
			break;
		}
		default:
			//发生WIN32错误。
			char* msg = new char[255]();
			FormatMessageA(FORMAT_MESSAGE_FROM_SYSTEM, NULL, errCode, MAKELANGID(LANG_NEUTRAL, SUBLANG_CHINESE_SIMPLIFIED), msg, 255, NULL);
			memcpy_s(outputMsg, 255, msg, 255);
			delete[] msg;
			return ERR_REPORT_SHOW_MSGBOX; //应显示信息框。
		}
		break;
	}
	default:
		break;
	}
	return ERR_REPORT_DO_NONE;
}

//extern "C" __declspec()

extern "C" __declspec(dllexport) void TestEvent()
{
	keybd_event(VK_MENU, MapVirtualKey(VK_MENU, 0), 0, 0);
	keybd_event(VK_F1, MapVirtualKey(VK_MENU, 0), 0, 0);
	keybd_event(VK_F1, MapVirtualKey(VK_MENU, 0), KEYEVENTF_KEYUP, 0);
	keybd_event(VK_MENU, MapVirtualKey(VK_MENU, 0), KEYEVENTF_KEYUP, 0);
}

extern "C" __declspec(dllexport) int PasteEventHandler(int index, char* input_param)
{
	HWND hWnd = NULL;
	OpenClipboard(hWnd);
	EmptyClipboard();    //清空剪切板

	size_t sizeOfChar = strnlen_s(input_param, 65535);//获取字符串长度

	char* str = new char[65535]();
	strcpy_s(str, 65535, input_param);

	strcat_s(str, 65535, " ");
	HANDLE handle = GlobalAlloc(GMEM_FIXED, 65535);
	if (handle != 0)
	{
		strcpy_s((char*)handle, 65535, input_param);

		SetClipboardData(CF_TEXT, handle);
		//模拟CTRL + V
		keybd_event(17, MapVirtualKeyA(17, 0), 0, 0); //按下CTRL键
		keybd_event(86, MapVirtualKeyA(86, 0), 0, 0); //按下V键

		keybd_event(17, MapVirtualKeyA(17, 0), KEYEVENTF_KEYUP, 0); //抬起CTRL键
		keybd_event(86, MapVirtualKeyA(86, 0), KEYEVENTF_KEYUP, 0); //抬起V键
	}
	return GetLastError();
}

