using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Test.Sample
{
    public class ASample
    {
        public BSample Thing { get; set; }

        public string NoParameters()
        {
            return string.Empty;
        }

        public string DoSomething(string something)
        {
            return something;
        }

        public BSample GetThing()
        {
            return this.Thing;
        }

        public string GenericMethod<T>()
        {
            return string.Empty;
        }
    }
}
