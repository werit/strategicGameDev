using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public class GameProcessing
    {
        private double m_timeDelta;
        private List<Pair<int, Pair<double, Action>>> m_effects = new List<Pair<int, Pair<double, Action>>>();
        public void Initialize(double delta)
        {
            this.m_timeDelta = delta;
        }
        public void AddAction(int duration,Dictionary<string,string> ns,string actionName)
        {
            Action act = null;
            GroundingParams.m_actions.TryGetValue(actionName, out act);
            if (act != null)
            {
                m_effects.Add(new Pair<int, Pair<double, Action>>(duration, new Pair<double, Action>(0.0, act)));
            }
        }
        public double TimeDelta
        {
            get { return this.m_timeDelta; }
            set { this.m_timeDelta = TimeDelta; }
        }
    }
}
