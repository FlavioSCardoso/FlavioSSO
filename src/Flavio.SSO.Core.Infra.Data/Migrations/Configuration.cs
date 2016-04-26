using System.Data.Entity.Migrations;

namespace Flavio.SSO.Core.Infra.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Flavio.SSO.Core.Infra.Data.Context.CoreContext>
    {
        public Configuration()
        {
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(Context.CoreContext context)
        {
        }
    }
}
