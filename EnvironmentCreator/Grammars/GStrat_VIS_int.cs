using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace EnvironmentCreator.Gammars
{
    class GStrat_VIS_int : GStratBaseVisitor<int>
    {
        public override int VisitAction(GStratParser.ActionContext context)
        {

            return 0;
            //return base.VisitAction(context);
        }
        public override int VisitInstance(GStratParser.InstanceContext context)
        {
            return base.VisitInstance(context);
        }
        public override int VisitFunctionCall(GStratParser.FunctionCallContext context)
        {
            return base.VisitFunctionCall(context);
        }
        public override int VisitNewType(GStratParser.NewTypeContext context)
        {

            Dictionary<string, int> intVariables = new Dictionary<string, int>();
            Dictionary<string, bool> boolVariables = new Dictionary<string, bool>();

            for (int i = 0; i < context.variable().Count; ++i)
            {
                if (GameStatData.m_returnType_VIS.Visit(context.variable(i)) == GameStatData.NodeReturnType.INT)
                    intVariables.Add(GameStatData.m_returnType_VIS.GetVarName(), 0);
                else
                    boolVariables.Add(GameStatData.m_returnType_VIS.GetVarName(), false);
            }

            Types ancestor = null;
            if (context.ID().Count > 1)
                if (!GroundingParams.m_types.TryGetValue(context.ID(1).GetText(), out ancestor))
                    throw new UnknownAncestorType(context.ID(1).GetText());
            Types typ = new Types(context.ID(0).GetText(), ancestor, intVariables, boolVariables);
            GroundingParams.m_types.Add(typ.GetName(), typ);
            return 0;
        }
    }
}
