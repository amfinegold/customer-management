using System;
using System.Collections.Generic;
using System.Text;
using CustomerWidget.Service.Implementations;
using CustomerWidget.Service.Interfaces;
using SimpleInjector;

namespace CustomerWidget.Ioc.IocRegistries
{
   internal class ServiceIocRegistry
    {
        public static void Register(Container container)
        {
            container.Register<ICustomerService, CustomerService>(Lifestyle.Scoped);
        }
    }
}
