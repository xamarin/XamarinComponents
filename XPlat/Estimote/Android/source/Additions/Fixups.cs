using System;
using System.Collections.Generic;
using Android.Runtime;

namespace EstimoteMgmtSdk.Feature.Settings.Mapping
{
	public partial class Version
	{
		int global::Java.Lang.IComparable.CompareTo (global::Java.Lang.Object another)
		{
			return CompareTo (another.JavaCast<global::EstimoteMgmtSdk.Feature.Settings.Mapping.Version> ());
		}
	}
}

namespace EstimoteSdk.Observation.Region.Beacon
{
	public partial class BeaconRegionRanger
	{
		static Delegate cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_;
#pragma warning disable 0169
		static Delegate GetProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_Handler ()
		{
			if (cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ == null)
				cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>)n_ProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_);
			return cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_;
		}

		static void n_ProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ (IntPtr jnienv, IntPtr native__this, IntPtr native_singleScan, IntPtr native_messenger)
		{
			global::EstimoteSdk.Observation.Region.Beacon.BeaconRegionRanger __this = global::Java.Lang.Object.GetObject<global::EstimoteSdk.Observation.Region.Beacon.BeaconRegionRanger> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var singleScan = global::Android.Runtime.JavaList.FromJniHandle (native_singleScan, JniHandleOwnership.DoNotTransfer);
			global::EstimoteSdk.Service.IBeaconServiceMessenger messenger = (global::EstimoteSdk.Service.IBeaconServiceMessenger)global::Java.Lang.Object.GetObject<global::EstimoteSdk.Service.IBeaconServiceMessenger> (native_messenger, JniHandleOwnership.DoNotTransfer);
			__this.ProcessNewScanCycle (singleScan, messenger);
		}
#pragma warning restore 0169

		static IntPtr id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.estimote.coresdk.observation.region.beacon']/class[@name='BeaconRegionRanger']/method[@name='processNewScanCycle' and count(parameter)=2 and parameter[1][@type='java.util.List&lt;com.estimote.coresdk.recognition.packets.Beacon&gt;'] and parameter[2][@type='com.estimote.coresdk.service.BeaconServiceMessenger']]"
		[Register ("processNewScanCycle", "(Ljava/util/List;Lcom/estimote/coresdk/service/BeaconServiceMessenger;)V", "GetProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_Handler")]
		public virtual unsafe void ProcessNewScanCycle (global::System.Collections.IList singleScan, global::EstimoteSdk.Service.IBeaconServiceMessenger messenger)
		{
			if (id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ == IntPtr.Zero)
				id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ = JNIEnv.GetMethodID (class_ref, "processNewScanCycle", "(Ljava/util/List;Lcom/estimote/coresdk/service/BeaconServiceMessenger;)V");
			IntPtr native_singleScan = global::Android.Runtime.JavaList<global::EstimoteSdk.Recognition.Packets.Beacon>.ToLocalJniHandle (singleScan);
			try
			{
				JValue* __args = stackalloc JValue[2];
				__args[0] = new JValue (native_singleScan);
				__args[1] = new JValue (messenger);

				if (((object)this).GetType () == ThresholdType)
					JNIEnv.CallVoidMethod (((global::Java.Lang.Object)this).Handle, id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_, __args);
				else
					JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "processNewScanCycle", "(Ljava/util/List;Lcom/estimote/coresdk/service/BeaconServiceMessenger;)V"), __args);
			}
			finally
			{
				JNIEnv.DeleteLocalRef (native_singleScan);
			}
		}
	}

	public partial class BeaconRegionMonitor
	{
		static Delegate cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_;
#pragma warning disable 0169
		static Delegate GetProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_Handler ()
		{
			if (cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_ == null)
				cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>)n_ProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_);
			return cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_;
		}

		static void n_ProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_ (IntPtr jnienv, IntPtr native__this, IntPtr native_regionDecorator, IntPtr native_packets)
		{
			global::EstimoteSdk.Observation.Region.Beacon.BeaconRegionMonitor __this = global::Java.Lang.Object.GetObject<global::EstimoteSdk.Observation.Region.Beacon.BeaconRegionMonitor> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::EstimoteSdk.Observation.Region.Beacon.BeaconRegionDecorator regionDecorator = global::Java.Lang.Object.GetObject<global::EstimoteSdk.Observation.Region.Beacon.BeaconRegionDecorator> (native_regionDecorator, JniHandleOwnership.DoNotTransfer);
			var packets = global::Android.Runtime.JavaSet.FromJniHandle (native_packets, JniHandleOwnership.DoNotTransfer);
			__this.ProcessFoundPacketsInRegion (regionDecorator, packets);
		}
