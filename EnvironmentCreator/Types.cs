using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public class Types
    {
        private Types m_ancestor;
        private Dictionary<string,int> m_intVariables;
        private Dictionary<string, bool> m_boolVariables;
        private string m_name = null;
        public Types()
        {
            m_intVariables = new Dictionary<string, int>();
            m_boolVariables = new Dictionary<string, bool>();
            m_ancestor = null;
        }
        public Types(string name, Types ancestor, Dictionary<string, int> intVariables, Dictionary<string, bool> boolVariables)
        {
            m_name = name != null ? name : "noname";
            m_ancestor = ancestor != null ? ancestor : null;
            m_intVariables = intVariables != null ? intVariables : new Dictionary<string, int>();
            m_boolVariables = boolVariables != null ? boolVariables : new Dictionary<string, bool>();
        }
        public string GetName()
        {
            return this.m_name;
        }
        public bool TryGetVariable(string variable, out int value)
        {
            if (m_intVariables.TryGetValue(variable, out value))
                return true;
            else
                if (m_ancestor != null)
                    return m_ancestor.TryGetVariable(variable, out value);
                else return false;
        }
        public bool TryGetVariable(string variable, out bool value)
        {
            if (m_boolVariables.TryGetValue(variable, out value))
                return true;
            else
                if (m_ancestor != null)
                    return m_ancestor.TryGetVariable(variable, out value);
                else return false;
        }
        public string[] GetAllIntVar()
        {
            return m_intVariables.Keys.ToArray();
        }
        public string[] GetAllBoolVar()
        {
            return m_boolVariables.Keys.ToArray();
        }
    }
}
