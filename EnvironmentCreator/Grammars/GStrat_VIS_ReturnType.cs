using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    class GStrat_VIS_ReturnType : GStratBaseVisitor<GameStatData.NodeReturnType>
    {
        private string m_variableName = "";
        public string GetVarName()
        {
            return m_variableName;
        }
        public override GameStatData.NodeReturnType VisitNewVariable(GStratParser.NewVariableContext context)
        {
            m_variableName = context.ID().GetText();
            if (context.opt.Type == GStratParser.NUMBER_ID)
                return GameStatData.NodeReturnType.INT;
            else
                return GameStatData.NodeReturnType.BOOL;
        }
    }
}
