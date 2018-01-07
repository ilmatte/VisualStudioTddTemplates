using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Rhino.Mocks;

namespace $rootnamespace$
{
    [TestFixture]
    public class $safeitemname$
    {
        MockRepository mocks;

        /// <summary>
        /// Prepares mock repository
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            mocks = new MockRepository();
        }

        /// <summary>
        /// template behavior and state testing method
        /// </summary>
        [Test]
        public void TestMethod1()
        {
            IDependency dependency = mocks.CreateMock<IDependency>();

            // Record expectations
            using (mocks.Record())
            {
                Expect.Call(dependency.Method1("parameter")).Return("result");
                dependency.Method2();
            }

            // Replay and validate interaction
            Subject subjectUnderTest;
            using (mocks.Playback())
            {
                subjectUnderTest = new Subject(dependency);
                subjectUnderTest.DoWork();
            }

            // Post-interaction assertion
            Assert.That(subjectUnderTest.WorkDone, Is.True);
        }
    }
 }
