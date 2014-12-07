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
            FindResult findResult = _valueClass.GetFindResult();
            AssignResult assignResult = _assigner.AssignValues(_valueClass.GetFindResult(), parseResult);
            Assert.IsTrue(assignResult.Success);
            Assert.AreEqual("Bom Dia!!!", _valueClass.ValueString);
        }
        
        [Test]
        public void TestDuplicateArgumentFails()
        {
            ParseResult parseResult = CreateParseResult(true, "ValueString", "FirstValue", "ValueString", "SecondValue");
            FindResult findResult = _valueClass.GetFindResult();
            AssignResult result = _assigner.AssignValues(findResult, parseResult);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorLog.GetErrorsPrettyPrint().Contains("ValueString"));
        }

        [Test]
        public void TestNonExistantArgumentFails()
        {
            ParseResult parseResult = CreateParseResult(true, "ThisArgDoesNotExist", "FirstValue");
            FindResult findResult = _valueClass.GetFindResult();
            AssignResult result = _assigner.AssignValues(findResult, parseResult);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorLog.GetErrorsPrettyPrint().Contains("ThisArgDoesNotExist"));
        }


        [Test]
        public void TestFailIfMandatoryArgumentNotPassed()
        {
            ParseResult parseResult = CreateParseResult(true, "ValueString", "NotSettingValueIntOnPurpose");
            FindResult findResult = _valueClass.GetFindResult("ValueInt");
            AssignResult result = _assigner.AssignValues(findResult, parseResult);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorLog.GetErrorsPrettyPrint().Contains("ValueInt"));
        }

        public ParseResult CreateParseResult(bool successfull, params string[] keyValues)
        {
            MultiDictionary<string, string> parsedValues = new MultiDictionary<string, string>();
            for (int i = 0; i < keyValues.Length / 2; ++i)
            {
                parsedValues.Add(keyValues[i * 2], keyValues[i * 2 + 1]);
            }
            return new ParseResult(successfull, parsedValues, ExecutionLog.CreateErrorLog());
        }

        private class ValueClass
        {
            public int ValueInt;
            public string ValueString;

            public FindResult GetFindResult(params string[] mandatoryFields)
            {
                HashSet<string> mandatoryFieldsSet = new HashSet<string>(mandatoryFields);
                List<ArgInstance> argInstances = new List<ArgInstance>();
                foreach (FieldInfo fieldInfo in GetType().GetFields())
                {
                    ArgAttribute attribute = new ArgAttribute("helpmessage");
                    if (mandatoryFieldsSet.Contains(fieldInfo.Name))
                    {
                        attribute.IsMandatory = true;
                    }
                    ArgInstance argInstance = new ArgInstance(attribute, fieldInfo, GetType(), this);
                    argInstances.Add(argInstance);
                }
                return new FindResult(argInstances, ExecutionLog.CreateErrorLog(), ExecutionLog.CreateWarningLog());
            }
        }
    }
}
