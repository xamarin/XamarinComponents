using System;
using Android.Runtime;

namespace Google.Android.ExoPlayer.SmoothStreaming
{
    public partial class SmoothStreamingManifestParser
    {
        public partial class SmoothStreamMediaParser
        {
            static IntPtr id_build;
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.exoplayer.smoothstreaming']/class[@name='SmoothStreamingManifestParser.SmoothStreamMediaParser']/method[@name='build' and count(parameter)=0]"
            [Register ("build", "()Ljava/lang/Object;", "GetBuildHandler")]
            protected override unsafe global::Java.Lang.Object Build ()
            {
                if (id_build == IntPtr.Zero)
                    id_build = JNIEnv.GetMethodID (class_ref, "build", "()Ljava/lang/Object;");
                try {

                    if (GetType () == ThresholdType)
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod (Handle, id_build), JniHandleOwnership.TransferLocalRef);
                    else
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "build", "()Ljava/lang/Object;")), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }

        public partial class ProtectionElementParser
        {
            static IntPtr id_build;
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.exoplayer.smoothstreaming']/class[@name='SmoothStreamingManifestParser.ProtectionElementParser']/method[@name='build' and count(parameter)=0]"
            [Register ("build", "()Ljava/lang/Object;", "GetBuildHandler")]
            protected override unsafe global::Java.Lang.Object Build ()
            {
                if (id_build == IntPtr.Zero)
                    id_build = JNIEnv.GetMethodID (class_ref, "build", "()Ljava/lang/Object;");
                try {

                    if (GetType () == ThresholdType)
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod (Handle, id_build), JniHandleOwnership.TransferLocalRef);
                    else
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "build", "()Ljava/lang/Object;")), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }

        public partial class StreamElementParser
        {
            static IntPtr id_build;
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.exoplayer.smoothstreaming']/class[@name='SmoothStreamingManifestParser.StreamElementParser']/method[@name='build' and count(parameter)=0]"
            [Register ("build", "()Ljava/lang/Object;", "GetBuildHandler")]
            protected override unsafe global::Java.Lang.Object Build ()
            {
                if (id_build == IntPtr.Zero)
                    id_build = JNIEnv.GetMethodID (class_ref, "build", "()Ljava/lang/Object;");
                try {

                    if (GetType () == ThresholdType)
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod (Handle, id_build), JniHandleOwnership.TransferLocalRef);
                    else
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "build", "()Ljava/lang/Object;")), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }

        public partial class TrackElementParser
        {
            static IntPtr id_build;
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.exoplayer.smoothstreaming']/class[@name='SmoothStreamingManifestParser.TrackElementParser']/method[@name='build' and count(parameter)=0]"
            [Register ("build", "()Ljava/lang/Object;", "GetBuildHandler")]
            protected override unsafe global::Java.Lang.Object Build ()
            {
                if (id_build == IntPtr.Zero)
                    id_build = JNIEnv.GetMethodID (class_ref, "build", "()Ljava/lang/Object;");
                try {

                    if (GetType () == ThresholdType)
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallObjectMethod (Handle, id_build), JniHandleOwnership.TransferLocalRef);
                    else
                        return global::Java.Lang.Object.GetObject<global::Java.Lang.Object> (JNIEnv.CallNonvirtualObjectMethod (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "build", "()Ljava/lang/Object;")), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }
}

namespace Google.VR.SDK.Widgets.Video
{
    public partial class VrVideoView
    {
        static IntPtr id_createRenderer_Manual;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.vr.sdk.widgets.common']/class[@name='VrWidgetView']/method[@name='createRenderer' and count(parameter)=5 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.google.vr.sdk.widgets.common.VrWidgetRenderer.GLThreadScheduler'] and parameter[3][@type='float'] and parameter[4][@type='float'] and parameter[5][@type='int']]"
        [Register ("createRenderer", "(Landroid/content/Context;Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer$GLThreadScheduler;FFI)Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer;", "GetCreateRenderer_Landroid_content_Context_Lcom_google_vr_sdk_widgets_common_VrWidgetRenderer_GLThreadScheduler_FFIHandler")]
        protected override unsafe global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer CreateRenderer (global::Android.Content.Context context, global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer.IGLThreadScheduler glThreadScheduler, float xMetersPerPixel, float yMetersPerPixel, int screenRotation)
        {
            if (id_createRenderer_Manual == IntPtr.Zero)
                id_createRenderer_Manual = JNIEnv.GetMethodID (class_ref, "createRenderer", "(Landroid/content/Context;Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer$GLThreadScheduler;FFI)Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer;");
            try {
                JValue* __args = stackalloc JValue [5];
                __args [0] = new JValue (context);
                __args [1] = new JValue (glThreadScheduler);
                __args [2] = new JValue (xMetersPerPixel);
                __args [3] = new JValue (yMetersPerPixel);
                __args [4] = new JValue (screenRotation);
                global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer __ret = global::Java.Lang.Object.GetObject<global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer> (JNIEnv.CallObjectMethod (Handle, id_createRenderer_Manual, __args), JniHandleOwnership.TransferLocalRef);
                return __ret;
            } finally {
            }
        }

    }
}

