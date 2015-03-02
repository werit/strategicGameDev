using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using EnvironmentCreator.Gammars;

namespace EnvironmentCreator
{
    /// <summary>
    /// Error message when name of <see cref="Instance"/> variable cannot be found.
    /// Parameter does not exist or <see cref="Instance"/> of this parameter was not created.
    /// </summary>
    public class UnknownParameterExc : System.Exception
    {
        /// <summary>
        /// Error message when name of <see cref="Instance"/> variable cannot be found.
        /// Parameter does not exist or <see cref="Instance"/> of this parameter was not created.
        /// </summary>
        public UnknownParameterExc(string id)
        {
            Console.Error.WriteLine("Parameter {0} was not found as variable of any type.", id);
        }
    }
    /// <summary>
    /// Class consist of exceptions related to unxpected/wrong type of parameter was passed to <see cref="Action"/>.
    /// </summary>
    public class UnxpectedParamTypeExc : SystemException
    {
        /// <summary>
        /// Method describes error of wrong variable during <see cref="GameStatData.NodeReturnType"/> compare operation.
        /// </summary>
        /// <param name="expectedType"><see cref="GameStatData.NodeReturnType"/> type of variable that was expected.</param>
        /// <param name="gotType"><see cref="GameStatData.NodeReturnType"/> type of variable that was passed to compare operation.</param>
        /// <param name="compareFunction">Specific compare operation.</param>
        public UnxpectedParamTypeExc(GameStatData.NodeReturnType expectedType, GameStatData.NodeReturnType gotType, GameStatData.BoolOperators compareFunction)
        {
            Console.Error.WriteLine("Different return types was passed as parameter of compare function '{0}', expected parameter type was '{1}' and got '{2}'",
                compareFunction, expectedType.ToString(), gotType.ToString());
        }
        /// <summary>
        /// Method describes error of wrong variable during <see cref="GameStatData.NodeReturnType"/> assign operation.
        /// </summary>
        /// <param name="expectedType"><see cref="GameStatData.NodeReturnType"/> type of variable that was expected.</param>
        /// <param name="gotType"><see cref="GameStatData.NodeReturnType"/> type of variable that was passed to assign operation.</param>
        /// <param name="compareFunction">Specific assign operation.</param>
        public UnxpectedParamTypeExc(GameStatData.NodeReturnType expectedType, GameStatData.NodeReturnType gotType, GameStatData.AssignOperators assignFunction)
        {
            Console.Error.WriteLine("Different return types was passed as parameter of assign function '{0}', expected parameter type was '{1}' and got '{2}'",
                assignFunction, expectedType.ToString(), gotType.ToString());
        }
    }
    /// <summary>
    /// Class handling error of not defined or supported function/<see cref="Action"/>.
    /// </summary>
    public class FunctionNotSupportedExcp : SystemException
    {
        /// <summary>
        /// Exception constructor. Construct exception describng error during call of not existing or not supported function/<see cref="Action"/>.
        /// </summary>
        /// <param name="methodBs">Parameter describing information about methods and constructors which made error.</param>
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
    /// <summary>
    /// Class handling exception of unexpected token during processing user input.
    /// </summary>
    public class UnexpectedParserToken : SystemException
    {
        /// <summary>
        /// Class constructor creating error output for unexpected token read throught grammar, which cannot be processed.
        /// </summary>
        /// <param name="where">Parameter describing which method grammar part contains error.</param>
        public UnexpectedParserToken(string where)
        {
            Console.Error.WriteLine("Found unexpected token in '{0}'",where);
        }
    }
    /// <summary>
    /// Class containing exception when parameter <see cref="GameStatData.NodeReturnType"/> cannot be choosen.
    /// </summary>
    /// <example>For example type has two variables of the same name of different types and program cannot choose which one to use for comparision.</example>
    public class CannotChooseVariableTypeExc : SystemException
    {
        /// <summary>
        /// Constructor of exception handles error message and describes which variable <see cref="GameStatData.NodeReturnType"/> cannot be determined.
        /// </summary>
        /// <param name="variable">Name of variable.</param>
        public CannotChooseVariableTypeExc(string variable)
        {
            Console.Error.WriteLine("Cannot choose variable type of '{0}' variable from context.", variable);
        }
    }
    /// <summary>
    /// Exception handling error when user set name of ancestor <see cref="Types"/> to not existing <see cref="Types"/>.
    /// Meaning that <see cref="Types"/> extends from non existing <see cref="Types"/>.
    /// </summary>
    public class UnknownAncestorType : SystemException
    {
        /// <summary>
        /// Constructor creates error message with string representation of name of ancestor <see cref="Types"/>, that was not found.
        /// </summary>
        /// <param name="ancestor">String representation of name of not found ancestor <see cref="Types"/>.</param>
        public UnknownAncestorType(string ancestor)
        {
            Console.Error.WriteLine("Ancestor with name '{0}', was not fund.", ancestor);
        }
    }
    /// <summary>
    /// Class representing exception when creating new <see cref="Instance"/> with not defined name of <see cref="Types"/>.
    /// </summary>
    public class UnknownType : SystemException
    {
        /// <summary>
        /// Constructor send message to error output. Message describes which name of parameter was not found.
        /// </summary>
        /// <param name="typeName">String representation of not found name of <see cref="Types"/>.</param>
        public UnknownType(string typeName)
        {
            Console.Error.WriteLine("Type with name '{0}', was not fund.", typeName);
        }
    }
}
