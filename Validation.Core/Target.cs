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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<ValidationResult> results;

        public string Name { get { return this.name; } }
        public object Value { get { return this.value; } }

        //TODO: write separate enumerator, this doesn't protect the list
        public IEnumerable<ValidationResult> Results { get { return this.results; } }

        public Target(string name, object value)
        {
            this.name = name;
            this.value = value;
            this.results = new List<ValidationResult>();
        }

        public void AddValidationResult(ValidationResult result)
        {
            this.results.Add(result);
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
        public IEnumerable<ValidationResult> Results { get { return target.Results; } }

        public TargetDebuggerProxy(Target target)
        {
            this.target = target;
        }
    }
}
