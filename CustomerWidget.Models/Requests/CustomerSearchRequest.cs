using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerWidget.Models.Requests
{
    public class CustomerSearchRequest : BaseSearchRequest
    {
        public int AgentId { get; set; }
    }
}
