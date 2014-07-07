using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public static class GroundingParams
    {
        public static Dictionary<string, int> m_InstanceIntegerVar;
        public static Dictionary<string, bool> m_InstanceBoolVar;
        public static Dictionary<string, Types> m_types;
        static GroundingParams()
        {
            m_InstanceBoolVar = new Dictionary<string, bool>();
            // inicilize bool identifiers and their bool value
            m_InstanceBoolVar.Add("true", true);
            m_InstanceBoolVar.Add("false", false);
            m_InstanceIntegerVar = new Dictionary<string, int>();
            m_types = new Dictionary<string, Types>();
        }
    }
}
