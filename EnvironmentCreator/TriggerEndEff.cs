using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    /**
     * Class encapsulating triggering end effects of action.
     * Class consist of 
     */
    class TriggerEndEff
    {
        private int m_waitDuration;
        private Action m_act;
        private Dictionary<string, string> m_namespace;
        /**
         * 
         */
        public void  AddNewAct(Action act,Dictionary<string, string> ns){
            m_waitDuration = act.GetDuration(ns);
            m_act = act;
            m_namespace = ns;
        }
        public TriggerEndEff()
        {
            m_waitDuration = 0;
            m_act = null;
            m_namespace = null;
        }
        public TriggerEndEff(Action act,Dictionary<string, string> ns)
        {
            this.AddNewAct(act, ns);
        }

        public void NextIteration(int delta)
        {
            this.m_waitDuration -= delta;
            if (this.m_waitDuration < 0)
                m_act.CallEndEFF(this.m_namespace);
        }

        public int GetLeftDur()
        {
            return this.m_waitDuration;
        }
    }
}
