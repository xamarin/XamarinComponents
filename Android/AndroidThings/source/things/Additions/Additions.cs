using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Android.Things.Pio
{

    [Register("com/google/android/things/pio/SpiDevice", DoNotGenerateAcw = true)]
    public abstract class SpiDevice : Java.Lang.Object
    {

        internal SpiDevice()
        {
        }

        // Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/field[@name='BIT_JUSTIFICATION_LSB_FIRST']"
        [Register("BIT_JUSTIFICATION_LSB_FIRST")]
        public const int BitJustificationLsbFirst = (int)1;

        // Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/field[@name='BIT_JUSTIFICATION_MSB_FIRST']"
        [Register("BIT_JUSTIFICATION_MSB_FIRST")]
        public const int BitJustificationMsbFirst = (int)0;

        // Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/field[@name='MODE0']"
        [Register("MODE0")]
        public const int Mode0 = (int)0;

        // Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/field[@name='MODE1']"
        [Register("MODE1")]
        public const int Mode1 = (int)1;

        // Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/field[@name='MODE2']"
        [Register("MODE2")]
        public const int Mode2 = (int)2;

        // Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/field[@name='MODE3']"
        [Register("MODE3")]
        public const int Mode3 = (int)3;

        // The following are fields from: java.io.Closeable

        // The following are fields from: Android.Runtime.IJavaObject

        // The following are fields from: System.IDisposable
    }

    [Register("com/google/android/things/pio/SpiDevice", DoNotGenerateAcw = true)]
    [global::System.Obsolete("Use the 'SpiDevice' type. This type will be removed in a future release.")]
    public abstract class SpiDeviceConsts : SpiDevice
    {

        private SpiDeviceConsts()
        {
        }
    }

    // Metadata.xml XPath interface reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']"
    [Register("com/google/android/things/pio/SpiDevice", "", "Android.Things.Pio.ISpiDeviceInvoker")]
    public partial interface ISpiDevice : global::Java.IO.ICloseable
    {

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='close' and count(parameter)=0]"
        [Register("close", "()V", "GetCloseHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void Close();

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='setBitJustification' and count(parameter)=1 and parameter[1][@type='int']]"
        [Register("setBitJustification", "(I)V", "GetSetBitJustification_IHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void SetBitJustification(int justification);

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='setBitsPerWord' and count(parameter)=1 and parameter[1][@type='int']]"
        [Register("setBitsPerWord", "(I)V", "GetSetBitsPerWord_IHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void SetBitsPerWord(int bitsPerWord);

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='setCsChange' and count(parameter)=1 and parameter[1][@type='boolean']]"
        [Register("setCsChange", "(Z)V", "GetSetCsChange_ZHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void SetCsChange(bool change);

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='setDelay' and count(parameter)=1 and parameter[1][@type='int']]"
        [Register("setDelay", "(I)V", "GetSetDelay_IHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void SetDelay(int delayUs);

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='setFrequency' and count(parameter)=1 and parameter[1][@type='int']]"
        [Register("setFrequency", "(I)V", "GetSetFrequency_IHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void SetFrequency(int frequencyHz);

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='setMode' and count(parameter)=1 and parameter[1][@type='int']]"
        [Register("setMode", "(I)V", "GetSetMode_IHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void SetMode(int mode);

        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.things.pio']/interface[@name='SpiDevice']/method[@name='transfer' and count(parameter)=3 and parameter[1][@type='byte[]'] and parameter[2][@type='byte[]'] and parameter[3][@type='int']]"
        [Register("transfer", "([B[BI)V", "GetTransfer_arrayBarrayBIHandler:Android.Things.Pio.ISpiDeviceInvoker, Xamarin.Android.Things")]
        void Transfer(byte[] txBuffer, byte[] rxBuffer, int length);

        void Write(byte[] buffer, int length);

        void Read(byte[] buffer, int length);

    }

    [global::Android.Runtime.Register("com/google/android/things/pio/SpiDevice", DoNotGenerateAcw = true)]
    internal class ISpiDeviceInvoker : global::Java.Lang.Object, ISpiDevice
    {

        internal new static readonly JniPeerMembers _members = new JniPeerMembers("com/google/android/things/pio/SpiDevice", typeof(ISpiDeviceInvoker));

        static IntPtr java_class_ref
        {
            get { return _members.JniPeerType.PeerReference.Handle; }
        }

        public override global::Java.Interop.JniPeerMembers JniPeerMembers
        {
            get { return _members; }
        }

        protected override IntPtr ThresholdClass
        {
            get { return class_ref; }
        }

        protected override global::System.Type ThresholdType
        {
            get { return _members.ManagedPeerType; }
        }

        IntPtr class_ref;

        public static ISpiDevice GetObject(IntPtr handle, JniHandleOwnership transfer)
        {
            return global::Java.Lang.Object.GetObject<ISpiDevice>(handle, transfer);
        }

        static IntPtr Validate(IntPtr handle)
        {
            if (!JNIEnv.IsInstanceOf(handle, java_class_ref))
                throw new InvalidCastException(string.Format("Unable to convert instance of type '{0}' to type '{1}'.",
                            JNIEnv.GetClassNameFromInstance(handle), "com.google.android.things.pio.SpiDevice"));
            return handle;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.class_ref != IntPtr.Zero)
                JNIEnv.DeleteGlobalRef(this.class_ref);
            this.class_ref = IntPtr.Zero;
            base.Dispose(disposing);
        }

        public ISpiDeviceInvoker(IntPtr handle, JniHandleOwnership transfer) : base(Validate(handle), transfer)
        {
            IntPtr local_ref = JNIEnv.GetObjectClass(((global::Java.Lang.Object)this).Handle);
            this.class_ref = JNIEnv.NewGlobalRef(local_ref);
            JNIEnv.DeleteLocalRef(local_ref);
        }

        static Delegate cb_close;
#pragma warning disable 0169
        static Delegate GetCloseHandler()
        {
            if (cb_close == null)
                cb_close = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr>)n_Close);
            return cb_close;
        }

        static void n_Close(IntPtr jnienv, IntPtr native__this)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.Close();
        }
#pragma warning restore 0169

        IntPtr id_close;
        public unsafe void Close()
        {
            if (id_close == IntPtr.Zero)
                id_close = JNIEnv.GetMethodID(class_ref, "close", "()V");
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_close);
        }

        static Delegate cb_setBitJustification_I;
#pragma warning disable 0169
        static Delegate GetSetBitJustification_IHandler()
        {
            if (cb_setBitJustification_I == null)
                cb_setBitJustification_I = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, int>)n_SetBitJustification_I);
            return cb_setBitJustification_I;
        }

        static void n_SetBitJustification_I(IntPtr jnienv, IntPtr native__this, int justification)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetBitJustification(justification);
        }
#pragma warning restore 0169

        IntPtr id_setBitJustification_I;
        public unsafe void SetBitJustification(int justification)
        {
            if (id_setBitJustification_I == IntPtr.Zero)
                id_setBitJustification_I = JNIEnv.GetMethodID(class_ref, "setBitJustification", "(I)V");
            JValue* __args = stackalloc JValue[1];
            __args[0] = new JValue(justification);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setBitJustification_I, __args);
        }

        static Delegate cb_setBitsPerWord_I;
