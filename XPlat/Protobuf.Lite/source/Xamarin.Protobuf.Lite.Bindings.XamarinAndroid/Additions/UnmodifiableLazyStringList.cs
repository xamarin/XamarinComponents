using System;

namespace Xamarin.Protobuf.Lite
{

    public partial class UnmodifiableLazyStringList
    {
        public override Java.Lang.Object Get(int index)
        {
            return this.GetRaw(index);
        }
    }
}
