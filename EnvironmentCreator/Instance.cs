using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator
{
    /// <summary>
    /// Class represents instance of <see cref="Types"/>.
    /// Class stores basic information about it's type, name and
    /// encapsultes access to these.
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// Variable storing pointer to <see cref="Types"/>, which is type of this instance.
        /// </summary>
        private Types m_instanceOf;
        /// <summary>
        /// Variable storing string representation of name of this instance.
        /// </summary>
        private string m_instName;
        /// <summary>
        /// Instance constructor takes two parameters and creates new instance with name <paramref name="name"/> and type <paramref name="type"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public Instance(string name, Types type)
        {
            this.m_instName = name;
            this.m_instanceOf = type;
            GroundingParams.m_instances.Add(this.GetInstanceName(), this);
            GroundingParams.GroundInstance(this);
        }
        /// <summary>
        /// Method for getting <see cref="Types"/>'s name of this instance.
        /// </summary>
        /// <returns>String representation of name of <see cref="Types"/> of this instance.</returns>
        public string GetTypeName()
        {
            return this.m_instanceOf.GetName();
        }
        /// <summary>
        /// Method returning type of instance in form of pointer to <see cref="Types"/>.
        /// </summary>
        /// <returns>Pointer to type of this instance.</returns>
        public Types GetInstanceType()
        {
            return this.m_instanceOf;
        }
        /// <summary>
        /// Method returning string representation of name of this instance.
        /// </summary>
        /// <returns>Name of this instance.</returns>
        public string GetInstanceName()
        {
            return this.m_instName;
        }
    }
}
