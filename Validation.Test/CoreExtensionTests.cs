using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Validation.Core;

namespace Validation.Test
{
    [TestClass]
    public class CoreExtensionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Passes_NullTarget()
        {
            Target<string> target = null;

            try
            {
                CoreExtensions.Passes<string>(target, t => false);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("target", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Passes_NullValidation()
        {
            Target<string> target = new Target<string>("name", "value");

            try
            {
                CoreExtensions.Passes(target, null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("validation", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        public void Passes_Pass()
        {
            var target = new Target<string>("name", "value");
            Expression<Predicate<string>> expr = s => true;

            CoreExtensions.Passes(target, expr);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationFailedException))]
        public void Passes_Fails()
        {
            var target = new Target<string>("name", "value");
            Expression<Predicate<string>> expr = s => false;

            try
            {
                CoreExtensions.Passes(target, expr);
            }
            catch (ValidationFailedException e)
            {
                Assert.AreEqual(target.Name, e.TargetName);
                Assert.AreEqual("False", e.Validation);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationFailedException))]
        public void Passes_Fails_Exception()
        {
            var target = new Target<string>("name", "value");

            var returnTarget = Expression.Label(typeof(bool));
            var returnExpression = Expression.Return(returnTarget, Expression.Constant(true, typeof(bool)), typeof(bool));
            var returnLabel = Expression.Label(returnTarget, Expression.Default(typeof(bool)));

            var body = Expression.Block(
                Expression.Throw(Expression.New(typeof(Exception))),
                returnExpression,
                returnLabel
                );

            Expression<Predicate<string>> expr = Expression.Lambda<Predicate<string>>(body, Expression.Parameter(typeof(string), "s"));

            try
            {
                CoreExtensions.Passes(target, expr);
            }
            catch (ValidationFailedException e)
            {
                Assert.IsNotNull(e.InnerException);
                Assert.AreEqual(e.TargetName, "name");
                Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
                throw;
            }
        }

        [TestMethod]
        public void Fails_Pass()
        {
            var target = new Target<string>("name", "value");
            Expression<Predicate<string>> expr = s => false;

            CoreExtensions.Fails(target, expr);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationFailedException))]
        public void Fails_Fails()
        {
            var target = new Target<string>("name", "value");
            Expression<Predicate<string>> expr = s => true;
            try
            {
                CoreExtensions.Fails(target, expr);
            }
            catch (ValidationFailedException e)
            {
                Assert.AreEqual(target.Name, e.TargetName);
                Assert.AreEqual("True", e.Validation);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationFailedException))]
        public void Fails_Fails_Exception()
        {
            var target = new Target<string>("name", "value");

            var returnTarget = Expression.Label(typeof(bool));
            var returnExpression = Expression.Return(returnTarget, Expression.Constant(true, typeof(bool)), typeof(bool));
            var returnLabel = Expression.Label(returnTarget, Expression.Default(typeof(bool)));

            var body = Expression.Block(
                Expression.Throw(Expression.New(typeof(Exception))),
                returnExpression,
                returnLabel
                );

            Expression<Predicate<string>> expr = Expression.Lambda<Predicate<string>>(body, Expression.Parameter(typeof(string), "s"));

            try
            {
                CoreExtensions.Fails(target, expr);
            }
            catch (ValidationFailedException e)
            {
                Assert.IsNotNull(e.InnerException);
                Assert.AreEqual(e.TargetName, "name");
                Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Fails_NullTarget()
        {
            Target<string> target = null;

            try
            {
                CoreExtensions.Fails(target, t => false);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("target", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Fails_NullValidation()
        {
            Target<string> target = new Target<string>("name", "value");
            try
            {
                CoreExtensions.Fails(target, null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("validation", e.ParamName);
                throw;
            }
        }
    }
}