namespace Google.Android.ExoPlayer.Chunks
{
    public partial class Format
    {
        public sealed partial class DecreasingBandwidthComparator
        {
            int Java.Util.IComparator.Compare (Java.Lang.Object lhs, Java.Lang.Object rhs)
            {
                return this.Compare ((global::Google.Android.ExoPlayer.Chunks.Format)lhs, (global::Google.Android.ExoPlayer.Chunks.Format)rhs);
            }
        }
    }

    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.exoplayer.chunk']/class[@name='ContainerMediaChunk']"
    public partial class ContainerMediaChunk
    {
        static IntPtr id_drmInitData_Manual;

        [Register ("drmInitData", "(Lcom/google/android/exoplayer/drm/DrmInitData;)V", "DrmInitDataHandler")]
        public unsafe void SetDrmInitData (global::Google.Android.ExoPlayer.Drm.IDrmInitData initData)
        {
            if (id_drmInitData_Manual == IntPtr.Zero)
                id_drmInitData_Manual = JNIEnv.GetMethodID (class_ref, "drmInitData", "(Lcom/google/android/exoplayer/drm/DrmInitData;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (initData);
                JNIEnv.CallVoidMethod (Handle, id_drmInitData_Manual, __args);
            } finally {
            }
        }

        IntPtr id_seekMap_Manual;

        [Register ("seekMap", "(Lcom/google/android/exoplayer/extractor/SeekMap;)V", "SeekMapHandler")]
        public unsafe void SetSeekMap (global::Google.Android.ExoPlayer.Extractor.ISeekMap seekMap)
        {
            if (id_seekMap_Manual == IntPtr.Zero)
                id_seekMap_Manual = JNIEnv.GetMethodID (class_ref, "seekMap", "(Lcom/google/android/exoplayer/extractor/SeekMap;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (seekMap);
                JNIEnv.CallVoidMethod (Handle, id_seekMap_Manual, __args);
            } finally {
            }
        }

        IntPtr id_format_Manual;

        [Register ("format", "(Lcom/google/android/exoplayer/MediaFormat;)V", "FormatHandler")]
        public unsafe void SetFormat (global::Google.Android.ExoPlayer.MediaFormat mediaFormat)
        {
            if (id_format_Manual == IntPtr.Zero)
                id_format_Manual = JNIEnv.GetMethodID (class_ref, "format", "(Lcom/google/android/exoplayer/MediaFormat;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (mediaFormat);
                JNIEnv.CallVoidMethod (Handle, id_format_Manual, __args);
            } finally {
            }
        }
    }

    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.exoplayer.chunk']/class[@name='InitializationChunk']"
    public partial class InitializationChunk
    {
        IntPtr id_drmInitData_Manual;

        [Register ("drmInitData", "(Lcom/google/android/exoplayer/drm/DrmInitData;)V", "DrmInitDataHandler")]
        public unsafe void SetDrmInitData (global::Google.Android.ExoPlayer.Drm.IDrmInitData initData)
        {
            if (id_drmInitData_Manual == IntPtr.Zero)
                id_drmInitData_Manual = JNIEnv.GetMethodID (class_ref, "drmInitData", "(Lcom/google/android/exoplayer/drm/DrmInitData;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (initData);
                JNIEnv.CallVoidMethod (Handle, id_drmInitData_Manual, __args);
            } finally {
            }
        }

        IntPtr id_seekMap_Manual;

        [Register ("seekMap", "(Lcom/google/android/exoplayer/extractor/SeekMap;)V", "SeekMapHandler")]
        public unsafe void SetSeekMap (global::Google.Android.ExoPlayer.Extractor.ISeekMap seekMap)
        {
            if (id_seekMap_Manual == IntPtr.Zero)
                id_seekMap_Manual = JNIEnv.GetMethodID (class_ref, "seekMap", "(Lcom/google/android/exoplayer/extractor/SeekMap;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (seekMap);
                JNIEnv.CallVoidMethod (Handle, id_seekMap_Manual, __args);
            } finally {
            }
        }

