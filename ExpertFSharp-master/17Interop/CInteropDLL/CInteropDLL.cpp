// CInteropDLL.cpp : Defines the entry point for the DLL application.
//
#include <windows.h>
#include "CInteropDLL.h"
#include <stdio.h>


BOOL APIENTRY DllMain( HMODULE hModule,
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

int CINTEROPDLL_API Sum(int i, int j)
{
	return i + j;
}

Complex CINTEROPDLL_API SumC(Complex c1, Complex c2)
{
	Complex ret;
	ret.re = c1.re + c2.re; 
	ret.im = c1.im + c2.im;
	return ret;
}

void CINTEROPDLL_API ZeroC(Complex* c)
{
	c->re = 0;
	c->im = 0;
}

void CINTEROPDLL_API HelloWorld()
{
	printf("Hello C world invoked by F#!\n");
}

void CINTEROPDLL_API echo(char* str)
{
	puts(str);
}

void CINTEROPDLL_API sayhello(char* str, int sz)
{
	static char* data = "Hello from C code!";
	int len = min(sz, strlen(data));
	strncpy(str, data, len );
	str[len] = 0;
}

void CINTEROPDLL_API sayhellow(wchar_t* str, int sz)
{
	static wchar_t* data = L"Hello from C code!";
	int len = min(sz, wcslen(data));
	wcsncpy(str, data, len );
	str[len] = 0;
}

void CINTEROPDLL_API transformArray(int* data, int count, TRANSFORM_CALLBACK fn)
{
	int i;
	for (i = 0; i < count; i++)
		data[i] = fn(data[i]);
}
