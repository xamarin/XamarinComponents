using Android.Runtime;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeTen.BP
{
    public partial class ZonedDateTime
    {
        //    //// Metadata.xml XPath method reference: path="/api/package[@name='org.threeten.bp']/class[@name='ZonedDateTime']/method[@name='plus' and count(parameter)=2 and parameter[1][@type='long'] and parameter[2][@type='org.threeten.bp.temporal.TemporalUnit']]"
        //    //[Register("plus", "(JLorg/threeten/bp/temporal/TemporalUnit;)Lorg/threeten/bp/ZonedDateTime;", "")]
        //    //public unsafe global::ThreeTen.BP.Temporal.ITemporal Plus(long amountToAdd, global::ThreeTen.BP.Temporal.ITemporalUnit unit)
        //    //{
        //    //    const string __id = "plus.(JLorg/threeten/bp/temporal/TemporalUnit;)Lorg/threeten/bp/ZonedDateTime;";
        //    //    try
        //    //    {
        //    //        JniArgumentValue* __args = stackalloc JniArgumentValue[2];
        //    //        __args[0] = new JniArgumentValue(amountToAdd);
        //    //        __args[1] = new JniArgumentValue((unit == null) ? IntPtr.Zero : ((global::Java.Lang.Object)unit).Handle);
        //    //        var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
        //    //        return global::Java.Lang.Object.GetObject<global::ThreeTen.BP.Temporal.ITemporal>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
        //    //    }
        //    //    finally
        //    //    {
        //    //    }
        //    //}
        public override global::System.Int32 CompareTo(global::Java.Lang.Object o)
        {
            return o.GetHashCode().Equals(this.GetHashCode()) ? 0 : -1;
        }
        // Metadata.xml XPath method reference: path="/api/package[@name='org.threeten.bp']/class[@name='ZonedDateTime']/method[@name='with' and count(parameter)=2 and parameter[1][@type='org.threeten.bp.temporal.TemporalField'] and parameter[2][@type='long']]"
        [Register("with", "(Lorg/threeten/bp/temporal/TemporalField;J)Lorg/threeten/bp/ZonedDateTime;", "")]
        public unsafe override global::ThreeTen.BP.Temporal.ITemporal With(global::ThreeTen.BP.Temporal.ITemporalField field, long newValue)
        {
            const string __id = "with.(Lorg/threeten/bp/temporal/TemporalField;J)Lorg/threeten/bp/ZonedDateTime;";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                __args[0] = new JniArgumentValue((field == null) ? IntPtr.Zero : ((global::Java.Lang.Object)field).Handle);
                __args[1] = new JniArgumentValue(newValue);
                var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
                return global::Java.Lang.Object.GetObject<global::ThreeTen.BP.Temporal.ITemporal>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
            }
            finally
            {
            }
        }
    }
    public partial class OffsetDateTime
    {
        //// Metadata.xml XPath method reference: path="/api/package[@name='org.threeten.bp']/class[@name='OffsetDateTime']/method[@name='plus' and count(parameter)=2 and parameter[1][@type='long'] and parameter[2][@type='org.threeten.bp.temporal.TemporalUnit']]"
        //[Register("plus", "(JLorg/threeten/bp/temporal/TemporalUnit;)Lorg/threeten/bp/OffsetDateTime;", "")]
        //public override unsafe global::ThreeTen.BP.Temporal.ITemporal Plus(long amountToAdd, global::ThreeTen.BP.Temporal.ITemporalUnit unit)
        //{
        //    const string __id = "plus.(JLorg/threeten/bp/temporal/TemporalUnit;)Lorg/threeten/bp/OffsetDateTime;";
        //    try
        //    {
        //        JniArgumentValue* __args = stackalloc JniArgumentValue[2];
        //        __args[0] = new JniArgumentValue(amountToAdd);
        //        __args[1] = new JniArgumentValue((unit == null) ? IntPtr.Zero : ((global::Java.Lang.Object)unit).Handle);
        //        var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
        //        return global::Java.Lang.Object.GetObject<global::ThreeTen.BP.Temporal.ITemporal>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
        //    }
        //    finally
        //    {
        //    }
        //}


        // Metadata.xml XPath method reference: path="/api/package[@name='org.threeten.bp']/class[@name='OffsetDateTime']/method[@name='with' and count(parameter)=2 and parameter[1][@type='org.threeten.bp.temporal.TemporalField'] and parameter[2][@type='long']]"
        [Register("with", "(Lorg/threeten/bp/temporal/TemporalField;J)Lorg/threeten/bp/OffsetDateTime;", "")]
        public override unsafe global::ThreeTen.BP.Temporal.ITemporal With(global::ThreeTen.BP.Temporal.ITemporalField field, long newValue)
        {
            const string __id = "with.(Lorg/threeten/bp/temporal/TemporalField;J)Lorg/threeten/bp/OffsetDateTime;";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                __args[0] = new JniArgumentValue((field == null) ? IntPtr.Zero : ((global::Java.Lang.Object)field).Handle);
                __args[1] = new JniArgumentValue(newValue);
                var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
                return global::Java.Lang.Object.GetObject<global::ThreeTen.BP.Temporal.ITemporal>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
            }
            finally
            {
            }
        }

    }
}
