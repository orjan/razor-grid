using System;
using System.Web;

namespace RazorGrid
{
    /// <summary>
    /// {_search=false&nd=1301351164778&rows=10&page=1&sidx=id&sord=desc}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JqGridFilter<T>
    {
        private readonly Func<T, object> order;


        public JqGridFilter(HttpRequestBase request, JqGridData<T> gridData)
        {
            Rows = int.Parse(request.Form["rows"]);
            Page = int.Parse(request.Form["page"]);

            OrderBy = request.Form["sidx"];
            Ascending = "desc" != request.Form["sord"];


            foreach (var column in gridData.Columns)
            {
                if (column.DisplayName == OrderBy)
                {
                    order = column.Function;
                    break;
                }
            }
        }

        public Func<T, object> OrderByFunc
        {
            get { return order; }
        }

        public int Page { get; set; }
        public int Rows { get; set; }
        public bool Ascending { get; set; }
        public string OrderBy { get; set; }
        // public string SortColumn { get; set; }
        // public string SortOrder { get; set; }
    }
}