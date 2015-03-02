using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    /// <summary>
    /// Class representing action of any object.
    /// Action consist of 
    /// <list type="bullet">
    /// <item><description>set of preconditions that must be fulfilled for action to by executed.</description></item>
    /// <item><description>duration\nDuration between immediate effects and end effects of action.</description></item>
    /// <item><description>effects of action(immediate).</description></item>
    /// <item><description>end effects of action.</description></item>
    /// </list>
    /// </summary>
    public class Action
    {
        /// <summary>
        /// String representing name of action.
        /// </summary>
        private string m_name;
        /// <summary>
        /// Variable storing duration between immediate effects and end effects.
        /// Value of duration is in miliseconds and this value is 
        /// </summary>
        private EvaluationNode m_duration;
        /// <summary>
        /// Variable storing set of preconditions which must be evaluated to true, 
        /// for action to be uasble on tuple of parameters.
        /// </summary>
        private EvaluationNode[] m_preconditions;
        /// <summary>
        /// Variable storing set of effects, that are evaluated after action is used 
        /// and all preconditions return true.
        /// </summary>
        private EvaluationNode[] m_startEffects;
        /// <summary>
        /// Variable storing set of effects, that are evaluated after passing of duration of action.
        /// </summary>
        private EvaluationNode[] m_endEffects;
        /// <summary>
        /// First parameter represents action parameter name.
        /// Secnd parameter represents action parametr type. 
        /// </summary>
        private Dictionary<string, string> m_typeParamLink;
        /// <summary>
        /// Key is action's parameter's name.
        /// Value is action's parameter's position.
        /// </summary>
        private string[] m_paramPos;
        /// <summary>
        /// Class constructor. 
        /// Creates action withou any effects or any preconditions.
        /// Might by used for test pourposes.
        /// </summary>
        private Action()
        {
            m_name = "noname";
            m_duration = new IntNode(0);
            m_preconditions = new EvaluationNode[0];
            m_startEffects = new EvaluationNode[0];
            m_endEffects = new EvaluationNode[0];
        }
        /// <summary>
        /// Constructor of action. Creates action which can be executed in simulation.
        /// </summary>
        /// <param name="name">Name identifier of action.</param>
        /// <param name="paramTypes">Array characterizing name of type of each action's argument.</param>
        /// <param name="paramNames">Array characterizing name of each action's argument.</param>
        /// <param name="duration">Duration between effects and end effects. Duration triggers only if action is admissible.</param>
        /// <param name="precond">Set of preconditions that have to be met for action to be admissible.</param>
        /// <param name="startEff">Effects executed when action is admissible.</param>
        /// <param name="endEff">Effects executed when action is admissible and <paramref name="duration"/> passed.</param>
        public Action(string name, string[] paramTypes, string[] paramNames, EvaluationNode duration, EvaluationNode[] precond, EvaluationNode[] startEff, EvaluationNode[] endEff)
        {
            m_name = name != null ? name : "noname";
            m_typeParamLink = new Dictionary<string, string>();
            //m_paramPos = new Dictionary<string,int>();
            m_paramPos = new string[paramNames.Length];
            for (int i = 0; i < paramNames.Length; i++)
            {
                //m_paramPos.Add(paramNames[i], i);
                m_paramPos[i] = paramNames[i];
                m_typeParamLink.Add(paramNames[i], paramTypes[i]);
            }
            m_duration = duration != null ? duration : new IntNode(0);
            m_preconditions = precond != null ? precond : new EvaluationNode[0];
            m_startEffects = startEff != null ? startEff : new EvaluationNode[0];
            m_endEffects = endEff != null ? endEff : new EvaluationNode[0];
        }

        /// <summary>
        /// Method handling evaluation of action in every update of a game engine.
        /// Method substitute parameters names for names of instances and then evaluate this action.
        /// </summary>
        /// <param name="parameters"> Array of names of instances of types corresponding to the parameters of the action.</param>
        public void Call(string[] parameters)
        {
            if (parameters == null || parameters.Length != this.m_typeParamLink.Count)
                throw new Exception();
            Dictionary<string, string> ns = new Dictionary<string, string>();
            for (int i = 0; i < parameters.Length; ++i)            
                ns.Add(m_paramPos[i], parameters[i]);
            
            bool result;
            for (int i = 0; i < m_preconditions.Length; ++i)
            {
                m_preconditions[i].evalNode(ns, out result);
                if (!result)
                    return;
            }
            // evaluate starting effects
            for (int i = 0; i < m_startEffects.Length; ++i)        
                m_startEffects[i].evalNode(ns);
            // rest of action will be handled through trigger end effect #TriggerEndEff
            TriggerEndEff effend = new TriggerEndEff(this,ns);
            GameStatData.game.AddActionEndEff(effend);
        }
        /// <summary>
        /// Method determining how much time has to pass between start and end effects.
        /// </summary>
        /// <param name="ns">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Time in miliseconds between effects of this action with current <paramref name="ns"/>.</returns>
        public int GetDuration(Dictionary<string, string> ns)
        {
            int result;
            m_duration.evalNode(ns, out result);
            return result;
            
        }
        /// <summary>
        /// Method handling execution of end effects of action with parameters <paramref name="ns"/>.
        /// </summary>
        /// <param name="ns">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public void CallEndEFF(Dictionary<string, string> ns)
        {
            // evaluate end effects
            for (int i = 0; i < m_endEffects.Length; ++i)            
                m_endEffects[i].evalNode(ns);
        }
        /// <summary>
        /// Method for access private variable storing name of action.
        /// </summary>
        /// <returns>Name of action.</returns>
        public string GetName()
        {
            return this.m_name;
        }
        /// <summary>
        /// Action parameters consist of parameter type and parameter name.
        /// Method gives access to name string representation of name of type.
        /// </summary>
        /// <returns>Array of string representation of each type name.</returns>
        public string[] GetParameterTypesNames()
        {
            return m_typeParamLink.Values.ToArray();
        }
    }
}
