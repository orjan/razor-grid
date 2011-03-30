using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RazorGrid.Example.Models;
using RazorGrid.Model;

namespace RazorGrid.Example.Controllers
{
    public class HomeController : Controller
    {
        // JqGridOptions specifies the default configuration of JqGrid and should be injected
        private readonly JqGridOptions options = new JqGridOptions("/home/griddata");

        public ActionResult Index()
        {
            JqGridData<Log> gridData = GetGridData();
            return View(gridData.InitializeOptions(options));
        }

        public ActionResult GridData()
        {
            JqGridData<Log> gridData = GetGridData();

            var filter = new JqGridFilter<Log>(Request, gridData);
            var jqGridJsonResult = new JqGridJsonResult<Log>(gridData, filter);


            IEnumerable<Log> logs = GetMockLogs();
            if (filter.OrderByFunc != null)
            {
                logs = filter.Ascending
                           ? logs.OrderBy(filter.OrderByFunc)
                           : logs.OrderByDescending(filter.OrderByFunc);
            }

            return jqGridJsonResult.Result(logs
                                               .Skip((filter.Page - 1)*filter.Rows)
                                               .Take(filter.Rows)
                                           , logs.Count());
        }

        private static IEnumerable<Log> GetMockLogs()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return new Log {Id = i, DateTime = DateTime.Now, Message = "Log message: " + i};
            }
        }

        /* Setup available columns for the grid */

        private static JqGridData<Log> GetGridData()
        {
            var gridData = new JqGridData<Log>();
            gridData.IdFunction = l => l.Id;
            gridData.AddColumn("Id", l => l.Id, true);
            gridData.AddColumn("Message", l => l.Message, true);
            gridData.AddColumn("Logged", l => l.DateTime, false);
            return gridData;
        }
    }
}