using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerWidget.Models
{
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

    public class ContactPhones
    {
        public string Primary { get; set; }
        public string Mobile { get; set; }
    }
}
