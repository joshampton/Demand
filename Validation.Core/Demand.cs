﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation.Core
{
    public static class Demand
    {
        public static Target<TargetType> That<TargetType>(string name, TargetType value)
        {
            return new Target<TargetType>(name, value);
        }
    }
}