#pragma warning disable 0169
        static Delegate GetSetBitsPerWord_IHandler()
        {
            if (cb_setBitsPerWord_I == null)
                cb_setBitsPerWord_I = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, int>)n_SetBitsPerWord_I);
            return cb_setBitsPerWord_I;
        }

        static void n_SetBitsPerWord_I(IntPtr jnienv, IntPtr native__this, int bitsPerWord)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetBitsPerWord(bitsPerWord);
        }
#pragma warning restore 0169

        IntPtr id_setBitsPerWord_I;
        public unsafe void SetBitsPerWord(int bitsPerWord)
        {
            if (id_setBitsPerWord_I == IntPtr.Zero)
                id_setBitsPerWord_I = JNIEnv.GetMethodID(class_ref, "setBitsPerWord", "(I)V");
            JValue* __args = stackalloc JValue[1];
            __args[0] = new JValue(bitsPerWord);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setBitsPerWord_I, __args);
        }

        static Delegate cb_setCsChange_Z;
#pragma warning disable 0169
        static Delegate GetSetCsChange_ZHandler()
        {
            if (cb_setCsChange_Z == null)
                cb_setCsChange_Z = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, bool>)n_SetCsChange_Z);
            return cb_setCsChange_Z;
        }

        static void n_SetCsChange_Z(IntPtr jnienv, IntPtr native__this, bool change)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetCsChange(change);
        }
#pragma warning restore 0169

        IntPtr id_setCsChange_Z;
        public unsafe void SetCsChange(bool change)
        {
            if (id_setCsChange_Z == IntPtr.Zero)
                id_setCsChange_Z = JNIEnv.GetMethodID(class_ref, "setCsChange", "(Z)V");
            JValue* __args = stackalloc JValue[1];
            __args[0] = new JValue(change);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setCsChange_Z, __args);
        }

        static Delegate cb_setDelay_I;
#pragma warning disable 0169
        static Delegate GetSetDelay_IHandler()
        {
            if (cb_setDelay_I == null)
                cb_setDelay_I = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, int>)n_SetDelay_I);
            return cb_setDelay_I;
        }

        static void n_SetDelay_I(IntPtr jnienv, IntPtr native__this, int delayUs)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetDelay(delayUs);
        }
