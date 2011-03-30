using System.Runtime.Serialization;

namespace RazorGrid
{
    [DataContract]
    public class ColumnModel
    {
        [DataMember(Name = "index")] public string Index;
        [DataMember(Name = "name")] public string Name;
        [DataMember(Name = "width")] public int Width;

        [DataMember(Name = "sortable")]
        public bool Sortable { get; set; }
    }
}