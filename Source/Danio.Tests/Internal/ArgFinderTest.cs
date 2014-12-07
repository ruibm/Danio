namespace Ruibm.Danio.Tests.Internal
{
    using NUnit.Framework;
    using Ruibm.Danio.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class ArgFinderTest
    {
        private ArgFinder _finder;

        public enum TestEnumType
        {
            EnumValue1,
            EnumValue2
        }

        [Arg("help")]
        public static int TestInt = 42;

        [Arg("help")]
        public static string TestString;

        [Arg("help")]
        public static TestEnumType TestEnum;

        [Arg("help")]
        private static TestEnumType PrivateTestEnum;

        [Arg("help")]
        private int TestShouldNotBeAnArgBecauseItIsNotStatic;

        public class InnerClass
        {
            [Arg("help")]
            public static double TestDouble;
        }

        [SetUp]
        public void SetUp()
        {
            _finder = new ArgFinder();
        }

        [Test]
        public void TestFindingThisClassArgs()
        {
            FindResult findResult = _finder.FindAllArgs(Assembly.GetExecutingAssembly());
            IEnumerable<ArgInstance> thisClassArgs = findResult.Instances.FindAll(x => x.ParentType.Equals(GetType()));
            Assert.AreEqual(4, thisClassArgs.Count());
        }

        [Test]
        public void TestFindingInnerClassArgs()
        {
            FindResult findResult = _finder.FindAllArgs(Assembly.GetExecutingAssembly());
            IEnumerable<ArgInstance> thisClassArgs = findResult.Instances.FindAll(x => x.ParentType.Equals(typeof(InnerClass)));
            Assert.AreEqual(1, thisClassArgs.Count());
        }

        [Test]
        public void TestDefaultValueIsSetForExplicitDefault()
        {
            FindResult findResult = _finder.FindAllArgs(Assembly.GetExecutingAssembly());
            Assert.AreEqual(1, findResult.IndexedInstances.GetArgInstancesForName("TestInt").Count);
            Assert.AreEqual(42, findResult.IndexedInstances.GetArgInstancesForName("TestInt")[0].DefaultValue);
        }

        [Test]
        public void TestDefaultValueIsSetForImplicitDefault()
        {
            FindResult findResult = _finder.FindAllArgs(Assembly.GetExecutingAssembly());
            Assert.AreEqual(1, findResult.IndexedInstances.GetArgInstancesForName("TestEnum").Count);
            Assert.AreEqual(TestEnumType.EnumValue1, findResult.IndexedInstances.GetArgInstancesForName("TestEnum")[0].DefaultValue);
        }
    }
}
