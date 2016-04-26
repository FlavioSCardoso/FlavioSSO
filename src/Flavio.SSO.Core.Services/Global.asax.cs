using Flavio.SSO.Core.Application.AutoMapper;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using System;

namespace Flavio.SSO.Core.Services
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			AutoMapperConfig.RegisterMappings();
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();

			Infra.CrossCutting.IoC.BootStrapper.Register(container);

			SimpleInjectorServiceHostFactory.SetContainer(container);

		}
	}
}