using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    /// <summary>
    /// Class encapsulating triggering end effects of <see cref="Action"/>.
    /// Class stores latter
    /// <list type="bullet">
    /// <item><description><see cref="Action"/> which end effects are executed.</description></item>
    /// <item><description>Duration which must pass before executing end effects.</description></item>
    /// <item><description>Name representing parameter name of <see cref="Action"/> and instance name attributable to that parameter.</description></item>
    /// </list>
    /// Method <see cref="TriggerEndEff.NextIteration"/> is used for updating duration left to execution of end effects.
    /// </summary>
    public class TriggerEndEff
    {
        /// <summary>
        /// Time left to execution of end effects of <see cref="Action"/>.
        /// </summary>
        private int m_waitDuration;
        /// <summary>
        /// <see cref="Action"/> which end effects are about to be executed.
        /// </summary>
        private Action m_act;
        /// <summary>
        /// Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.
        /// </summary>
        private Dictionary<string, string> m_namespace;
        /// <summary>
        /// Method used when setting which <see cref="Action"/>'s end effects should be executed\n
        /// and names of instances for parameter of <see cref="Action"/>.
        /// </summary>
        /// <param name="act"><see cref="Action"/> which end effects should be executed.</param>
        /// <param name="ns">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public void  AddNewAct(Action act,Dictionary<string, string> ns){
            System.Diagnostics.Debug.Assert(act != null && ns != null);
            m_waitDuration = act.GetDuration(ns);
            m_act = act;
            m_namespace = ns;
        }
        /// <summary>
        /// Constructor create's class to execute end effects of <see cref="Action"/> passed in aprameter.
        /// </summary>
        /// <param name="act"><see cref="Action"/> which end effects should be executed.</param>
        /// <param name="ns">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public TriggerEndEff(Action act,Dictionary<string, string> ns)
        {
            this.AddNewAct(act, ns);
        }
        /// <summary>
        /// Method correct time to execution of <see cref="Action"/>'s end effects depending time passed as parameter <paramref name="delta"/>.
        /// When duration between <see cref="Action"/>'s effects reach 0, then method starts end effects.
        /// Method sjould be called in every update of game engine.
        /// </summary>
        /// <param name="delta">Time passed in engine.</param>
        public void NextIteration(int delta)
        {
            this.m_waitDuration -= delta;
            if (this.m_waitDuration < 0)
                m_act.CallEndEFF(this.m_namespace);
        }
        /// <summary>
        /// Method return time left between start and end effects of <see cref="Action"/>.
        /// </summary>
        /// <returns>When method return's number greater then 0, end effects wait. There fore they are not executed.\n
        /// When 0 or less is returned. End effects were executed.</returns>
        public int GetLeftDur()
        {
            return this.m_waitDuration;
        }
    }
}