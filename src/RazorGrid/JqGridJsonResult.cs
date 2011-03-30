using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RazorGrid
{
    public class JqGridJsonResult<T>
    {
        private readonly JqGridData<T> data;
        private readonly JqGridFilter<T> filter;

        public JqGridJsonResult(JqGridData<T> data, JqGridFilter<T> filter)
        {
            this.data = data;
            this.filter = filter;
        }

        public JsonResult Result(IEnumerable<T> items, long numberOfRecords)
        {
            var rows = new object[items.Count()];
            int i = 0;
            foreach (T row in items)
            {
                rows[i] =
                    new
                        {
                            id = data.IdFunction.Invoke(row).ToString(),
                            cell = GetCells(row)
                        };
                i++;
            }

            var result = new JsonResult();
            long numberOfPages = DivRoundUp(numberOfRecords, filter.Rows);
            result.Data =
                new
                    {
                        page = filter.Page.ToString(),
                        records = numberOfRecords.ToString(),
                        rows,
                        total = numberOfPages
                    };

            return result;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/921180/how-can-i-ensure-that-a-division-of-integers-is-always-rounded-up
        /// </summary>
        private static long DivRoundUp(long a, long b)
        {
            long div = a/b;
            if (a%b != 0)
                div++;

            return div;
        }

        private object[] GetCells(T entity)
        {
            return data.Columns.Select(col => col.Function.Invoke(entity).ToString()).ToArray();
        }
    }
}