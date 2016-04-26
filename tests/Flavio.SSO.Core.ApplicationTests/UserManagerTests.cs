using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Flavio.SSO.Core.Application.Interfaces;
using Flavio.SSO.Core.Domain.Interfaces.UserManagement;
using Flavio.SSO.Core.Domain.Interfaces.Repositories;
using Flavio.SSO.Core.Infra.Data.Interfaces;
using Flavio.SSO.Core.Infra.Data.Repositories;
using Flavio.SSO.Core.Infra.Data.Context;
using Flavio.SSO.Core.Infra.Data.UoW;
using Flavio.SSO.Core.Application.AutoMapper;
using System.Collections.Generic;
using Flavio.SSO.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Flavio.SSO.Core.Application.Tests
{
	[TestClass()]
	public class UserManagerTests
	{
		private readonly IUserAuthentication _authenticator;
		private readonly IUserRepository _userRepository;
		private readonly IGroupRepository _groupRepository;
		private readonly IUnitOfWork _uow;
		private readonly IUserManager _userManager;
		private readonly MockContext _context;
		private readonly string _userName;
		private readonly string _userPassword;
		private readonly int _userGroupsCount;

		public UserManagerTests()
		{
			_userName = "UserName"; // System.Configuration.ConfigurationManager.AppSettings["DomainUserName"];
			_userPassword = "pakepwd"; // System.Configuration.ConfigurationManager.AppSettings["DomainUserPassword"];
			_userGroupsCount = 3; // Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DomainUserGroupCount"]);

			using (CoreContext removeContext = new CoreContext())
			{
				var usrToRemove = removeContext.Users.FirstOrDefault(u => u.Username == _userName);
				if (usrToRemove != null)
				{
					removeContext.Users.Remove(usrToRemove);
					removeContext.SaveChanges();
				}
			}

			_context = new MockContext();
			_authenticator = new MockUserAuthentication();
			_userRepository = new MockUserRepository(_context);
			_groupRepository = new MockGroupRepository(_context);
			_uow = new UnitOfWork(new CoreContext());
			_userManager = new UserManager(_authenticator, _userRepository, _uow, _groupRepository);


			AutoMapperConfig.RegisterMappings();
        }

		[TestMethod()]
		public void AuthenticateUserTest()
		{
			Assert.IsNull(_userManager.AuthenticateUser(_userName, "error"));
			var correctUser = _userManager.AuthenticateUser(_userName, _userPassword);
            Assert.IsNotNull(correctUser);
			Assert.AreEqual(_userGroupsCount, correctUser.Groups.Count);
		}

		[TestMethod()]
		public void AuthenticatedUserIsValidTest()
		{
			var correctUser = _userManager.AuthenticateUser(_userName, _userPassword);
			Assert.IsNotNull(correctUser);
			Assert.IsTrue(_userManager.AuthenticatedUserIsValid(correctUser.Token));
		}

		[TestMethod()]
		public void AuthenticatedUserIsValidFailTest()
		{
			var correctUser = _userManager.AuthenticateUser(_userName, _userPassword);
			Assert.IsNotNull(correctUser);
			correctUser.ExpirationTime = DateTime.Now.AddMinutes(-10);
			Assert.IsTrue(_userManager.AuthenticatedUserIsValid(correctUser.Token));
		}

		[TestMethod()]
		public void LogOffUserTest()
		{
			var correctUser = _userManager.AuthenticateUser(_userName, _userPassword);
			_userManager.LogOffUser(correctUser.Token);
			Assert.IsFalse(_userManager.AuthenticatedUserIsValid(correctUser.Token));
        }


		//Actually the application connects to Active Directory to retrieve user
		private class MockUserAuthentication : IUserAuthentication
		{
			private readonly List<User> Users;
			private readonly List<Group> Groups;

			public MockUserAuthentication()
			{
				Users.Add(new User
				{
					Id = new Guid("5515a01a-e005-4e09-a954-a6788a7479f2"),
					Name = "Teste",
					Username = "UserName",
					Groups = {
						new Group() { Id = new Guid("646e3a48-444e-4ad1-9cc2-a9e2aeaa59c7"), Name = "Test Group"},
						new Group() { Id = new Guid("992ee407-2bc5-4ec7-bd50-49e0505f8f9d"), Name = "Test Group 2" },
						new Group() { Id = new Guid("adbd8149-430a-4f5e-b8bf-f2d5f04b0646"), Name = "Test Group 3"},
					}
				});
			}

			public User GetAuthenticatedUser(string userName, string password)
			{
				return Users.First(u => u.Name == userName);
			}
		}

		private class MockGroupRepository : IGroupRepository
		{
			private MockContext _context;

			public MockGroupRepository(MockContext context)
			{
				_context = context;
			}

			public Group Add(Group obj)
			{
				_context.Groups.Add(obj);
				return obj;
			}

			public IEnumerable<Group> AddRange(ICollection<Group> groups)
			{
				_context.Groups.AddRange(groups);
				return groups;
			}

			public void Dispose()
			{
				_context.Groups.Clear();
			}

			public IEnumerable<Group> GetGroupsById(IEnumerable<Guid> gids)
			{
				return _context.Groups.Where(g => gids.Contains(g.Id)).ToList();
			}

			public void Remove(Guid id)
			{
				_context.Groups.Remove(_context.Groups.Find(g => g.Id == id));
			}

			public void RemoveAllFromUser(Guid userId)
			{
				return;
			}

			public IEnumerable<Group> Search(Expression<Func<Group, bool>> predicate)
			{
				return _context.Groups;
			}

			public Group SearchById(Guid id)
			{
				return _context.Groups.FirstOrDefault(g => g.Id == id);
			}

			public IEnumerable<Group> SelectAll()
			{
				return _context.Groups;
			}

			public Group Update(Group obj)
			{
				return obj;
			}
		}

		private class MockUserRepository : IUserRepository
		{
			private MockContext _context;

			public MockUserRepository(MockContext context)
			{
				_context = context;
			}

			public User Add(User obj)
			{
				_context.Users.Add(obj);
				return obj;
			}

			public void Dispose()
			{
				_context.Users.Clear();
			}

			public void Remove(Guid id)
			{
				_context.Users.Remove(_context.Users.Find(g => g.Id == id));
			}

			public IEnumerable<User> Search(Expression<Func<User, bool>> predicate)
			{
				return _context.Users;
			}

			public User SearchById(Guid id)
			{
				return _context.Users.Find(u => u.Id == id);
			}

			public User SearchByToken(string token)
			{
				return _context.Users.Find(u => u.Token == token);
			}

			public IEnumerable<User> SelectAll()
			{
				return _context.Users;
			}

			public User Update(User obj)
			{
				return obj;
			}
		}

		private class MockContext
		{
			public MockContext()
			{
				Users = new List<User>();
				Groups = new List<Group>();
			}
			public List<User> Users { get; set; }
			public List<Group> Groups { get; set; }
		}

	}
}