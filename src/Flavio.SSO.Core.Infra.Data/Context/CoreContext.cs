using Flavio.SSO.Core.Infra.Data.EntityConfig;
using Flavio.SSO.Core.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace Flavio.SSO.Core.Infra.Data.Context
{
	public class CoreContext : DbContext
	{
		public CoreContext()
		:base("DefaultConnection")
		{

#if DEBUG
			Debug.WriteLine("ContextCreated");
			Debug.WriteLine("Log Init");
			Database.Log = s => Debug.WriteLine(s);
#endif
		}

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Group> Groups { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<User>()
			.HasMany(u => u.Groups)
			.WithMany(g => g.Users);

			modelBuilder.Configurations.Add(new UserConfig());
			modelBuilder.Configurations.Add(new GroupConfig());

		}

	}
}
