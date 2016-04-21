// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the APPLICATIONINTERFACES_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// APPLICATIONINTERFACES_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef APPLICATIONINTERFACES_EXPORTS
#define APPLICATIONINTERFACES_API __declspec(dllexport)
#else
#define APPLICATIONINTERFACES_API __declspec(dllimport)
#endif

// This class is exported from the ApplicationInterfaces.dll
class APPLICATIONINTERFACES_API CApplicationInterfaces {
public:
	CApplicationInterfaces(void);
	// TODO: add your methods here.
};

extern APPLICATIONINTERFACES_API int nApplicationInterfaces;

extern "C"  APPLICATIONINTERFACES_API int fnApplicationInterfaces();