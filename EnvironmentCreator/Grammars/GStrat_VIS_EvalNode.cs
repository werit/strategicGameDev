using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    public class GStrat_VIS_EvalNode : GStratBaseVisitor<EvaluationNode>
    {
        public override EvaluationNode VisitAddSub(GStratParser.AddSubContext context)
        {
            EvaluationNode lNode = Visit(context.expression(0));
            EvaluationNode rNode = Visit(context.expression(1));
            BinaryMathOp thisNode;
            if (context.opt.Type == GStratParser.ADD)
                thisNode = new PlusNode();
            else
                thisNode = new MinusNode();
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        public override EvaluationNode VisitMulDivMod(GStratParser.AddSubContext context)
        {
            EvaluationNode lNode = Visit(context.expression(0));
            EvaluationNode rNode = Visit(context.expression(1));
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
        public override EvaluationNode VisitId(GStratParser.IdContext context)
        {
            return base.VisitId(context);
        }
        public override EvaluationNode VisitIdent(GStratParser.IdentContext context)
        {
            return new IDNode(context.ID().GetText());
        }
        public override EvaluationNode VisitPrecondExpr(GStratParser.PrecondExprContext context)
        {
            EvaluationNode lNode = Visit(context.ID());
            EvaluationNode rNode = Visit(context.expression());
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
        public override EvaluationNode VisitInt(GStratParser.IntContext context)
        {
            return new IntNode(int.Parse(context.INT().GetText()));
        }
        public override EvaluationNode VisitParenth(GStratParser.ParenthContext context)
        {
            return Visit(context.expression());
        }
        public override EvaluationNode VisitAssignExpr(GStratParser.AssignExprContext context)
        {
            EvaluationNode lNode = Visit(context.ID());
            EvaluationNode rNode = Visit(context.expression());
            BinaryOp thisNode;
            switch(context.opt.Type){
                case GStratParser.ASSIGN:
                    thisNode = new Assign();
                    break;
                case GStratParser.ASSIGN_MINUS:
                    thisNode = new SubstractAssign();
                    break;
                case GStratParser.ASSIGN_DIV:
                    thisNode = new DivisionAssign();
                    break;
                case GStratParser.ASSIGN_MUL:
                    thisNode = new MultiplAssign();
                    break;
                case GStratParser.ASSIGN_MOD:
                    thisNode = new ModAssign();
                    break;
                case GStratParser.ASSIGN_ADD:
                    thisNode = new AddAssign();
                    break;
                default:
                    throw new UnexpectedParserToken("assign expresion");
            }
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
    }
}
