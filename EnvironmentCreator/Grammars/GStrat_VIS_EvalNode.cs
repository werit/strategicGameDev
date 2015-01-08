using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    public class GStrat_VIS_EvalNode : GStratBaseVisitor<EvaluationNode>
    {
        public override EvaluationNode VisitAssignExpr(GStratParser.AssignExprContext context)
        {
            EvaluationNode lNode = GameStatData.m_assignNode_VIS.Visit(context.id());
            EvaluationNode rNode = GameStatData.m_assignNode_VIS.Visit(context.expression());
            NewAssign thisNode;
            switch (context.opt.Type)
            {
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
        public override EvaluationNode VisitCallFnPrecond(GStratParser.CallFnPrecondContext context)
        {
            return base.VisitCallFnPrecond(context);
        }

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
        public override EvaluationNode VisitMulDivMod(GStratParser.MulDivModContext context)
        {
            EvaluationNode lNode = Visit(context.expression(0));
            EvaluationNode rNode = Visit(context.expression(1));
            BinaryMathOp thisNode;
            switch (context.opt.Type)
            {
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
        /* @brief Method creating Identification evaluation node.
         * @param Parsing context.
         * Method return two types of identifier. One is just containing variable. Second identifier takes name of instance of class and parameter of this class. 
         * For example function header defined for function 'fn', class 'Soldier' with variable 'bulletCount' : fn(Soldier shooter)
         * 
         * identifier referencing to bulletCount of shooter will look like shooter.bulletCount (during runetime for shooter will be assigned real instance).
         */
        public override EvaluationNode VisitId(GStratParser.IdContext context)
        {
            if (context.NAME().Count > 1)
                return new IDNode(context.NAME(0).GetText(), context.NAME(1).GetText());
            else
                return new IDNode(context.NAME(0).GetText());
        }
        public override EvaluationNode VisitIdent(GStratParser.IdentContext context)
        {
            return Visit(context.id());
        }
        public override EvaluationNode VisitPrecondExpr(GStratParser.PrecondExprContext context)
        {
            EvaluationNode lNode = Visit(context.id());
            EvaluationNode rNode = Visit(context.expression());
            BinaryCompareOp thisNode;
            switch (context.op.Type)
            {
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
                default:
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
    }
}
