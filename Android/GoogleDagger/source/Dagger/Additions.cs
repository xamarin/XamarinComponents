using System;
using Android.Runtime;
using Java.Interop;

namespace Dagger.Internal
{
    partial class MapFactory
    {
        public unsafe global::Java.Lang.Object Get()
        {
            return Dictionary as Java.Lang.Object;
        }
    }

    partial class SetFactory
    {
        public unsafe global::Java.Lang.Object Get()
        {
            return Collection as Java.Lang.Object;
        }
    }

    partial class ProviderOfLazy
    {
        public unsafe global::Java.Lang.Object Get()
        {
            return Lazy as Java.Lang.Object;
        }
    }

    partial class MapProviderFactory : global::Dagger.ILazy, global::Dagger.Internal.IFactory
    {
        // This method is explicitly implemented as a member of an instantiated Dagger.ILazy
        public global::Java.Lang.Object Get()
        {
            return Dictionary as global::Java.Lang.Object;
        }
    }
}
