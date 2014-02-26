using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation.Core;

namespace Validation.Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Hello World!";

            var result = Demand.That<string>("s", s)
                .Passes(t => !string.IsNullOrEmpty(t));
        }
    } 
}
