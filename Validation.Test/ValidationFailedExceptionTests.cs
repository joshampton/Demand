﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
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
        public void SerializationConstructor()
        {
            var input = new ValidationFailedException("targetNameValue", "validationValue");

            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, input);

            stream.Seek(0, SeekOrigin.Begin);

            var output = (ValidationFailedException)formatter.Deserialize(stream);

            Assert.IsFalse(object.ReferenceEquals(input, output));
            Assert.AreEqual(input.TargetName, output.TargetName);
            Assert.AreEqual(input.Validation, output.Validation);
        }

        [TestMethod]
        public void SerializationConstructor_InnerException()
        {
            var inner = new Exception("innerExceptionMessage");

            var input = new ValidationFailedException("targetNameValue", "validationValue", inner);

            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, input);

            stream.Seek(0, SeekOrigin.Begin);

            var output = (ValidationFailedException)formatter.Deserialize(stream);

            Assert.IsFalse(object.ReferenceEquals(input, output));
            Assert.AreEqual(input.TargetName, output.TargetName);
            Assert.AreEqual(input.Validation, output.Validation);
            Assert.IsNotNull(output.InnerException);
            Assert.IsFalse(object.ReferenceEquals(output.InnerException, inner));
            Assert.AreEqual(inner.Message, output.InnerException.Message);
        }

        [TestMethod]
        public void Constructor()
        {
            string targetName = "name";
            string validation = "validation";

            var exception = new ValidationFailedException(targetName, validation);

            Assert.AreEqual(targetName, exception.TargetName);
            Assert.AreEqual(validation, exception.Validation);

        }

        [TestMethod]
        public void Constructor_InnerException()
        {
            string targetName = "name";
            string validation = "validation";
            var innerException = new Exception();

            var exception = new ValidationFailedException(targetName, validation, innerException);

            Assert.AreEqual(targetName, exception.TargetName);
            Assert.AreEqual(validation, exception.Validation);
            Assert.AreEqual(exception.InnerException, innerException);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyName()
        {
            string targetName = string.Empty;
            string validation = "validation";
            try
            {

                var exception = new ValidationFailedException(targetName, validation);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("targetName", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullName()
        {
            string targetName = null;
            string validation = "validation";

            try
            {
                var exception = new ValidationFailedException(targetName, validation);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("targetName", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyValidation()
        {
            string targetName = "name";
            string validation = string.Empty;

            try
            {
                var exception = new ValidationFailedException(targetName, validation);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("validation", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        public void GetObjectData()
        {
            var info = new SerializationInfo(typeof(ValidationFailedException), new FormatterConverter());
            var context = new StreamingContext();

            var exception = new ValidationFailedException("targetNameValue", "validationValue");

            exception.GetObjectData(info, context);

            Assert.AreEqual("targetNameValue", info.GetString("targetName"));
            Assert.AreEqual("validationValue", info.GetString("validation"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetObjectData_NullInfo()
        {
            var exception = new ValidationFailedException("asdf", "asdf");

            try
            {
                exception.GetObjectData(null, new StreamingContext());
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("info", e.ParamName);
                throw;
            }
        }
    }
}
