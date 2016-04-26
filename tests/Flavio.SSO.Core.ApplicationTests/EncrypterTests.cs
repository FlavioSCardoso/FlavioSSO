using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flavio.SSO.Core.Application;

namespace Flavio.SSO.Core.ApplicationTests
{
	[TestClass]
	public class EncrypterTests
	{
		[TestMethod]
		public void ShouldEncryptAndDecrypt()
		{
			Encrypter encrypter = new Encrypter();
			string id = Guid.NewGuid().ToString();
			string encrypted = encrypter.Encrypt(id);
			string decrypted = encrypter.Decrypt(encrypted);
			Assert.AreEqual(id, decrypted);
		}
	}
}
