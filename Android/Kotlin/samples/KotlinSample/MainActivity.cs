using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using Java.Util;

using Kotlin;
using Kotlin.Jvm;
using Kotlin.Jvm.Internal;
using Kotlin.Reflect.Full;
using KotlinSampleLibrary;

namespace KotlinSample
{
	[Activity(Label = "KotlinSample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			var textView = FindViewById<TextView>(Resource.Id.textView);

			// test the sample bindings work

			var instance = new TestClass();
			var list = instance.Test(new[] { "first", "second", "third" });
			textView.Text = $"There are {list.Count} items.\n\n";

			// test the magic for collections and related

			ICollection c = new Coll();
			IList l = new List();
			IMap m = new Map();
			ISet s = new Set();
			textView.Text += $"Collection: {s.Size()} = {s.Contains("one")}; {c.Size()} = {c.Contains("one")}; {l.Size()} = {l.Get(1)}; {m.Size()} = {m.Get(1)}.\n\n";

			// test the magic for the spread builders

			var spreadBuilder = new BooleanSpreadBuilder(10);
			var array = spreadBuilder.ToArray();
			textView.Text += $"\nArray size from spread builder: {array.Length}.\n\n";

			// test the magic for the reflection things

			var javaClass = Java.Lang.Class.FromType(typeof(TestClass));
			var kotlinClass = JvmClassMappingKt.GetKotlinClass(javaClass);
			var members = kotlinClass.Members;
			textView.Text += $"\nThere are {members.Count} members in TestClass.\n\n";

			var properties = KClasses.GetMemberProperties(kotlinClass);
			var firstProp = properties.FirstOrDefault();
			textView.Text += $"\nThere are {properties.Count} properties in TestClass, the first is {firstProp.Name}: {firstProp.ReturnType}.\n\n";


            // test is Event interface
            for (int i = 0; i < 5; i++)
			{
				textView.Text += $"{instance.TestIsEvent(3*i)}\n";
			}

			// test bit operations from kotlin 1.4
			textView.Text += instance.TestBitOperations();
		}

}

	public class Set : Kotlin.Collections.AbstractMutableSet
	{
		private readonly JavaList<Java.Lang.Object> list = new JavaList<Java.Lang.Object> { "one", "two", "three" };

		public override bool Add(Java.Lang.Object p0)
		{
			list.Add(p0);
			return true;
		}

		public override int GetSize() => list.Size();

		public override IIterator Iterator() => list.Iterator();
	}

	public class Coll : Kotlin.Collections.AbstractMutableCollection
	{
		private readonly JavaList<Java.Lang.Object> list = new JavaList<Java.Lang.Object> { "one", "two", "three" };

		public override bool Add(Java.Lang.Object p0)
		{
			list.Add(p0);
			return true;
		}

		public override int GetSize() => list.Size();

		public override IIterator Iterator() => list.Iterator();
	}

	public class List : Kotlin.Collections.AbstractMutableList
	{
		private readonly JavaList<Java.Lang.Object> list = new JavaList<Java.Lang.Object> { "one", "two", "three" };

		public override void Add(int p0, Java.Lang.Object p1) => list.Add(p0, p1);

		public override Java.Lang.Object Get(int index) => list[index];

		public override int GetSize() => list.Size();

		public override IIterator Iterator() => list.Iterator();

		public override Java.Lang.Object RemoveAt(int p0) => list.Remove(p0);

		public override Java.Lang.Object Set(int p0, Java.Lang.Object p1) => list.Set(p0, p1);
	}

	public class Map : Kotlin.Collections.AbstractMap
	{
		private readonly JavaDictionary<Java.Lang.Object, Java.Lang.Object> list = new JavaDictionary<Java.Lang.Object, Java.Lang.Object>
		{
			{ 1, "one" },
			{ 2, "two" },
			{ 3, "three" }
		};

		public override System.Collections.ICollection Entries => list.Select(i => new Entry(i)).ToArray();

		public class Entry : Java.Lang.Object, IMapEntry
		{
			private (Java.Lang.Object Key, Java.Lang.Object Value) i;

			public Entry(KeyValuePair<Java.Lang.Object, Java.Lang.Object> i)
			{
				this.i = (i.Key, i.Value);
			}

			public Java.Lang.Object Key => i.Key;

			public Java.Lang.Object Value => i.Value;

			public Java.Lang.Object SetValue(Java.Lang.Object value)
			{
				var old = i.Value;
				i = (i.Key, value);
				return old;
			}
		}
	}
}
