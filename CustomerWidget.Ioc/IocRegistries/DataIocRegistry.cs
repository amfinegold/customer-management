using CustomerWidget.Repository.Implementations;
using CustomerWidget.Repository.Interfaces;
using SimpleInjector;

namespace CustomerWidget.Ioc.IocRegistries
{
    public class DataIocRegistry
    {
        public static void Register(Container container)
        {
            container.Register<IMongoDbContext, MongoDbContext>(Lifestyle.Singleton);
            
            container.Register<IAgentRepository, AgentRepository>(Lifestyle.Scoped);
            container.Register<ICustomerRepository, CustomerRepository>(Lifestyle.Scoped);
        }
    }
}