#pragma warning restore 0169

		static IntPtr id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.estimote.coresdk.observation.region.beacon']/class[@name='BeaconRegionMonitor']/method[@name='processFoundPacketsInRegion' and count(parameter)=2 and parameter[1][@type='com.estimote.coresdk.observation.region.beacon.BeaconRegionDecorator'] and parameter[2][@type='java.util.Set&lt;com.estimote.coresdk.recognition.packets.Beacon&gt;']]"
		[Register ("processFoundPacketsInRegion", "(Lcom/estimote/coresdk/observation/region/beacon/BeaconRegionDecorator;Ljava/util/Set;)V", "GetProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_Handler")]
		protected override unsafe void ProcessFoundPacketsInRegion (global::Java.Lang.Object regionDecorator, global::System.Collections.ICollection packets)
		{
			if (id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_ == IntPtr.Zero)
				id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_ = JNIEnv.GetMethodID (class_ref, "processFoundPacketsInRegion", "(Lcom/estimote/coresdk/observation/region/beacon/BeaconRegionDecorator;Ljava/util/Set;)V");
			IntPtr native_packets = global::Android.Runtime.JavaSet<global::EstimoteSdk.Recognition.Packets.Beacon>.ToLocalJniHandle (packets);
			try
			{
				JValue* __args = stackalloc JValue[2];
				__args[0] = new JValue (regionDecorator);
				__args[1] = new JValue (native_packets);

				if (((object)this).GetType () == ThresholdType)
					JNIEnv.CallVoidMethod (((global::Java.Lang.Object)this).Handle, id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_beacon_BeaconRegionDecorator_Ljava_util_Set_, __args);
				else
					JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "processFoundPacketsInRegion", "(Lcom/estimote/coresdk/observation/region/beacon/BeaconRegionDecorator;Ljava/util/Set;)V"), __args);
			}
			finally
			{
				JNIEnv.DeleteLocalRef (native_packets);
			}
		}
	}
}

namespace EstimoteSdk.Observation.Region.Mirror
{
	public partial class MirrorRegionRanger
	{
		static Delegate cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_;
#pragma warning disable 0169
		static Delegate GetProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_Handler ()
		{
			if (cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ == null)
				cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>)n_ProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_);
			return cb_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_;
		}

		static void n_ProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ (IntPtr jnienv, IntPtr native__this, IntPtr native_singleScan, IntPtr native_messenger)
		{
			global::EstimoteSdk.Observation.Region.Mirror.MirrorRegionRanger __this = global::Java.Lang.Object.GetObject<global::EstimoteSdk.Observation.Region.Mirror.MirrorRegionRanger> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var singleScan = global::Android.Runtime.JavaList.FromJniHandle (native_singleScan, JniHandleOwnership.DoNotTransfer);
			global::EstimoteSdk.Service.IBeaconServiceMessenger messenger = (global::EstimoteSdk.Service.IBeaconServiceMessenger)global::Java.Lang.Object.GetObject<global::EstimoteSdk.Service.IBeaconServiceMessenger> (native_messenger, JniHandleOwnership.DoNotTransfer);
			__this.ProcessNewScanCycle (singleScan, messenger);
		}
