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
    public static class GameStatData
    {
        /** @brief Enumeration representing all compare operators
         * 
         */ 
        public enum BoolOperators
        {
            EQUAL,
            NOT_EQUAL,
            LESS_THEN,
            MORE_THEN,
            LESS_OR_EQUAL,
            MORE_OR_EQUAL
        }
        public enum NodeReturnType
        {
            INT,
            BOOL,
            INT_BOOL
        }
        public static Dictionary<string, BoolOperators> m_compareOper;
        public static GStrat_VIS_CondNode m_condNode_VIS;
        public static GStrat_VIS_ReturnType m_returnType_VIS;
        public static GStrat_VIS_int m_int_VIS;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
        /** @brief Static initialisation of dictionary data.
         * Constructor takes dictionaries of this class and initialise them.
         */
        static GameStatData()
        {
            m_condNode_VIS = new GStrat_VIS_CondNode();
            m_returnType_VIS = new GStrat_VIS_ReturnType();
            m_int_VIS = new GStrat_VIS_int();

            m_compareOper = new Dictionary<string, BoolOperators>();

            m_compareOper.Add("==", BoolOperators.EQUAL);
            m_compareOper.Add("!=", BoolOperators.NOT_EQUAL);
            m_compareOper.Add("<", BoolOperators.LESS_THEN);
            m_compareOper.Add(">", BoolOperators.MORE_THEN);
            m_compareOper.Add("<=", BoolOperators.LESS_OR_EQUAL);
            m_compareOper.Add(">=", BoolOperators.MORE_OR_EQUAL);
        }
    }
}
