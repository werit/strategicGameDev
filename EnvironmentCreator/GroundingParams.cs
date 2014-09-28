using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public static class GroundingParams
    {
        /** @brief Variable storing all integer variables of all instances
         * 
         */
        public static Dictionary<string,Dictionary<string, int>> m_InstanceIntegerVar;
        /** @brief Variable storing all boolean variables of all instances
         * 
         */
        public static Dictionary<string,Dictionary<string, bool>> m_InstanceBoolVar;
        /** @brief Variable storing type name and type.
         * 
         */
        public static Dictionary<string, Types> m_types;
        static GroundingParams()
        {
            m_InstanceBoolVar = new Dictionary<string,Dictionary<string, bool>>();
            // inicilize bool identifiers and their bool value
            m_InstanceBoolVar.Add("def_bool",new Dictionary<string,bool>());
            Dictionary<string,bool> defBool;
            m_InstanceBoolVar.TryGetValue("def_bool", out defBool);
            defBool.Add("true", true);
            defBool.Add("false", false);
            m_InstanceIntegerVar = new Dictionary<string,Dictionary<string, int>>();
            m_types = new Dictionary<string, Types>();
        }

        /** @brief Method grounding instance variables.
         * Method takes each variable of type and recursively of parent type till root type and creates them with prexif as instance name in the 
         * 
         * m_InstanceBoolVar or m_InstanceIntegerVar, depending if it is variable of type boolean or int.
         * 
         */
        public static void GroundInstance(Instance inst)
        {
            Types parent = inst.GetInstanceType();
            Dictionary<string, bool> boolDict;
            Dictionary<string, int> intDict;
            while (parent != null)
            {
                intDict = new Dictionary<string, int>();
                boolDict = new Dictionary<string, bool>();
                // filling integer variables
                for (int i = 0; i < parent.GetAllIntVar().Count(); ++i)
                {
                    intDict.Add(parent.GetAllIntVar()[i], 0);
                }
                m_InstanceIntegerVar.Add(inst.GetInstanceName(), intDict);
                // filling bool variables
                for (int i = 0; i < parent.GetAllBoolVar().Count(); ++i)
                {
                    boolDict.Add(parent.GetAllIntVar()[i], false);
                }
                m_InstanceBoolVar.Add(inst.GetInstanceName(), boolDict);
                // prepare for interation of parent
                parent = parent.GetAncestor();
            }
        }
    }
}
