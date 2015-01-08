using System;

namespace EnvironmentCreator
{
    public sealed  class Bridge
    {
        private static Bridge m_bridge;
        private static object syncRoot = new Object();
        private Bridge()
        {
        }
        public static Bridge InstBridge
        {
            get
            {
                if (m_bridge == null)
                {
                    lock (syncRoot)
                        if (m_bridge == null)
                        {
                            m_bridge = new Bridge();
                        }
                }
                return m_bridge;
            }
        }
    }
}
