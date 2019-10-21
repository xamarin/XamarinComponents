using System;
using Android.Runtime;

namespace Xamarin.Io.OpenCensus.Stats
{
    public abstract partial class ViewData
    {

//        internal partial class ViewDataInvoker
//        {
//            static Delegate cb_getAggregationMap;
//#pragma warning disable 0169
//            static Delegate GetGetAggregationMapHandler()
//            {
//                if (cb_getAggregationMap == null)
//                    cb_getAggregationMap = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetAggregationMap);
//                return cb_getAggregationMap;
//            }

//            static IntPtr n_GetAggregationMap(IntPtr jnienv, IntPtr native__this)
//            {
//                global::Xamarin.Io.OpenCensus.Stats.ViewData __this = global::Java.Lang.Object.GetObject<global::Xamarin.Io.OpenCensus.Stats.ViewData>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
//                return global::Android.Runtime.JavaDictionary<global::System.Collections.Generic.IList<global::Xamarin.Io.OpenCensus.Tags.TagValue>, Xamarin.Io.OpenCensus.Stats.AggregationData>.ToLocalJniHandle(__this.AggregationMap);
//            }
//#pragma warning restore 0169

//            public abstract global::System.Collections.Generic.IDictionary<global::System.Collections.Generic.IList<global::Xamarin.Io.OpenCensus.Tags.TagValue>, Xamarin.Io.OpenCensus.Stats.AggregationData> AggregationMap
//            {
//                // Metadata.xml XPath method reference: path="/api/package[@name='io.opencensus.stats']/class[@name='ViewData']/method[@name='getAggregationMap' and count(parameter)=0]"
//                [Register("getAggregationMap", "()Ljava/util/Map;", "GetGetAggregationMapHandler")]
//                get;
//            }
//      }
    }
}
