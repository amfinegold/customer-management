using System;
using System.Collections.Generic;
using System.Text;
using CustomerWidget.Repository.Implementations;
using CustomerWidget.Repository.Interfaces;
using SimpleInjector;

namespace CustomerWidget.Ioc.IocRegistries
{
    public class DataIocRegistry
    {
        public static void Register(Container container)
        {
            container.Register<ICustomerRepository, CustomerRepository>(Lifestyle.Scoped);
        }
    }
}
