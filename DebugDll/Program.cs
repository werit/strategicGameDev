using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DebugDll
{
    class Program
    {
        static void Main(string[] args)
        {
            //Factory fctr = new Factory();
            //IGramChoice choice = Factory.getChoiceObj("car");
            //Console.WriteLine(choice.Buy());
            //fctr.ParsGramm();
            Dictionary<int, int> test = new Dictionary<int, int>();
            test[0] = 1;
            int i;
            test.TryGetValue(0,out i);
            i = 5;
            Console.WriteLine(test[0]);
        }
    }
}