#pragma warning restore 0169

		static IntPtr id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.estimote.coresdk.observation.region.mirror']/class[@name='MirrorRegionRanger']/method[@name='processNewScanCycle' and count(parameter)=2 and parameter[1][@type='java.util.List&lt;com.estimote.coresdk.recognition.packets.Mirror&gt;'] and parameter[2][@type='com.estimote.coresdk.service.BeaconServiceMessenger']]"
		[Register ("processNewScanCycle", "(Ljava/util/List;Lcom/estimote/coresdk/service/BeaconServiceMessenger;)V", "GetProcessNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_Handler")]
		public virtual unsafe void ProcessNewScanCycle (global::System.Collections.IList singleScan, global::EstimoteSdk.Service.IBeaconServiceMessenger messenger)
		{
			if (id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ == IntPtr.Zero)
				id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_ = JNIEnv.GetMethodID (class_ref, "processNewScanCycle", "(Ljava/util/List;Lcom/estimote/coresdk/service/BeaconServiceMessenger;)V");
			IntPtr native_singleScan = global::Android.Runtime.JavaList<global::EstimoteSdk.Recognition.Packets.Mirror>.ToLocalJniHandle (singleScan);
			try
			{
				JValue* __args = stackalloc JValue[2];
				__args[0] = new JValue (native_singleScan);
				__args[1] = new JValue (messenger);

				if (((object)this).GetType () == ThresholdType)
					JNIEnv.CallVoidMethod (((global::Java.Lang.Object)this).Handle, id_processNewScanCycle_Ljava_util_List_Lcom_estimote_coresdk_service_BeaconServiceMessenger_, __args);
				else
					JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "processNewScanCycle", "(Ljava/util/List;Lcom/estimote/coresdk/service/BeaconServiceMessenger;)V"), __args);
			}
			finally
			{
				JNIEnv.DeleteLocalRef (native_singleScan);
			}
		}
	}

	public partial class MirrorRegionMonitor
	{
		static Delegate cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_;
#pragma warning disable 0169
		static Delegate GetProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_Handler ()
		{
			if (cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_ == null)
				cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>)n_ProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_);
			return cb_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_;
		}

		static void n_ProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_ (IntPtr jnienv, IntPtr native__this, IntPtr native_region, IntPtr native_packets)
		{
			global::EstimoteSdk.Observation.Region.Mirror.MirrorRegionMonitor __this = global::Java.Lang.Object.GetObject<global::EstimoteSdk.Observation.Region.Mirror.MirrorRegionMonitor> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::EstimoteSdk.Observation.Region.Mirror.MirrorRegionDecorator region = global::Java.Lang.Object.GetObject<global::EstimoteSdk.Observation.Region.Mirror.MirrorRegionDecorator> (native_region, JniHandleOwnership.DoNotTransfer);
			var packets = global::Android.Runtime.JavaSet.FromJniHandle (native_packets, JniHandleOwnership.DoNotTransfer);
			__this.ProcessFoundPacketsInRegion (region, packets);
		}
#pragma warning restore 0169

		static IntPtr id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.estimote.coresdk.observation.region.mirror']/class[@name='MirrorRegionMonitor']/method[@name='processFoundPacketsInRegion' and count(parameter)=2 and parameter[1][@type='com.estimote.coresdk.observation.region.mirror.MirrorRegionDecorator'] and parameter[2][@type='java.util.Set&lt;com.estimote.coresdk.recognition.packets.Mirror&gt;']]"
		[Register ("processFoundPacketsInRegion", "(Lcom/estimote/coresdk/observation/region/mirror/MirrorRegionDecorator;Ljava/util/Set;)V", "GetProcessFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_Handler")]
		protected override unsafe void ProcessFoundPacketsInRegion (global::Java.Lang.Object region, global::System.Collections.ICollection packets)
		{
			if (id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_ == IntPtr.Zero)
				id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_ = JNIEnv.GetMethodID (class_ref, "processFoundPacketsInRegion", "(Lcom/estimote/coresdk/observation/region/mirror/MirrorRegionDecorator;Ljava/util/Set;)V");
			IntPtr native_packets = global::Android.Runtime.JavaSet<global::EstimoteSdk.Recognition.Packets.Mirror>.ToLocalJniHandle (packets);
			try
			{
				JValue* __args = stackalloc JValue[2];
				__args[0] = new JValue (region);
				__args[1] = new JValue (native_packets);

				if (((object)this).GetType () == ThresholdType)
					JNIEnv.CallVoidMethod (((global::Java.Lang.Object)this).Handle, id_processFoundPacketsInRegion_Lcom_estimote_coresdk_observation_region_mirror_MirrorRegionDecorator_Ljava_util_Set_, __args);
				else
					JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "processFoundPacketsInRegion", "(Lcom/estimote/coresdk/observation/region/mirror/MirrorRegionDecorator;Ljava/util/Set;)V"), __args);
			}
			finally
			{
				JNIEnv.DeleteLocalRef (native_packets);
			}
		}
	}
}