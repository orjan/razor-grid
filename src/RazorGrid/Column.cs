using System;

namespace RazorGrid
{
    public class Column<T>
    {
        public string DisplayName;
        public Func<T, object> Function;
        public string Name;
        public bool Sortable { get; set; }
    }
}