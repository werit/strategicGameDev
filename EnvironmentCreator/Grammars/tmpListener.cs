using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    class tmpListener : CalculatorBaseListener
    {
        public override void EnterAddSub(CalculatorParser.AddSubContext context)
        {
            System.Console.Out.WriteLine("Som v EnterAddSub v listeneri");
        }
    }
}
