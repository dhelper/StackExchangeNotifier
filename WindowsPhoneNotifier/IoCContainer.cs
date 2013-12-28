using Ninject;
using StackExchangeClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneNotifier
{
    public static class IoCContainter
    {
        private static readonly IKernel Kernel = new StandardKernel();

        public static void Initialize()
        {
            Kernel.Bind<IJsonClient>().To<JsonWebClient>();
            Kernel.Bind<IUserRepository>().To<UserRepository>();
            // Kernel.Bind<IUserRepository>().To<DummyUserRepository>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