        IntPtr id_format_Manual;

        [Register ("format", "(Lcom/google/android/exoplayer/MediaFormat;)V", "FormatHandler")]
        public unsafe void SetFormat (global::Google.Android.ExoPlayer.MediaFormat mediaFormat)
        {
            if (id_format_Manual == IntPtr.Zero)
                id_format_Manual = JNIEnv.GetMethodID (class_ref, "format", "(Lcom/google/android/exoplayer/MediaFormat;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (mediaFormat);
                JNIEnv.CallVoidMethod (Handle, id_format_Manual, __args);
            } finally {
            }
        }
    }

    public partial class ChunkExtractorWrapper
    {
        IntPtr id_drmInitData_Manual;

        [Register ("drmInitData", "(Lcom/google/android/exoplayer/drm/DrmInitData;)V", "SetDrmInitDataHandler")]
        public unsafe void SetDrmInitData (global::Google.Android.ExoPlayer.Drm.IDrmInitData initData)
        {
            if (id_drmInitData_Manual == IntPtr.Zero)
                id_drmInitData_Manual = JNIEnv.GetMethodID (class_ref, "drmInitData", "(Lcom/google/android/exoplayer/drm/DrmInitData;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (initData);
                JNIEnv.CallVoidMethod (Handle, id_drmInitData_Manual, __args);
            } finally {
            }
        }

        IntPtr id_seekMap_Manual;

        [Register ("seekMap", "(Lcom/google/android/exoplayer/extractor/SeekMap;)V", "SeekMapHandler")]
        public unsafe void SetSeekMap (global::Google.Android.ExoPlayer.Extractor.ISeekMap seekMap)
        {
            if (id_seekMap_Manual == IntPtr.Zero)
                id_seekMap_Manual = JNIEnv.GetMethodID (class_ref, "seekMap", "(Lcom/google/android/exoplayer/extractor/SeekMap;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (seekMap);
                JNIEnv.CallVoidMethod (Handle, id_seekMap_Manual, __args);
            } finally {
            }
        }

        IntPtr id_format_Manual;

        [Register ("format", "(Lcom/google/android/exoplayer/MediaFormat;)V", "FormatHandler")]
        public unsafe void SetFormat (global::Google.Android.ExoPlayer.MediaFormat mediaFormat)
        {
            if (id_format_Manual == IntPtr.Zero)
                id_format_Manual = JNIEnv.GetMethodID (class_ref, "format", "(Lcom/google/android/exoplayer/MediaFormat;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (mediaFormat);
                JNIEnv.CallVoidMethod (Handle, id_format_Manual, __args);
            } finally {
            }
        }
    }
}


namespace Google.Android.ExoPlayer.Text.SubRip
{
    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.exoplayer.text.subrip']/class[@name='SubripParser']"
    public partial class SubripParser
    {
        static IntPtr id_parse_arrayBII;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.exoplayer.text.subrip']/class[@name='SubripParser']/method[@name='parse' and count(parameter)=3 and parameter[1][@type='byte[]'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
        [Register ("parse", "([BII)Lcom/google/android/exoplayer/text/subrip/SubripSubtitle;", "")]
        public unsafe global::Google.Android.ExoPlayer.Text.ISubtitle Parse (byte [] p0, int p1, int p2)
        {
            if (id_parse_arrayBII == IntPtr.Zero)
                id_parse_arrayBII = JNIEnv.GetMethodID (class_ref, "parse", "([BII)Lcom/google/android/exoplayer/text/subrip/SubripSubtitle;");
            IntPtr native_p0 = JNIEnv.NewArray (p0);
            try {
                JValue* __args = stackalloc JValue [3];
                __args [0] = new JValue (native_p0);
                __args [1] = new JValue (p1);
                __args [2] = new JValue (p2);
                global::Google.Android.ExoPlayer.Text.SubRip.SubripSubtitle __ret = global::Java.Lang.Object.GetObject<global::Google.Android.ExoPlayer.Text.SubRip.SubripSubtitle> (JNIEnv.CallObjectMethod (Handle, id_parse_arrayBII, __args), JniHandleOwnership.TransferLocalRef);
                return __ret;
            } finally {
                if (p0 != null) {
                    JNIEnv.CopyArray (native_p0, p0);
                    JNIEnv.DeleteLocalRef (native_p0);
                }
            }
        }
    }
}

