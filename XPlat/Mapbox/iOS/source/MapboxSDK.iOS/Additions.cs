using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using System.Runtime.InteropServices;
using UIKit;

namespace Mapbox
{
    //    static class Messaging
    //    {
    //        static internal System.Reflection.Assembly this_assembly = typeof (Messaging).Assembly;
    //
    //        const string LIBOBJC_DYLIB = "/usr/lib/libobjc.dylib";
    //
    //        [DllImport (LIBOBJC_DYLIB, EntryPoint="objc_msgSendSuper")]
    //        internal extern static global::System.IntPtr IntPtr_objc_msgSendSuper_IntPtr_UInt32 (IntPtr receiver, IntPtr selector, IntPtr arg1, global::System.UInt32 arg2);
    //
    //        [DllImport (LIBOBJC_DYLIB, EntryPoint="objc_msgSend")]
    //        internal extern static global::System.IntPtr IntPtr_objc_msgSend_IntPtr_UInt32 (IntPtr receiver, IntPtr selector, IntPtr arg1, global::System.UInt32 arg2);
    //    }

    partial class Style
    {
        public static readonly nint DefaultVersion = 9;
    }

    partial class Polygon 
    {

        public static Polygon WithCoordinates (CLLocationCoordinate2D[] coords, nuint count)
        {
            return WithCoordinates(GetPointer(coords), count);
        }
    }

	partial class MultiPoint {
        public CLLocationCoordinate2D[] GetCoordinates() { 
			return PtrToCLLocationCoordinate2DArray(Convert.ToInt32(this.PointCount), this.Coordinates);
        }

        public void GetCoordinates(CLLocationCoordinate2D[] coords, NSRange range) {
            var ptr = GetPointer(coords);
            this._GetCoordinates(ptr, range);
        }

        public void SetCoordinates(CLLocationCoordinate2D[] coords) {
            this._SetCoordinates(GetPointer(coords), Convert.ToUInt32(coords.Length));
        }

		public void InsertCoordinates(CLLocationCoordinate2D[] coords, int index)
		{
            this._InsertCoordinates(GetPointer(coords), Convert.ToUInt32(coords.Length), Convert.ToUInt32(index));
		}

		public void AppendCoordinates(CLLocationCoordinate2D[] coords)
		{
			this._AppendCoordinates(GetPointer(coords), Convert.ToUInt32(coords.Length));
		}

        public void _ReplaceCoordinatesInRange(NSRange range, CLLocationCoordinate2D[] coords)
		{
			this._ReplaceCoordinatesInRange(range, GetPointer(coords), Convert.ToUInt32(coords.Length));
		}

		unsafe internal static IntPtr GetPointer(CLLocationCoordinate2D[] coordinates)
		{
			fixed (CLLocationCoordinate2D* ptr = &coordinates[0])
				return (IntPtr)ptr;
		}

		static CLLocationCoordinate2D[] PtrToCLLocationCoordinate2DArray(int count, IntPtr cllocationCoordinate2DArray)
        {
            if (count < 0)
            	throw new ArgumentOutOfRangeException(nameof(count), "< 0");
            if (cllocationCoordinate2DArray == IntPtr.Zero)
            	return new CLLocationCoordinate2D[count];

            int structSize = Marshal.SizeOf(typeof(CLLocationCoordinate2D));

            var members = new CLLocationCoordinate2D[count];

            for (int i = 0; i < count; ++i)
            {
            	var data = IntPtr.Add(cllocationCoordinate2DArray, structSize * i);
            	members[i] = Marshal.PtrToStructure<CLLocationCoordinate2D>(data);
            }


            return members;
        }
	}

    partial class Polyline 
    {

        public static Polyline WithCoordinates (CLLocationCoordinate2D[] coords, nuint count)
        {
            return WithCoordinates(GetPointer(coords), count);
        }              
    }

    partial class MapView
    {
        const string frameworkPath = "Frameworks/Mapbox.framework/Mapbox";

        // TODO: These Deceleration rates are currently hard coded based on the source of Mapbox
        // They may change in the future!!!
        // They are not exposed from the framework since they are not publicly declared in it
        public static readonly nfloat DecelerationRateNormal = UIScrollView.DecelerationRateNormal;
        public static readonly nfloat DecelerationRateFast = UIScrollView.DecelerationRateFast;
        public static readonly nfloat DecelerationRateImmediate = 0.0f;

        public void SetVisibleCoordinates (CLLocationCoordinate2D[] coordinates, nuint count, UIEdgeInsets insets, bool animated)
        {
            SetVisibleCoordinates(GetPointer(coordinates), count, insets, animated);
        }

        unsafe internal static IntPtr GetPointer (CLLocationCoordinate2D[] coordinates)
        {
            fixed (CLLocationCoordinate2D* ptr = &coordinates[0])
            return (IntPtr) ptr;
        }
    }

	partial class CoordinateSpanConstants
	{
		static CoordinateSpan? zero;

		public static CoordinateSpan Zero
		{
			get
			{
				if (zero == null)
				{
					IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen(null, 0);
					IntPtr ptr = Dlfcn.dlsym(RTLD_MAIN_ONLY, "MGLCoordinateSpanZero");
					zero = Marshal.PtrToStructure<CoordinateSpan>(ptr);
					Dlfcn.dlclose(RTLD_MAIN_ONLY);
				}

				return zero.Value;
			}
		}
	}
}

