using System.Data.Common;
using System.Data.Entity;
using SQLite.CodeFirst;

namespace ReactiveThings.Specification.EF.Tests
{
    public class TestContextInitializer : SqliteDropCreateDatabaseAlways<TestContext>
    {
        public TestContextInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
    }
    public class TestContext : DbContext
    {
        public DbSet<TestObject> TestObjects { get; set; }

        public TestContext(DbConnection existingConnection) : base(existingConnection, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new TestContextInitializer(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }


    }
}