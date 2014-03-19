using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Core;

namespace Validation.Test
{
    [TestClass]
    public class DemandTests
    {
        [TestMethod]
        public void Target_Explicit()
        {
            string name = "asdf";
            string value = "fdsa";

            var target = Demand.That(name, value);

            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(value, target.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Target_Explicit_NullName()
        {
            string name = null;
            string value = "asdf";

            try
            {
                var target = Demand.That(name, value);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("name", e.ParamName);
                Assert.AreEqual("name cannot be null or empty\r\nParameter name: name", e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Target_Explicit_EmptyName()
        {
            string name = string.Empty;
            string value = "asdf";

            try
            {
                var target = Demand.That(name, value);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("name", e.ParamName);
                Assert.AreEqual("name cannot be null or empty\r\nParameter name: name", e.Message);
                throw;
            }
        }

        [TestMethod]
        public void Target_Expression()
        {
            string name = "asdf";

            var target = Demand.That(() => name);

            Assert.AreEqual("name", target.Name);
            Assert.AreEqual("asdf", target.Value);
        }
    }
}
