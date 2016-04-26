using Flavio.SSO.Core.Application;
using Flavio.SSO.Core.Application.Interfaces;
using Flavio.SSO.Core.Domain.Interfaces.Repositories;
using Flavio.SSO.Core.Domain.Interfaces.UserManagement;
using Flavio.SSO.Core.Infra.Data.Context;
using Flavio.SSO.Core.Infra.Data.Interfaces;
using Flavio.SSO.Core.Infra.Data.Repositories;
using Flavio.SSO.Core.Infra.Data.UoW;
using Flavio.SSO.Core.Infra.LDAP;
using SimpleInjector;

namespace Flavio.SSO.Core.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void Register(Container container)
        {
            // APP
            container.Register<IUserManager, UserManager>(Lifestyle.Scoped);

            // Domain
            container.Register<IUserAuthentication, UserAuthentication>(Lifestyle.Scoped);

            // Infra Data
            container.Register<IGroupRepository, GroupRepository>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
			container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<CoreContext>(Lifestyle.Scoped);
		}

		public static void RegisterForNonWebApps(Container container) 
		{
			container.Register<IUserManager, UserManager>(Lifestyle.Singleton);

			// Domain
			container.Register<IUserAuthentication, UserAuthentication>(Lifestyle.Singleton);

			// Infra Data
			container.Register<IGroupRepository, GroupRepository>(Lifestyle.Singleton);
			container.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
			container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
			container.Register<CoreContext>(Lifestyle.Singleton);
		}
	}
}