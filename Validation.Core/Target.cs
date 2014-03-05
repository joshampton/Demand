using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Validation.Core
{
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
            this.target = target;
        }
    }
}
