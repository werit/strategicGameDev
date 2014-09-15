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
        private List<EvaluationNode> m_preconditions;
        private List<EvaluationNode> m_startEffects;
        private List<EvaluationNode> m_endEffects;
        public Action() {
            m_name =  "noname";
            m_duration =  new IntNode(0);
            m_preconditions =  new List<EvaluationNode>();
            m_startEffects =  new List<EvaluationNode>();
            m_endEffects =  new List<EvaluationNode>();
        }
        public Action(string name, EvaluationNode duration, List<EvaluationNode> precond, List<EvaluationNode> startEff, List<EvaluationNode> endEff)
        {
            m_name = name != null ? name : "noname";
            m_duration = duration != null ? duration : new IntNode(0);
            m_preconditions = precond != null ? precond : new List<EvaluationNode>();
            m_startEffects = startEff != null ? startEff : new List<EvaluationNode>();
            m_endEffects = endEff != null ? endEff : new List<EvaluationNode>();
        }

    }
}
