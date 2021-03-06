﻿using NUnit.Framework;

namespace NServiceKit.OrmLite.SqlServerTests.Expressions
{
    /// <summary>A conditional expression test.</summary>
    public class ConditionalExpressionTest : ExpressionsTestBase
    {
        /// <summary>Can select conditional and expression.</summary>
        [Test]
        public void Can_select_conditional_and_expression()
        {
            var expected = new TestType()
            {
                IntColumn = 3,
                BoolColumn = true,
                StringColumn = "4"
            };

            EstablishContext(10, expected);

            var actual = OpenDbConnection().Select<TestType>(q => q.IntColumn > 2 && q.IntColumn < 4);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
            CollectionAssert.Contains(actual, expected);
        }

        /// <summary>Can select conditional or expression.</summary>
        [Test]
        public void Can_select_conditional_or_expression()
        {
            var expected = new TestType()
            {
                IntColumn = 3,
                BoolColumn = true,
                StringColumn = "4"
            };

            EstablishContext(10, expected);

            var actual = OpenDbConnection().Select<TestType>(q => q.IntColumn == 3 || q.IntColumn < 0);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
            CollectionAssert.Contains(actual, expected);
        }

        /// <summary>Can select evaluated conditional and expression.</summary>
        [Test]
        public void Can_select_evaluated_conditional_and_expression()
        {
            // ReSharper disable ConvertToConstant.Local
            var a = 10;
            var b = 5;
            // ReSharper restore ConvertToConstant.Local

            var expected = new TestType()
            {
                IntColumn = 3,
                BoolColumn = true,
                StringColumn = "4"
            };

            EstablishContext(10, expected);

            var actual = OpenDbConnection().Select<TestType>(q => q.BoolColumn == (a >= b && a > 0));

            Assert.IsNotNull(actual);
            Assert.Greater(actual.Count, 0);
            CollectionAssert.Contains(actual, expected);
        }

        /// <summary>Can select evaluated conditional or expression.</summary>
        [Test]
        public void Can_select_evaluated_conditional_or_expression()
        {
            // ReSharper disable ConvertToConstant.Local
            var a = 10;
            var b = 5;
            // ReSharper restore ConvertToConstant.Local

            var expected = new TestType()
            {
                IntColumn = 3,
                BoolColumn = true,
                StringColumn = "4"
            };

            EstablishContext(10, expected);

            var actual = OpenDbConnection().Select<TestType>(q => q.IntColumn == 3 || a > b);

            Assert.IsNotNull(actual);
            Assert.AreEqual(11, actual.Count);
            CollectionAssert.Contains(actual, expected);
        }

        /// <summary>Can select evaluated invalid conditional or valid expression.</summary>
        [Test]
        public void Can_select_evaluated_invalid_conditional_or_valid_expression()
        {
            // ReSharper disable ConvertToConstant.Local
            var a = true;
            // ReSharper restore ConvertToConstant.Local

            var expected = new TestType()
            {
                IntColumn = 3,
                BoolColumn = true,
                StringColumn = "4"
            };

            EstablishContext(10, expected);

            var actual = OpenDbConnection().Select<TestType>(q => !q.BoolColumn || a);

            Assert.IsNotNull(actual);
            Assert.Greater(actual.Count, 0);
            CollectionAssert.Contains(actual, expected);
        }

        /// <summary>Can select evaluated conditional and valid expression.</summary>
        [Test]
        public void Can_select_evaluated_conditional_and_valid_expression()
        {
            var model = new
            {
                StringValue = "4"
            };

            var expected = new TestType()
            {
                IntColumn = 3,
                BoolColumn = true,
                StringColumn = "4"
            };

            EstablishContext(10, expected);

            var actual = OpenDbConnection().Select<TestType>(q => q.BoolColumn && q.StringColumn == model.StringValue);

            Assert.IsNotNull(actual);
            Assert.Greater(actual.Count, 0);
            CollectionAssert.Contains(actual, expected);
        }
    }
}