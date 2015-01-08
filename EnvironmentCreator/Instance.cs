using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    public class Instance
    {
        private Types m_instanceOf;
        private string m_instName;

        /** @brief List of planned actions of instance in  order.
         * Variable storing all actions that are about to happen.
         * Possible to be empty in case no action prepared.
         */
        private List<Action> m_actionQue = new List<Action>();

        public Instance(string name, Types type)
        {
            this.m_instName = name;
            this.m_instanceOf = type;
            GroundingParams.m_instances.Add(this.GetInstanceName(), this);
        }
        /** @brief Method for adding actions to action que of instance.
         * Separates user usage from implementation.
         */
        public void AddAction(Action act)
        {
            this.m_actionQue.Add(act);
        }
        

        /** @brief Method for getting type name of action.
         * Separates user usage from implementation.
         */
        public string GetTypeName()
        {
            return this.m_instanceOf.GetName();
        }
        /** @brief Method returning type of instance.
         * Separates user usage from implementation.
         */
        public Types GetInstanceType()
        {
            return this.m_instanceOf;
        }
        /** @brief Method returning name of this instance.
         * 
         */
        public string GetInstanceName()
        {
            return this.m_instName;
        }
    }
}
