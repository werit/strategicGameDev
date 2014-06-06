using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EnvironmentCreator
{
    class Exceptions
    {
    }
    public class UnknownParameterExc : System.Exception
    {
        public UnknownParameterExc(string id)
        {
            Console.Error.WriteLine("Parameter {0} was not found as variable of any type.", id);
        }
    }
    public class UnxpectedParamTypeExc : SystemException
    {
        public UnxpectedParamTypeExc(GameStatData.NodeReturnType expectedType, GameStatData.NodeReturnType gotType, GameStatData.BoolOperators compareFunction)
        {
            Console.Error.WriteLine("Different return types was passed as parameter of compare function '{0}', expected parameter type was '{1}' and got '{2}'",
                compareFunction, expectedType.ToString(), gotType.ToString());
        }
    }
    public class FunctionNotSupportedExcp : SystemException
    {
        public FunctionNotSupportedExcp(MethodBase methodBs)
        {
            Console.Error.Write("Method '{0}' with parameters:", methodBs.Name.ToString());
            for (int i = 0; i < methodBs.GetParameters().Length; ++i)
            {
                Console.Error.Write(" '{0}' '{1}'",methodBs.GetParameters()[i].GetType().Name.ToString(),methodBs.GetParameters()[i].Name.ToString());
            }
            Console.Error.WriteLine(" is not supported.");
        }
    }
}
