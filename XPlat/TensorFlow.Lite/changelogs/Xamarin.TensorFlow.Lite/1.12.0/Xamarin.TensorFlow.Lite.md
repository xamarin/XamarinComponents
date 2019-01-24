# API diff: Xamarin.TensorFlow.Lite.dll

## Xamarin.TensorFlow.Lite.dll

### Namespace Xamarin.TensorFlow.Lite

#### Type Changed: Xamarin.TensorFlow.Lite.Interpreter

Removed constructor:

```csharp
public Interpreter (Java.Nio.MappedByteBuffer mappedByteBuffer, int numThreads);
```

Obsoleted constructors:

```diff
 [Obsolete ()]
 public Interpreter (Java.Nio.MappedByteBuffer mappedByteBuffer);
 [Obsolete ()]
 public Interpreter (Java.IO.File modelFile, int numThreads);
 [Obsolete ()]
 public Interpreter (Java.Nio.ByteBuffer byteBuffer, int numThreads);
```

Added constructors:

```csharp
public Interpreter (Java.IO.File modelFile, Interpreter.Options options);
public Interpreter (Java.Nio.ByteBuffer byteBuffer, Interpreter.Options options);
```

Added properties:

```csharp
public int InputTensorCount { get; }
public int OutputTensorCount { get; }
```

Obsoleted methods:

```diff
 [Obsolete ()]
 public void SetNumThreads (int numThreads);
 [Obsolete ()]
 public void SetUseNNAPI (bool useNNAPI);
```

Added methods:

```csharp
public Tensor GetInputTensor (int inputIndex);
public Tensor GetOutputTensor (int outputIndex);
```


#### New Type: Xamarin.TensorFlow.Lite.DataType

```csharp
public sealed class DataType : Java.Lang.Enum {
	// properties
	public static DataType Float32 { get; }
	public static DataType Int32 { get; }
	public static DataType Int64 { get; }
	public override Java.Interop.JniPeerMembers JniPeerMembers { get; }
	protected override IntPtr ThresholdClass { get; }
	protected override System.Type ThresholdType { get; }
	public static DataType Uint8 { get; }
	// methods
	public int ByteSize ();
	public static DataType ValueOf (string name);
	public static DataType[] Values ();
}
```

#### New Type: Xamarin.TensorFlow.Lite.Tensor

```csharp
public sealed class Tensor : Java.Lang.Object {
	// properties
	public override Java.Interop.JniPeerMembers JniPeerMembers { get; }
	protected override IntPtr ThresholdClass { get; }
	protected override System.Type ThresholdType { get; }
	// methods
	public DataType DataType ();
	public int NumBytes ();
	public int NumDimensions ();
	public int NumElements ();
	public int[] Shape ();
}
```


