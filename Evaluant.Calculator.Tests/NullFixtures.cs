using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NCalc.Domain;
using NUnit.Framework;
using ThreadState = System.Threading.ThreadState;

namespace NCalc.Tests
{
    [TestFixture]
    public class NullFixtures
    {
        [Test]
        public void ShouldRecognizeNullOperator()
        {
            var e = new Expression("null");
            Assert.AreEqual(null, e.Evaluate());
        }

        [Test]
        public void ShouldNotAllowArithmeticOpsAgainstNull()
        {
            try
            {
                new Expression("1 + null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 - null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 * null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 / null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1.1 / null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 % null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null + 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null - 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null * 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null / 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null / 1.1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null % 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }
        }

        [Test]
        public void ShouldHandleLogicalOpsWithNull()
        {
            Assert.AreEqual(true, new Expression("null == null").Evaluate());
            Assert.AreEqual(true, new Expression("null == false").Evaluate());
            Assert.AreEqual(true, new Expression("null || true").Evaluate());
            Assert.AreEqual(true, new Expression("null != 5").Evaluate());
            Assert.AreEqual(true, new Expression("null != 0").Evaluate());
            Assert.AreEqual(false, new Expression("null && false").Evaluate());
            Assert.AreEqual(false, new Expression("null == true").Evaluate());
            Assert.AreEqual(false, new Expression("null < 0").Evaluate());
            Assert.AreEqual(false, new Expression("null > 0").Evaluate());
            Assert.AreEqual(false, new Expression("null <= 0").Evaluate());
            Assert.AreEqual(false, new Expression("null >= 0").Evaluate());
            Assert.AreEqual(false, new Expression("null && true").Evaluate());
            Assert.AreEqual(false, new Expression("null || false").Evaluate());
        }

        [Test]
        public void MinAndMaxHandleNulls()
        {
            Assert.AreEqual(10, new Expression("Min(null, 10)").Evaluate());
            Assert.AreEqual(10, new Expression("Min(10, null)").Evaluate());
            Assert.AreEqual(10, new Expression("Max(null, 10)").Evaluate());
            Assert.AreEqual(10, new Expression("Max(10, null)").Evaluate());
        }

        [Test]
        public void BitwiseOperatorsShouldNotWorkWithNulls()
        {
            try
            {
                new Expression("1 << null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 >> null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 ^ null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 & null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("1 | null").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null >> 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null << 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null ^ 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null & 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }

            try
            {
                new Expression("null | 1").Evaluate();
                Assert.Fail("Should have thrown InvalidOperationException");
            }
            catch (InvalidOperationException) { }
        }

        [Test]
        public void ShouldCompareDatesToNull()
        {
            Assert.AreEqual(false, new Expression("#2012-05-02# == null").Evaluate());
            Assert.AreEqual(false, new Expression("#2012-05-02# < null").Evaluate());
            Assert.AreEqual(false, new Expression("#2012-05-02# <= null").Evaluate());
            Assert.AreEqual(true, new Expression("#2012-05-02# > null").Evaluate());
            Assert.AreEqual(true, new Expression("#2012-05-02# >= null").Evaluate());
            Assert.AreEqual(true, new Expression("#2012-05-02# != null").Evaluate());
            Assert.AreEqual(false, new Expression("null == #2012-05-02#").Evaluate());
            Assert.AreEqual(true, new Expression("null < #2012-05-02#").Evaluate());
            Assert.AreEqual(true, new Expression("null <= #2012-05-02#").Evaluate());
            Assert.AreEqual(false, new Expression("null > #2012-05-02#").Evaluate());
            Assert.AreEqual(false, new Expression("null >= #2012-05-02#").Evaluate());
            Assert.AreEqual(true, new Expression("null != #2012-05-02#").Evaluate());
        }

        [Test]
        public void EmptyStringShouldEqualToNull()
        {
            Assert.AreEqual(true, new Expression("'' == null").Evaluate());
            Assert.AreEqual(false, new Expression("'' != null").Evaluate());
            Assert.AreEqual(true, new Expression("null == ''").Evaluate());
            Assert.AreEqual(false, new Expression("null != ''").Evaluate());
        }
    }
}