using System;
using System.Collections.Generic;
using System.Text;
using CustomerWidget.Repository.Interfaces;

namespace CustomerWidget.Repository.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        public string DoSomething()
        {
            return "Something";
        }
    }
}
