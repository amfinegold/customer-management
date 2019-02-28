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
    }
}
