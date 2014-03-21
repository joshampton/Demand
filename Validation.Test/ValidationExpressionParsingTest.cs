using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validation.Test.Sample;
using Validation.Core;
using System.Linq.Expressions;

namespace Validation.Test
{
    [TestClass]
    public class ValidationExpressionParsingTest
    {
        [TestMethod]
        public void Property()
        {
            var dog = new ASample();

            Expression<Func<BSample>> temp = () => dog.Thing;

            string result = temp.Visit();

            Assert.AreEqual("dog.Thing", result);
        }

        [TestMethod]
        public void Method()
        {
            var dog = new ASample();
            Expression<Func<string>> temp = () => dog.NoParameters();
            var visitor = new ValidationExpressionVisitor();
            visitor.Visit(temp);

            Assert.AreEqual("dog.NoParameters()", visitor.Expression);
        }

        [TestMethod]
        public void Method_Parameters()
        {
            var dog = new ASample();

            Expression<Func<string>> temp = () => dog.DoSomething("argumentValue");

            string result = temp.Visit();

            Assert.AreEqual("dog.DoSomething(String something)", result);
        }

        [TestMethod]
        public void ChainedMethod_Parameters()
        {
            var dog = new ASample();
            Expression<Func<string>> temp = () => dog.DoSomething("argValue").Replace("a", "b");

            string result = temp.Visit();

            Assert.AreEqual("dog.DoSomething(String something).Replace(String oldValue, String newValue)", result);
        }

        [TestMethod]
        public void ChainedProps()
        {
            var dog = new ASample { Thing = new BSample { Prop = 20 } };
            Expression<Func<float>> temp = () => dog.Thing.Prop;
            var visitor = new ValidationExpressionVisitor();
            visitor.Visit(temp);

            Assert.AreEqual("dog.Thing.Prop", visitor.Expression);
        }

        [TestMethod]
        public void ChainedMethods()
        {
            var dog = new ASample { Thing = new BSample { Prop = 20f } };
            Expression<Func<float>> temp = () => dog.GetThing().Prop;

            string result = temp.Visit();

            Assert.AreEqual("dog.GetThing().Prop", result);
        }

        [TestMethod]
        public void ChainedProps_And_Methods()
        {
            var dog = new ASample { Thing = new BSample { Prop = 20f } };
            Expression<Func<string>> temp = () => dog.Thing.DoSomething();
            var visitor = new ValidationExpressionVisitor();
            visitor.Visit(temp);

            Assert.AreEqual("dog.Thing.DoSomething()", visitor.Expression);
        }

        [TestMethod]
        public void GenericMethod()
        {
            var dog = new ASample();

            Expression<Func<string>> temp = () => dog.GenericMethod<string>();
            string result = temp.Visit();

            Assert.AreEqual("dog.GenericMethod<String>()", result);
        }

        [TestMethod]
        public void SimpleConstant()
        {
            var dog = new ASample();

            Expression<Predicate<ASample>> expr = d => false;

            string result = expr.Visit();

            Assert.AreEqual("False", result);
        }
    }
}
