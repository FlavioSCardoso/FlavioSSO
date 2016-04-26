using System;
using System.Text;
using System.Web.Security;
using System.Runtime.CompilerServices;
using Flavio.SSO.Core.Domain.Entities;

[assembly: InternalsVisibleTo("Flavio.SSO.Core.ApplicationTests")]

namespace Flavio.SSO.Core.Application
{
	internal class Encrypter
	{
		private string _purpose;
		private readonly Encoding encoding;

		public Encrypter()
		{
			_purpose =  "32E35872597989D14CC1D5D9F5B1E94238D0EE32CF10AA2D2059533DF6035F4F";
			encoding = Encoding.ASCII;
		}

		internal string Encrypt(string token)
		{
			if (string.IsNullOrEmpty(token)) return null;

			byte[] bytes = encoding.GetBytes(token);
			string string64 = Convert.ToBase64String(bytes);
			bytes = Convert.FromBase64String(string64);
			var encrypted = MachineKey.Protect(bytes, _purpose);
			return Convert.ToBase64String(encrypted);
		}

		internal string Decrypt(string encryptedToken)
		{
			if (string.IsNullOrWhiteSpace(encryptedToken)) return null;

			var bytes = Convert.FromBase64String(encryptedToken);
			bytes = MachineKey.Unprotect(bytes, _purpose);
			string decrypted = encoding.GetString(bytes);
			return decrypted;
		}

		public User EncryptUserToken(User user) {
			if (user == null)
				return null;

			string token = user.Id.ToString();
			_purpose = user.LogonTime.ToString();

			user.Token = Encrypt(user.Id.ToString());
			return user;	
		}

		public bool VerifyUserToken(User user) {
			if (user == null)
				return false;
			
			_purpose = user.LogonTime.ToString();

			var decrypted = Decrypt(user.Token);

			return decrypted.Equals(user.Id.ToString());
		}
	}
}
