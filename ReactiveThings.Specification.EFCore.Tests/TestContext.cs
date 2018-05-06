using Microsoft.EntityFrameworkCore;

namespace ReactiveThings.Specification.EFCore.Tests
{
    public class TestContext : DbContext
    {
        public DbSet<TestObject> TestObjects { get; set; }
        public TestContext()
        { }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        { }

    }
}
