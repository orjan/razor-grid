using System;
using System.Collections.Generic;
using RazorGrid.Model;

namespace RazorGrid
{
    /// <summary>
    /// Responsible for the columns that should be returned to JqGrid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JqGridData<T>
    {
        private readonly IList<Column<T>> columns = new List<Column<T>>();

        public IEnumerable<Column<T>> Columns
        {
            get { return columns; }
        }

        public Func<T, object> IdFunction { get; set; }

        /// <summary>
        /// Adds a new column to be displayed by jqGrid
        /// </summary>
        /// <param name="name">This property will specifiy index and name use for each column in jqGrid.</param>
        /// <param name="value">A function to calculate the value for each cell for the column.</param>
        /// <param name="sortable">Sets whether it's possible to sort by the column. 
        /// This property is mandatory since it can be very expensive to sort unindex column for a large result set.</param>
        public void AddColumn(string name, Func<T, object> value, bool sortable)
        {
            columns.Add(new Column<T>
                            {
                                Name = name,
                                DisplayName = name,
                                Function = value,
                                Sortable = sortable
                            });
        }


        /// <summary>
        /// Populates the JqGridOptions from the columns specified by the controller
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public JqGridOptions InitializeOptions(JqGridOptions options)
        {
            foreach (var column in Columns)
            {
                options.ColumnNames.Add(column.DisplayName);

                options.ColumnModels.Add(new ColumnModel
                                             {
                                                 Index = column.DisplayName,
                                                 Name = column.Name,
                                                 Sortable = column.Sortable
                                             });
            }
            return options;
        }
    }
}