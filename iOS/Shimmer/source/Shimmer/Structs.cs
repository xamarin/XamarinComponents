using System;
using ObjCRuntime;

namespace Shimmer
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
