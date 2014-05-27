using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    class Car :IGramChoice
    {
        override public String Buy()
        {
            return "bought a car";
        }
    }
}
