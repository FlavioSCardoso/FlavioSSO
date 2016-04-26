using AutoMapper;
using Flavio.SSO.Core.Application.Interfaces;
using Flavio.SSO.Core.Application.ViewModels;
using Flavio.SSO.Core.Domain.Entities;
using Flavio.SSO.Core.Domain.Interfaces.Repositories;
using Flavio.SSO.Core.Domain.Interfaces.UserManagement;
using Flavio.SSO.Core.Domain.Validations.Users;
using Flavio.SSO.Core.Infra.Data.Interfaces;
using System;
using System.Linq;

namespace Flavio.SSO.Core.Application
{
	public class UserManager : IDisposable, IUserManager
	{

		private readonly IUserAuthentication _authenticator;
		private readonly IUserRepository _userRepository;
		private readonly IGroupRepository _groupRepository;
		private readonly IUnitOfWork _uow;
		private readonly Encrypter _encrypter;

		public UserManager(IUserAuthentication authenticator, IUserRepository userRepository, IUnitOfWork uow, IGroupRepository groupRepository)
		{
			_authenticator = authenticator;
			_userRepository = userRepository;
			_groupRepository = groupRepository;
			_uow = uow;
			_encrypter = new Encrypter();

			AutoMapper.AutoMapperConfig.RegisterMappings();
		}

		public UserViewModel AuthenticateUser(string userName, string password)
		{
			var user = _authenticator.GetAuthenticatedUser(userName, password);
			if (user != null)
			{
				var searchedUser = _userRepository.SearchById(user.Id);
				if (searchedUser == null)
				{
					user.LogonTime = DateTime.Now;
					_uow.BeginTransaction();
					user.ExpirationTime = DateTime.Now;
					user = SlideUserExpirationTime(user, false);
					if (user.Groups.Count > 0)
					{
						var groups = _groupRepository.GetGroupsById(user.Groups.Select(g => g.Id).ToList());

						foreach (var group in groups)
						{
							user.Groups.Remove(user.Groups.First(g => g.Id == group.Id));
							user.Groups.Add(group);
						}
					}
					user.Token = GenerateUserToken(user);
					user = _userRepository.Add(user);
					_uow.Commit();
				}
				else
				{
					user = searchedUser;
					user = SlideUserExpirationTime(user);
				}
			}
			return Mapper.Map<UserViewModel>(user);
		}

		public UserViewModel GetAuthenticateUserByToken(string token)
		{
			var user = FindUser(token);
			if (user == null)
				return null;

			if (IsUserValid(user))
			{
				SlideUserExpirationTime(user);
				return Mapper.Map<UserViewModel>(user);
			}

			return null;
		}
		//public bool AuthenticatedUserIsValid(UserViewModel viewModel)
		//{
		//	if (viewModel == null)
		//		return false;
		//	User user = Mapper.Map<User>(viewModel);
		//	if (!UserExpirationTimeIsValid(user))
		//		return false;
		//	return SearchAuthenticatedUserIsValid(user.Id);
		//}

		//public bool AuthenticatedUserIsValid(Guid id)
		//{
		//	return SearchAuthenticatedUserIsValid(id);
		//}

		public bool AuthenticatedUserIsValid(string token)
		{
			if (string.IsNullOrEmpty(token))
				return false;
			User user = FindUser(token);
			if (user == null)
				return false;

			return IsUserValid(user);
		}

		private User FindUser(string token)
		{
			return _userRepository.SearchByToken(token);
		}

		private bool IsUserValid(User user)
		{
			if (user == null)
				return false;

			return _encrypter.VerifyUserToken(user) && UserExpirationTimeIsValid(user);
		}

		private bool UserExpirationTimeIsValid(User user)
		{
			return new UserHasValidExpirationTimeValidation().Validate(user).IsValid;
		}

		//public void LogOffUser(UserViewModel viewModel)
		//{
		//	User user = Mapper.Map<User>(viewModel);

		//	RemoveUser(viewModel.Id);
		//}

		//public void LogOffUser(Guid id)
		//{
		//	RemoveUser(id);
		//}

		public void LogOffUser(string token)
		{
			var user = FindUser(token);
			if (user == null)
				return;

			RemoveUser(user.Id);
		}

		private void RemoveUser(Guid id)
		{
			_uow.BeginTransaction();
			_userRepository.Remove(id);
			_uow.Commit();
		}

		//public UserViewModel SlideUserExpirationTime(UserViewModel viewModel, bool updateUser = true)
		//{
		//	User user = Mapper.Map<User>(viewModel);
		//	user = SlideUserExpirationTime(user, updateUser);
		//	return Mapper.Map<UserViewModel>(user);
		//}

		private User SlideUserExpirationTime(User user, bool updateUser = true)
		{
			user.ExpirationTime = DateTime.Now.AddMinutes(Configuration.SlidingMinutes);
			if (updateUser)
			{
				_uow.BeginTransaction();
				user = _userRepository.Update(user);
				_uow.Commit();
			}
			return user;
		}

		private string GenerateUserToken(User user)
		{
			return _encrypter.EncryptUserToken(user).Token; //.Encrypt(id.ToString());
		}

		public void Dispose()
		{
			_userRepository.Dispose();
			_groupRepository.Dispose();
		}
	}
}
