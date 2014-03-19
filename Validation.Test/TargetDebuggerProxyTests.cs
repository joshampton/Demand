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
    public class TargetDebuggerProxyTests
    {
        [TestMethod]
        public void Constructor()
        {
            var target = new Target<string>("asdf", "fdsa");
            var proxy = new TargetDebuggerProxy(target);

            Assert.AreEqual("asdf", proxy.Name);
            Assert.AreEqual("fdsa", proxy.Value);
            Assert.ReferenceEquals(target.Name, proxy.Name);
            Assert.ReferenceEquals(target.Value, target.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullTarget()
        {
            var temp = new TargetDebuggerProxy(null);
        }
    }
}
