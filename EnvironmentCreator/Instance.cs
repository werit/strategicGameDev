using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    class Instance
    {
        private Types m_instanceOf;
        /** @brief List of planned actions of instance in  order.
         * Variable storing all actions that are about to happen.
         * Possible to be empty in case no action prepared.
         */
        public List<Action> m_actionQue = new List<Action>();

    }
}
