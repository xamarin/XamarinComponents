using System;
using Android.Runtime;

namespace Google.VR.VrCore.Nano
{
	public partial class SdkConfigurationSdkConfigurationParams
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vr.vrcore.nano']/class[@name='SdkConfiguration.SdkConfigurationParams']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vr/vrcore/nano/SdkConfiguration$SdkConfigurationParams;", "")]
		public unsafe new global::Google.VR.VrCore.Nano.SdkConfigurationSdkConfigurationParams Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vr/vrcore/nano/SdkConfiguration$SdkConfigurationParams;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VR.VrCore.Nano.SdkConfigurationSdkConfigurationParams>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class SdkConfigurationSdkConfigurationRequest
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vr.vrcore.nano']/class[@name='SdkConfiguration.SdkConfigurationRequest']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vr/vrcore/nano/SdkConfiguration$SdkConfigurationRequest;", "")]
		public unsafe new global::Google.VR.VrCore.Nano.SdkConfigurationSdkConfigurationRequest Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vr/vrcore/nano/SdkConfiguration$SdkConfigurationRequest;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VR.VrCore.Nano.SdkConfigurationSdkConfigurationRequest>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}
}

namespace Google.VR.VrCore.Proto.Nano
{

	public partial class NfcNfcParams
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vr.vrcore.proto.nano']/class[@name='Nfc.NfcParams']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vr/vrcore/proto/nano/Nfc$NfcParams;", "")]
		public unsafe new global::Google.VR.VrCore.Proto.Nano.NfcNfcParams Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vr/vrcore/proto/nano/Nfc$NfcParams;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VR.VrCore.Proto.Nano.NfcNfcParams>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}
}

namespace Google.VRToolkit.Cardboard.Proto.Nano
{
	public partial class CardboardDeviceCardboardInternalParams
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='CardboardDevice.CardboardInternalParams']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$CardboardInternalParams;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceCardboardInternalParams Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$CardboardInternalParams;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceCardboardInternalParams>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class CardboardDeviceDaydreamInternalParams
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='CardboardDevice.DaydreamInternalParams']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$DaydreamInternalParams;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceDaydreamInternalParams Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$DaydreamInternalParams;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceDaydreamInternalParams>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class CardboardDeviceDeviceParams
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='CardboardDevice.DeviceParams']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$DeviceParams;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceDeviceParams Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$DeviceParams;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceDeviceParams>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class CardboardDeviceScreenAlignmentMarker
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='CardboardDevice.ScreenAlignmentMarker']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$ScreenAlignmentMarker;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceScreenAlignmentMarker Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/CardboardDevice$ScreenAlignmentMarker;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.CardboardDeviceScreenAlignmentMarker>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class PhonePhoneParams
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='Phone.PhoneParams']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Phone$PhoneParams;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.PhonePhoneParams Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Phone$PhoneParams;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.PhonePhoneParams>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class PreferencesDeveloperPrefs
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='Preferences.DeveloperPrefs']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Preferences$DeveloperPrefs;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.PreferencesDeveloperPrefs Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Preferences$DeveloperPrefs;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.PreferencesDeveloperPrefs>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class PreferencesUserPrefs
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='Preferences.UserPrefs']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Preferences$UserPrefs;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.PreferencesUserPrefs Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Preferences$UserPrefs;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.PreferencesUserPrefs>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}

	public partial class SessionTrackerState
	{
		static IntPtr id_clone;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.vrtoolkit.cardboard.proto.nano']/class[@name='Session.TrackerState']/method[@name='clone' and count(parameter)=0]"
		[Register("clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Session$TrackerState;", "")]
		public unsafe new global::Google.VRToolkit.Cardboard.Proto.Nano.SessionTrackerState Clone()
		{
			if (id_clone == IntPtr.Zero)
				id_clone = JNIEnv.GetMethodID(class_ref, "clone", "()Lcom/google/vrtoolkit/cardboard/proto/nano/Session$TrackerState;");
			try
			{
				return global::Java.Lang.Object.GetObject<global::Google.VRToolkit.Cardboard.Proto.Nano.SessionTrackerState>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_clone), JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}
}