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
        static GroundingParams()
        {
            m_InstanceBoolVar = new Dictionary<string, bool>();
            m_InstanceIntegerVar = new Dictionary<string, int>();
        }
    }
}