#pragma warning restore 0169

        IntPtr id_setDelay_I;
        public unsafe void SetDelay(int delayUs)
        {
            if (id_setDelay_I == IntPtr.Zero)
                id_setDelay_I = JNIEnv.GetMethodID(class_ref, "setDelay", "(I)V");
            JValue* __args = stackalloc JValue[1];
            __args[0] = new JValue(delayUs);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setDelay_I, __args);
        }

        static Delegate cb_setFrequency_I;
#pragma warning disable 0169
        static Delegate GetSetFrequency_IHandler()
        {
            if (cb_setFrequency_I == null)
                cb_setFrequency_I = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, int>)n_SetFrequency_I);
            return cb_setFrequency_I;
        }

        static void n_SetFrequency_I(IntPtr jnienv, IntPtr native__this, int frequencyHz)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetFrequency(frequencyHz);
        }
#pragma warning restore 0169

        IntPtr id_setFrequency_I;
        public unsafe void SetFrequency(int frequencyHz)
        {
            if (id_setFrequency_I == IntPtr.Zero)
                id_setFrequency_I = JNIEnv.GetMethodID(class_ref, "setFrequency", "(I)V");
            JValue* __args = stackalloc JValue[1];
            __args[0] = new JValue(frequencyHz);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setFrequency_I, __args);
        }

        static Delegate cb_setMode_I;
#pragma warning disable 0169
        static Delegate GetSetMode_IHandler()
        {
            if (cb_setMode_I == null)
                cb_setMode_I = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, int>)n_SetMode_I);
            return cb_setMode_I;
        }

        static void n_SetMode_I(IntPtr jnienv, IntPtr native__this, int mode)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            __this.SetMode(mode);
        }
#pragma warning restore 0169

        IntPtr id_setMode_I;
        public unsafe void SetMode(int mode)
        {
            if (id_setMode_I == IntPtr.Zero)
                id_setMode_I = JNIEnv.GetMethodID(class_ref, "setMode", "(I)V");
            JValue* __args = stackalloc JValue[1];
            __args[0] = new JValue(mode);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setMode_I, __args);
        }

        static Delegate cb_transfer_arrayBarrayBI;
#pragma warning disable 0169
        static Delegate GetTransfer_arrayBarrayBIHandler()
        {
            if (cb_transfer_arrayBarrayBI == null)
                cb_transfer_arrayBarrayBI = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, IntPtr, IntPtr, int>)n_Transfer_arrayBarrayBI);
            return cb_transfer_arrayBarrayBI;
        }

        static void n_Transfer_arrayBarrayBI(IntPtr jnienv, IntPtr native__this, IntPtr native_txBuffer, IntPtr native_rxBuffer, int length)
        {
            global::Android.Things.Pio.ISpiDevice __this = global::Java.Lang.Object.GetObject<global::Android.Things.Pio.ISpiDevice>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            byte[] txBuffer = (byte[])JNIEnv.GetArray(native_txBuffer, JniHandleOwnership.DoNotTransfer, typeof(byte));
            byte[] rxBuffer = (byte[])JNIEnv.GetArray(native_rxBuffer, JniHandleOwnership.DoNotTransfer, typeof(byte));
            __this.Transfer(txBuffer, rxBuffer, length);
            if (txBuffer != null)
                JNIEnv.CopyArray(txBuffer, native_txBuffer);
            if (rxBuffer != null)
                JNIEnv.CopyArray(rxBuffer, native_rxBuffer);
        }
#pragma warning restore 0169

        IntPtr id_transfer_arrayBarrayBI;
        public unsafe void Transfer(byte[] txBuffer, byte[] rxBuffer, int length)
        {
            if (id_transfer_arrayBarrayBI == IntPtr.Zero)
                id_transfer_arrayBarrayBI = JNIEnv.GetMethodID(class_ref, "transfer", "([B[BI)V");
            IntPtr native_txBuffer = JNIEnv.NewArray(txBuffer);
            IntPtr native_rxBuffer = JNIEnv.NewArray(rxBuffer);
            JValue* __args = stackalloc JValue[3];
            __args[0] = new JValue(native_txBuffer);
            __args[1] = new JValue(native_rxBuffer);
            __args[2] = new JValue(length);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_transfer_arrayBarrayBI, __args);
            if (txBuffer != null)
            {
                JNIEnv.CopyArray(native_txBuffer, txBuffer);
                JNIEnv.DeleteLocalRef(native_txBuffer);
            }
            if (rxBuffer != null)
            {
                JNIEnv.CopyArray(native_rxBuffer, rxBuffer);
                JNIEnv.DeleteLocalRef(native_rxBuffer);
            }
        }

        public void Write(byte[] buffer, int length)
        {
            throw new NotImplementedException();
        }

        public void Read(byte[] buffer, int length)
        {
            throw new NotImplementedException();
        }
    }

}
