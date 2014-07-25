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
    public class ArgAssignerTest
    {
        private ArgAssigner _assigner;
        private ValueClass _valueClass;

        [SetUp]
        public void SetUp()
        {
            _assigner = new ArgAssigner();
            _valueClass = new ValueClass();
        }

        [Test]
        public void TestAssigningInt()
        {
            ParseResult parseResult = CreateParseResult(true, "ValueInt", "42");
            AssignResult assignResult = _assigner.AssignValues(_valueClass.GetFindResult(), parseResult);
            Assert.IsTrue(assignResult.Success);
            Assert.AreEqual(42, _valueClass.ValueInt);
        }

        [Test]
        public void TestAssigningString()
        {
            ParseResult parseResult = CreateParseResult(true, "ValueString", "Bom Dia!!!");
            var instances = _valueClass.GetFindResult();
            AssignResult assignResult = _assigner.AssignValues(_valueClass.GetFindResult(), parseResult);
            Assert.IsTrue(assignResult.Success);
            Assert.AreEqual("Bom Dia!!!", _valueClass.ValueString);
        }

        public ParseResult CreateParseResult(bool successfull, params string[] keyValues) 
        {
            MultiDictionary<string, string> parsedValues = new MultiDictionary<string,string>();
            for (int i = 0; i < keyValues.Length / 2; ++i) 
            {
                parsedValues.Add(keyValues[i * 2], keyValues[i * 2 + 1]);
            }
            return new ParseResult(successfull, parsedValues, new ErrorLog());
        }

        private class ValueClass
        {
            public int ValueInt;
            public string ValueString;

            public FindResult GetFindResult()
            {
                List<ArgInstance> argInstances = new List<ArgInstance>();
                foreach (FieldInfo fieldInfo in GetType().GetFields())
                {
                    ArgAttribute attribute = new ArgAttribute("helpmessage");
                    ArgInstance argInstance = new ArgInstance(attribute, fieldInfo, GetType(), this);
                    argInstances.Add(argInstance);
                }
                return new FindResult(argInstances, new ErrorLog());
            }
        }
    }
}
