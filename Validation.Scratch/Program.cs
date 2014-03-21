using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Validation.Core;

namespace Validation.Scratch
{

    public class Dog
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Sound { get; set; }

        public string MakeSound()
        {
            return this.Sound;
        }

        public string DoSomething(string something)
        {
            return something;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    } 
}
