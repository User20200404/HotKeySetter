// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "Hook.h"
#include <stdlib.h>
#include <Windows.h>
HookTest hk, hk2;

#pragma region CreateProcessWHook
BOOL  MyCreateProcessW(
    _In_opt_ LPCWSTR lpApplicationName,
    _Inout_opt_ LPWSTR lpCommandLine,
    _In_opt_ LPSECURITY_ATTRIBUTES lpProcessAttributes,
    _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
    _In_ BOOL bInheritHandles,
    _In_ DWORD dwCreationFlags,
    _In_opt_ LPVOID lpEnvironment,
    _In_opt_ LPCWSTR lpCurrentDirectory,
    _In_ LPSTARTUPINFOW lpStartupInfo,
    _Out_ LPPROCESS_INFORMATION lpProcessInformation
)
{
    return FALSE;
}

#pragma endregion
#pragma region CreateWindowExAHook

HWND MyCreateWindowExA(
    _In_ DWORD dwExStyle,
    _In_opt_ LPCSTR lpClassName,
    _In_opt_ LPCSTR lpWindowName,
    _In_ DWORD dwStyle,
    _In_ int X,
    _In_ int Y,
    _In_ int nWidth,
    _In_ int nHeight,
    _In_opt_ HWND hWndParent,
    _In_opt_ HMENU hMenu,
    _In_opt_ HINSTANCE hInstance,
    _In_opt_ LPVOID lpParam)
{
    SetLastError(1400);
    return NULL;
}
#pragma endregion
#pragma region  MyCreateWindowExW
HWND MyCreateWindowExW(
    _In_ DWORD dwExStyle,
    _In_opt_ LPCWSTR lpClassName,
    _In_opt_ LPCWSTR lpWindowName,
    _In_ DWORD dwStyle,
    _In_ int X,
    _In_ int Y,
    _In_ int nWidth,
    _In_ int nHeight,
    _In_opt_ HWND hWndParent,
    _In_opt_ HMENU hMenu,
    _In_opt_ HINSTANCE hInstance,
    _In_opt_ LPVOID lpParam)
{
    SetLastError(1400);
    return NULL;
}
#pragma endregion

#pragma region OpenProcessHook
HANDLE
MyOpenProcess(
    _In_ DWORD dwDesiredAccess,
    _In_ BOOL bInheritHandle,
    _In_ DWORD dwProcessId
)
{
    char* pid_s = new char[15];
    char* appDataPath = new char[2048]();
    char* username = new char[256];
    int pid;
    size_t buffer_count;
    _dupenv_s(&username, &buffer_count, "username");
    strcat_s(appDataPath, 2048, "C:\\Users\\");
    strcat_s(appDataPath, 2048, username);
    strcat_s(appDataPath, 2048, "\\AppData\\Local\\HotKeySetter\\Hook.ini");
    GetPrivateProfileStringA("Ring3Hook", "protectedID", "", pid_s, 15, appDataPath);
    pid = atoi(pid_s);
    HANDLE pHandle = NULL;
    if (dwProcessId == pid)
    {
        SetLastError(5);
        return 0;
    }
    else
    {
        hk.UnHook();
        pHandle = OpenProcess(dwDesiredAccess, bInheritHandle, dwProcessId);
        hk.ReHook();
        return pHandle;
    }
}
#pragma endregion

BOOL WINAPI MyDeleteFileW(_In_ LPCWSTR lpFileName)
{
    return FALSE;
}

BOOL
WINAPI
MyMoveFileW(
    _In_ LPCWSTR lpExistingFileName,
    _In_ LPCWSTR lpNewFileName
)
{
    return FALSE;
}

BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    {
        hk.Hook("kernel32.dll", "OpenProcess", (PROC)MyOpenProcess);
    }
    break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
VOID HookTest::UnHook()
{
    SIZE_T szRet = 0;
    WriteProcessMemory(GetCurrentProcess(), m_FuncAddress, m_OldBytes, 5, &szRet);
    return;
}
HookTest::HookTest()
{
    m_FuncAddress = NULL;
    memset(m_OldBytes, 0, 5);
    memset(m_NewBytes, 0, 5);
}
HookTest::~HookTest()
{
    UnHook();
    m_FuncAddress = NULL;
    memset(m_OldBytes, 0, 5);
    memset(m_NewBytes, 0, 5);
}
int HookTest::Hook(LPCSTR pszModuleName, LPCSTR pszFuncName, PROC pfnHookFunc)
{
    m_FuncAddress = (PROC)GetProcAddress(GetModuleHandleA(pszModuleName), pszFuncName);
    SIZE_T dwRet = 0;
    ReadProcessMemory(GetCurrentProcess(), m_FuncAddress, m_OldBytes, 5, &dwRet);

    m_NewBytes[0] = '\xE9';
    *(DWORD*)(m_NewBytes + 1) = (DWORD)pfnHookFunc - (DWORD)m_FuncAddress - 5;

    WriteProcessMemory(GetCurrentProcess(), m_FuncAddress, m_NewBytes, 5, &dwRet);
    return 0;
}
int HookTest::ReHook()
{
    if (m_FuncAddress != 0)
    {
        SIZE_T szRet = 0;
        WriteProcessMemory(GetCurrentProcess(), m_FuncAddress, m_NewBytes, 5, &szRet);
    }
    return 0;
}