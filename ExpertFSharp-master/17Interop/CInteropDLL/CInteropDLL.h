// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the CINTEROPDLL_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// CINTEROPDLL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef CINTEROPDLL_EXPORTS
#define CINTEROPDLL_API __declspec(dllexport)
#else
#define CINTEROPDLL_API __declspec(dllimport)
#endif
extern "C" {

typedef struct _Complex {
	double re;
	double im;
} Complex;

int CINTEROPDLL_API Sum(int i, int j);
Complex CINTEROPDLL_API SumC(Complex c1, Complex c2);
void CINTEROPDLL_API ZeroC(Complex* c);

void CINTEROPDLL_API HelloWorld();

void CINTEROPDLL_API echo(char* str);

void CINTEROPDLL_API sayhello(char* str, int sz);

void CINTEROPDLL_API sayhellow(wchar_t* str, int sz);

typedef int (CALLBACK *TRANSFORM_CALLBACK)(int);

void CINTEROPDLL_API transformArray(int* data, int count, TRANSFORM_CALLBACK fn);
}