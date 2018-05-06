using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.Tests
{
    public class DerivedFromTestObject : TestObject
    {
        public bool AdditionalProperty { get; set; }
    }

    public class DerivedFromTestObjectSpecification : SpecificationBuilder<DerivedFromTestObject, DerivedFromTestObjectSpecification>
    {
        public DerivedFromTestObjectSpecification AdditionalProperty(bool value)
        {
            return Where(p => p.AdditionalProperty == value);
        }
    }

    public class TestObjectSpecification : SpecificationBuilder<TestObject,TestObjectSpecification>
    {
        public TestObjectSpecification BooleanProperty(bool value)
        {
            return Where(p => p.BooleanProperty == value);
        }

        public TestObjectSpecification BooleanProperty1(bool value)
        {
            return Where(p => p.BooleanProperty1 == value);
        }

        public TestObjectSpecification ByProperty(Action<TestObjectSpecification> build)
        {
            return Where(p => p.Property, build);
        }

        public TestObjectSpecification CollectionAll(Action<TestObjectSpecification> build)
        {
            return All(p => p.Collection, build);
        }

        public TestObjectSpecification CollectionAny(Action<TestObjectSpecification> build)
        {
            return Any(p => p.Collection, build);
        }

        public TestObjectSpecification ByDerived(Action<DerivedFromTestObjectSpecification> build)
        {
            return Where(p => p as DerivedFromTestObject, build);
        }
    }

    public class SpecificationBuilderTests
    {
        [Fact]
        public void EmptySpecificationCreatesTrueSpecification()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification();

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }
        [Fact]
        public void WhereAddsSingleSpecification()
        {
            var testObject = new TestObject() {
                BooleanProperty = true
            };

            var specification = new TestObjectSpecification().BooleanProperty(true);

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);

        }

        [Fact]
        public void WhereCombineSpecificationsByConjunction()
        {
            var testObject = new TestObject()
            {
                BooleanProperty = true,
                BooleanProperty1 = true
            };

            var specification = new TestObjectSpecification()
                .BooleanProperty(true)
                .BooleanProperty1(true);

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }
        [Fact]
        public void OrThrowsWhenSpecificationIsEmpty()
        {
            var testObject = new TestObject()
            {
                BooleanProperty1 = true
            };

            var specification = new TestObjectSpecification();
            Assert.Throws<Exception>(() =>
            {
                specification.Or(or => or.BooleanProperty1(false));
            });
        }

        [Fact]
        public void OrCombineSpecificationsByDisjunction()
        {
            var testObject = new TestObject()
            {
                BooleanProperty = false,
                BooleanProperty1 = true
            };

            var specification = new TestObjectSpecification()
                .BooleanProperty(true).Or(or => or.BooleanProperty1(true));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void OrWithManyParametersCombinePreviousWithExistingSpecificationByByConjunction()
        {
            var testObject = new TestObject()
            {
                BooleanProperty = false,
                BooleanProperty1 = true
            };

            var specification = new TestObjectSpecification()
                .BooleanProperty(false)
                .Or(or => or.BooleanProperty1(false), or => or.BooleanProperty1(true));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void OrNotCombineSpecificationsByNegatedDisjunction()
        {
            var testObject = new TestObject()
            {
                BooleanProperty = false,
                BooleanProperty1 = true
            };

            var specification = new TestObjectSpecification()
                .BooleanProperty(true).OrNot(or => or.BooleanProperty1(false));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void OrNotThrowsWhenSpecificationIsEmpty()
        {
            var testObject = new TestObject()
            {
                BooleanProperty1 = true
            };

            var specification = new TestObjectSpecification();
            Assert.Throws<Exception>(() =>
            {
                specification.OrNot(or => or.BooleanProperty1(false));
            });
        }

        [Fact]
        public void OrNotWithManyParametersCombinePreviousWithExistingSpecificationByByNegatedConjunction()
        {
            var testObject = new TestObject()
            {
                BooleanProperty = false,
                BooleanProperty1 = true
            };

            var specification = new TestObjectSpecification()
                .BooleanProperty(false)
                .OrNot(or => or.BooleanProperty1(false), or => or.BooleanProperty1(false));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void NotNegateSpecification()
        {
            var testObject = new TestObject()
            {
                BooleanProperty = false
            };

            var specification = new TestObjectSpecification()
                .Not(p => p.BooleanProperty(true));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }


        [Fact]
        public void AllowsPropertyToBeFilteredBySpecification()
        {
            var testObject = new TestObject()
            {
                Property = new TestObject()
                {
                    BooleanProperty = true
                }
            };

            var specification = new TestObjectSpecification()
                .ByProperty(p => p.BooleanProperty(true));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void AllowsCollectionToBeFilteredBySpecificationWithAnyOperator()
        {
            var testObject = new TestObject()
            {
                Collection = new List<TestObject>()
                {
                    new TestObject(){BooleanProperty = true}
                }
            };

            var specification = new TestObjectSpecification()
                .CollectionAny(p => p.BooleanProperty(true));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void AllowsCollectionToBeFilteredBySpecificationWithAllOperator()
        {
            var testObject = new TestObject()
            {
                Collection = new List<TestObject>()
                {
                    new TestObject(){BooleanProperty = true},
                    new TestObject(){BooleanProperty = true}
                }
            };

            var specification = new TestObjectSpecification()
                .CollectionAll(p => p.BooleanProperty(true));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void AllowsApplySpecificationForDerivedObjects()
        {
            var testObject = new DerivedFromTestObject()
            {
                AdditionalProperty = true
            };

            var specification = new TestObjectSpecification()
                .ByDerived(p => p.AdditionalProperty(true));

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void WhenSpecificationForPropertyIsEmptyOmitSpecification()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification()
                .ByProperty(p => {});

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void WhenSpecificationForCollectionAnyIsEmptyOmitSpecification()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification()
                .CollectionAny(p => { });

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void WhenSpecificationForCollectionAllIsEmptyOmitSpecification()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification()
                .CollectionAll(p => { });

            var expectedResult = true;

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void IsSatisfiedBy()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification();

            specification.IsSatisfiedBy(testObject).ShouldBeTrue();
        }
    }
}
