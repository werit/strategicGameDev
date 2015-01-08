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
            string actName = context.NAME(0).GetText();
            string[] paramNames = new string[(context.NAME().Count - 1) / 2];
            string[] paramTypes = new string[(context.NAME().Count - 1) / 2];
            EvaluationNode[] effs = new EvaluationNode[context.effect().Count];
            EvaluationNode[] effend = new EvaluationNode[context.effecte().Count];
            EvaluationNode dur = GameStatData.m_assignNode_VIS.Visit(context.expression());
            EvaluationNode[] precond = new EvaluationNode[context.precondition().Count];
            for (int i = 1; i < context.NAME().Count; i += 2)
            {
                paramNames[(i - 1) / 2] = context.NAME(i).GetText();
                paramNames[(i - 1) / 2] = context.NAME(i + 1).GetText();
            }

            for (int i = 0; i < context.precondition().Count; i++)
            {
                precond[i] = GameStatData.m_assignNode_VIS.Visit(context.precondition(i));
            }

            for (int i = 0; i < context.effect().Count; i++)
            {
                effs[i] = GameStatData.m_assignNode_VIS.Visit(context.effect(i));
            }

            for (int i = 0; i < context.effecte().Count; i++)
            {
                effs[i] = GameStatData.m_assignNode_VIS.Visit(context.effecte(i));
            }
            Action act = new Action(actName, paramTypes, paramNames, dur, precond, effs, effend);

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

            List<string> intVariables = new List<string>();
            List<string> boolVariables = new List<string>();
            // get all variables of type
            for (int i = 0; i < context.variable().Count; ++i)
            {
                if (GameStatData.m_returnType_VIS.Visit(context.variable(i)) == GameStatData.NodeReturnType.INT)
                    intVariables.Add(GameStatData.m_returnType_VIS.GetVarName()); // getVarName stores name of variable last read
                else
                    boolVariables.Add(GameStatData.m_returnType_VIS.GetVarName());
            }

            Types ancestor = null;
            if (context.NAME().Count > 1)
                if (!GroundingParams.m_types.TryGetValue(context.NAME(1).GetText(), out ancestor))
                    throw new UnknownAncestorType(context.NAME(1).GetText());
            // create right now defined type
            Types typ = new Types(context.NAME(0).GetText(), ancestor, intVariables, boolVariables);
            GroundingParams.m_types.Add(typ.GetName(), typ);
            return 0;
        }

        /** @brief Method used when new instance of type is beign created.
         * Method reads name of type and prepares instances with selected names.
         * Then new type is created and added to set of types in GroundingParams::m_types.
         * @throws UnknownAncestorType when unknown ancestor was defined or ancestor is defined later then this type.
         * @param context of grammar.
         */
        public override int VisitNewInstances(GStratParser.NewInstancesContext context)
        {
            Types type = null;
            Instance[] inst = new Instance[context.NAME().Count-1];
            if (!GroundingParams.m_types.TryGetValue(context.NAME(0).GetText(), out type))
                throw new UnknownType(context.NAME(0).GetText());
            for (int i = 1; i < context.NAME().Count; ++i)
            {
                inst[i-1] = new Instance(context.NAME(i).GetText(),type);
            }
            type.AddInst(inst);
            return 0;
        }
    }
}
