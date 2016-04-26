using System.DirectoryServices.AccountManagement;
using Flavio.SSO.Core.Domain.Interfaces.UserManagement;
using Flavio.SSO.Core.Domain.Entities;

namespace Flavio.SSO.Core.Infra.LDAP
{
	public class UserAuthentication : IUserAuthentication
	{
		private string Domain;

		public UserAuthentication()
		{
			Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
		}

		public User GetAuthenticatedUser(string userName, string password)
		{
			using (var domainContext = new PrincipalContext(ContextType.Domain, Domain, userName, password))
			{
				using (var userToSearch = new UserPrincipal(domainContext))
				{
					try
					{
						userToSearch.SamAccountName = userName;

						using (var principalSearcher = new PrincipalSearcher(userToSearch))
						{
							try
							{
								UserPrincipal userPrincipal = principalSearcher.FindOne() as UserPrincipal;
								if (userPrincipal != null)
								{
									User user = new User()
									{
										Id = userPrincipal.Guid.Value,
										Name = userPrincipal.Name,
										Username = userName
									};

									foreach (var group in userPrincipal.GetAuthorizationGroups())
									{
										user.Groups.Add(
											new Group()
											{
												Id = group.Guid.Value,
												Name = group.Name
											}
										);
									}

									return user;
								}
								else
								{
									return null;
								}
							}
							catch
							{
								return null;
							}
						}
					}
					catch 
					{
						return null;
					}
				}
			}
		}
	}
}
