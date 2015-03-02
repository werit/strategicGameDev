using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using EnvironmentCreator.Gammars;

namespace EnvironmentCreator
{
    /// <summary>
    /// Class storing all common static data.
    /// Class stores 
    /// <list type="bullet">
    /// <item><see cref="BoolOperators"/></item>
    /// <item><see cref="AssignOperators"/></item>
    /// <item><see cref="AritmeticOperators"/></item>
    /// <item><see cref="NodeReturnType"/></item>
    /// <item>grammar processors</item>
    /// <item>game processor <see cref="GameProcessing"/></item>
    /// </list>
    /// </summary>
    public static class GameStatData
    {
        /// <summary>
        /// Enumeration representing all compare operators used in <see cref="BinaryOp"/>.
        /// </summary>
        public enum BoolOperators
        {
            /// <summary>
            /// Operator representing equality between two nodes.
            /// </summary>
            EQUAL,
            /// <summary>
            /// Operator representing inequality between two nodes.
            /// </summary>
            NOT_EQUAL,
            /// <summary>
            /// Operator representing boolean oparator representing that left node is less than right node.
            /// </summary>
            LESS_THEN,
            /// <summary>
            /// Operator representing boolean oparator representing that left node is greater than right node.
            /// </summary>
            MORE_THEN,
            /// <summary>
            /// Operator representing boolean oparator representing that left node is less than or equal to right node.
            /// </summary>
            LESS_OR_EQUAL,
            /// <summary>
            /// Operator representing boolean oparator representing that left node is more than or equal to right node.
            /// </summary>
            MORE_OR_EQUAL
        }
        /// <summary>
        /// Enumeration representing all assign operators used in <see cref="BinaryOp"/>.
        /// </summary>
        public enum AssignOperators
        {
            /// <summary>
            /// Operator representing assigning value of right node to left node.
            /// </summary>
            ASSIGN,
            /// <summary>
            /// Operator representing subtracting value of right node from left node and assigning result to left node.
            /// </summary>
            SUBSTRACT_ASSIGN,
            /// <summary>
            /// Operator representing adding value of right node to left node and assigning result to left node.
            /// </summary>
            ADD_ASSIGN,
            /// <summary>
            /// Operator representing multiplicating value of left node by value of right node and assigning result to left node.
            /// </summary>
            MULTIPL_ASSIGN,
            /// <summary>
            /// Operator representing dividing value of left node by the value of right node and assigning result to left node.
            /// </summary>
            DIVISION_ASSIGN,
            /// <summary>
            /// Operator representing moduling value of left node by the value of right node and assigning result to left node.
            /// </summary>
            MODULO_ASSIGN
        }
        /// <summary>
        /// Enumeration representing defined artmetic operations on integers.
        /// Those aritmatic operations are used in <see cref="BinaryOp"/>.
        /// As of now there are defined five basic aritmatic operations on integers.
        /// </summary>
        public enum AritmeticOperators
        {
            /// <summary>
            /// Addition between left and right node of <see cref="EvaluationNode"/>.
            /// </summary>
            ADDITION,
            /// <summary>
            /// Subtraction of left node by right node.
            /// </summary>
            SUBSTRACTION,
            /// <summary>
            /// Multiplication between left and right node of <see cref="EvaluationNode"/>.
            /// </summary>
            MULTIPLICATION,
            /// <summary>
            /// Division of left node by right node.
            /// </summary>
            DIVISION,
            /// <summary>
            /// Modulation of left node by right node.
            /// </summary>
            MODULATION
        }
        /// <summary>
        /// Enumeration representing all return possible types of <see cref="EvaluationNode"/>.
        /// </summary>
        public enum NodeReturnType
        {
            /// <summary>
            /// Node can return only integer value.
            /// </summary>
            INT,
            /// <summary>
            /// Node can return only boolean value.
            /// </summary>
            BOOL,
            /// <summary>
            /// There is some uncertainty and node can return both integer or boolean.
            /// This might be due to same name of variable in parent and child node.
            /// </summary>
            INT_BOOL
        }
        /// <summary>
        /// Structure storing comparison operators in pair of its string representation and corresponding <see cref="BoolOperators"/>.
        /// </summary>
        public static Dictionary<string, BoolOperators> m_compareOper;
        /// <summary>
        /// ANTLR grammar visitor, which return type is <see cref="EvaluationNode"/>.
        /// </summary>
        public static GStrat_VIS_EvalNode m_assignNode_VIS;
        /// <summary>
        /// ANTLR grammar visitor, which return type is <see cref="NodeReturnType"/>.
        /// </summary>
        public static GStrat_VIS_ReturnType m_returnType_VIS;
        /// <summary>
        /// ANTLR grammar visitor, which return type is integer but its value is not used for any purpose.
        /// </summary>
        public static GStrat_VIS_int m_int_VIS;
        /// <summary>
        /// Method determining name of currently executed method.
        /// Of course before call of this method.
        /// </summary>
        /// <returns>String representation of name of last method on the stack.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }
        /// <summary>
        /// Only instance of <see cref="GameProcessing"/>, which process whole simulation.
        /// </summary>
        public static GameProcessing game;
        /// <summary>
        /// Static constructor which initialize all static variables.
        /// </summary>
        static GameStatData()
        {
            m_returnType_VIS = new GStrat_VIS_ReturnType();
            m_assignNode_VIS = new GStrat_VIS_EvalNode();
            m_int_VIS = new GStrat_VIS_int();

            m_compareOper = new Dictionary<string, BoolOperators>();

            m_compareOper.Add("==", BoolOperators.EQUAL);
            m_compareOper.Add("!=", BoolOperators.NOT_EQUAL);
            m_compareOper.Add("<", BoolOperators.LESS_THEN);
            m_compareOper.Add(">", BoolOperators.MORE_THEN);
            m_compareOper.Add("<=", BoolOperators.LESS_OR_EQUAL);
            m_compareOper.Add(">=", BoolOperators.MORE_OR_EQUAL);

            game = new GameProcessing();
        }
    }
}
