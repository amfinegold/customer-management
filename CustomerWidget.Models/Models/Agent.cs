using System.Diagnostics.CodeAnalysis;

namespace CustomerWidget.Models.Models
{
    [ExcludeFromCodeCoverage]
    public class Agent : BaseDocument
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int Tier { get; set; }
        public ContactPhones Phone { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ContactPhones
    {
        public string Primary { get; set; }
        public string Mobile { get; set; }
    }
}
