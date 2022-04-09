#include <tchar.h>
#include <Windows.h>
#include <iostream>
int count =0,count2 = 0;
static long wlExStylesArray[27] = {
	WS_EX_ACCEPTFILES,
	WS_EX_APPWINDOW,
	WS_EX_CLIENTEDGE,
	WS_EX_COMPOSITED,
	WS_EX_CONTEXTHELP,
	WS_EX_CONTROLPARENT,
	WS_EX_DLGMODALFRAME,
	WS_EX_LAYERED,
	WS_EX_LAYOUTRTL,
	WS_EX_LEFT,
	WS_EX_LEFTSCROLLBAR,
	WS_EX_LTRREADING,
	WS_EX_MDICHILD,
	WS_EX_NOACTIVATE,
	WS_EX_NOINHERITLAYOUT,
	WS_EX_NOPARENTNOTIFY,
	WS_EX_NOREDIRECTIONBITMAP,
	WS_EX_OVERLAPPEDWINDOW,
	WS_EX_PALETTEWINDOW,
	WS_EX_RIGHT,
	WS_EX_RIGHTSCROLLBAR,
	WS_EX_RTLREADING,
	WS_EX_STATICEDGE,
	WS_EX_TOOLWINDOW,
	WS_EX_TOPMOST,
	WS_EX_TRANSPARENT,
	WS_EX_WINDOWEDGE
};
static char* wlConstExStylesCharArray[27] = {
	"WS_EX_ACCEPTFILES",
	"WS_EX_APPWINDOW",
	"WS_EX_CLIENTEDGE",
	"WS_EX_COMPOSITED",
	"WS_EX_CONTEXTHELP",
	"WS_EX_CONTROLPARENT",
	"WS_EX_DLGMODALFRAME",
	"WS_EX_LAYERED",
	"WS_EX_LAYOUTRTL",
	"WS_EX_LEFT",
	"WS_EX_LEFTSCROLLBAR",
	"WS_EX_LTRREADING",
	"WS_EX_MDICHILD",
	"WS_EX_NOACTIVATE",
	"WS_EX_NOINHERITLAYOUT",
	"WS_EX_NOPARENTNOTIFY",
	"WS_EX_NOREDIRECTIONBITMAP",
	"WS_EX_OVERLAPPEDWINDOW",
	"WS_EX_PALETTEWINDOW",
	"WS_EX_RIGHT",
	"WS_EX_RIGHTSCROLLBAR",
	"WS_EX_RTLREADING",
	"WS_EX_STATICEDGE",
	"WS_EX_TOOLWINDOW",
	"WS_EX_TOPMOST",
	"WS_EX_TRANSPARENT",
	"WS_EX_WINDOWEDGE"
};
static long wlStylesArray[27] = {
	WS_BORDER,
	WS_CAPTION,
	WS_CHILD,
	WS_CHILDWINDOW,
	WS_CLIPCHILDREN,
	WS_CLIPSIBLINGS,
	WS_DISABLED,
	WS_DLGFRAME,
	WS_GROUP,
	WS_HSCROLL,
	WS_ICONIC,
	WS_MAXIMIZE,
	WS_MAXIMIZEBOX,
	WS_MINIMIZE,
	WS_MINIMIZEBOX,
	WS_OVERLAPPED,
	WS_OVERLAPPEDWINDOW,
	WS_POPUP,
	WS_POPUPWINDOW,
	WS_SIZEBOX,
	WS_SYSMENU,
	WS_TABSTOP,
	WS_THICKFRAME,
	WS_TILED,
	WS_TILEDWINDOW,
	WS_VISIBLE,
	WS_VSCROLL
};
static wchar_t* wlConstStylesCharArray[27] = {
	L"WS_BORDER",
	L"WS_CAPTION",
	L"WS_CHILD",
	L"WS_CHILDWINDOW",
	L"WS_CLIPCHILDREN",
	L"WS_CLIPSIBLINGS",
	L"WS_DISABLED",
	L"WS_DLGFRAME",
	L"WS_GROUP",
	L"WS_HSCROLL",
	L"WS_ICONIC",
	L"WS_MAXIMIZE",
	L"WS_MAXIMIZEBOX",
	L"WS_MINIMIZE",
	L"WS_MINIMIZEBOX",
	L"WS_OVERLAPPED",
	L"WS_OVERLAPPEDWINDOW",
	L"WS_POPUP",
	L"WS_POPUPWINDOW",
	L"WS_SIZEBOX",
	L"WS_SYSMENU",
	L"WS_TABSTOP",
	L"WS_THICKFRAME",
	L"WS_TILED",
	L"WS_TILEDWINDOW",
	L"WS_VISIBLE",
	L"WS_VSCROLL"
};
#pragma region 扩展属性下标常量定义
#define WIN_WS_EX_ACCEPTFILES 0
#define WIN_WS_EX_APPWINDOW 1
#define WIN_WS_EX_CLIENTEDGE 2
#define WIN_WS_EX_COMPOSITED 3
#define WIN_WS_EX_CONTEXTHELP 4
#define WIN_WS_EX_CONTROLPARENT 5
#define WIN_WS_EX_DLGMODALFRAME 6
#define WIN_WS_EX_LAYERED 7
#define WIN_WS_EX_LAYOUTRTL 8
#define WIN_WS_EX_LEFT 9
#define WIN_WS_EX_LEFTSCROLLBAR 10
#define WIN_WS_EX_LTRREADING 11
#define WIN_WS_EX_MDICHILD 12
#define WIN_WS_EX_NOACTIVATE 13
#define WIN_WS_EX_NOINHERITLAYOUT 14
#define WIN_WS_EX_NOPARENTNOTIFY 15
#define WIN_WS_EX_NOREDIRECTIONBITMAP 16
#define WIN_WS_EX_OVERLAPPEDWINDOW 17
#define WIN_WS_EX_PALETTEWINDOW 18
#define WIN_WS_EX_RIGHT 19
#define WIN_WS_EX_RIGHTSCROLLBAR 20
#define WIN_WS_EX_RTLREADING 21
#define WIN_WS_EX_STATICEDGE 22
#define WIN_WS_EX_TOOLWINDOW 23
#define WIN_WS_EX_TOPMOST 24
#define WIN_WS_EX_TRANSPARENT 25
#define WIN_WS_EX_WINDOWEDGE 26
#pragma endregion
#pragma region 属性下标常量定义
#define WIN_WS_BORDER 0
#define WIN_WS_CAPTION 1
#define WIN_WS_CHILD 2
#define WIN_WS_CHILDWINDOW 3
#define WIN_WS_CLIPCHILDREN 4
#define WIN_WS_CLIPSIBLINGS 5
#define WIN_WS_DISABLED 6
#define WIN_WS_DLGFRAME 7
#define WIN_WS_GROUP 8
#define WIN_WS_HSCROLL 9
#define WIN_WS_ICONIC 10
#define WIN_WS_MAXIMIZE 11
#define WIN_WS_MAXIMIZEBOX 12
#define WIN_WS_MINIMIZE 13
#define WIN_WS_MINIMIZEBOX 14
#define WIN_WS_OVERLAPPED 15
#define WIN_WS_OVERLAPPEDWINDOW 16
#define WIN_WS_POPUP 17
#define WIN_WS_POPUPWINDOW 18
#define WIN_WS_SIZEBOX 19
#define WIN_WS_SYSMENU 20
#define WIN_WS_TABSTOP 21
#define WIN_WS_THICKFRAME 22
#define WIN_WS_TILED 23
#define WIN_WS_TILEDWINDOW 24
#define WIN_WS_VISIBLE 25
#define WIN_WS_VSCROLL 26
#pragma endregion
char* wchartochar(const wchar_t* 待转字符) //宽字符转化
{
	char* translatedchar;
	int length = WideCharToMultiByte(CP_ACP, NULL, 待转字符, wcslen(待转字符), NULL, 0, NULL, NULL);
	translatedchar = new char[(INT64)length + (INT64)1];
	WideCharToMultiByte(CP_ACP, NULL, 待转字符, wcslen(待转字符), translatedchar, length, NULL, NULL);
	translatedchar[length] = '\0';
	return translatedchar;
}
BOOL CALLBACK lpEnumCountFunc(HWND hwnd, LPARAM lParam)
{
	if (hwnd != NULL)
	{
		count++;                    //如果使用了std命名空间，为了防止和std::count冲突，需要用::count声明
		return TRUE;                 //如果hwnd不为空，说明窗口有效，返回TRUE继续枚举。如果hwnd为空说明已完成枚举，返回FALSE来结束EnumWindows
	}
	else
	{
		return FALSE;
	}
}
BOOL CALLBACK lpEnumFunc(HWND hwnd, LPARAM lParam)  //将句柄通过指针传回给hwnd数组，并记录有效句柄数
{
	HWND* h = (HWND*)lParam;
	if (hwnd != NULL)
	{
		h[count2] = hwnd;
		count2++;
		return TRUE;
	}
	else
	{
		return FALSE;
	}
}
extern "C" _declspec(dllexport) long GetWindowStyle(long STYLE_LONG, LPCWSTR STYLE_STRING, int STYLE_INDEX)
{
	if (STYLE_LONG != NULL) //如果已经有值，直接返回。
		return STYLE_LONG;
	if (STYLE_STRING != NULL)
	{
		for (int i = 0; i < 27; i++)
		{
			if (lstrcmpiW(STYLE_STRING, wlConstStylesCharArray[i]) == 0)
			{
				return wlStylesArray[i];
			}
		}
		return 0;
	}
	else
	{
		if (STYLE_INDEX >= 0 && STYLE_INDEX <= 26)
		{
			return wlExStylesArray[STYLE_INDEX];
		}
		else
		{
			return 0;
		}
	}
}
extern "C" _declspec(dllexport) long GetWindowExstyle(long EX_STYLE_LONG, LPCWSTR EX_STYLE_STRING, int EX_STYLE_INDEX)//通过STRING或INDEX获得exstyle的long值。
{
	if (EX_STYLE_LONG != NULL)
		return EX_STYLE_LONG;
	if (EX_STYLE_STRING != NULL)
	{
		for (int i = 0; i < 27; i++)
		{
			if (_strcmpi(wchartochar(EX_STYLE_STRING), wlConstExStylesCharArray[i]) == 0)
			{
				return wlExStylesArray[i];
			}
		}
		return 0;
	}
	else
	{
		if (EX_STYLE_INDEX >= 0 && EX_STYLE_INDEX <= 26)
		{
			return wlExStylesArray[EX_STYLE_INDEX];
		}
		else
		{
			return 0;
		}
	}
}
extern "C" _declspec(dllexport) bool GetAllWindowExstyles(HWND hWnd, bool state[27])//获取指定窗口的所有EX_STYLE的状态，sbool应传入长度大于等于27的数组
{
	long winLong;
	int err;
	winLong = GetWindowLong(hWnd, GWL_EXSTYLE);//即使winLong为0，如果lasterr=0，函数也执行成功。
    err = GetLastError();
	if (err == 0)
	{
		for (int i = 0; i < 27; i++)
		{
			state[i] = (winLong == (winLong | wlExStylesArray[i])); //如果窗口已拥有某个属性，当前long值|该属性long值 的结果应还是原先long值
		}
		return true;
	}
	else return false;
}
extern "C" _declspec(dllexport) bool GetAllWindowStyles(HWND hWnd, bool state[27])
{
	long winLong;
	int err;
	winLong = GetWindowLong(hWnd, GWL_STYLE);
	err = GetLastError();
	if (err == 0 || winLong != 0)
	{
		for (int i = 0; i < 27; i++)
		{
			state[i] = (winLong == (winLong | wlStylesArray[i]));
		}
		return true;
	}
	else return false;

}
extern "C" _declspec(dllexport) bool IsWindowStyleOwned(HWND hWnd, long STYLE_LONG, LPCWSTR STYLE_STRING, int STYLE_INDEX)
{
	long winLong = GetWindowLong(hWnd, GWL_STYLE);
	return (winLong == (winLong | GetWindowStyle(STYLE_LONG,STYLE_STRING,STYLE_INDEX)));
}
extern "C" _declspec(dllexport) bool IsWindowExstyleOwned(HWND hWnd,long EX_STYLE_LONG, LPCWSTR EX_STYLE_STRING, int EX_STYLE_INDEX)//检查指定窗口是否拥有指定EX_STYLE，有返回true，否则返回false
{
	long winLong = GetWindowLong(hWnd,GWL_EXSTYLE);
	return (winLong == (winLong | GetWindowExstyle(EX_STYLE_LONG, EX_STYLE_STRING, EX_STYLE_INDEX)));
	//return (winLong == (winLong | GetWindowExstyle(EX_STYLE_LONG,EX_STYLE_STRING,EX_STYLE_INDEX)));
}
extern "C" _declspec(dllexport) bool DeleteWindowExstyle(HWND hWnd, long EX_STYLE_LONG, LPCWSTR EX_STYLE_STRING, int EX_STYLE_INDEX)//去除指定窗口的指定EX_STYLE样式，某些属性可能无法完成删除。删除成功返回true，否则返回false
{
	bool currentExstyles[27];
	long newlong = 0, rtn1, winexstyles;
	int err = 0;
	bool rtn;
	winexstyles = GetWindowExstyle(EX_STYLE_LONG, EX_STYLE_STRING, EX_STYLE_INDEX);
	if (winexstyles != 0)
	{
		if (winexstyles == WS_EX_TOPMOST)
		{
			SetWindowPos(hWnd, HWND_NOTOPMOST, NULL, NULL, NULL, NULL, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
			if (err == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			if (IsWindowExstyleOwned(hWnd, winexstyles,NULL,NULL))
			{
				rtn = GetAllWindowExstyles(hWnd, currentExstyles);
				if (rtn == true)
				{
					for (int i = 0; i < 27; i++) //循环27次
					{
						if (currentExstyles[i] == true && wlExStylesArray[i] != winexstyles) //使新long不包含winexstyles要删除的扩展风格
						{
							newlong |= wlExStylesArray[i];
						}
					}
				}
				else
				{
					return false;
				}
				rtn1 = SetWindowLong(hWnd, GWL_EXSTYLE, newlong);
				return (rtn1 != 0 && IsWindowExstyleOwned(hWnd, winexstyles, NULL, NULL) == false); //如果setwindowlong成功
			}
			else
			{
				return false;
			}
		}
	}
	else
	{
		return false;
	}
}
extern "C" _declspec(dllexport) bool AddWindowExstyle(HWND hWnd, long EX_STYLE_LONG, LPCWSTR EX_STYLE_STRING, int EX_STYLE_INDEX)
{
	long oldlong, newlong, winexstyle, templong;
	int err = 0;
	if (hWnd != NULL)
	{
		winexstyle = GetWindowExstyle(EX_STYLE_LONG, EX_STYLE_STRING, EX_STYLE_INDEX);     //将String注明的exstyle转为具体long值
		if (winexstyle != 0)
		{
			if (winexstyle == WS_EX_TOPMOST)//WS_EX_TOPMOST不能通过setwindowlong()来设置，此时应转而使用SetWindowPos()
			{
				SetWindowPos(hWnd, HWND_TOPMOST, NULL, NULL, NULL, NULL, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
				err = GetLastError();
				if (err == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				SetLastError(0);
				oldlong = GetWindowLong(hWnd, GWL_EXSTYLE);
				err = GetLastError();//即使oldlong为0，如果lasterr==0，函数也执行成功。
				if (err == 0)
				{
					newlong = oldlong | winexstyle;
					SetLastError(0);
					templong = SetWindowLongPtr(hWnd, GWL_EXSTYLE, newlong);
					err = GetLastError();
					return (err == 0 || err == 6); //莫名其妙的句柄无效，但实际上成功。
				}
				else
				{
					return false;
				}
			}
		}
		else
		{
			return false;
		}
	}
	else
	{
		SetLastError(1400);//无效的窗口句柄。
		return false;
	}

}
extern "C" _declspec(dllexport) bool AddWindowStyle(HWND hWnd, long STYLE_LONG, LPCWSTR STYLE_STRING, int STYLE_INDEX)
{
	long oldlong, newlong, winstyle;
	int err = 0,rtn;
	if (hWnd != NULL)
	{
		winstyle = GetWindowStyle(STYLE_LONG,STYLE_STRING, STYLE_INDEX);
		if (winstyle != 0)
		{
			oldlong = GetWindowLong(hWnd, GWL_STYLE);
			err = GetLastError();
			if (err == 0 || oldlong !=0)
			{
				newlong = oldlong | winstyle;
				rtn = SetWindowLong(hWnd, GWL_STYLE, newlong); //SetWindowLong操作按钮等时可能会报句柄无效的错误，接收其返回值加以判断是否成功。
				err = GetLastError();
				if (rtn != 0)
				{
					SetLastError(0); //重置错误信息
					return true;
				}
				else return false;
			}
			else return false;
		}
		else return false;
	}
	else
	{
		SetLastError(1400); //无效的窗口句柄。
		return false;
	}
}
extern "C" _declspec(dllexport) bool DeleteWindowStyle(HWND hWnd, long STYLE_LONG, LPCWSTR STYLE_STRING, int STYLE_INDEX)
{
	bool currentStyles[27];
	long winstyle = 0,newlong = 0,rtn;
	if (hWnd != NULL)
	{
		winstyle = GetWindowStyle(STYLE_LONG, STYLE_STRING, STYLE_INDEX);
		if (winstyle != 0)
		{
			if (IsWindowStyleOwned(hWnd, winstyle, NULL, NULL))
			{
				if (GetAllWindowStyles(hWnd, currentStyles)) //如果操作成功
				{
					for (int i = 0; i < 27; i++)
					{
						if (currentStyles[i] == true && winstyle != wlStylesArray[i])
						{
							newlong |= wlStylesArray[i]; //使得newlong不包含指定long值
						}
					}
				}
				else return false;
				if (SetWindowLong(hWnd, GWL_STYLE, newlong) != 0 && !IsWindowStyleOwned(hWnd, winstyle, NULL, NULL))//SetWindowLong()成功且属性成功去除,则成功，否则失败。
				{
					SetLastError(0); //如果窗口是按钮等，可能会报句柄无效的错误，这里重置为0。
					return true;
				}
			}
			else return false; //要删除的long值并不包含，无需进行任何操作。
		}
		else return false;
	}
	else
	{
		SetLastError(1400);
		return false;
	}
}
extern "C" _declspec(dllexport) int GetTopWindowCount()  //函数成功返回顶层窗口个数，失败返回-1
{
	count = 0;
	if(EnumWindows(lpEnumCountFunc,NULL)!=FALSE)
	{
		return count;
	}
	else
	{
		return -1;
	}
}
extern "C" _declspec(dllexport) int GetTopWindows(BOOL AllowEmpty,BOOL AllowDisabled,BOOL AllowHidden,HWND hwndArray_ret[],int MaxCount)//获取前n个符合条件窗口的句柄，以数组方式返回。调用时应传递最大数组长度，若MAXCOUNT为0，本函数会返回所有符合条件的窗口。返回值为实际回传句柄个数
{
	count2 = 0;
	int n = 0,windowcount = GetTopWindowCount(),maxc = MaxCount;//n初始为0，每回传一个句柄就加上1；先用GetTopWindowCount()确定句柄数，以决定声明的HWND变量数组长度
	bool text_empty,disabled,hidden;
	HWND* hwndArray_temp = new HWND[windowcount];
	wchar_t* lpTextString = new wchar_t[2];//用于判断标题是否为空。由于只是判断标题是否为空，此处缓冲区可以很小来减少内存消耗。
	if(EnumWindows(lpEnumFunc,(LPARAM)hwndArray_temp)!=FALSE)
	{
		if(maxc==0)          //如果MAXCOUNT为0，则应回传所有符合条件的句柄，使maxc = count2(有效句柄数) 可实现目的； windowcount和count2通常是同一数值，但距离该代码最近的count2可靠性更高。
		{
			maxc = count2;
		}
		for (int i = 0; i < count2 && i < maxc; i++) //循环判断窗口状态(空标题/不可操作/可视)，不符合条件的不添加。
		{
			if (hwndArray_temp[i] != NULL)
			{
				//变量储存相关信息
				text_empty = (GetWindowTextW((HWND)hwndArray_temp[i],lpTextString,2) == 0); //GetWindowText()在窗口标题为空时候会返回0
				disabled = !IsWindowEnabled(hwndArray_temp[i]);
				hidden = !IsWindowVisible(hwndArray_temp[i]);
				hwndArray_ret[n] = hwndArray_temp[i];                       //将句柄回传至数组                             
				if (!((AllowHidden == false && hidden == true) || (AllowDisabled == false && disabled == true) || (AllowEmpty == false && text_empty == true)))  //如果回传的句柄符合要求，则n递增1，下次回传将传给数组的下一个坐标。不符合要求则n不变，下次操作会覆盖本次回传。
				{
					n++; //符合要求
				}
				else hwndArray_ret[n] = NULL; //此句清除不符合要求的句柄，因为如果这是最后一项，其数据不会被覆盖，应手动去除。
			}
		}
	}
	delete[] hwndArray_temp;
	return n;   //返回实际上回传的句柄数，理论上如果函数失败，n为初始值(0)
}
extern "C" _declspec(dllexport) wchar_t* Unsafe_GetFormatMessage(int ErrorCode)  //通过传入的错误码返回错误文本，如果指定的错误码为-1，函数会获取最后错误来返回错误文本。注：采用这种方法返回字符串后，该字符串内存无法被释放，酌情使用。
{
	int lasterr;
	wchar_t* msg =new wchar_t[255];
	if(ErrorCode == -1)
	{
		lasterr = GetLastError();
	}
	else
	{
		lasterr = ErrorCode;
	}
	if(FormatMessageW(FORMAT_MESSAGE_FROM_SYSTEM,NULL,lasterr,0,msg,255,NULL)!=0) //倒数第二个参数不能为0，必须指定长度。
	{
		return msg; //不能释放内存，即该函数为不安全函数。
	}
	else
	{
		return NULL;
	}
}