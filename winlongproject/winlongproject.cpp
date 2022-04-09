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
#pragma region ��չ�����±곣������
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
#pragma region �����±곣������
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
char* wchartochar(const wchar_t* ��ת�ַ�) //���ַ�ת��
{
	char* translatedchar;
	int length = WideCharToMultiByte(CP_ACP, NULL, ��ת�ַ�, wcslen(��ת�ַ�), NULL, 0, NULL, NULL);
	translatedchar = new char[(INT64)length + (INT64)1];
	WideCharToMultiByte(CP_ACP, NULL, ��ת�ַ�, wcslen(��ת�ַ�), translatedchar, length, NULL, NULL);
	translatedchar[length] = '\0';
	return translatedchar;
}
BOOL CALLBACK lpEnumCountFunc(HWND hwnd, LPARAM lParam)
{
	if (hwnd != NULL)
	{
		count++;                    //���ʹ����std�����ռ䣬Ϊ�˷�ֹ��std::count��ͻ����Ҫ��::count����
		return TRUE;                 //���hwnd��Ϊ�գ�˵��������Ч������TRUE����ö�١����hwndΪ��˵�������ö�٣�����FALSE������EnumWindows
	}
	else
	{
		return FALSE;
	}
}
BOOL CALLBACK lpEnumFunc(HWND hwnd, LPARAM lParam)  //�����ͨ��ָ�봫�ظ�hwnd���飬����¼��Ч�����
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
	if (STYLE_LONG != NULL) //����Ѿ���ֵ��ֱ�ӷ��ء�
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
extern "C" _declspec(dllexport) long GetWindowExstyle(long EX_STYLE_LONG, LPCWSTR EX_STYLE_STRING, int EX_STYLE_INDEX)//ͨ��STRING��INDEX���exstyle��longֵ��
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
extern "C" _declspec(dllexport) bool GetAllWindowExstyles(HWND hWnd, bool state[27])//��ȡָ�����ڵ�����EX_STYLE��״̬��sboolӦ���볤�ȴ��ڵ���27������
{
	long winLong;
	int err;
	winLong = GetWindowLong(hWnd, GWL_EXSTYLE);//��ʹwinLongΪ0�����lasterr=0������Ҳִ�гɹ���
    err = GetLastError();
	if (err == 0)
	{
		for (int i = 0; i < 27; i++)
		{
			state[i] = (winLong == (winLong | wlExStylesArray[i])); //���������ӵ��ĳ�����ԣ���ǰlongֵ|������longֵ �Ľ��Ӧ����ԭ��longֵ
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
extern "C" _declspec(dllexport) bool IsWindowExstyleOwned(HWND hWnd,long EX_STYLE_LONG, LPCWSTR EX_STYLE_STRING, int EX_STYLE_INDEX)//���ָ�������Ƿ�ӵ��ָ��EX_STYLE���з���true�����򷵻�false
{
	long winLong = GetWindowLong(hWnd,GWL_EXSTYLE);
	return (winLong == (winLong | GetWindowExstyle(EX_STYLE_LONG, EX_STYLE_STRING, EX_STYLE_INDEX)));
	//return (winLong == (winLong | GetWindowExstyle(EX_STYLE_LONG,EX_STYLE_STRING,EX_STYLE_INDEX)));
}
extern "C" _declspec(dllexport) bool DeleteWindowExstyle(HWND hWnd, long EX_STYLE_LONG, LPCWSTR EX_STYLE_STRING, int EX_STYLE_INDEX)//ȥ��ָ�����ڵ�ָ��EX_STYLE��ʽ��ĳЩ���Կ����޷����ɾ����ɾ���ɹ�����true�����򷵻�false
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
					for (int i = 0; i < 27; i++) //ѭ��27��
					{
						if (currentExstyles[i] == true && wlExStylesArray[i] != winexstyles) //ʹ��long������winexstylesҪɾ������չ���
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
				return (rtn1 != 0 && IsWindowExstyleOwned(hWnd, winexstyles, NULL, NULL) == false); //���setwindowlong�ɹ�
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
		winexstyle = GetWindowExstyle(EX_STYLE_LONG, EX_STYLE_STRING, EX_STYLE_INDEX);     //��Stringע����exstyleתΪ����longֵ
		if (winexstyle != 0)
		{
			if (winexstyle == WS_EX_TOPMOST)//WS_EX_TOPMOST����ͨ��setwindowlong()�����ã���ʱӦת��ʹ��SetWindowPos()
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
				err = GetLastError();//��ʹoldlongΪ0�����lasterr==0������Ҳִ�гɹ���
				if (err == 0)
				{
					newlong = oldlong | winexstyle;
					SetLastError(0);
					templong = SetWindowLongPtr(hWnd, GWL_EXSTYLE, newlong);
					err = GetLastError();
					return (err == 0 || err == 6); //Ī������ľ����Ч����ʵ���ϳɹ���
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
		SetLastError(1400);//��Ч�Ĵ��ھ����
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
				rtn = SetWindowLong(hWnd, GWL_STYLE, newlong); //SetWindowLong������ť��ʱ���ܻᱨ�����Ч�Ĵ��󣬽����䷵��ֵ�����ж��Ƿ�ɹ���
				err = GetLastError();
				if (rtn != 0)
				{
					SetLastError(0); //���ô�����Ϣ
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
		SetLastError(1400); //��Ч�Ĵ��ھ����
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
				if (GetAllWindowStyles(hWnd, currentStyles)) //��������ɹ�
				{
					for (int i = 0; i < 27; i++)
					{
						if (currentStyles[i] == true && winstyle != wlStylesArray[i])
						{
							newlong |= wlStylesArray[i]; //ʹ��newlong������ָ��longֵ
						}
					}
				}
				else return false;
				if (SetWindowLong(hWnd, GWL_STYLE, newlong) != 0 && !IsWindowStyleOwned(hWnd, winstyle, NULL, NULL))//SetWindowLong()�ɹ������Գɹ�ȥ��,��ɹ�������ʧ�ܡ�
				{
					SetLastError(0); //��������ǰ�ť�ȣ����ܻᱨ�����Ч�Ĵ�����������Ϊ0��
					return true;
				}
			}
			else return false; //Ҫɾ����longֵ������������������κβ�����
		}
		else return false;
	}
	else
	{
		SetLastError(1400);
		return false;
	}
}
extern "C" _declspec(dllexport) int GetTopWindowCount()  //�����ɹ����ض��㴰�ڸ�����ʧ�ܷ���-1
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
extern "C" _declspec(dllexport) int GetTopWindows(BOOL AllowEmpty,BOOL AllowDisabled,BOOL AllowHidden,HWND hwndArray_ret[],int MaxCount)//��ȡǰn�������������ڵľ���������鷽ʽ���ء�����ʱӦ����������鳤�ȣ���MAXCOUNTΪ0���������᷵�����з��������Ĵ��ڡ�����ֵΪʵ�ʻش��������
{
	count2 = 0;
	int n = 0,windowcount = GetTopWindowCount(),maxc = MaxCount;//n��ʼΪ0��ÿ�ش�һ������ͼ���1������GetTopWindowCount()ȷ����������Ծ���������HWND�������鳤��
	bool text_empty,disabled,hidden;
	HWND* hwndArray_temp = new HWND[windowcount];
	wchar_t* lpTextString = new wchar_t[2];//�����жϱ����Ƿ�Ϊ�ա�����ֻ���жϱ����Ƿ�Ϊ�գ��˴����������Ժ�С�������ڴ����ġ�
	if(EnumWindows(lpEnumFunc,(LPARAM)hwndArray_temp)!=FALSE)
	{
		if(maxc==0)          //���MAXCOUNTΪ0����Ӧ�ش����з��������ľ����ʹmaxc = count2(��Ч�����) ��ʵ��Ŀ�ģ� windowcount��count2ͨ����ͬһ��ֵ��������ô��������count2�ɿ��Ը��ߡ�
		{
			maxc = count2;
		}
		for (int i = 0; i < count2 && i < maxc; i++) //ѭ���жϴ���״̬(�ձ���/���ɲ���/����)�������������Ĳ���ӡ�
		{
			if (hwndArray_temp[i] != NULL)
			{
				//�������������Ϣ
				text_empty = (GetWindowTextW((HWND)hwndArray_temp[i],lpTextString,2) == 0); //GetWindowText()�ڴ��ڱ���Ϊ��ʱ��᷵��0
				disabled = !IsWindowEnabled(hwndArray_temp[i]);
				hidden = !IsWindowVisible(hwndArray_temp[i]);
				hwndArray_ret[n] = hwndArray_temp[i];                       //������ش�������                             
				if (!((AllowHidden == false && hidden == true) || (AllowDisabled == false && disabled == true) || (AllowEmpty == false && text_empty == true)))  //����ش��ľ������Ҫ����n����1���´λش��������������һ�����ꡣ������Ҫ����n���䣬�´β����Ḳ�Ǳ��λش���
				{
					n++; //����Ҫ��
				}
				else hwndArray_ret[n] = NULL; //�˾����������Ҫ��ľ������Ϊ����������һ������ݲ��ᱻ���ǣ�Ӧ�ֶ�ȥ����
			}
		}
	}
	delete[] hwndArray_temp;
	return n;   //����ʵ���ϻش��ľ�������������������ʧ�ܣ�nΪ��ʼֵ(0)
}
extern "C" _declspec(dllexport) wchar_t* Unsafe_GetFormatMessage(int ErrorCode)  //ͨ������Ĵ����뷵�ش����ı������ָ���Ĵ�����Ϊ-1���������ȡ�����������ش����ı���ע���������ַ��������ַ����󣬸��ַ����ڴ��޷����ͷţ�����ʹ�á�
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
	if(FormatMessageW(FORMAT_MESSAGE_FROM_SYSTEM,NULL,lasterr,0,msg,255,NULL)!=0) //�����ڶ�����������Ϊ0������ָ�����ȡ�
	{
		return msg; //�����ͷ��ڴ棬���ú���Ϊ����ȫ������
	}
	else
	{
		return NULL;
	}
}