using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    /// <summary>
    /// Visitor class of grammar.
    /// Class process all grammar rules that hame some return value.
    /// </summary>
    public class GStrat_VIS_ReturnType : GStratBaseVisitor<GameStatData.NodeReturnType>
    {
        /// <summary>
        /// Variable storing name of variable which is mathed by grammar rule.
        /// </summary>
        private string m_variableName = "";
        /// <summary>
        /// Method give acess to the name of last processed 'variable' mathed by grammar rule.
        /// </summary>
        /// <returns>Name of last processed variable in string format.</returns>
        public string GetVarName()
        {
            return m_variableName;
        }
        /// <summary>
        /// Method processing grammar rule matching NewVariable.
        /// Method stores name of variable in internal storage and return type of that variable.
        /// Variable name can be acessed throught method <see cref="GStrat_VIS_ReturnType.GetVarName"/>
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Method returns information of processed variable return type in form of <see cref="GameStatData.NodeReturnType"/> class.</returns>
        public override GameStatData.NodeReturnType VisitNewVariable(GStratParser.NewVariableContext context)
        {
            m_variableName = context.NAME().GetText();
            if (context.opt.Type == GStratParser.NUMBER_ID)
                return GameStatData.NodeReturnType.INT;
            else
                return GameStatData.NodeReturnType.BOOL;
        }
    }
}
