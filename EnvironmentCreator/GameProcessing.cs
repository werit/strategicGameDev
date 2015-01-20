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
        private List<TriggerEndEff> m_endEffects = new List<TriggerEndEff>();
        
        public void Initialize(double delta)
        {
            if (delta > 0.0)
                this.m_timeDelta = delta;
            else
                this.m_timeDelta = 0.2;
        }
        public void Start()
        {

        }
        public void Update() {
            for (int i = 0; i < m_endEffects.Count; ++i)
            {
                m_endEffects[i].NextIteration((int)m_timeDelta);
            }
            m_endEffects.RemoveAll(delegate(TriggerEndEff eff) { return eff.GetLeftDur() <= 0; });
            // evaluate all actions

            foreach (Action act in GroundingParams.m_actions.Values) {
                string[] paramTypeName = act.GetParameterTypesNames();
                Types paramType;
                string[] param = new string[paramTypeName.Length];
                int[] paramCnt = Enumerable.Repeat(0,paramTypeName.Length).ToArray();
                for (int i = 0; i < paramTypeName.Length; i++)
                {
                    
                    while (true)
                    {

                        break;
                    }
                    if(GroundingParams.m_types.TryGetValue(paramTypeName[i],out paramType))
                        for (int j = 0; j < paramType.GetInst().Length; ++j)
                        {
                            string s = paramType.GetInst()[j].GetInstanceName();
                            act.Call(new string[] { s });
                        }

                }
            }
        }

        public void AddActionEndEff(TriggerEndEff effEnd)
        {
            m_endEffects.Add(effEnd);
        }

        public double TimeDelta
        {
            get { return this.m_timeDelta; }
            set { this.m_timeDelta = TimeDelta; }
        }
    }
}
