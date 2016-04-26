using AutoMapper;
using Flavio.SSO.Core.Application.ViewModels;
using Flavio.SSO.Core.Domain.Entities;

namespace Flavio.SSO.Core.Application.AutoMapper
{
	public class DomainToViewModelMappingProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<User, UserViewModel>();
			CreateMap<Group, GroupViewModel>();
		}
	}
}
