using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Validation.Core;

namespace Validation.Test
{
    [TestClass]
    public class ValidationFailedExceptionTests
    {
        [TestMethod]
        public void Constructor()
        {
            var exception = new ValidationFailedException();
        }

        [TestMethod]
        public void Constructor_Message()
        {
            var message = "asdf";
            var exception = new ValidationFailedException(message);

            Assert.AreEqual("asdf", exception.Message);
        }

        [TestMethod]
        public void Constructor_Message_and_Exception()
        {
            var message = "asdf";
            var innerException = new Exception();
            var exception = new ValidationFailedException(message, innerException);

            Assert.AreEqual("asdf", message);
            Assert.AreEqual(innerException, exception.InnerException);
            Assert.ReferenceEquals(innerException, exception.InnerException);
        }
    }
}