namespace Google.Android.ExoPlayer.Text.WebVTT
{
    public partial class Mp4WebvttParser
    {
        static IntPtr id_parse_Manual;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.exoplayer.text.subrip']/class[@name='SubripParser']/method[@name='parse' and count(parameter)=3 and parameter[1][@type='byte[]'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
        [Register ("parse", "([BII)Lcom/google/android/exoplayer/text/webvtt/Mp4WebvttSubtitle;", "")]
        public unsafe global::Google.Android.ExoPlayer.Text.ISubtitle Parse (byte [] p0, int p1, int p2)
        {
            if (id_parse_Manual == IntPtr.Zero)
                id_parse_Manual = JNIEnv.GetMethodID (class_ref, "parse", "([BII)Lcom/google/android/exoplayer/text/webvtt/Mp4WebvttSubtitle;");
            IntPtr native_p0 = JNIEnv.NewArray (p0);
            try {
                JValue* __args = stackalloc JValue [3];
                __args [0] = new JValue (native_p0);
                __args [1] = new JValue (p1);
                __args [2] = new JValue (p2);
                var __ret = global::Java.Lang.Object.GetObject<global::Google.Android.ExoPlayer.Text.WebVTT.Mp4WebvttSubtitle> (JNIEnv.CallObjectMethod (Handle, id_parse_Manual, __args), JniHandleOwnership.TransferLocalRef);
                return __ret;
            } finally {
                if (p0 != null) {
                    JNIEnv.CopyArray (native_p0, p0);
                    JNIEnv.DeleteLocalRef (native_p0);
                }
            }
        }
    }
}

namespace Google.Android.ExoPlayer.Extractor
{

    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.exoplayer.extractor']/class[@name='DefaultTrackOutput']"
    public partial class DefaultTrackOutput
    {
        IntPtr id_format_Manual;

        [Register ("format", "(Lcom/google/android/exoplayer/MediaFormat;)V", "FormatHandler")]
        public unsafe void SetFormat (global::Google.Android.ExoPlayer.MediaFormat mediaFormat)
        {
            if (id_format_Manual == IntPtr.Zero)
                id_format_Manual = JNIEnv.GetMethodID (class_ref, "format", "(Lcom/google/android/exoplayer/MediaFormat;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (mediaFormat);
                JNIEnv.CallVoidMethod (Handle, id_format_Manual, __args);
            } finally {
            }
        }
    }

    public partial class DummyTrackOutput
    {
        IntPtr id_format_Manual;

        [Register ("format", "(Lcom/google/android/exoplayer/MediaFormat;)V", "FormatHandler")]
        public unsafe void SetFormat (global::Google.Android.ExoPlayer.MediaFormat mediaFormat)
        {
            if (id_format_Manual == IntPtr.Zero)
                id_format_Manual = JNIEnv.GetMethodID (class_ref, "format", "(Lcom/google/android/exoplayer/MediaFormat;)V");
            try {
                JValue* __args = stackalloc JValue [1];
                __args [0] = new JValue (mediaFormat);
                JNIEnv.CallVoidMethod (Handle, id_format_Manual, __args);
            } finally {
            }
        }
    }
}

namespace Google.Android.ExoPlayer.Upstream.Cache
{
    public sealed partial class CacheSpan
    {
        int Java.Lang.IComparable.CompareTo (Java.Lang.Object obj)
        {
            return CompareTo ((Google.Android.ExoPlayer.Upstream.Cache.CacheSpan)obj);
        }
    }

    public partial class LeastRecentlyUsedCacheEvictor
    {
        int Java.Util.IComparator.Compare (Java.Lang.Object lhs, Java.Lang.Object rhs)
        {
            return this.Compare ((global::Google.Android.ExoPlayer.Upstream.Cache.CacheSpan)lhs, (global::Google.Android.ExoPlayer.Upstream.Cache.CacheSpan)rhs);
        }
    }
}

namespace Google.Android.ExoPlayer.Extractor.HLS
{
    public partial class HlsMediaPlaylist
    {
        public sealed partial class Segment
        {
            int Java.Lang.IComparable.CompareTo (Java.Lang.Object obj)
            {
                return this.CompareTo ((Java.Lang.Long)obj);
            }
        }
    }
}

namespace Google.Android.ExoPlayer.Drm
{

    internal partial interface IExoMediaDrmOnEventListener<TT> : IExoMediaDrmOnEventListener
    {

    }
}
