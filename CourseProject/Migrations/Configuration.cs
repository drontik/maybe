using CourseProject.DbContext;
using CourseProject.Domain.Entities;

namespace CourseProject.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            context.Categories.AddOrUpdate(
              new CreativeCategory { Id  = 1, Name = "Free Writing"}
            );
        }
    }
}
