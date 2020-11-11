using Java.Nio.Charset;

namespace Square.OkIO
{
    partial class OkBuffer
    {
        IBufferedSink IBufferedSink.Emit() => Emit();

        IBufferedSink IBufferedSink.EmitCompleteSegments() => EmitCompleteSegments();

        IBufferedSink IBufferedSink.Write(ISource source, long byteCount) => Write(source, byteCount);

        IBufferedSink IBufferedSink.Write(byte[] source) => Write(source);

        IBufferedSink IBufferedSink.Write(byte[] source, int offset, int byteCount) => Write(source, offset, byteCount);

        IBufferedSink IBufferedSink.Write(ByteString byteString) => Write(byteString);

        IBufferedSink IBufferedSink.Write(ByteString byteString, int offset, int byteCount) => Write(byteString, offset, byteCount);

        IBufferedSink IBufferedSink.WriteByte(int b) => WriteByte(b);

        IBufferedSink IBufferedSink.WriteDecimalLong(long v) => WriteDecimalLong(v);

        IBufferedSink IBufferedSink.WriteHexadecimalUnsignedLong(long v) => WriteHexadecimalUnsignedLong(v);

        IBufferedSink IBufferedSink.WriteInt(int i) => WriteInt(i);

        IBufferedSink IBufferedSink.WriteIntLe(int i) => WriteIntLe(i);

        IBufferedSink IBufferedSink.WriteLong(long v) => WriteLong(v);

        IBufferedSink IBufferedSink.WriteLongLe(long v) => WriteLongLe(v);

        IBufferedSink IBufferedSink.WriteShort(int s) => WriteShort(s);

        IBufferedSink IBufferedSink.WriteShortLe(int s) => WriteShortLe(s);

        IBufferedSink IBufferedSink.WriteString(string str, int beginIndex, int endIndex, Charset charset) => WriteString(str, beginIndex, endIndex, charset);

        IBufferedSink IBufferedSink.WriteString(string str, Charset charset) => WriteString(str, charset);

        IBufferedSink IBufferedSink.WriteUtf8(string str) => WriteUtf8(str);

        IBufferedSink IBufferedSink.WriteUtf8(string str, int beginIndex, int endIndex) => WriteUtf8(str, beginIndex, endIndex);

        IBufferedSink IBufferedSink.WriteUtf8CodePoint(int codePoint) => WriteUtf8CodePoint(codePoint);
    }
}
