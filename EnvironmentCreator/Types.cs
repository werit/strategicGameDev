using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public class Types
    {
        private static int m_nonameCnt;
        /*
         * Reference to ancestor of type.
         */
        private Types m_ancestor;
        
        /**
         * Internal storage for all descendants of this type
         */
        private List<Types> m_desc = new List<Types>();

        /* 
         * Default ancestor of all types.
         */
        private static Types m_defaultAncestor;
        /**
         * Method creates copy of descendants and returns this copy. Therefore changes in array will not affect source.
         * @return Method returns array of descendants of this type.
         */
        public Types[] GetDesc()
        {
            return this.m_desc.ToArray();
        }

        /**
         * Add descendant passed as parameter to list of descendants.
         */
        public void AddDesc(Types additionDesc)
        {
            m_desc.Add(additionDesc);
        }
        
        /*
         * Pointer to all instances of this type.
         */
        private List<Instance> m_typeInstances = new List<Instance>();

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

        /**
         * Method creates copy of list of instances and returns this copy. Therefore changes in array will not affect source.
         * @return Method returns array of instances of this type.
         */
        public Instance[] GetInst()
        {
            return this.m_typeInstances.ToArray();
        }

        /**
         * Add instance passed as parameter to list of instances.
         */
        public void AddInst(Instance[] newInstances)
        {
            this.m_typeInstances.AddRange(newInstances);
        }

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
            m_nonameCnt = 0;
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
            m_name = name != null ? name : "noname" + m_nonameCnt++;
            if (ancestor == null)
            {
                this.m_ancestor = Types.m_defaultAncestor;
                Types.m_defaultAncestor.AddDesc(this);
            }
            else
            {
                this.m_ancestor = ancestor;
                this.m_ancestor.AddDesc(this);
            }
            this.m_ancestor.AddDesc(this);
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
        /** @brief Method returning ancestor of type.
         * 
         */
        public Types GetAncestor()
        {
            return this.m_ancestor;
        }
    }
}
