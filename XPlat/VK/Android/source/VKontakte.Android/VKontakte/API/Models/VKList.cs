using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android.Runtime;
using Android.Views;

namespace VKontakte.API.Models
{
	[Register ("com/vk/sdk/api/model/VKList", DoNotGenerateAcw = true)]
	public class VKList<T> : VKList, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		where T : Java.Lang.Object
	{
		[Register (".ctor", "()V", "")]
		public VKList ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (base.Handle != IntPtr.Zero) {
				return;
			}
			if (base.GetType () == typeof(VKList<T>)) {
				base.SetHandle (JNIEnv.StartCreateInstance ("com/vk/sdk/api/model/VKList", "()V", new JValue[0]), JniHandleOwnership.TransferLocalRef);
			} else {
				base.SetHandle (JNIEnv.StartCreateInstance (base.GetType (), "()V", new JValue[0]), JniHandleOwnership.TransferLocalRef);
			}
			JNIEnv.FinishCreateInstance (base.Handle, "()V", new JValue[0]);
		}

		public VKList (IntPtr handle, JniHandleOwnership transfer)
			: base (handle, transfer)
		{
		}

		public VKList (IEnumerable<T> items)
			: this ()
		{
			if (items == null) {
				base.Dispose ();
				throw new ArgumentNullException ("items");
			}
			foreach (T current in items) {
				this.Add (current);
			}
		}


		public T this [int index] {
			get {
				return Get (base.Get (index));
			}
			set {
				base.Set (index, value);
			}
		}

		public new T GetById (int id)
		{
			return Get (base.GetById (id));
		}

		public void Add (T item)
		{
			base.Add (item);
		}

		public void AddRange (IEnumerable<T> items)
		{
			foreach (T current in items) {
				this.Add (current);
			}
		}

		public bool Contains (T item)
		{
			return base.Contains (item);
		}

		public void CopyTo (T[] array, int array_index)
		{
			if (array == null) {
				throw new ArgumentNullException ("array");
			}
			if (array_index < 0) {
				throw new ArgumentOutOfRangeException ("array_index");
			}
			if (array.Length < array_index + base.Count) {
				throw new ArgumentException ("array");
			}
			for (int i = 0; i < base.Count; i++) {
				array [array_index + i] = this [i];
			}
		}

		public IEnumerator<T> GetEnumerator ()
		{
			return AsEnumerable ().GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator ();
		}

		public IEnumerable<T> AsEnumerable ()
		{
			var iterator = base.Iterator ();
			while (iterator.HasNext) {
				yield return Get (iterator.Next ());
			}
		}

		public int IndexOf (T item)
		{
			return base.IndexOf (item);
		}

		public void Insert (int index, T item)
		{
			base.Add (index, item);
		}

		public bool Remove (T item)
		{
			int num = this.IndexOf (item);
			if (num < 0 && num >= base.Count) {
				return false;
			}
			base.Remove (num);
			return true;
		}

		public void RemoveAt (int index)
		{
			base.Remove (index);
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		[Preserve (Conditional = true)]
		public static IntPtr ToLocalJniHandle (IList<T> items)
		{
			if (items == null) {
				return IntPtr.Zero;
			}
			VKList<T> vkList = items as VKList<T>;
			if (vkList != null) {
				return JNIEnv.ToLocalJniHandle (vkList);
			}
			VKList<T> vkList2;
			vkList = (vkList2 = new VKList<T> (items));
			IntPtr result;
			try {
				result = JNIEnv.ToLocalJniHandle (vkList);
			} finally {
				if (vkList2 != null) {
					((IDisposable)vkList2).Dispose ();
				}
			}
			return result;
		}

		private static T Get (Java.Lang.Object value)
		{
			if (value == null) {
				return null;
			}
			return value.JavaCast<T> ();
		}

		private static void DeleteRef (IntPtr handle, JniHandleOwnership transfer)
		{
			if (transfer == JniHandleOwnership.TransferLocalRef) {
				JNIEnv.DeleteLocalRef (handle);
			} else if (transfer == JniHandleOwnership.TransferGlobalRef) {
				JNIEnv.DeleteGlobalRef (handle);
			}
		}
	}
}
