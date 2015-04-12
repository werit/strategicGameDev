using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EnvironmentCreator
{
    /// <summary>
    /// Class stores:
    /// <list type="bullet">
    /// <item><see cref="Types"/></item>
    /// <item><see cref="Instance"/></item>
    /// <item><see cref="Action"/></item>
    /// </list>
    /// Class also encapsulates work with all <see cref="Instance"/>'s variables.\n
    /// This class "grounds" them. Stores them and contain methods for their usage and change.
    /// </summary>
    public static class GroundingParams
    {
        
        /// <summary>
        /// Variable storing all integer variables of all <see cref="Instance"/>s.
        /// </summary>
        public static Dictionary<string,Dictionary<string, int>> m_InstanceIntegerVar;
        /// <summary>
        /// Variable storing all boolean variables of all <see cref="Instance"/>s.
        /// </summary>
        public static Dictionary<string,Dictionary<string, bool>> m_InstanceBoolVar;
        /// <summary>
        /// Public storage for all <see cref="Instance"/>s.
        /// </summary>
        public static Dictionary<string, Instance> m_instances;
        /// <summary>
        /// Variable storing couple <see cref="Types"/>'s name in string format and <see cref="Type"/>.
        /// </summary>
        public static Dictionary<string, Types> m_types;
        /// <summary>
        /// Variable storing pair name of <see cref="Action"/> in string format and <see cref="Action"/> corresponding to this name.
        /// </summary>
        public static Dictionary<string, Action> m_actions;
        /// <summary>
        /// Variable storing pair name of <see cref="Function"/> in string format and <see cref="Function"/> corresponding to this name.
        /// </summary>
        public static Dictionary<string, Function> m_functions;
        /// <summary>
        /// Static constructor, which initialize storage for 
        /// <list type="bullet">
        /// <item><see cref="GroundingParams.m_types"/></item>
        /// <item><see cref="GroundingParams.m_instances"/></item>
        /// <item><see cref="GroundingParams.m_actions"/></item>
        /// </list>
        /// </summary>
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
            m_instances = new Dictionary<string, Instance>();
            m_actions = new Dictionary<string, Action>();
        }
        /// <summary>
        /// Method grounding <see cref="Instance"/>'s variables.\n
        /// Method takes each variable of <see cref="Types"/> and ancestor <see cref="Types"/>. This is done recursively till <see cref="Types.m_defaultAncestor"/> of <see cref="Types"/>\n
        /// and creates them with prexif as <see cref="Instance"/>'s name in the \n
        /// <see cref="GroundingParams.m_InstanceBoolVar"/> or <see cref="GroundingParams.m_InstanceIntegerVar"/>, depending if it is variable of type boolean or integer.
        /// </summary>
        /// <param name="inst">Inastance which parameters are "grounded".</param>
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
                if (!m_InstanceIntegerVar.ContainsKey(inst.GetInstanceName()))
                    m_InstanceIntegerVar.Add(inst.GetInstanceName(), new Dictionary<string, int>());
                for (int i = 0; i < parent.GetAllIntVar().Count(); ++i)
                {
                    m_InstanceIntegerVar[inst.GetInstanceName()].Add(parent.GetAllIntVar()[i], 0);
                }
                // filling bool variables
                if (!m_InstanceBoolVar.ContainsKey(inst.GetInstanceName()))
                    m_InstanceBoolVar.Add(inst.GetInstanceName(), new Dictionary<string, bool>());
                for (int i = 0; i < parent.GetAllBoolVar().Count(); ++i)
                {
                    m_InstanceBoolVar[inst.GetInstanceName()].Add(parent.GetAllBoolVar()[i], false);
                }
                // prepare for interation of parent
                parent = parent.GetAncestor();
            }
        }
        /// <summary>
        /// Method handles addidng new action to set of all actions in the simulation.
        /// </summary>
        /// <param name="act">Action to be added to <see cref="GroundingParams.m_actions"/></param>
        public static void AddAction(Action act)
        {
            if (!m_actions.ContainsKey(act.GetName()))
                m_actions.Add(act.GetName(), act);
        }
    }
}
