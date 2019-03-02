using CustomerWidget.Ioc.IocRegistries;
using SimpleInjector;

namespace CustomerWidget.Ioc
{
    public class IocInitializer
    {
        /// <summary>
        /// Initialize the dependency injection container.
        /// </summary>
        /// <param name="container">This is the injection container.</param>     
        /// <param name="buildVersion">The auto-incrementing File Version generated per build</param>
        /// <returns></returns>
        public static void InitializeContainer(Container container, string buildVersion)
        {
            DataIocRegistry.Register(container);
            ServiceIocRegistry.Register(container);
        }
    }
}
