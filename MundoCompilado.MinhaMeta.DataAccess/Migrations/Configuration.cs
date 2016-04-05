using System.Data.Entity.Migrations;

namespace MundoCompilado.MinhaMeta.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

    }
}
