using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentCreator;

namespace DebugDll
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory fctr = new Factory();
            IGramChoice choice = Factory.getChoiceObj("car");
            Console.WriteLine(choice.Buy());
            fctr.ParsGramm();
        }
    }
}
