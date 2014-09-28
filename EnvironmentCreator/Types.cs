using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public class Types
    {
        /* 
         * Default ancestor of all types.
         */
        private static Types m_defaultAncestor;
        /*
         * Default action of default ancestor.
         */
        private static Action m_ancestDefaultAction;
        /*
         * Reference to ancestor of type.
         */
        private Types m_ancestor;
        /* 
         * Default action of this type.
         */ 
        private Action m_defaultAction;
        /*
         * Pointer to all instances of this type.
         */
        private List<Instance> m_typeInstances;
        /**
         * Variable containing names of all integer values of this type and theirs values.
         */
        private List<string> m_intVariables;
        /**
         * Variable containing names of all boolean values of this type and theirs values.
         */
        private List<string> m_boolVariables;
        /*
         * Variable storing type name.
         */
        private string m_name = null;

        /** @brief Static constructor for initialising.
         * Static constructor to inicialize static variables:
         *  - m_defaultAncestor
         *  - m_defaultAction
         */
        static Types()
        {
            // TODO: default action
            m_defaultAncestor = new Types("defaultAncestor");
            m_defaultAncestor.m_ancestor = null;
            //m_defaultAction = new Action("doNothing"); 
        }
        public Types()
        {
            m_intVariables = new List<string>();
            m_boolVariables = new List<string>();
            m_ancestor = Types.m_defaultAncestor;
        }
        public Types(string name, Types ancestor = null, List<string> intVariables = null, List<string> boolVariables = null)
        {
            m_name = name != null ? name : "noname";
            m_ancestor = ancestor != null ? ancestor : Types.m_defaultAncestor;
            m_intVariables = intVariables != null ? intVariables : new List<string>();
            m_boolVariables = boolVariables != null ? boolVariables : new List<string>();
        }
        public string GetName()
        {
            return this.m_name;
        }
        //public bool TryGetVariable(string variable, out int value)
        //{
        //    if (m_intVariables.TryGetValue(variable, out value))
        //        return true;
        //    else
        //        if (m_ancestor != null)
        //            return m_ancestor.TryGetVariable(variable, out value);
        //        else return false;
        //}
        //public bool TryGetVariable(string variable, out bool value)
        //{
        //    if (m_boolVariables.TryGetValue(variable, out value))
        //        return true;
        //    else
        //        if (m_ancestor != null)
        //            return m_ancestor.TryGetVariable(variable, out value);
        //        else return false;
        //}
        public string[] GetAllIntVar()
        {
            return m_intVariables.ToArray();
        }
        public string[] GetAllBoolVar()
        {
            return m_boolVariables.ToArray();
        }
        /** @brief Method returning default action of type.
         * Method returns default action of this type.
         * If default action is set to null, then method will set it's instance's #m_defaultAction to default action of ancestor. 
         * @return Value set as default action of this type. Null is never return value.
         */
        public Action GetDefaultAction()
        {
            if (this.m_defaultAction != null)
                return this.m_defaultAction;
            else
            {
                Action act = m_ancestor.GetDefaultAction();
                this.m_defaultAction = act;
                return act;
            }
        }
        /** @brief Method returning ancestor of type.
         * 
         */
        public Types GetAncestor()
        {
            return this.m_ancestor;
        }
    }
}
