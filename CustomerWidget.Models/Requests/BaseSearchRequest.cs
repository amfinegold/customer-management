using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CustomerWidget.Models.Requests
{
    public class BaseSearchRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool IsSortDescending { get; set; }
    }
}
