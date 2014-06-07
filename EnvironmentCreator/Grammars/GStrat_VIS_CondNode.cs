using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    class GStrat_VIS_CondNode : GStratBaseVisitor<ConditionNode>
    {
        public override ConditionNode VisitAddSub(GStratParser.AddSubContext context)
        {
            ConditionNode lNode = Visit(context.expression(0));
            ConditionNode rNode = Visit(context.expression(1));
            BinaryMathOp thisNode;
            if (context.opt.Type == GStratParser.ADD)
                thisNode = new PlusNode();
            else
                thisNode = new MinusNode();
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        public override ConditionNode VisitMulDivMod(GStratParser.AddSubContext context)
        {
            ConditionNode lNode = Visit(context.expression(0));
            ConditionNode rNode = Visit(context.expression(1));
            BinaryMathOp thisNode;
            switch(context.opt.Type){
                case GStratParser.MUL:
                    thisNode = new MulNode();
                    break;
                case GStratParser.MOD:
                    thisNode = new ModNode();
                    break;
                case GStratParser.DIV:
                    thisNode = new DivNode();
                    break;
                default:
                    throw new UnexpectedParserToken("MulDivMod expresion");
            }
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        public override ConditionNode VisitIdent(GStratParser.IdentContext context)
        {
            return new IDNode(context.ID().GetText());
        }
        public override ConditionNode VisitPrecondExpr(GStratParser.PrecondExprContext context)
        {
            ConditionNode lNode = Visit(context.ID());
            ConditionNode rNode = Visit(context.expression());
            BinaryCompareOp thisNode;
            switch(context.op.Type){
                case GStratParser.EQUAL:
                    thisNode = new EqualNode();
                    break;
                case GStratParser.NOT_EQUAL:
                    thisNode = new NotEqualNode();
                    break;
                case GStratParser.LESS_THEN:
                    thisNode = new LessThenNode();
                    break;
                case GStratParser.LESS__OR_EQ:
                    thisNode = new LessOrEQNode();
                    break;
                case GStratParser.MORE_THEN:
                    thisNode = new MoreThenNode();
                    break;
                case GStratParser.MORE_OR_EQ:
                    thisNode = new MoreOrEQNode();
                    break;
                default :
                    throw new UnexpectedParserToken("precondition expresion");
            }
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        public override ConditionNode VisitInt(GStratParser.IntContext context)
        {
            return new IntNode(int.Parse(context.INT().GetText()));
        }
        public override ConditionNode VisitParenth(GStratParser.ParenthContext context)
        {
            return Visit(context.expression());
        }

        public override ConditionNode VisitAction(GStratParser.ActionContext context)
        {
            return base.VisitAction(context);
        }
        public override ConditionNode VisitInstance(GStratParser.InstanceContext context)
        {
            return base.VisitInstance(context);
        }
        public override ConditionNode VisitFunctionCall(GStratParser.FunctionCallContext context)
        {
            return base.VisitFunctionCall(context);
        }
        
    }
}
