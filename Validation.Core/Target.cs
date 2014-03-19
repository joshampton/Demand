using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Validation.Core
{
    [DebuggerTypeProxy(typeof(TargetDebuggerProxy))]
    public abstract class Target
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly object value;

        public string Name { get { return this.name; } }
        public object Value { get { return this.value; } }

        public Target(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be null or empty", "name");

            this.name = name;
            this.value = value;
        }
    }

    [DebuggerTypeProxy(typeof(TargetDebuggerProxy))]
    public sealed class Target<TargetType> : Target
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TargetType typeSafeValue;
        
        public TargetType TypeSafeValue { get { return typeSafeValue; } }

        public Target(string name, TargetType value) : base(name, value)
        {
            this.typeSafeValue = value;
        }
    }

    [DebuggerDisplay("Name = {Name}")]
    internal sealed class TargetDebuggerProxy
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Target target;

        public string Name { get { return target.Name; } }
        public object Value { get { return target.Value; } }

        public TargetDebuggerProxy(Target target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            this.target = target;
        }
    }
}