using AutoMapper;
using Flavio.SSO.Core.Application.ViewModels;
using Flavio.SSO.Core.Domain.Entities;

namespace Flavio.SSO.Core.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
			CreateMap<UserViewModel, User>();
			CreateMap<GroupViewModel, Group>();
		}
	}
}

