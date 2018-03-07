using System;
using ObjCRuntime;

namespace Shimmer.iOS
{
    [Native]
    public enum ShimmerDirection : long
    {
        Right,
        Left,
        Up,
        Down
    }
}
