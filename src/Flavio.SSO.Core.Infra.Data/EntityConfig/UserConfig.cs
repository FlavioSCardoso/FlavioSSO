using Flavio.SSO.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Flavio.SSO.Core.Infra.Data.EntityConfig
{
	class UserConfig : EntityTypeConfiguration<User>
	{
		public UserConfig()
		{
			HasKey(u => u.Id);

			Property(u => u.Name)
				.HasMaxLength(60)
				.IsRequired();

			Property(u => u.Username)
				.HasMaxLength(60)
				.IsRequired();

			Property(u => u.LogonTime)
				.IsRequired();

			Property(u => u.Token)
				.HasMaxLength(130)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));


			ToTable("Users");
		}
	}
}
