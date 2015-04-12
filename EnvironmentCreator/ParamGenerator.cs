using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace EnvironmentCreator
{
    /// <summary>
    /// Class designed to perform parameter generating.
    /// Class consist of inicialization <see cref="ParamGenerator.GenerateParamFromType"/> and parameter 
    /// returning <see cref="ParamGenerator.NextParamSet"/> method. Class generates tuples of parameters(<see cref="Instance"/>'s names)
    /// for <see cref="Action"/>s in world.
    /// </summary>
    public class ParamGenerator
    {
        /// <summary>
        /// Variable represents array of arrays, where first index is order of parameter of some <see cref="Action"/>
        /// and second index is exact instance, that can be inserted into this parameter.
        /// </summary>
        /// <example>Imagine <see cref="Action"/> shoot(Person fighter,Weapon gun) and  this variable will be 2D array {{Rmbo,Robocop},{Glock,Desert_eagle}}
        /// where Rambo and  Robocop are <see cref="Instance"/>s of 'Person' and they correspond to first parameter so they are at first index(0).
        /// similary Glock and Desert_eagle are <see cref="Instance"/>s of 'Weapon' and they have second positionm therefore correspond to second action's parameter.
        /// </example>
        private List<string>[] m_instancesOfTypes;
        /// <summary>
        /// Counter storing information, which tuple of <see cref="Instance"/>s in <see cref="ParamGenerator.m_instancesOfTypes"/> is going to be checked.
        /// </summary>
        private int[] m_paramCounter;
        /// <summary>
        /// Information if exists another tuple of <see cref="Instance"/>s for <see cref="Action"/>.
        /// </summary>
        private bool m_hasNextValue;
        /// <summary>
        /// Method initialize generator of tuples of <see cref="Instance"/>s for some <see cref="Action"/>.
        /// </summary>
        /// <param name="instancesOfTypes">2D array of names of <see cref="Instance"/>s corresponding to parameters of some <see cref="Action"/>.
        /// List storing arrays of names of instances.</param>
        public void GenerateParamFromType(List<string>[] instancesOfTypes)
        {
            Debug.Assert(instancesOfTypes != null);
            for (int i = 0; i < instancesOfTypes.GetLength(0); ++i)
            {
                Debug.Assert(instancesOfTypes[i] != null);
            }
            // assume there are next values for instanceOfTypes
            m_hasNextValue =  true;
            for (int i = 0; i < instancesOfTypes.GetLength(0); ++i)
            {
                if (instancesOfTypes[i].Count < 1)
                {   // no next values, some type does not have any instance
                    m_hasNextValue = false;
                    break;
                }
            }
            this.m_instancesOfTypes = instancesOfTypes;
            this.m_paramCounter = new int[this.m_instancesOfTypes.GetLength(0)];
            for (int i = 0; i < m_paramCounter.Length; ++i)
            {
                m_paramCounter[i] = 0;
            }
        }
        /// <summary>
        /// Method returns tuplne of <see cref="Instance"/> names. For each parameter of <see cref="Action"/> one <see cref="Instance"/> name.
        /// Method returns array until last possible combination of <see cref="Instance"/> names for each parameter is tested.
        /// </summary>
        /// <returns>String array of names of <see cref="Instance"/>s, which was passed to <see cref="ParamGenerator.GenerateParamFromType"/></returns>
        public string[] NextParamSet()
        {
            if (!m_hasNextValue)
                return null;
            string[] result = new string[m_paramCounter.Length];
            bool transferOccured = true;
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_instancesOfTypes[i][m_paramCounter[i]];
                // switch just one index
                if (transferOccured)
                {
                    // set counter for next instance
                    if (m_instancesOfTypes[i].Count > m_paramCounter[i] + 1)
                    {
                        m_paramCounter[i] += 1;
                        transferOccured = false;
                    }
                    else 
                        m_paramCounter[i] = 0;
                }
            }
            if (transferOccured)
                m_hasNextValue = false;
            return result;
        }
    }
}
