using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public class Action
    {
        private string m_name;
        private EvaluationNode m_duration;
        private EvaluationNode[] m_preconditions;
        private EvaluationNode[] m_startEffects;
        private EvaluationNode[] m_endEffects;
        /**
         * First parameter represents action parameter name.
         * Secnd parameter represents action parametr type.
         */
        private Dictionary<string, string> m_typeParamLink;
        /**
         * Key is parameter's name.
         * Value is parameter's position.
         */
        //private Dictionary<string,int> m_paramPos;
        private string[] m_paramPos;

        private Action() {
            m_name =  "noname";
            m_duration =  new IntNode(0);
            m_preconditions = new EvaluationNode[0];
            m_startEffects = new EvaluationNode[0];
            m_endEffects = new EvaluationNode[0];
        }
        public Action(string name, string[] paramTypes, string[] paramNames, EvaluationNode duration, EvaluationNode[] precond, EvaluationNode[] startEff, EvaluationNode[] endEff)
        {
            m_name = name != null ? name : "noname";
            m_typeParamLink = new Dictionary<string,string>();
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

        /**
         * Method handling evaluation in every update.
         */
        public void Call(string[] parameters){
            Dictionary<string, string> ns = new Dictionary<string, string>();
            for (int i = 0; i < parameters.Length; i++)
			{
                ns.Add(m_paramPos[i], parameters[i]);
			}
            bool result;
            for (int i = 0; i < m_preconditions.Length; ++i)
            {
                m_preconditions[i].evalNode(ns,out result);
                if (!result)
                    return;
            }
            // evaluate starting effects
            for (int i = 0; i < m_startEffects.Length; ++i)
            {
                m_startEffects[i].evalNode(ns);
            }
            // wait for duration
            int res;
            m_duration.evalNode(ns, out res);
            GameStatData.game.AddAction(res, ns, this.m_name);
        }
    }
}
