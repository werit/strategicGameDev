using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnvironmentCreator
{
    /// <summary>
    /// Class used as base for all user defined functions that are used in simulation.
    /// User will inherit from this clas and override method <see cref="Function.evalNode(Dictionary{string, string} ,out bool )"/> 
    /// or <see cref="Function.evalNode(Dictionary{string, string} , out int )"/>.
    /// If it returns <see cref="GameStatData.NodeReturnType.BOOL"/> then override for <see cref="Function.evalNode(Dictionary{string, string} ,out bool )"/> is needed.
    /// In case it returns <see cref="GameStatData.NodeReturnType.INT"/> then override for <see cref="Function.evalNode(Dictionary{string, string} , out int )"/> is needed.
    /// </summary>
    public abstract class Function : EvaluationNode
    {
        /// <summary>
        /// String representation of function identifier.
        /// </summary>
        protected string m_functionId;
        /// <summary>
        /// On index 'i' is name of parameter at i-th position. 
        /// </summary>
        protected string[] m_paramPos;
        /// <summary>
        /// Constructor creates new function with name passed in parametert.
        /// </summary>
        /// <param name="fnName">String identifier of function.</param>
        public Function(string fnName)
        {
            this.m_functionId = fnName;
        }
        /// <summary>
        /// Method returns string representation of identifier of function.
        /// </summary>
        /// <returns>String representation identifier of function.</returns>
        public string GetFnName()
        {
            return this.m_functionId;
        }
        /// <summary>
        /// Method creates new copy of function. Parameter <paramref name="paramPos"/> stores information which
        /// parameter was on which position in function.
        /// </summary>
        /// <param name="paramPos">String array have on i-th position name of i-th parameter of this function.</param>
        /// <returns>New instance of this function with set names and positions of parameters.</returns>
        public virtual Function FnCopy(string[] paramPos)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }

        /// <summary>
        /// Model function that has to be overwritten if function returns boolean value.
        /// </summary>
        /// /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="retVal">True or false, depending of function implementation.</param>
        /// <exception cref="FunctionNotSupportedExcp">This method, if not redefined, throws this exception.</exception>
        public override void evalNode(Dictionary<string, string> mapParam,out bool retVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        /// <summary>
        /// Model function that has to be overwritten if function returns integer value.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Integer, depending of function implementation.</param>
        /// <exception cref="FunctionNotSupportedExcp">This method, if not redefined, throws this exception.</exception>
        public override void evalNode(Dictionary<string, string> mapParam, out int returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
    }
    /// <summary>
    /// Function determining if two game objects passed as parameters are in close range.
    /// </summary>
    public class Close_Range : Function
    {
        /// <summary>
        /// Constructor calls constructor of <see cref="Function"/> and creates function identified by name 'close_range'
        /// </summary>
        public Close_Range()
            : base("close_range")
        {
        }
        /// <summary>
        /// Method creates new copy of function. Parameter <paramref name="paramPos"/> stores information which
        /// parameter was on which position in function.
        /// </summary>
        /// <param name="paramPos">String array have on i-th position name of i-th parameter of this function.</param>
        /// <returns>New instance of this function with set names and positions of parameters.</returns>
        public override Function FnCopy(string[] paramPos)
        {
            Close_Range cr = new Close_Range();
            cr.m_paramPos = paramPos;
            return cr;
        }
        /// <summary>
        /// Evaluation method. Method checks if two objects are near each other.
        /// Those two objects are passed in container <paramref name="mapParam"/> in form of string representation of names of <see cref="Instance"/>s.
        /// Container contain original name matched in grammar and <see cref="Instance"/> name, which was substitued for it.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="retVal">True if object 1 is in range of object 2. False is returned otherwise.</param>
        public override void evalNode(Dictionary<string, string> mapParam, out bool retVal)
        {
            // method needs two parameters
            System.Diagnostics.Debug.Assert(mapParam != null && mapParam.Count >= 2);
            retVal = false;
            // get instances and search for them in real game
            // UNITY GET NAME OF OBJECT
            for (int i = 0; i < 2; i++)
            {
                if (!GroundingParams.m_instances.ContainsKey(mapParam.Values.ElementAt(i)))
                    throw new UnknownInstanceExc(mapParam.Values.ElementAt(i));
            }

            System.Diagnostics.Debug.Assert(GameObject.Find(mapParam.Values.ElementAt(0)) != null && GameObject.Find(mapParam.Values.ElementAt(1)) != null);
            if (Vector3.Distance(GameObject.Find(mapParam.Values.ElementAt(0)).transform.position, GameObject.Find(mapParam.Values.ElementAt(1)).transform.position) < GameStatData.close_range)
                retVal = true;
        }
        /// <summary>
        /// This function returns boolean. Therefore method returns boolean node type.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns <see cref="GameStatData.NodeReturnType.BOOL"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.BOOL;
        }
    }
}
