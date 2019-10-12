using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Kotlin.Time
{
	// TODO: WORKAROUND FOR https://github.com/xamarin/java.interop/issues/496
	partial class ClockMark
	{
		static Delegate cb_minus;
#pragma warning disable 0169
		static Delegate GetMinus_DHandler()
		{
			if (cb_minus == null)
				cb_minus = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, double, IntPtr>)n_Minus_D);
			return cb_minus;
		}

		static IntPtr n_Minus_D(IntPtr jnienv, IntPtr native__this, double p0)
		{
			global::Kotlin.Time.ClockMark __this = global::Java.Lang.Object.GetObject<global::Kotlin.Time.ClockMark>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle(__this.Minus(p0));
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='kotlin.time']/class[@name='ClockMark']/method[@name='minus-LRDsOJo' and count(parameter)=1 and parameter[1][@type='double']]"
		[Register("minus-LRDsOJo", "(D)Lkotlin/time/ClockMark;", "GetMinus_DHandler")]
		public virtual unsafe global::Kotlin.Time.ClockMark Minus(double p0)
		{
			const string __id = "minus-LRDsOJo.(D)Lkotlin/time/ClockMark;";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
				__args[0] = new JniArgumentValue(p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Kotlin.Time.ClockMark>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}

		static Delegate cb_plus;
#pragma warning disable 0169
		static Delegate GetPlus_DHandler()
		{
			if (cb_plus == null)
				cb_plus = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, double, IntPtr>)n_Plus_D);
			return cb_plus;
		}

		static IntPtr n_Plus_D(IntPtr jnienv, IntPtr native__this, double p0)
		{
			global::Kotlin.Time.ClockMark __this = global::Java.Lang.Object.GetObject<global::Kotlin.Time.ClockMark>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle(__this.Plus(p0));
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='kotlin.time']/class[@name='ClockMark']/method[@name='plus-LRDsOJo' and count(parameter)=1 and parameter[1][@type='double']]"
		[Register("plus-LRDsOJo", "(D)Lkotlin/time/ClockMark;", "GetPlus_DHandler")]
		public virtual unsafe global::Kotlin.Time.ClockMark Plus(double p0)
		{
			const string __id = "plus-LRDsOJo.(D)Lkotlin/time/ClockMark;";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
				__args[0] = new JniArgumentValue(p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Kotlin.Time.ClockMark>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}
}
