namespace HelloAreaDescription
{
    /// <summary>
    /// Contains an ADF Name and its UUID.
    /// </summary>
    public class AdfData
    {
        public AdfData(string uuid, string name)
        {
            Uuid = uuid;
            Name = name;
        }

        public string Uuid { get; set; }

        public string Name { get; set; }
    }
}
