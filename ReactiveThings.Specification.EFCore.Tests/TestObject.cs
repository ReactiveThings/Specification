using System.Collections.Generic;

namespace ReactiveThings.Specification.EFCore.Tests
{
    public class TestObject
    {
        public int Id { get; set; }
        public bool BooleanProperty { get; set; }
        public TestObject Property { get; set; }
        public IList<TestObject> Collection { get; set; } = new List<TestObject>();
    }
}
