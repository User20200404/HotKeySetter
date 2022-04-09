#pragma once
#include <Windows.h>
class HookTest
{
public:
	HookTest();
	~HookTest();
	BOOL Hook(LPCSTR pszModuleName, LPCSTR pszFuncName, PROC pfnHookFunc);
	VOID UnHook();
	BOOL ReHook();
private:
	PROC m_FuncAddress;
	BYTE m_OldBytes[5];
	BYTE m_NewBytes[5];
};