using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public class Factory
    {
        static public IGramChoice getChoiceObj(String objName){
            IGramChoice choice = null;
            switch (objName.ToLower())
	        {
                case "car" :
                    choice = new Car();
                    break;
                    
	        }
            return choice;
        }
        public void start()
        {
            Console.Out.WriteLine("Hello");
        }
        public 
    }
}
