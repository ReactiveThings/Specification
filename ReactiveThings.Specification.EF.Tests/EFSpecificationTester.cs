using Shouldly;
using System.Data.SQLite;
using System.Linq;

namespace ReactiveThings.Specification.EF.Tests
{
    public static class EFSpecificationTester
    {
        public static void TestSpecification(ISpecification<TestObject> specification, TestObject testObject, bool result)
        {
            using (var sqLiteConnection = new SQLiteConnection("data source=:memory:;cache=shared"))
            {
                sqLiteConnection.Open();
                using (var context = new TestContext(sqLiteConnection))
                {
                    context.Database.Initialize(true);
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