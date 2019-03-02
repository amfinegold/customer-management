using System.Collections.Generic;
using CustomerWidget.Models.Models;

namespace CustomerWidget.Models.Responses
{
    public class SearchResponse<T>
        where T : BaseDocument
    {
        public List<T> Data { get; set; } = new List<T>();
        public long TotalRecords { get; set; }
        public int CurrentPageIndex { get; set; }
        public int RecordsPerPage { get; set; }
    }
}
