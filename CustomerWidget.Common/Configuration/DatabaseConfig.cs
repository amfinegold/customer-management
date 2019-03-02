using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CustomerWidget.Common.Configuration
{
    [ExcludeFromCodeCoverage]
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AgentCollectionName { get; set; }
        public string CustomerCollectionName { get; set; }
    }
}
