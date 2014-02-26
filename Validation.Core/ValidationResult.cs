using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Validation.Core
{
    [DebuggerDisplay("Pass = {Pass}, Message = {Message}")]
    public sealed class ValidationResult
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool pass;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string message;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Exception exception;

        public bool Pass { get { return this.pass; } }
        public string Message { get { return this.message; } }
        public Exception Exception { get { return this.exception; } }

        public ValidationResult(bool pass, string message, Exception exception)
        {
            this.pass = pass;
            this.message = message;
            this.exception = exception;
        }
    }
}
