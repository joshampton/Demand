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
            Dog d = new Dog { Sound = "Woof" };

            if (d == null)
                throw new ValidationFailedException();

            if (string.IsNullOrEmpty(d.Sound))
                throw new ValidationFailedException();

            if (d.Sound != "Woof")
                throw new ValidationFailedException();

            Demand.That(() => d)
                .Passes(x => x != null)
                .Passes(x => !string.IsNullOrEmpty(x.Sound))
                .Passes(x => x.Sound == "Woof");
        }
    } 
}
