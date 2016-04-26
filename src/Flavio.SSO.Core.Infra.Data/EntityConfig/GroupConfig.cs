using Flavio.SSO.Core.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Flavio.SSO.Core.Infra.Data.EntityConfig
{
	class GroupConfig : EntityTypeConfiguration<Group>
	{
		public GroupConfig()
		{
			HasKey(u => u.Id);

			Property(u => u.Name)
				.HasMaxLength(60)
				.IsRequired();

			ToTable("Groups");
		}
	}
}
