using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RazorGrid.Model
{
    /* Example configuration of jqGrid
            url: '/home/griddata',
            datatype: "json",
            colNames: ['Id', 'Date', 'Message'],
            mtype: "POST",
            colModel: [
                { name: 'id', index: 'id', width: 55 },
                { name: 'invdate', index: 'invdate', width: 120 },
                { name: 'name', index: 'name asc, invdate', width: 200 },
            ],
            rowNum: 10,
            rowList: [10, 20, 30],
            pager: '#pager',
            sortname: 'id',
            sortorder: "desc",
            caption: "My listing"
     */

    [DataContract]
    public class JqGridOptions
    {
        private readonly IList<ColumnModel> columnModels = new List<ColumnModel>();
        private readonly IList<string> columnNames = new List<string>();

        public JqGridOptions(string url)
        {
            DataType = "json";
            Mtype = "POST";
            RowNum = 10;
            RowList = new List<int> { 10, 20, 30 };

            Url = url;
        }

        /* Needs to be specified every time */

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        /* Site specific data */

        [DataMember(Name = "datatype")]
        public string DataType { get; set; }

        [DataMember(Name = "mtype")]
        public string Mtype { get; set; }

        [DataMember(Name = "rowNum")]
        public int RowNum { get; set; }

        [DataMember(Name = "rowList")]
        public IList<int> RowList { get; set; }

        /* Controller specific data */

        [DataMember(Name = "colNames")]
        public IList<string> ColumnNames
        {
            get { return columnNames; }
        }

        [DataMember(Name = "colModel")]
        public IList<ColumnModel> ColumnModels
        {
            get { return columnModels; }
        }

        /// <summary>
        /// Converts the view model to a Json string appropriate for configuration of jqGrid 
        /// http://pietschsoft.com/post/2008/02/NET-35-JSON-Serialization-using-the-DataContractJsonSerializer.aspx
        /// </summary>
        /// <returns>Returns a Json serialized version of JqGridOptions</returns>
        public string ToJson()
        {
            var serializer = new DataContractJsonSerializer(GetType());
            var ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}