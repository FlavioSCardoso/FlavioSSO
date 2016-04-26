using System.ServiceModel;

namespace Flavio.SSO.Client
{
	class ServiceConfigurator
	{
		internal static SecurityService.UserServiceClient CreateService(string url) 
		{
			BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
			EndpointAddress endpointAddress = new EndpointAddress(url);

			var service = new SecurityService.UserServiceClient(binding, endpointAddress);

			return service;
		}
	}
}
