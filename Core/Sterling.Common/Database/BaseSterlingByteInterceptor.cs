using System;
#if __ANDROID__
using Android.Runtime;
#elif __IOS__
using Foundation;
#elif WINDOWS_PHONE

#else

#endif
namespace Wintellect.Sterling.Database
{
    public abstract class BaseSterlingByteInterceptor : ISterlingByteInterceptor
    {
      
        public BaseSterlingByteInterceptor(){

        }
        virtual public byte[] Save(byte[] sourceStream)
        {
            throw new NotImplementedException();
        }

        virtual public byte[] Load(byte[] sourceStream)
        {
            throw new NotImplementedException();
        }
    }

}
