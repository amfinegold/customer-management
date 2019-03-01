using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace CustomerWidget.Models.Models
{
    [ExcludeFromCodeCoverage]
    public class Customer : BaseDocument
    {
        public int AgentId { get; set; }
        public UniqueId Guid { get; set; }
        public bool IsActive { get; set; }
        public string Balance { get; set; }
        public int Age { get; set; }
        public CustomerName Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Registered { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    [ExcludeFromCodeCoverage]
    public class CustomerName
    {
        public string First { get; set; }
        public string Last { get; set; }
    }
}
