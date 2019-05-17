#pragma once

namespace WinRTLibrary
{
	using namespace Platform;
	using namespace Windows::Foundation;

    public ref class Class1 sealed
    {
    public:
        Class1();
		String^ SayHello();
    };
}
