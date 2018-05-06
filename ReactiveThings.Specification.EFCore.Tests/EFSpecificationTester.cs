using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Shouldly;
using System.Linq;

namespace ReactiveThings.Specification.EFCore.Tests
{
    public static class EFCoreSpecificationTester
    {
        public static readonly ILoggerFactory MyLoggerFactory = new LoggerFactory().AddDebug();
        public static void TestSpecification(ISpecification<TestObject> specification, TestObject testObject, bool result)
        {
            using (var sqLiteConnection = new SqliteConnection("data source=:memory:;cache=shared"))
            {
                var options = new DbContextOptionsBuilder<TestContext>()
                        .UseSqlite(sqLiteConnection)
                        .UseLoggerFactory(MyLoggerFactory)
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        .Options;
                using (var context = new TestContext(options))
                {
                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.TestObjects.Add(testObject);
                    context.SaveChanges();

                    var results = context.TestObjects.Where(specification).SingleOrDefault();
                    if (result)
                    {
                        results.ShouldNotBeNull();
                    }
                    else
                    {
                        results.ShouldBeNull();
                    }
                }
            }
        }
    }
}