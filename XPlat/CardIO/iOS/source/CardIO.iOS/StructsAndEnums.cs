using System;

#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

namespace Card.IO
{
    [Native]
    public enum CreditCardType : long /* nint */ {
        Unrecognized = 0,
        Ambiguous = 1,
        Amex = 51 /*'3'*/,
        JCB = 74 /*'J'*/,
        Visa = 52 /*'4'*/,
        Mastercard = 53 /*'5'*/,
        Discover = 54 /*'6'*/
    }

    [Native]
    public enum DetectionMode : long /* nint */ {
        CardImageAndNumber = 0,
        CardImageOnly,
        Automatic
    }

}

