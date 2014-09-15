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
    public class GStrat_VIS_int : GStratBaseVisitor<int>
    {
        public override int VisitAction(GStratParser.ActionContext context)
        {

            return 0;
            //return base.VisitAction(context);
        }
       /* public override int  // (GStratParser.InstanceContext context)
        {
            for (int i = 0; i < context.NAME().Count; i++)
            {
                
            }
            return 0;
        } */
        public override int VisitFunctionCall(GStratParser.FunctionCallContext context)
        {
            return base.VisitFunctionCall(context);
        }
        

        /** @brief Method used when new type is beign defined.
         * Method reads name ,variables and ancestor of new type.
         * Then new type is created and added to set of types in GroundingParams::m_types.
         * @throws UnknownAncestorType when unknown ancestor was defined or ancestor is defined later then this type.
         * @param context of grammar.
         */
        public override int VisitNewType(GStratParser.NewTypeContext context)
        {

            Dictionary<string, int> intVariables = new Dictionary<string, int>();
            Dictionary<string, bool> boolVariables = new Dictionary<string, bool>();
            // get all variables of type
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
            // create right now defined type
            Types typ = new Types(context.ID(0).GetText(), ancestor, intVariables, boolVariables);
            GroundingParams.m_types.Add(typ.GetName(), typ);
            return 0;
        }
    }
}
