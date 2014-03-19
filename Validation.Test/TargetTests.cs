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
    public class TargetTests
    {
        [TestMethod]
        public void Constructor()
        {
            string name = "asdf";
            string value = "asdf";

            var target = new Target<string>(name, value);

            Assert.AreEqual(target.Name, name);
            Assert.AreEqual(target.Value, value);
            Assert.AreEqual(target.TypeSafeValue, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullName()
        {
            string value = "asdf";
            try
            {

                var target = new Target<string>(null, value);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("name cannot be null or empty\r\nParameter name: name", e.Message);
                Assert.AreEqual("name", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyName()
        {
            string name = string.Empty;
            string value = "asdf";
            try
            {
                var target = new Target<string>(name, value);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("name cannot be null or empty\r\nParameter name: name", e.Message);
                Assert.AreEqual("name", e.ParamName);
                throw;
            }
        }
    }
}
