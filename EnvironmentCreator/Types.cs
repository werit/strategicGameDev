using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EnvironmentCreator
{
    /// <summary>
    /// Class representing types of instances used in simulation.\n
    /// For example you have type tree and it's instance might be spurce or beech.\n
    /// Each instance in similation have some type.\n
    /// Types implement inheritance structure. Therefore any property of parent type, will also have instance of descendant type.\n
    /// For example descndat type of tree is conifer. Tree has property of root. Instance of conifer, pine, will also have property of root\n
    /// without necessity of it's defining in type conifer.
    /// </summary>
    public class Types
    {
        /// <summary>
        /// Internal counter of noname types.
        /// </summary>
        private static int m_nonameCnt;
        /// <summary>
        /// Reference to ancestor of type.
        /// </summary>
        private Types m_ancestor;
        /// <summary>
        /// Internal storage for all descendants of this type
        /// </summary>
        private List<Types> m_desc = new List<Types>();
        /// <summary>
        /// Default ancestor of all types.
        /// </summary>
        private static Types m_defaultAncestor;
        /// <summary>
        /// Method creates copy of descendants and returns this copy. Therefore changes in array will not affect source.
        /// </summary>
        /// <returns>Method returns array of descendants of this type.</returns>
        public Types[] GetDesc()
        {
            return this.m_desc.ToArray();
        }
        /// <summary>
        /// Add descendant passed as parameter to list of descendants of this type.
        /// </summary>
        /// <param name="additionDesc">Descendant which is added to set of descendants of this type.</param>
        public void AddDesc(Types additionDesc)
        {
            m_desc.Add(additionDesc);
        }
        /// <summary>
        /// Pointer to all instances of this type.\nDo not contain instances of descendants.
        /// </summary>
        private List<Instance> m_typeInstances = new List<Instance>();

        /// <summary>
        /// Variable containing names of all integer values of this type and their values.
        /// </summary>
        private List<string> m_intVariables;
        /// <summary>
        /// Variable containing names of all boolean values of this type and their values.
        /// </summary>
        private List<string> m_boolVariables;
        /// <summary>
        /// Variable storing type name.
        /// </summary>
        private string m_name = null;

        /// <summary>
        ///  Method creates copy of list of instances and returns this copy. Therefore changes in array will not affect source.
        /// </summary>
        /// <returns>Method returns array of instances of this type.</returns>
        public Instance[] GetInst()
        {
            return this.m_typeInstances.ToArray();
        }
        /// <summary>
        /// Add instance passed as parameter to list of instances.
        /// </summary>
        /// <param name="newInstance">New <see cref="Instance"/> to be added as instance of this type.</param>
        public void AddInst(Instance newInstance)
        {
            this.m_typeInstances.Add(newInstance);
        }
        /// <summary>
        /// Add instance passed as parameter to list of instances.
        /// </summary>
        /// <param name="newInstances">New <see cref="Instance"/> to be added as instance of this type.</param>
        public void AddInst(Instance[] newInstances)
        {
            this.m_typeInstances.AddRange(newInstances);
        }
        /// <summary>
        /// Static constructor to inicialize static variables:
        /// <list type="bullet">
        /// <item>m_defaultAncestor</item>
        /// <item>m_nonameCnt</item>
        /// </list>
        /// </summary>
        static Types()
        {
            m_defaultAncestor = new Types();
            m_defaultAncestor.m_ancestor = null;
            m_nonameCnt = 0;
        }
        /// <summary>
        /// Constructor inicialize array for names of integer and boolean variables of <see cref="Types"/>.
        /// Also ancestor of <see cref="Types"/> is set.
        /// </summary>
        public Types()
        {
            m_intVariables = new List<string>();
            m_boolVariables = new List<string>();
            m_ancestor = Types.m_defaultAncestor;
            if (m_defaultAncestor != null) // first addition will be during creation of default ancestor
                Types.m_defaultAncestor.AddDesc(this);
        }
        /// <summary>
        /// Constructor inicialize array for names of integer and boolean variables of <see cref="Types"/>.\n
        /// Also ancestor of <see cref="Types"/> is set. In case of null ancestor, it is default ancestor.\n
        /// Else, <see cref="Types"/> passed as parameter is set to be ancestor of this <see cref="Types"/>.
        /// </summary>
        /// <param name="name">Name of type.</param>
        /// <param name="ancestor">Ancestor of this <see cref="Types"/>.</param>
        /// <param name="intVariables">Array of integer variables of this <see cref="Types"/>.</param>
        /// <param name="boolVariables">Array of boolean variables of this <see cref="Types"/>.</param>
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
            m_intVariables = intVariables != null ? intVariables : new List<string>();
            m_boolVariables = boolVariables != null ? boolVariables : new List<string>();
        }
        /// <summary>
        /// Method return string representation of name of type stored in private variable.
        /// </summary>
        /// <returns>Name of <see cref="Types"/> in string representation.</returns>
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
        /// <summary>
        /// Method returns copy of array of all names of integer variables of this <see cref="Types"/>.
        /// Therefore change in array will not affect names of variables of this <see cref="Types"/>.
        /// </summary>
        /// <returns>Array of all integer variable names.</returns>
        public string[] GetAllIntVar()
        {
            return m_intVariables.ToArray();
        }
        /// <summary>
        /// Method returns copy of array of all names of boolean variables of this <see cref="Types"/>.
        /// Therefore change in array will not affect names of variables of this <see cref="Types"/>.
        /// </summary>
        /// <returns>Array of all boolean variable names.</returns>
        public string[] GetAllBoolVar()
        {
            return m_boolVariables.ToArray();
        }
        /// <summary>
        /// Method returning ancestor of this <see cref="Types"/>.
        /// </summary>
        /// <returns>Pointer to ancestor of this <see cref="Types"/></returns>
        public Types GetAncestor()
        {
            return this.m_ancestor;
        }
    }
}
