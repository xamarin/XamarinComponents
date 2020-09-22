namespace Square.OkIO
{
    partial class Options
    {
        public unsafe ByteString GetOption(int i) => (ByteString)Get(i);

        public bool Remove(Java.Lang.Object o) => base.Remove(o);

        public ByteString RemoveAt(int index)
        {
            var o = GetOption(index);
            if (o != null)
                Remove(o);
            return o;
        }

        public int Size() => base.Size();
    }
}
